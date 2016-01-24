using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerBoundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerContol : MonoBehaviour {
	private Rigidbody rigidbodyComp;  //player rigidbody; Get comp in Start()
	public PlayerBoundary playerBoundry; // player movement boundrys
	private AudioSource shotAudioComp; //shot audio comp attached to player game object
    public GameObject shot; //bolt gameobject
    public Transform shotSpawn; // location where the bolt will spawn when fired

    public float fireRate; 
    private float nextFire;
	public float playerTilt;
	public float playerSpeed;

    void Start()
    {	
		//Used for Player playermovment and shooting
		//gets the components in the player gameobject
		rigidbodyComp = GetComponent<Rigidbody>();
		shotAudioComp = GetComponent<AudioSource>();
		nextFire = 0.0f;
    }


    void Update()
    {
		if (Input.GetButton("Fire1") && Time.time > nextFire)//if the firebutton is pressed and enough time had passed
        {
            nextFire = Time.time + fireRate; // sets nextFire to the current time + the time of firerate to calculate when the next shot can be fired
			Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation); //make the gameobject "bolt" in this case
			shotAudioComp.Play(); // play the audio for fireing weapon
        }
    }


void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbodyComp.velocity = movement * playerSpeed;

        rigidbodyComp.position = new Vector3
            (
                Mathf.Clamp(rigidbodyComp.position.x, playerBoundry.xMin, playerBoundry.xMax),
                0.0f,
                Mathf.Clamp(rigidbodyComp.position.z, playerBoundry.zMin, playerBoundry.zMax)
            );


        rigidbodyComp.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbodyComp.velocity.x * -playerTilt);
    }




}


    