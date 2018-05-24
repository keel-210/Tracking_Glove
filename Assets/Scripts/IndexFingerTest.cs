using UnityEngine;

public class IndexFingerTest : MonoBehaviour
{
    [SerializeField] Transform Index1, Index2, Index3;
    [SerializeField] int maxAngular, minAngular;
    [SerializeField, Range(0, 1)] float AngleRate1, AngleRate2, AngleRate3;
    public int Resistance;
    Quaternion index1Rot, index2Rot, index3Rot;
    float angle, angularOffset;
    void Start()
    {
        if (Index1)
            index1Rot = Index1.localRotation;
        if (Index2)
            index2Rot = Index2.localRotation;
        if (Index3)
            index3Rot = Index3.localRotation;
        angularOffset = 90f / (maxAngular - minAngular);
    }
    void Update()
    {
        angle = Mathf.Lerp(angle, (Resistance - minAngular) * angularOffset, 0.1f);
        if (Index1)
        {
            Index1.localRotation = index1Rot * Quaternion.Euler(AngleRate1 * angle * Vector3.up);
        }
        if (Index2)
        {
            Index2.localRotation = index2Rot * Quaternion.Euler(AngleRate2 * angle * Vector3.up);
        }
        if (Index3)
        {
            Index3.localRotation = index3Rot * Quaternion.Euler(AngleRate3 *+ angle * Vector3.up);
        }
    }
}