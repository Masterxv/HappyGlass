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
public class TrignometricTanMovement : MonoBehaviour {

    public Vector3 Distance;
    public Vector3 MovementFrequency;
    Vector3 Moveposition;
    Vector3 startPosition;
    public bool circular;

    void Start()
    {
        startPosition = transform.localPosition;
    }
    void Update()
    {

        Moveposition.x = startPosition.x + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.x) * Distance.x;
        if (circular)
        {
            Moveposition.y = startPosition.y + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
        }
        else
        {
            Moveposition.y = startPosition.y + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
        }

        Moveposition.z = startPosition.z + Mathf.Tan(Time.timeSinceLevelLoad * MovementFrequency.z) * Distance.z;
        transform.localPosition = new Vector3(Moveposition.x, Moveposition.y, Moveposition.z);


    }
}
