using System;
using UnityEngine;
using UnityEngine.UI;

namespace VRShooting.Scripts.UI
{
    /// <summary>
    /// 一定時間見つめた後にUIアニメーションを始めるためのクラス
    /// </summary>
    public class CheckSight : MonoBehaviour
    {
        [SerializeField] private KeyBoardAnimationManager manager;
        [SerializeField] private float goalSec;
        [SerializeField] private float range;
        private float nowSec = 0;
        private Vector3 pos;
        private void Start()
        {
            pos = transform.position;
        }

        private void Update()
        {
            var cameraDir = Camera.main.transform.forward;
            var dot = Vector3.Dot(cameraDir, (transform.position-Camera.main.transform.position).normalized);
            if (Mathf.Abs(dot) > range)
            {
                nowSec += 0.1f*Time.deltaTime;
                if (nowSec > goalSec)
                {
                    manager.DoFadeInAnimation();
                    this.enabled = false;
                }
            }
            else
            {
                if(nowSec < 0)
                    return;
                nowSec -= 0.1f*Time.deltaTime;
            }
        }
    }
}