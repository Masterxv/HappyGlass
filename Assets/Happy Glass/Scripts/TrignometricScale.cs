using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>
public class TrignometricScale : MonoBehaviour
{

    public Vector3 ScaleLimit;
    public Vector3 ScaleFrequency;
    Vector3 FinalScale;
    Vector3 StartRotation;
    void Start()
    {
        StartRotation = transform.localScale;
    }
    void Update()
    {
        FinalScale.x = StartRotation.x + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.x) * ScaleLimit.x;
        FinalScale.y = StartRotation.y + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.y) * ScaleLimit.y;
        FinalScale.z = StartRotation.z + Mathf.Sin(Time.timeSinceLevelLoad * ScaleFrequency.z) * ScaleLimit.z;
        transform.localScale = new Vector3(FinalScale.x, FinalScale.y, FinalScale.z);
    }
}
