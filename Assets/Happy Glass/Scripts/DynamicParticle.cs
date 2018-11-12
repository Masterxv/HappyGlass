using UnityEngine;
using System.Collections;

/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update.
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>

public class DynamicParticle : MonoBehaviour {

    public float GasFlotability = -.5f;                      //How fast does the gas goes up
    public float particleLifeTime = 1f;                      //How much time before the particle scalesdown and dies	
    float startTime;
    float scaleValue;
    bool gas;

    void FixedUpdate()
    {
        ScaleDown();
    }

    // The effect for the gas particle to seem to fade away
    void ScaleDown()  
    {
        if (gas)
        {
            scaleValue = 1.0f - ((Time.time - startTime) / particleLifeTime);
            Vector2 particleScale = Vector2.one;
            if (scaleValue <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                particleScale.x = scaleValue;
                particleScale.y = scaleValue;
                transform.localScale = particleScale;
            }
        }
    }
    void OnBecameInvisible()
    {
        if (FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().WaterCheck();
        }       
    }
    void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.transform.tag == "Hot")
        {
            startTime = Time.time;
            gas = true;
            Destroy(GetComponent<TrailRenderer>());
            GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f, 1);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = GasFlotability;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
    }
}
