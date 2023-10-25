using UnityEngine;
using VRShooting.Scripts.Utility;

/// <summary>
/// FirstStageのBGMを管理するクラス
/// </summary>
public class Audio : MonoBehaviour
{
    [SerializeField] private AudioClip BGM;

    [SerializeField] private float _volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //インスペクターで設定した値でBGMを流し、ループさせる
        AudioPlayer.PlayOneShotAudioAtPoint(BGM, Vector3.zero,_volume).loop = true;
    }
}
