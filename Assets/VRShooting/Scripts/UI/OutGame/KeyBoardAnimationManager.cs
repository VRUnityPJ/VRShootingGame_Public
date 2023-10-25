using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace VRShooting.Scripts.UI
{
    public class KeyBoardAnimationManager : MonoBehaviour
    {
        [SerializeField] private float duration = 0.05f;
        
        [SerializeField] private GameObject keyBoard;
        [SerializeField] private EnterButton enterButton;
        [SerializeField] private DeleteButton deleteButton;

        [SerializeField] private Image inputFieldImage;
        [SerializeField] private TextMeshProUGUI inputFieldText;
        
        [SerializeField] private KeyButton[] keyButtons;
        [SerializeField] private GameObject panel,wall;
        private Sequence FadeInSequence;

        private void Start()
        {
            FadeInSequence = DOTween.Sequence().Pause();
            FadeInSequence.Append(inputFieldText.DOFade(1, duration));
            // FadeInSequence.AppendInterval(3f);
            FadeInSequence.Append(inputFieldText.DOText("welcome",0.7f).SetEase(Ease.Linear));
            FadeInSequence.Append(inputFieldText.DOFade(0, 1f).SetEase(Ease.Flash,5,1));
            FadeInSequence.Append((inputFieldImage.DOFillAmount(0.08f, 0.7f)));
            FadeInSequence.Join(inputFieldText.DOText("", 0));
            FadeInSequence.Join(inputFieldText.DOColor(Color.white, 0));
            FadeInSequence.Join((inputFieldImage.DOColor(Color.grey, 0.7f)));

            foreach (var keyButton in keyButtons) // TODO 
            {
                FadeInSequence.AppendCallback(() =>
                {
                    keyButton.DoFadeIn(1, duration*2);
                });
                FadeInSequence.AppendInterval(duration);
            }

            
            FadeInSequence.AppendCallback(() =>
            {
                enterButton.DoFadeIn(1, duration*2);
            });
            
            
            FadeInSequence.AppendCallback(() =>
            {
                deleteButton.DoFadeIn(1, duration*2);
            });

            FadeInSequence.AppendCallback(() =>
            {
                wall.SetActive(false);
            });
            ResetColors();
        }

        public void DoFadeInAnimation()
        {
            if (FadeInSequence != null)
            {
                ResetColors();
                FadeInSequence.Restart();
            }
            else
            {
                Debug.Log("sequenceがnullです");
            }
        }

        private void ResetColors()
        {
            enterButton.DoFadeIn(0,0);
            deleteButton.DoFadeIn(0,0);
            foreach (var btn in keyButtons)
            {
                btn.DoFadeIn(0,0);
            }

            var imgColor =  inputFieldImage.color;
            var txtColor = inputFieldText.color;

            inputFieldImage.color = new Color(imgColor.r, imgColor.g, imgColor.b, 1);
            inputFieldText.color = new Color(txtColor.r, txtColor.g, txtColor.b, 0);

        }

        public void PressEnterButton()
        {
            if (PlayerNameStorage.GetPlayerName().Length < 1)
            {
                inputFieldText.text = "Enter name";
                inputFieldText.DOFade(0, 1f).SetEase(Ease.Flash,8);
            }
            else
            {
                TurnOffKeyBoard();
            }
        }
        
        private void TurnOffKeyBoard()
        {
            panel.SetActive(true);
            var manager =  panel.GetComponentInChildren<StageSelectPanelAnimationManager>();
            manager.DoShowAnimation();
            wall.SetActive((true));
            keyBoard.SetActive(false);
        }
    }
}