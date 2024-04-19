using UnityEngine;

public class Login : MonoBehaviour
{
    void Awake()
    {
        PlayFabManager.LogIn();
    }
}
