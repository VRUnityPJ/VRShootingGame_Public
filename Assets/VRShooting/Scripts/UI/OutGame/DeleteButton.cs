using UnityEngine;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;
using VRShooting.Scripts.Utility;

namespace VRShooting.Scripts.UI
{
    public class DeleteButton : KeyBoardButton
    {
        [SerializeField] private TextUpdater updater;
        public override async void Clicked()
        {
            await DelChar();
            updater.UpdateText();
            if(clickedSE != null)
                AudioPlayer.PlayOneShotAudioAtPoint(clickedSE, transform.position);
        }
        
        private async UniTask DelChar()
        {
            PlayerNameStorage.DelChar(); 
        }
    }
}