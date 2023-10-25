using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace VRShooting.Scripts.UI
{
    public class KeyBoardButton : VRButton
    {
        [SerializeField] protected Color pointedColor;
        [SerializeField] protected Color clickedColor;
        [SerializeField] protected Color defaultColor;
        // [SerializeField] protected float fadeInTime = 0.5f;

        private Image frame;
        protected TextMeshProUGUI text;

        private void Awake()
        {
            frame = GetComponentInChildren<Image>();
            text = GetComponentInChildren<TextMeshProUGUI>();

            var btn = gameObject.GetComponent<Button>();
            btn.onClick.AddListener(Clicked); 
        }

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
            text.color = pointedColor;
        }

        public void ChangeColorToClicked()
        {
            frame.color = clickedColor;
            text.color = clickedColor;
        }

        public void ChangeColorToDefault()
        {
            frame.color = defaultColor;
            text.color = defaultColor;
        }

        public void DoFadeIn(float endValue, float duration)
        {
            frame.DOFade(endValue, duration);
            text.DOFade(endValue, duration);
        }

        public virtual void Clicked(){}
    }
}