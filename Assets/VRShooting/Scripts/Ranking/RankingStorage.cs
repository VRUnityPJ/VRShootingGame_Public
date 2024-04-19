using Cysharp.Threading.Tasks;
using Ranking.Scripts.DataBase;
using UnityEngine;

namespace VRShooting.Scripts.Ranking
{
    public class RankingStorage : MonoBehaviour
    {
        [SerializeField] private BoardUpdater updater;
        [SerializeField] private string stageName;
        [SerializeField] private GameObject panel;
        [SerializeField] private RankingType[] _rankingTypes;
        private RankingData[] nowNormalData, nowHardData;
        private bool isMoving = true;

        private void Awake()
        {
            nowHardData = new RankingData[5]{null,null,null,null,null};
            nowNormalData = new RankingData[5]{null,null,null,null,null};
        }

        private void OnEnable()
        {
            isMoving = true;
            Flow();
        }

        private void OnDisable()
        {
            isMoving = false;
        }

        private async void Flow()
        {
            if (!isMoving) return;
            await UniTask.Delay(1000);
            foreach (var type in _rankingTypes)
            {
                RankingData[] data = await PlayFabManager.GetLeaderboardAsync(5,type);
                //データが前回と同じなら更新しない
                if(IsEqual(data,type))
                    continue;
                updater.UpdateRanking(data,type);
            }

            //合計10秒のインターバル
            await UniTask.Delay(9000);
            Flow();
        }

        //Rankingdataを比べる
        private bool IsEqual(RankingData[] data ,RankingType type)
        {
            Debug.Log($"{type} {(int)type}");
            
            
            RankingData[] nowData;
            //Hardモードの判別
            if ((int)type >= 3)
            {
                nowData = nowHardData;
            }
            else
            {
                nowData = nowNormalData;
            }

            if (nowData[0] == null)
            {
                UpdateNowData(data,type);
                return false;
            }
            
            for (int i = 0; i < nowData.Length; i++)
            {
                if (data[i].GetData<Point>().point != nowData[i].GetData<Point>().point)
                {
                    UpdateNowData(data,type);
                    return false;
                }
            }

            return true;
        }

        //Nowdataを更新する
        private void UpdateNowData(RankingData[] data,RankingType type)
        {
            //nowDataを更新
            if ((int)type >= 3)
            {
                nowHardData  = data;
            }
            else
            {
                nowNormalData = data;
            }

        }
    }
}