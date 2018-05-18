using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    public int LOCAL_PORT;
    static UdpClient udp;
    Thread thread;

    void Start()
    {
        udp = new UdpClient(LOCAL_PORT);
        Debug.Log(udp);
    }

    void Update()
    {
        Debug.Log("runnnig");
        IPEndPoint remoteEP = null;
        byte[] data = udp.Receive(ref remoteEP);
        string text = Encoding.ASCII.GetString(data);
        Debug.Log(text);
    }

}