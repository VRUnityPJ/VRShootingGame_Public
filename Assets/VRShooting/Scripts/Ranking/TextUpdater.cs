using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    private TextMeshProUGUI textGUI;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<TextMeshProUGUI>(out textGUI))
        {
            Debug.LogError($"{gameObject.name}：Textが取得できていません");
        }
    }

    public async void UpdateText()
    {
        //これどうにかしたい
        // await UniTask.Delay(1);
        textGUI.text = PlayerNameStorage.GetPlayerName();
    }
}
