using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor;

public class UDPReceive : MonoBehaviour
{
    public int LOCAL_PORT;
    public string ESP_ADDRESS;
    static UdpClient udp;
    Thread thread;

    void Start()
    {
        udp = new UdpClient(new IPEndPoint(IPAddress.Parse(ESP_ADDRESS),LOCAL_PORT));
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    void Update()
    {
        Debug.Log(udp.Available);
        if (udp.Available > 0)
        {
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            string text = Encoding.ASCII.GetString(data);
            Debug.Log(text);
        }
    }

    void OnApplicationQuit()
    {
        thread.Abort();
        udp.Close();
    }

    private static void ThreadMethod()
    {
        while (true)
        {
            Debug.Log(udp.Available);
            IPEndPoint remoteEP = null;
            Debug.Log(udp.Available);
            byte[] data = udp.Receive(ref remoteEP);
            Debug.Log(data.Length);
            string text = Encoding.ASCII.GetString(data);
            Debug.Log(text);
        }
    }

}