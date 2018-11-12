using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>

public class GlassFill : MonoBehaviour {

    int trigCont;
    GameManager gm;
    int Star;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DynamicParticle")
        {
            if (trigCont == 0)
            {
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = gm.SurpriseGlass;

            }
            col.gameObject.tag = "InGlassWater";
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = .3f;
            col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity /4;
            trigCont++;

            if (trigCont > 50)
            {
                if (trigCont == 51)
                {
                    if (Mathf.FloorToInt(gm.PenCapacity.value * 100) > 75)
                    {
                        Star = 3;
                    }
                    else if (Mathf.FloorToInt(gm.PenCapacity.value * 100) > 50)
                    {
                        Star = 2;
                    }
                    else if (Mathf.FloorToInt(gm.PenCapacity.value * 100) > 25)
                    {
                        Star = 1;
                    }
                    print(Star + "star");
                    transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = gm.HappyGlass;
                    Camera.main.GetComponent<AudioSource>().Play();
                    Invoke("nextScene", 2);
                    CancelInvoke("Check");
                    for (int i = 0; i < Camera.main.transform.childCount; i++)
                    {
                        if (Camera.main.transform.GetChild(i).GetComponent<ParticleSystem>() != null)
                        {
                            Camera.main.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                        }
                    }
                }               
            }
            else
            {
                CancelInvoke("Check");
                Invoke("Check",5);
            }
            if (trigCont > 60)
            {                                               //You can write  a function over here if you want to give a star according to glass fill
                print("two star");
            }           
            if (trigCont > 70)
            {
                print("three star");
            }
        }
    }
    void Check()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void nextScene()
    {
        PlayerPrefs.SetInt((SceneManager.GetActiveScene().buildIndex).ToString(), 1);
        PlayerPrefs.SetInt("Star" + SceneManager.GetActiveScene().name, Star);
        gm.LevComp.SetActive(true);
        if (Star > 2)
        {
            gm.LevComp.transform.GetChild(0).gameObject.SetActive(true);
            gm.LevComp.transform.GetChild(1).gameObject.SetActive(true);
            gm.LevComp.transform.GetChild(2).gameObject.SetActive(true);
        } 
        else if (Star > 1)
        {
            gm.LevComp.transform.GetChild(0).gameObject.SetActive(true);
            gm.LevComp.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Star>0)
        {
            gm.LevComp.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
