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

public class ParticleGenerator : MonoBehaviour {
    
    public float WaterSpawnTimeInterval = 0.01f;        //How much time until the next particle spawns
    public float TapClosingTime = 2.5f;                 //How much time iy will take to close the tap
    float lastSpawnTime = float.MinValue;               //The last spawn time
    private Vector3 particleForce;                      //Is there a initial force particles should have?
	private Transform particlesParent;                  //Where will the spawned particles will be parented (To avoid covering the whole inspector with them)

	void Start() {
        Invoke("TapClose", TapClosingTime);
        GetComponent<AudioSource>().Play();
        particlesParent = this.transform;
        if (transform.eulerAngles == Vector3.zero)
        {
            particleForce = Vector3.zero;
        }
        if (transform.eulerAngles.z == 90)
        {
            particleForce = new Vector3(90,0,0);
        }
        if (transform.eulerAngles.z == 270)
        {
            particleForce = new Vector3(-90, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (lastSpawnTime + WaterSpawnTimeInterval < Time.time)                                                 // Is it time already for spawning a new particle?
        { 
            GameObject newLiquidParticle = (GameObject)Instantiate(Resources.Load("DynamicParticle"), new Vector3(Random.Range(transform.position.x - .001f,
                transform.position.x + .001f), Random.Range(transform.position.y - .001f, transform.position.y
                + .001f), 0), Quaternion.identity);                                                                                      //Spawn a particle Dont remove the particle from resource folder
            newLiquidParticle.GetComponent<Rigidbody2D>().AddForce(particleForce);                              //Add our custom force
            DynamicParticle particleScript = newLiquidParticle.GetComponent<DynamicParticle>();                 // Get the particle script
                                                                                 // Relocate to the spawner position
            newLiquidParticle.transform.parent = particlesParent;                                               // Add the particle to the parent container			
            lastSpawnTime = Time.time;                                                                          // Register the last spawnTime			
        }
    }

    void TapClose()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<ParticleGenerator>().enabled = false;
    }
}
