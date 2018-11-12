using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>
[ExecuteInEditMode]
public class LevelTextSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "L e v e l  " + SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
