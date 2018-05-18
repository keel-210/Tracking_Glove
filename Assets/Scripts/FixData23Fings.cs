using UnityEngine;
public class FixData23Fings : MonoBehaviour
{
    [SerializeField] IndexFingerTest index, middle, ring;
    SerialHandler serialHandler;
    int len;
    void Start ()
    {
        serialHandler = GetComponent<SerialHandler> ();
        serialHandler.OnDataReceived += OnDataReceived;
    }
    void OnDataReceived (string message)
    {
        var _datas = message.Split (' ');
        if (_datas.Length > 1)
        {
            index.Resistance = int.Parse (_datas[0]);
            middle.Resistance = int.Parse (_datas[1]);
            ring.Resistance = int.Parse (_datas[2]);
        }
    }
}