using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>
[ExecuteInEditMode]
public class LockedLevels : MonoBehaviour {


    void Awake()
    {
        transform.GetChild(1).localScale = Vector3.one;
        PlayerPrefs.SetInt("1", 1);
   
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
        UnLockLevels();
        
    }
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<Text>().text = transform.name;
        transform.GetChild(0).gameObject.GetComponent<Text>().fontSize = 75;
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void UnLockLevels()
    {

        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(1,1,1,1) ;
            GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Star" + gameObject.name) == 3)
            {
                for (int i = 0; i < transform.GetChild(1).childCount; i++)
                {
                    transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                }
            }
            else if (PlayerPrefs.GetInt("Star" + gameObject.name) == 2)
            {
                for (int i = 0; i < transform.GetChild(1).childCount-1; i++)
                {
                    transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
                }
            }
            else if (PlayerPrefs.GetInt("Star" + gameObject.name) == 1)
            {
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);

            }
            else if (PlayerPrefs.GetInt("Star" + gameObject.name) == 0)
            {
                for (int i = 0; i < transform.GetChild(1).childCount; i++)
                {
                    transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GetComponent<Button>().interactable = false;
            transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(1, 1, 1, .5f);
        }
    }
    public void LevelMenu()
    {
        SceneManager.LoadScene(transform.name);
    }
}

