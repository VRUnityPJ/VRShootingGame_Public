using System.Collections.Generic;
using System.Collections;
using Ranking.Scripts.DataBase;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace VRShooting.Scripts.Ranking
{
    public class BoardUpdater : MonoBehaviour
    {
        [SerializeField] private BoardAnimationManager _animationManager;
        public void UpdateRanking(RankingData[] ranking,RankingType type)
        {
            //ノーマルステージの時
            if ((int)type < 3)
            {
                StartCoroutine(UpdateNormalBoard(ranking));
            }
            //ハードステージの時
            else
            {
                StartCoroutine(UpdateHardBoard(ranking));
            }
        }

        private IEnumerator UpdateNormalBoard(RankingData[] ranking)
        {
            Debug.Log("board更新するよ");
            _animationManager.FadeOut();
            yield return new WaitForSeconds(0.6f);
            _animationManager.UpdateNormalText(ranking);
            yield return new WaitForSeconds(0.4f);
            _animationManager.FadeIn();
        }
        
        private IEnumerator UpdateHardBoard(RankingData[] ranking)
        {
            Debug.Log("board更新するよ");
            _animationManager.FadeOut();
            yield return new WaitForSeconds(0.6f);
            _animationManager.UpdateHardText(ranking);
            yield return new WaitForSeconds(0.4f);
            _animationManager.FadeIn();
        }
    } 
}