using UnityEngine;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;
using VRShooting.Scripts.Utility;

namespace VRShooting.Scripts.UI
{
    public class KeyButton : KeyBoardButton
    {
        [SerializeField] private TextUpdater updater;
        [SerializeField] private string myChar;

        private void Start()
        {
            text.text = myChar;
        }

        public override async void Clicked()
        {
            await AddChar();
            updater.UpdateText();
            if(clickedSE != null)
                AudioPlayer.PlayOneShotAudioAtPoint(clickedSE, transform.position);
        }

        private async UniTask AddChar()
        {
            PlayerNameStorage.AddChar(myChar); 
        }
    }
}