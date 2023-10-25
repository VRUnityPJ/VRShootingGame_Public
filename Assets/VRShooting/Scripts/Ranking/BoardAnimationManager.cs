using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace VRShooting.Scripts.Ranking
{
    public class BoardAnimationManager : MonoBehaviour
    {
        [SerializeField] private GameObject NormalName;
        [SerializeField] private GameObject HardName;
        [SerializeField] private Button[] buttons;
        private TextMeshProUGUI[] normalNameTexts;
        private TextMeshProUGUI[] hardNameTexts;
        private void Start()
        {
            normalNameTexts = NormalName.GetComponentsInChildren<TextMeshProUGUI>();
            hardNameTexts = HardName.GetComponentsInChildren<TextMeshProUGUI>();
        }

        public void FadeOut()
        {
            foreach (var btn in buttons)
            {
                btn.interactable = false;
            }
            
            foreach (var txt in normalNameTexts)
            {
                txt.DOFade(0, 0.5f);
            }
            
            foreach (var txt in hardNameTexts)
            {
                txt.DOFade(0, 0.5f);
            }
        }

        public void FadeIn()
        {
            var seq = DOTween.Sequence().Pause();
            
            for (int i = 0; i < normalNameTexts.Length; i++)
            {
                seq.Append(normalNameTexts[i].DOFade(1, 0.4f));
                seq.Join(hardNameTexts[i].DOFade(1, 0.4f));
                seq.AppendInterval(0.2f); 
            }

            seq.AppendCallback(() =>
            {
                foreach (var btn in buttons)
                {
                    btn.interactable = true;
                }
            });
            seq.Restart();
        }

        public void UpdateText(List<List<Record>> records)
        {
            for (int i = 0; i < normalNameTexts.Length; i++)
            {
                normalNameTexts[i].text =  records[0][i].score.ToString()+ "\n" + records[0][i].name;
                hardNameTexts[i].text =  records[1][i].score.ToString()+ "\n" + records[1][i].name;
            }
        }
    }
}