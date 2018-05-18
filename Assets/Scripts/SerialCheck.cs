using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialCheck : MonoBehaviour
{
	SerialHandler serialHandler;
	string Resistance;
	int len;
	void Start ()
	{
		serialHandler = GetComponent<SerialHandler> ();
		serialHandler.OnDataReceived += OnDataReceived;
	}

	void Update ()
	{
		Debug.Log (Resistance);
	}

	void OnDataReceived (string message)
	{
		Resistance = message;
	}
}