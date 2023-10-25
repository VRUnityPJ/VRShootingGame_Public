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
        public static bool isHard;
        public static int stageNumber = 1;

        private const int MAX_STAGE_NUMBER = 3;
        private const int MIN_STAGE_NUMBER = 1;
        private void Awake()
        {
            isHard = false;
            Debug.Log(stageNumber);
            if (stageNumber is < MIN_STAGE_NUMBER or > MAX_STAGE_NUMBER)
                throw new IndexOutOfRangeException("StageNumberが不正な値です");
        }
    }
}