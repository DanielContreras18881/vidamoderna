using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dj : MonoBehaviour {

	[Header ("Movement Settings")]
	public float speed;
	public float limit;
	Vector3 center;
	int movementDirection = -1;

	[Header ("Throw Items Settings")]
	public float fireRatep;
	public float fireRatem;
	public GameObject itemToThrow;
	public float throwSpeed;
	public GameObject[] items;
	AudioSource throwAudio;
	float nextFire;

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Camera camera;
	GameObject gameController;
	GameController gameControllerScript;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
		gameController = GameObject.Find ("GameController");
		gameControllerScript = gameController.GetComponent<GameController> ();
		items = GameObject.FindGameObjectsWithTag ("Item");
		throwAudio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		camera = GetComponent<Camera> ();
		center = transform.position;
		gameControllerScript.OnVariableChange += InitDj;
	}

	public void InitDj () {
		anim.enabled = true;
		StartCoroutine (WaitForFire ());
	}

	void Fire () {
		// Randomize fire rotation and object thrown
		GameObject tempItem = Instantiate (items[Random.Range (0, 3)], transform.position, transform.rotation) as GameObject;
		//GameObject tempItem = Instantiate (itemToThrow, transform.position, transform.rotation) as GameObject;
		Rigidbody2D tempRigidBodyBullet = tempItem.GetComponent<Rigidbody2D> ();
		Quaternion rotation = Quaternion.Euler (0, 0, Random.Range (-30, 30));
		Vector3 direction = rotation * tempRigidBodyBullet.transform.up;
		tempRigidBodyBullet.AddForce (direction * -25f * throwSpeed);

		if (tempItem != null) {
			Destroy (tempItem, 25f);
		}

		// Play Audio
		throwAudio.Play ();

		StartCoroutine (WaitForFire ());
	}

	IEnumerator WaitForFire () {
		yield return new WaitForSeconds (Random.Range (fireRatep, fireRatem));
		Fire ();
	}

	public void UpdateLevel (float value) {
		speed *= (.95f + value / 2);
		throwSpeed *= (1.025f + value / 2);
		fireRatep += value;
		fireRatem -= value / 5;

	}

	void FixedUpdate () {
		if (anim.enabled && gameControllerScript.GameStarted) {

			if (Vector3.Distance (center, transform.position) > limit) {
				movementDirection *= -1;
			}

			Vector3 move = Vector3.right * movementDirection * speed * Time.deltaTime;

			transform.Translate (move);

			bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
			if (flipSprite) {
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}
		}
	}
}