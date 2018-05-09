using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour {

	public float rotationSpeed = 2.0f;

	GameObject gameController;
	GameController gameControllerScript;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("Game");
		gameControllerScript = gameController.GetComponent<GameController> ();

	}

	// Update is called once per frame
	void Update () {
		rotationSpeed *= gameControllerScript.GameLevel;
		// rotate lights located in the center of the screen
		// transform.Rotate (0, 0, rotationSpeed * Time.deltaTime);

		// rotate lights located around the screen
		gameObject.transform.GetChild (0).Rotate (0, 0, rotationSpeed * Time.deltaTime);
		gameObject.transform.GetChild (1).Rotate (0, 0, rotationSpeed * 2 * Time.deltaTime);
		gameObject.transform.GetChild (2).Rotate (0, 0, rotationSpeed * 3 * Time.deltaTime);
		gameObject.transform.GetChild (3).Rotate (0, 0, -rotationSpeed * 2 * Time.deltaTime);
		gameObject.transform.GetChild (4).Rotate (0, 0, -rotationSpeed * Time.deltaTime);
	}
}