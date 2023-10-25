using System;
using NaughtyAttributes;
using TMPro;
using UniRx;
using UnityEngine;
using VRShooting.Scripts.Ranking;
using VRShooting.Scripts.Stage;

namespace VRShooting.Scripts.UI
{
    public class ScoreUIPresenter : MonoBehaviour
    {
        [SerializeField, Required] private LevelStateController _levelStateController;
        [SerializeField, Required] private TextMeshProUGUI _scoreText;
        [SerializeField, Required] private TextMeshProUGUI _timeText;

        private void Start()
        {
            _levelStateController.OnEndGame
                .Subscribe(_ => gameObject.SetActive(false))
                .AddTo(this);
        }

        private void Update()
        {
            // テキストを更新
            var score = PointStorage.GetPoint();
            var time = _levelStateController.GetTime(); 
            _scoreText.text = $"Score : {score}";
            _timeText.text = $"Time : {time} sec";
        }
    }
}