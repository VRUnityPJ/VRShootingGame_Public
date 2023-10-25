using System;
using System.Collections;
using System.Collections.Generic;
using NCMB;
using UnityEngine;

//TODO ここら辺の読みにくいコードをなおす
namespace VRShooting.Scripts.Ranking
{
    public class RankingStorage : MonoBehaviour
    {
        [SerializeField] private BoardUpdater updater;
        [SerializeField] private string stageName;
        [SerializeField] private GameObject panel;
        private List<Record> nowNormalRanking = new List<Record>();
        private List<Record> nowHardRanking = new List<Record>();

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                var record = new Record();
                nowNormalRanking.Add(record);
                nowHardRanking.Add(record);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(Flow());
        }

        private void OnDisable()
        {
            StopCoroutine(Flow()); 
        }

        private IEnumerator Flow()
        {
            yield return new WaitForSeconds(1);
            GetNewRanking();
            StartCoroutine(Flow());
            yield return new WaitForSeconds(9);
        }
        
        public void GetNewRanking()
        {
            var newNormalRanking = new List<Record>();
            var newHardRanking = new List<Record>();
            
            NCMBQuery<NCMBObject> hardQuery = new NCMBQuery<NCMBObject>(stageName);

            hardQuery.WhereEqualTo("IsHard", true);
            //Scoreを元に降順に並べる
            hardQuery.OrderByDescending("Score");
            //参照数を5に制限
            hardQuery.Limit = 5;
            
            hardQuery.Find((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.Log("取得失敗");
                }
                else
                {
                    foreach (var obj in objList)
                    {
                        var record = new Record();

                        record.name = Convert.ToString(obj["Name"]);
                        record.score = Convert.ToInt32(obj["Score"]);
                        Debug.Log(record.name + record.score);
                        newHardRanking.Add(record);
                    }
                    
                    for (int i = 0; i < newHardRanking.Count; i++)
                    {
                        if (nowHardRanking[i].score != newHardRanking[i].score || nowHardRanking[i].name != newHardRanking[i].name)
                        {
                            nowHardRanking = newHardRanking;
                            List<List<Record>> record = new List<List<Record>>();
                            record.Add(nowNormalRanking);
                            record.Add(nowHardRanking);
                            updater.UpdateRanking(record);
                            return;
                        }
                    }
                    
                }
            });
            
            NCMBQuery<NCMBObject> normalQuery = new NCMBQuery<NCMBObject>(stageName);
            
            normalQuery.WhereEqualTo("IsHard", false);
            //Scoreを元に降順に並べる
            normalQuery.OrderByDescending("Score");
            //参照数を5に制限
            normalQuery.Limit = 5;
            
            normalQuery.Find((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.Log("取得失敗");
                }
                else
                {
                    foreach (var obj in objList)
                    {
                        var record = new Record();
                        record.name = Convert.ToString(obj["Name"]);
                        record.score = Convert.ToInt32(obj["Score"]);
                        Debug.Log(record.name + record.score);
                        newNormalRanking.Add(record);
                    }
                    
                    for (int i = 0; i < newNormalRanking.Count; i++)
                    {
                        if (nowNormalRanking[i].score != newNormalRanking[i].score || nowNormalRanking[i].name != newNormalRanking[i].name)
                        {
                            nowNormalRanking = newNormalRanking;
                            List<List<Record>> record = new List<List<Record>>();
                            record.Add(nowNormalRanking);
                            record.Add(nowHardRanking);
                            updater.UpdateRanking(record);
                            return;
                        }
                    }
                    
                }
            });
        }
    }

    public class Record
    {
        public string name;
        public int score;
    }
}