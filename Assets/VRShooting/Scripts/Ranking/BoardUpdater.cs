using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace VRShooting.Scripts.Ranking
{
    public class BoardUpdater : MonoBehaviour
    {
        [SerializeField] private BoardAnimationManager _animationManager;
        public void UpdateRanking(List<List<Record>> ranking)
        {
            StartCoroutine(UpdateBoard(ranking));
        }

        private IEnumerator UpdateBoard(List<List<Record>> ranking)
        {
            Debug.Log("board更新するよ");
            _animationManager.FadeOut();
            yield return new WaitForSeconds(0.6f);
            _animationManager.UpdateText(ranking);
            yield return new WaitForSeconds(0.4f);
            _animationManager.FadeIn();
        }
    } 
}