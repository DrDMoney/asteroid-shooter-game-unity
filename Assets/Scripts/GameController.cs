using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;

	private GUIText scoreText;
	private GUIText gameOverText;
	private GUIText restartText;
	private int score;

	void Start()
	{
		//Spawning waves
		StartCoroutine(SpawnWaves ());


		//Scoring
		//Gets the gameobject instance with the tag "score text"
		score = 0;
		GameObject scoreTextObject = GameObject.FindGameObjectWithTag ("Score Text");
		if (scoreTextObject != null)
		{
			scoreText = scoreTextObject.GetComponent<GUIText> ();
		}

		if (scoreText == null) {
			Debug.Log("Cannot find scoring text");
		}
		UpdateScore ();


		//Game over
		GameObject gameOverObject = GameObject.FindGameObjectWithTag ("Game Over");
		if (gameOverObject != null)
		{
			gameOverText = gameOverObject.GetComponent<GUIText> ();
		}
		if (gameOverText == null) {
			Debug.Log("Cannot find game-over text");
		}

		//restart
		GameObject restartObject = GameObject.FindGameObjectWithTag ("Restart");
		if (restartObject != null)
		{
			restartText = restartObject.GetComponent<GUIText> ();
		}
		if (restartText == null) {
			Debug.Log("Cannot find restart text");
		}
			
		gameOverText.enabled = false;
		restartText.enabled = false;
		restartText.text = "";
		gameOverText.text = "";
	}

	void Update(){
		if (restartText.enabled) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		
		}
	}

	//swawn hazzard
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, Random.Range(spawnValues.z,spawnValues.z + 1.0f));
				Quaternion spawnRotation = Quaternion.identity;
				var instanAsteroid = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;
				float num= Random.Range (.3f, 2.5f);
				instanAsteroid.transform.localScale = new Vector3 (num, num, num);
				instanAsteroid.GetComponent<DestroyByContact> ().scoreValue = (int)(num * 10);
				instanAsteroid.GetComponent<Rigidbody> ().mass = num;






				yield return new WaitForSeconds (spawnWait);
			}
				
			yield return new WaitForSeconds (waveWait);
			if (gameOverText.enabled) {break;}

		}
	}

	public void AddScore(int newScoreValue)
	{
		score = score + newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void PlayerDeath (){
		gameOverText.text = "Game over";
		gameOverText.enabled = true;
		restartText.text = "Press 'R' to Restart";
		restartText.enabled = true;

	}
}
