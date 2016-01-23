using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject asteroid;
	public int scoreValue;
	private GameController gameController;
	private float chunksScaleX;

	public float chunks;
	public float variance;

	void Start (){
	
		//Finds instance of gamecontoller object
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			//gets the gamecontoller script
			gameController = gameControllerObject.GetComponent <GameController>();
		}

		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script");
		}

	}


	void OnTriggerEnter (Collider other)
    {
		//Don't want to distroy asteroids inside the boundy and asteroids that have nodamage do no damnage to player.  
		if (other.tag == "Boundry" || (gameObject.tag == "NoDamage" && other.tag != "Bolt"))
        {
            return;
        }

		var explosionIntance = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		explosionIntance.transform.localScale = gameObject.transform.localScale;

		if (gameObject.transform.localScale.x > 1.0) {
			makeAsteroidChunks (chunks);

		}

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			//gameController.PlayerDeath ();
		}
		gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
    }




	void makeAsteroidChunks(float chunks){
		for (int i = 0; i < chunks; i = i + 2) {
			//random size of chunks based off current gameobject size
			chunksScaleX = gameObject.transform.localScale.x/(chunks/Random.Range(1.0f,2.5f));
			var asteroidIntance = Instantiate (asteroid, new Vector3 (transform.position.x + Random.Range (-variance, variance), 0.0f, transform.position.z + Random.Range (-variance, variance)), new Quaternion (Random.Range (-180, 180), Random.Range (-180, 180), Random.Range (-180, 180), Random.Range (-180, 180))) as GameObject;
			asteroidIntance.transform.localScale = new Vector3 (chunksScaleX, chunksScaleX, chunksScaleX);
			if (asteroidIntance.transform.localScale.x < 0.5f) {
				asteroidIntance.tag ="NoDamage";
			}

		}
	
	
	}
}