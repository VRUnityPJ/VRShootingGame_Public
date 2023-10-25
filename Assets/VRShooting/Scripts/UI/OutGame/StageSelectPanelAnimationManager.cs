using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRShooting.Scripts.UI;

namespace VRShooting.Scripts.UI
{
    public class StageSelectPanelAnimationManager : MonoBehaviour
    {
        private StageSelectButton[] normalStageButtons;
        private StageSelectButton[] hardStageButtons;
        [SerializeField] private GameObject normal, hard;
        [SerializeField] private Image normalLineImage,normalBackImage;
        [SerializeField] private Image hardLineImage,hardBackImage;
        [SerializeField] private TextMeshProUGUI normalText, hardText;
        [SerializeField] private TextMeshProUGUI comingSoonText;
        [SerializeField] private TextMeshProUGUI stageSelectText;
        [SerializeField] private Image stageSelectLine1, stageSelectLine2;
        [SerializeField] private GameObject wall;
        private Sequence showSequence;

        private void Awake()
        {
            
            normalStageButtons = normal.GetComponentsInChildren<StageSelectButton>();
            hardStageButtons = hard.GetComponentsInChildren<StageSelectButton>();
            
            ResetColors();

            showSequence = DOTween.Sequence().Pause();
            
            showSequence.Append(stageSelectText.DOFade(1, 0.4f))
                .Join(stageSelectLine1.DOFillAmount(1, 2f))
                .Join(stageSelectLine2.DOFillAmount(1, 2f))
                .Append(stageSelectText.DOFade(0, 0.5f).SetEase(Ease.Flash, 5,1))
                .Join(stageSelectLine1.DOFade(0, 0.5f))
                .Join(stageSelectLine2.DOFade(0, 0.5f));

            showSequence
                .Append(normalBackImage.DOFade(1, 0.3f))
                .Join(hardBackImage.DOFade(1, 0.3f));
                
                
            
            showSequence.Append(normalLineImage.DOFillAmount(1f, 0.3f));
            showSequence.Join(hardLineImage.DOFillAmount(1f, 0.3f));
            showSequence.Join(normalText.DOFade(1f, 0.3f));
            showSequence.Join(hardText.DOFade(1f, 0.3f));
            showSequence.Join(comingSoonText.DOFade(1f, 0.3f));
            

            foreach (var button in normalStageButtons)
            {
                showSequence.AppendCallback(() =>
                    {
                        button.DoFadeIn(1,0.2f);
                    }
                );
                showSequence.AppendInterval(0.4f);
            }

            foreach (var button in hardStageButtons)
            {
                showSequence.AppendCallback(() =>
                    {
                        button.DoFadeIn(1,0.2f);
                    }
                );
                showSequence.AppendInterval(0.4f);
            }

            showSequence.AppendInterval(1f);
            
            showSequence.AppendCallback(() =>
            {
                wall.SetActive(false);
            });
        }

        public void DoShowAnimation()
        {
            if (showSequence != null)
            {
                ResetColors();
                wall.SetActive(true);
                showSequence.Restart();
            }
            else
            {
                Debug.Log("sequenceがないよ");
            }
        }

        private void ResetColors()
        {
            normalLineImage.fillAmount = 0;
            normalBackImage.DOFade(0, 0);
            normalText.DOFade(0, 0);

            hardLineImage.fillAmount = 0;
            hardBackImage.DOFade(0, 0);
            hardText.DOFade(0, 0);

            comingSoonText.DOFade(0, 0);

            stageSelectLine1.fillAmount = 0;
            stageSelectLine2.fillAmount = 0;
            stageSelectText.DOFade(0, 0);

            foreach (var btn in normal.GetComponentsInChildren<StageSelectButton>())
            {
                btn.DoFadeIn(0,0);
            }

            foreach (var btn in hard.GetComponentsInChildren<StageSelectButton>())
            {
                btn.DoFadeIn(0,0);
            }
        }
    }
}