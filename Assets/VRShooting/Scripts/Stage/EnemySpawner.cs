using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;


namespace VRShooting.Scripts.Stage
{
    /// <summary>
    /// 敵をスポーンさせるクラス。
    /// スポーンの実行範囲はSpawnerLevelScaleと違い、自身のみと小さい。
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        /// <summary>
        /// 全てのスポーンが終わったかどうか
        /// </summary>
        public bool isEndSpawn => _isEndSpawn;
        
        /// <summary>
        /// スポーン設定
        /// </summary>
        [SerializeField, Required]
        private EnemySpawnSetting _spawnSetting;
        
        /// <summary>
        /// スポーン範囲を表すボックス。
        /// </summary>
        [SerializeField, Required] 
        private Transform _spawnRoomBox;
        
        /// <summary>
        /// スポーンが終わっているかどうかを示すフラグ
        /// </summary>
        private bool _isEndSpawn = false;
        
        /// <summary>
        /// 敵をスポーンさせる
        /// </summary>
        /// <param name="token"></param>
        public async UniTask SpawnAsync(CancellationToken token)
        {
            _isEndSpawn = false;

            // スポーンボックスの回転を取得
            var spawnBoxQuaternion = _spawnRoomBox.transform.rotation;
            
            // スポーンする範囲を計算
            var spawnBoxScale = _spawnRoomBox.localScale;
            var spawnHalfScaleX = Mathf.Abs(spawnBoxScale.x / 2f);
            var spawnHalfScaleY = Mathf.Abs(spawnBoxScale.y / 2f);
            var spawnHalfScaleZ = Mathf.Abs(spawnBoxScale.z / 2f);
            
            foreach (var spawnEnemyConfig in _spawnSetting.enemyList)
            {
                // スポーンする数
                var spawnNumber = spawnEnemyConfig.spawnNumber;
                
                // スポーン間隔を計算
                var spawnInterval = (int)(spawnEnemyConfig.secondDuration / spawnNumber * 1000);
                
                for (int i = 0; i < spawnNumber; i++)
                {
                    // スポーンする座標を計算
                    var dx = Random.Range(-spawnHalfScaleX, spawnHalfScaleX);
                    var dy = Random.Range(-spawnHalfScaleY, spawnHalfScaleY);
                    var dz = Random.Range(-spawnHalfScaleZ, spawnHalfScaleZ);
                    var spawnPositionDelta = spawnBoxQuaternion * new Vector3(dx, dy, dz);
                    var spawnPosition = spawnPositionDelta + transform.position;
                    
                    // 敵オブジェクトをスポーン
                    var spawnedObject = Instantiate(spawnEnemyConfig.enemyObject.gameObject);
                    spawnedObject.transform.position = spawnPosition;
                    
                    // インターバル分だけ待機
                    // UniTask.Delay( 待機したい時間をミリ秒単位で指定, キャンセルトークン )
                    await UniTask.Delay(spawnInterval, cancellationToken: token);
                }
            }

            _isEndSpawn = true;
        }
    }
}