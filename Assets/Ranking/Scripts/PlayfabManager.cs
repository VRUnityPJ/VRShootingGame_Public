using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using Ranking.Scripts;
using Ranking.Scripts.DataBase;
using UnityEngine;
using VRShooting.Scripts.Ranking;


//参考　https://kan-kikuchi.hatenablog.com/entry/PlayFabLogin

/// <summary>
/// PlayFabに関する操作をまとめておくクラス
/// </summary>
public static class PlayFabManager
{
    //ランキングの名前
    private const string RANKING_NAME = "Demo_Ranking";
    //ログイン時のＩＤ
    private static string _customID;
    //リーダーボード（ランキング）を表示できるか
    private static bool canShowLeaderBoard;
    //取得したランキングを格納するリスト
    private static RankingData[] rankarray;

    //ランキング名とkeyのペア
    private static Dictionary<RankingType, string> rankingName = new Dictionary<RankingType, string>()
    {
        { RankingType.FirstStageNormal, "FirstStage_Normal" },
        { RankingType.SecondStageNormal, "SecondStage_Normal" },
        { RankingType.ThirdStageNormal, "ThirdStage_Normal" },
        { RankingType.FirstStageHard, "FirstStage_Hard" },
        { RankingType.SecondStageHard, "SecondStage_Hard" },
        { RankingType.ThirdStageHard, "ThirdStage_Hard" }
    };

    public static event Action onCompleteLogin;
    public static event Action onFailedLogin;
    public static event Action onCompleteRegister;
    public static event Action onFailedRegister;
    public static event Action onCompleteGetLeaderBoard;
    public static event Action onFailedGetLeaderBoard;
    
    /// <summary>
    /// ログインする関数
    /// </summary>
    public static void LogIn()
    {
        //カスタムＩＤを生成
        _customID = GenerateCustomID();
        
        //ログイン
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = _customID, CreateAccount = true },
            OnLoginSuccess,
            error =>
            {
                onFailedLogin?.Invoke();
                Debug.Log("ログイン失敗");
            });
    }
    
    /// <summary>
    /// ログインが成功したとき
    /// </summary>
    private static void OnLoginSuccess(LoginResult result)
    {
        //IDが既に使われていた場合
        if (!result.NewlyCreated)
        {
            Debug.LogWarning($"CustomId : {_customID} は既に使われています。");
            //ログインしなおし
            LogIn();
        }
        else
        {
            onCompleteLogin?.Invoke();
            Debug.Log($"Id {_customID} でログインしました");
        }
    }

    public static void RegisterRankingData(RankingData data , RankingType type = RankingType.Null)
    {
        var scoreData = data.GetData<Point>();
        var nameData = data.GetData<PlayerName>();
        RegisterScore(scoreData , type);
        RegisterPlayerName(nameData);
    }

    /// <summary>
    /// スコアを登録する関数
    /// </summary>
    /// <param name="score"></param>
    private static void RegisterScore(Point score,RankingType type = RankingType.Null)
    {
        int data = score.point;
        string staticsName;
        
        staticsName = type != RankingType.Null ? rankingName[type] : RANKING_NAME;
        
        PlayFabClientAPI.UpdatePlayerStatistics
        (
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>()
                {
                    new StatisticUpdate
                    {
                        StatisticName = staticsName,
                        Value = data
                    }
                }
            },
            result =>
            {
                onCompleteRegister?.Invoke();
                Debug.Log($"{data}:スコア送信");
            },
            error =>
            {
                onFailedRegister?.Invoke();
                Debug.Log($"{data}:スコア送信に失敗\n{error.GenerateErrorReport()}");
            }
        );
    }
    
    /// <summary>
    /// プレイヤー名の登録
    /// </summary>
    /// <param name="playerName"></param>
    private static void RegisterPlayerName(PlayerName playerName)
    {
        string data = playerName.name;
        
        //PlayFabは名前を３文字以上にする規定があるため空白で埋める
        if (data.Length < 3)
            data = "  " + data;
        
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = data
        };
        //ユーザ名の更新
        Debug.Log($"ユーザ名の更新開始");
        PlayFabClientAPI.UpdateUserTitleDisplayName
        (
            request,
            result =>
            {
                onCompleteRegister?.Invoke();
                Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
            },
            error =>
            {
                onFailedRegister?.Invoke();
                Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
            }
        );    
    }
    
    //=================================================================================
    //カスタムIDの生成
    //=================================================================================
    
    //IDに使用する文字
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";

    //IDを生成する
    private static string GenerateCustomID() {
        int idLength = 32;//IDの長さ
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();

        //ランダムにIDを生成
        for (int i = 0; i < idLength; i++){
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }

        return stringBuilder.ToString();
    }
    
    //=================================================================================
    //ランキングボードの取得
    //=================================================================================
    
    /// <summary>
    /// 非同期可能
    /// ランキング(リーダーボード)を取得
    /// </summary>
    public static async UniTask<RankingData[]> GetLeaderboardAsync(int _maxResultCount, RankingType type = RankingType.Null) 
    { 
        //ランキングを表示できないのでfalse
        canShowLeaderBoard = false;
        
        string staticsName;
        staticsName = type != RankingType.Null ? rankingName[type] : RANKING_NAME;
        
        //GetLeaderboardRequestのインスタンスを生成
        var request = new GetLeaderboardRequest{
            StatisticName   = staticsName, //ランキング名(統計情報名)
            StartPosition   = 0,                 //何位以降のランキングを取得するか
            MaxResultsCount = _maxResultCount              //ランキングデータを何件取得するか(最大100)
        };

        //ランキング(リーダーボード)を取得
        Debug.Log($"ランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboard
            (
                request,
                //取得に成功したとき
                result =>
                {
                    // ランキングデータ配列の作成
                    rankarray = new RankingData[result.Leaderboard.Count];
                    
                    foreach (var r in result.Leaderboard)
                    {
                        RankingData dataForDataBase = RankingData.GenerateRankingDataWithoutDictionary();
                        
                        //順位、スコア、名前を取得
                        int i = r.Position;
                        int i_score = r.StatValue;
                        string i_name = r.DisplayName;
                        
                        if(i == null || i_score == null || i_name == null)
                            Debug.LogError("取得したランキングが欠落しています");
                        
                        Point score = new Point(i_score);
                        PlayerName playerName = new PlayerName(i_name);
                        
                        //データを更新
                        dataForDataBase.UpdateData(score);
                        dataForDataBase.UpdateData(playerName);
                        
                        //配列に格納
                        rankarray[i] = dataForDataBase;
                    }
                    onCompleteGetLeaderBoard?.Invoke();
                    Debug.Log("ランキングの取得に成功しました");
                    //rankingDataの生成が完了したのでtrue
                    canShowLeaderBoard = true;
                },
                //取得に失敗したとき
                error =>
                {
                    onFailedGetLeaderBoard?.Invoke();
                    Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
                });

        //ランキング取得後戻り値として返す
        for (int i = 0; i < 100; i++)
        {
            if (canShowLeaderBoard)
            {
                return rankarray;
            }

            //0.1秒待機する
            await UniTask.Delay(TimeSpan.FromMilliseconds(100));
        }
        Debug.Log("ランキングが取得できません");
        return null;
    }
}

public enum RankingType
{
    FirstStageNormal  = 0,
    SecondStageNormal = 1,
    ThirdStageNormal = 2,
    FirstStageHard = 3,
    SecondStageHard = 4,
    ThirdStageHard = 5,
    Null = 6
}
