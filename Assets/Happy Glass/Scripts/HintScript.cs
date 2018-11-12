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
[ExecuteInEditMode]
public class HintScript : MonoBehaviour {

    LineRenderer line;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        line = GetComponent<LineRenderer>();
        line.positionCount = transform.childCount;
        for (int i = 0; i < transform.childCount; i++)
        {
            line.SetPosition(i,transform.GetChild(i).position);
        }
	}

}
