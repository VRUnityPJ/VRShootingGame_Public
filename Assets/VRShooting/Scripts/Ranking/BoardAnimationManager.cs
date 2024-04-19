using DG.Tweening;
using Ranking.Scripts.DataBase;
using TMPro;
using UnityEngine;
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

            for (int i = 0; i < normalNameTexts.Length; i++)
            {
                normalNameTexts[i].text = "0" + "\n" + "none";
                hardNameTexts[i].text = "0" + "\n" + "none";
            }
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

        public void UpdateNormalText(RankingData[] records)
        {
            if(records.Length != normalNameTexts.Length)
                Debug.Log("取得したランキングの数がリーダーボードと一致していません");
            
            for (int i = 0; i < records.Length; i++)
            {
                Point point = records[i].GetData<Point>();
                PlayerName name = records[i].GetData<PlayerName>();

                normalNameTexts[i].text = point.point.ToString() + "\n" + name.name;
            }
        }
        public void UpdateHardText(RankingData[] records)
        {
            if(records.Length != hardNameTexts.Length)
                Debug.Log("取得したランキングの数がリーダーボードと一致していません");
            
            for (int i = 0; i < records.Length; i++)
            {
                Point point = records[i].GetData<Point>();
                PlayerName name = records[i].GetData<PlayerName>();

                hardNameTexts[i].text = point.point.ToString() + "\n" + name.name;
            }
        }
    }
}