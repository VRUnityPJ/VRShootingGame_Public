using UnityEngine;
using DG.Tweening;
using System;
using VRShooting.Scripts.Utility;

namespace VRShooting.Scripts.UI
{
    public class EnterButton : KeyBoardButton
    {
        public override void Clicked()
        {
            if(clickedSE != null)
                AudioPlayer.PlayOneShotAudioAtPoint(clickedSE, transform.position); 
        }
    }
}