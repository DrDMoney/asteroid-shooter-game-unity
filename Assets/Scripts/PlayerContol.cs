﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerContol : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    public Boundary boundary;
    public float tilt;
	private AudioSource aS;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
		aS = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
			aS.Play();
        }
    }

	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;



        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );


        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


    }
}
    