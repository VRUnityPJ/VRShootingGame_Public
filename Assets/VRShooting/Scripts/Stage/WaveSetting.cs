﻿using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace VRShooting.Scripts.Stage
{
    [Serializable]
    public class WaveSetting
    {
        /// <summary>
        /// 敵のスポナー
        /// </summary>
        public List<EnemySpawner> spawners => _spawners;
        
        /// <summary>
        /// ウェーブの時間
        /// </summary>
        public float waveTimeSeconds => _waveTimeSeconds;
        
        [SerializeField]
        private List<EnemySpawner> _spawners;
        [SerializeField, MinValue(0f)]
        private float _waveTimeSeconds = 10f;
    }
}