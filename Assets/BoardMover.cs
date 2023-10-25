using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class BoardMover : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    private int num = 1;


    async void Start()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(15f));
        panels[num].SetActive(false);
        panels[num+1].SetActive(false);
    }

    public void ToLeft()
    {
        panels[num-1].SetActive(false);
        panels[num-2].SetActive(true);
        num--;
    }

    public void ToRight()
    {
        panels[num-1].SetActive(false);
        panels[num].SetActive(true);
        num++;
    }
}
