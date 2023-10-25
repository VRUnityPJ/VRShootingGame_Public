using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VRShooting.Scripts.Score.Interfaces;

namespace VRShooting.Scripts.Score
{
    public class ScoreData : MonoBehaviour, IScoreData
    {
        /// <summary>
        /// スコアが変わったときに発火される
        /// </summary>
        public IReadOnlyReactiveProperty<Score> onChangeScore => _score;
        private ReactiveProperty<Score> _score = new (new Score(0f));
        
        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(float value)
        {
            _score.Value = _score.Value.Add(value);
        }
        
        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(Score value)
        {
            _score.Value = _score.Value.Add(value);
        }
    }
}