using System;
using UnityEngine;

namespace VRShooting.Scripts.Ranking
{
    /// <summary>
    /// Stageの番号、難易度を保持するクラス
    /// FirstStageで初期化している
    /// </summary>
    public class StageData : MonoBehaviour
    {
        public static RankingType stagetype = RankingType.Null;

        ///今HardStageかどうか
        public static bool IsHardStage()
        {
            if ((int)stagetype < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}