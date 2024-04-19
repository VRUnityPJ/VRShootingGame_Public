using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRShooting.Scripts.Ranking;
using VRShooting.Scripts.Stage;

namespace VRShooting.Scripts.UI
{
    public class ResultUIPresenter : MonoBehaviour
    {
        [Header("Model")] [SerializeField, Required]
        private LevelStateController _levelStateController;

        [SerializeField] private AudioSource _audioSource;
        
        /// <summary>
        /// 名前表示用のテキスト
        /// </summary>
        [Header("Views")] [SerializeField, Required]
        private Text _nickNameText;
        
        /// <summary>
        /// スコア表示用のテキスト
        /// </summary>
        [SerializeField, Required]
        private Text _scoreText;
        
        
        /// <summary>
        /// 難易度表示用のテキスト
        /// </summary>
        [SerializeField, Required]
        private Text _modeText;

        [Header("Parameters")] [SerializeField, MinValue(0f)]
        private float _delay;

        [SerializeField, Scene]
        private string _sceneName;

        [SerializeField] private AudioClip _openAudioClip;
        

        private CancellationToken _token;

        private void Start()
        {
            // キャンセルトークンの取得
            _token = this.GetCancellationTokenOnDestroy();
            
            // イベントのサブスクライブ
            _levelStateController.OnEndGame
                .Subscribe(_ => OnEndGame(_token).Forget())
                .AddTo(this);
            
            // UIを非表示にする
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ゲームが終了したときに呼び出される処理
        /// </summary>
        private async UniTask OnEndGame(CancellationToken token)
        {
            // テキストの書き換え
            _nickNameText.text = $"名前 ： {PlayerNameStorage.GetPlayerName()}";
            _scoreText.text = $"スコア ： {PointStorage.GetPoint()}";
            _modeText.text = StageData.IsHardStage() ? "Hard" : "Normal";
            // データをランキングに登録
            RankingRegister.Register();
            
            // UIを表示
            gameObject.SetActive(true);
            
            // 開いたときの音を再生
            if(_openAudioClip != null)
                _audioSource.PlayOneShot(_openAudioClip);
            
            // 遅延をかける
            await UniTask.Delay((int)(_delay * 1000), cancellationToken: token);

            // シーンの読み込み
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}