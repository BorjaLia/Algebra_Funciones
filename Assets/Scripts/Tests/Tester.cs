using UnityEngine;
using CustomMath;

public class Tester : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Values:");
        Debug.Log($"Epsilon: {MathU.EPSILON}");
        Debug.Log($"DegToRad: {MathU.DEGTORAD}");
        Debug.Log($"RadtoDeg: {MathU.RADTODEG}");
    }

    void Update()
    {
        
    }
}
