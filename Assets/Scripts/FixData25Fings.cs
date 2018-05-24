using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class FixData25Fings : MonoBehaviour
{
    public List<IndexFingerTest> fingerList = new List<IndexFingerTest>();
    SerialHandler serialHandler;
    int len;
    void Start()
    {
        serialHandler = GetComponent<SerialHandler>();
        serialHandler.OnDataReceived += OnDataReceived;
    }
    void OnDataReceived(string message)
    {
        var _datas = message.Split(' ');
        for (int i = 0; i < _datas.Length; i++)
        {
            fingerList[i].Resistance = int.Parse(_datas[i]);
        }
    }
}