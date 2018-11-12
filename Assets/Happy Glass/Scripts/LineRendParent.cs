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
public class LineRendParent : MonoBehaviour {
    LineRenderer lines;
    // Use this for initialization
    void Start () {
        lines = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        lines.positionCount = 2;
        if (transform.name == "PreDictLine")
        {
            lines.SetPosition(0, transform.position);
            lines.SetPosition(1, (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
        else
        {
            lines.SetPosition(0, transform.position);
            lines.SetPosition(1, transform.parent.GetChild(transform.GetSiblingIndex() - 1).position);
            lines.sortingOrder = 3;
        }
    }
}
