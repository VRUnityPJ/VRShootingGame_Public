using System;
using Ranking.Scripts;
using Ranking.Scripts.Interface;
using UniRx;
using UnityEngine;

namespace Ranking.Demo.Scripts.DemoGame
{
    //ランキングDemoシーンにおいてPlayerNameを保持するクラス
    public class PlayerNameHolder : MonoBehaviour,IRankingDataHolder<Ranking.Scripts.PlayerName>
    {
        private RankingStorage _storage;
        private ReactiveProperty<Ranking.Scripts.PlayerName> _playerName = new ReactiveProperty<Ranking.Scripts.PlayerName>(new Ranking.Scripts.PlayerName("Player"));
        public IReadOnlyReactiveProperty<Ranking.Scripts.PlayerName> PlayerName => _playerName;
        
        private void Start()
        {
            SetStorage();
        }

        public void SetStorage()
        {
            _storage = RankingStorage.instance;
        }
        
        public void SendData(Ranking.Scripts.PlayerName name)
        {
            _storage.UpdateData(name);
        }
        /// <summary>
        /// PlayerNameHolderのplayerNameを更新し、ストレージに保存する関数
        /// </summary>
        public void UpdatePlayerName(Ranking.Scripts.PlayerName playerName)
        {
            _playerName.Value = playerName;
            SendData(_playerName.Value);
        }
    }
}