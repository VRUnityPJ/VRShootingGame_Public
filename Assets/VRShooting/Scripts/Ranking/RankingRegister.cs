using Ranking.Scripts.DataBase;
using VRShooting.Scripts.Ranking;

public class RankingRegister
{
    /// <summary>
    /// ランキングに登録する関数
    /// </summary>
    public static void Register()
    {
        var finalPoint = PointStorage.NowPoint;
        var playerName = PlayerNameStorage.NowPlayerName;
        RankingType stageType = StageData.stagetype;

        var data = RankingData.GenerateRankingDataWithoutDictionary();
        data.UpdateData(finalPoint);
        data.UpdateData(playerName);
        
        PlayFabManager.RegisterRankingData(data,StageData.stagetype);
    }
}
