using UnityEngine;
using System.Collections;

[System.Serializable]
public class CamBoundry {
	public float xMin, xMax, yMin, yMax, zMin, zMax; // I don't know I will be able to use the y values without cutting off the background at this time
}

public class FollowPlayer : MonoBehaviour {
	public CamBoundry camBoundry; // camera movement boundrys
	public float cameraZoomSpeed;
	private GameObject playerOBJ;

	void Start(){
		playerOBJ = GameObject.FindGameObjectWithTag ("Player");

	}

	
	// Update is called once per frame
	void Update () {
		if (playerOBJ == null) return;
		camFollowPlayerStatic ();


		gameObject.GetComponent<Camera>().fieldOfView = gameObject.GetComponent<Camera>().fieldOfView + Input.GetAxis("Mouse ScrollWheel");
	

	}

	void camFollowPlayerStatic(){
		Vector3 updateCamPosition = new Vector3 (playerOBJ.transform.position.x, transform.position.y, (playerOBJ.transform.position.z));

		//if the player is farther than the camera boundrys set the position to the max or min
		if (updateCamPosition.x > camBoundry.xMax) {
			updateCamPosition.x = camBoundry.xMax;
		}
		if (updateCamPosition.x < camBoundry.xMin) {
			updateCamPosition.x = camBoundry.xMin;
		}
		if (updateCamPosition.z > camBoundry.zMax) {
			updateCamPosition.z = camBoundry.zMax;
		}
		if (updateCamPosition.z < camBoundry.zMin) {
			updateCamPosition.z = camBoundry.zMin;
		}
		//update the camera position
		gameObject.transform.position = updateCamPosition;
	}



}
