using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using VRShooting.Scripts.Ranking;
using VRShooting.Scripts.Utility;

namespace VRShooting.Scripts.UI
{
    public class StageSelectButton : VRButton
    {
        [SerializeField, Scene,Space,Required] private string sceneName;
        [SerializeField, Space, Required] private RankingType _stagetype;
        
        [SerializeField,Space] private Color pointedColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color clickedColor;
        [SerializeField] private float fadeInTime = 0.5f;
        [SerializeField] private Image frame;
        [SerializeField] private Image stageImage;
        [SerializeField] private TextMeshProUGUI textGUI;
        
        


        private void Start()
        {
            EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

            EventTrigger.Entry entry1 = new EventTrigger.Entry();
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            EventTrigger.Entry entry4 = new EventTrigger.Entry();

            entry1.eventID = EventTriggerType.PointerEnter;
            entry2.eventID = EventTriggerType.PointerExit;
            entry3.eventID = EventTriggerType.PointerDown;
            entry4.eventID = EventTriggerType.PointerUp;

            entry1.callback.AddListener((eventDate) => { ChangeColorToPointed(); });
            entry2.callback.AddListener((eventDate) => { ChangeColorToDefault(); });
            entry3.callback.AddListener((eventDate) => { ChangeColorToClicked(); });
            entry4.callback.AddListener((eventDate) => { ChangeColorToDefault(); });

            trigger.triggers.Add(entry1);
            trigger.triggers.Add(entry2);
            trigger.triggers.Add(entry3);
            trigger.triggers.Add(entry4);
        }
        public void ChangeColorToPointed()
        {
            frame.color = pointedColor;
        }

        public void ChangeColorToDefault()
        {
            frame.color = defaultColor;
        }

        public void ChangeColorToClicked()
        {
            frame.color = clickedColor;
        }

        public void DoFadeIn(float endValue,float duration)
        {
            frame.DOFade(endValue,duration);
            stageImage.DOFade(endValue,duration);
            textGUI.DOFade(endValue, duration);
        }

        public void Clicked()
        {
            if(clickedSE != null)
                AudioPlayer.PlayOneShotAudioAtPoint(clickedSE, transform.position);

            StageData.stagetype = _stagetype;
            SceneManager.LoadScene(sceneName);
        }
    }
}