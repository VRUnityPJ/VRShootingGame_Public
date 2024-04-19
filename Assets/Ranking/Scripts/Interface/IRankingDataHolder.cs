﻿using Ranking.Scripts;
using UniRx;
using UnityEngine;

namespace Ranking.Scripts.Interface
{
    /// <summary>
    /// ランキングに登録するデータを持つ役割を持つ
    /// </summary>
    public interface IRankingDataHolder<T>
    {
        public void SetStorage();
        public void SendData(T data);
    }
}