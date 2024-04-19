using System.Collections;
using System.Collections.Generic;
using System;
using NaughtyAttributes;
using Ranking.Scripts.DataBase;
using UnityEngine;
using VRShooting.Scripts.Ranking;

public class Register : MonoBehaviour
{
    [SerializeField] private int point;
    [SerializeField] private string name;
    private RankingType type;
    
    [Button]
    private void Go()
    {
        foreach (var i in Enum.GetValues(typeof(RankingType)))
        {
            PlayFabManager.LogIn();
            type = (RankingType)i;
            var data = RankingData.GenerateRankingDataWithoutDictionary();
            data.UpdateData(new Point(point));
            data.UpdateData(new PlayerName(name));
            PlayFabManager.RegisterRankingData(data,type);
        }
        
        
    }
}
