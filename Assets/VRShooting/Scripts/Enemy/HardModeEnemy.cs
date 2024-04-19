using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRShooting.Scripts.Ranking;
using Random = System.Random;

namespace VRShooting.Scripts.Enemy
{
    public class HardModeEnemy : MonoBehaviour
    {
        // /// <summary>
        // /// ハードシーンの名前
        // /// </summary>
        // [SerializeField,Scene] private string[] _hardSceneNames;

        /// <summary>
        /// 移動スピードを上げる倍率
        /// </summary>
        [SerializeField, Range(1, 2)] private float _speedUpRatio = 1.3f;
        
        /// <summary>
        /// scaleを下げる倍率
        /// </summary>
        [SerializeField, Range(0,1)] private float _scaleRatio;

        private void Start()
        {
            if (IsHardScene())
            {
                if(!TryGetComponent<EnemyMovement>(out var movement))
                    Debug.Log("EnemyMovementが取得できません");
                
                movement.SpeedUp(_speedUpRatio);
                this.gameObject.transform.localScale *= _scaleRatio;

                Debug.Log("Enemy:ハードモードです");
            }
        }

        private bool IsHardScene()
        {
            //ステージタイプが2以下でなかった場合hard
            return !((int)StageData.stagetype < 3);
        }
    }
}