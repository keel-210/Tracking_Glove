using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPServer : MonoBehaviour
{
	[SerializeField] int LOCAL_PORT = 8888;
	[SerializeField] string host = "224.0.0.251";
	UdpClient udp;
	Thread thread;

	void Start()
	{
		udp = new UdpClient(LOCAL_PORT);
		//udp.Connect(host, LOCAL_PORT);
		thread = null;
		thread = new Thread(new ThreadStart(ThreadMethod));
		thread.IsBackground = true;
		thread.Start();
	}

	void OnApplicationQuit()
	{
		try
		{
			thread.Abort();
		}
		catch
		{
			Debug.Log("Thread Abording Failed");
		}
		Debug.Log(thread);
		Debug.Log(thread.IsAlive);
	}

	private void ThreadMethod()
	{
		Thread.Sleep(10);
		try
		{
			byte[] dgram = Encoding.UTF8.GetBytes("hello world!");
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(host), LOCAL_PORT);
			udp.BeginSend(dgram, dgram.Length, remoteEP, new AsyncCallback(send), udp);
			udp.BeginReceive(new AsyncCallback(recv), udp);
			Debug.Log("Bigin Receiving UDP Packets");
		}
		catch
		{
			Debug.Log("UDP Connection Error");
		}
	}
	void send(IAsyncResult result)
	{
		udp.EndSend(result);
		Debug.Log("send UDP packet");
	}
	void recv(IAsyncResult result)
	{
		Debug.Log("receive UDP packet");
		IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, LOCAL_PORT);
		byte[] received = udp.EndReceive(result, ref remoteEP);

		Debug.Log(Encoding.UTF8.GetString(received));
		udp.BeginReceive(new AsyncCallback(recv), null);
	}
}