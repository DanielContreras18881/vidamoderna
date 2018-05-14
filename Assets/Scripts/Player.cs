using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[Header ("Player Life")]
	public float life = 100f;
	public Slider healthSlider;

	[Header ("Points Management")]
	float points; //Player Score
	public GameObject pointsText; //UI 3d text object

	[Header ("Physics Settings")]
	float floor;
	public float jumpForce;
	public float speed;
	bool isGrounded;
	bool isInsideLimits;

	Rigidbody2D rb;
	GameObject gameController;
	GameController gameControllerScript;
	SpriteRenderer spriteRenderer;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		gameController = GameObject.Find ("Game");
		gameControllerScript = gameController.GetComponent<GameController> ();
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		floor = Mathf.RoundToInt (transform.position.y);
		healthSlider.value = life;
		isInsideLimits = true;
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Wall") {
			rb.isKinematic = true;
			isInsideLimits = false;
			isGrounded = Mathf.RoundToInt (transform.position.y) == floor;
			if (isGrounded) {
				rb.velocity = Vector3.zero;
			} else {
				rb.velocity = Vector3.down * jumpForce;
				//rb.AddForce (Vector2.down * jumpForce, ForceMode2D.Impulse);
			}

		}

	}

	void FixedUpdate () {

		Vector3 move = new Vector3 (Input.GetAxis ("Horizontal") * speed, rb.velocity.y, 0f);
		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));

		if (flipSprite) {
			isInsideLimits = true;
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		if (isInsideLimits) {
			rb.isKinematic = false;
			isGrounded = Mathf.RoundToInt (transform.position.y) == floor;
			if (isGrounded) {

				rb.velocity = move;

				if (Input.GetKeyDown (KeyCode.Space) || Input.GetAxis ("Jump") > 0) {
					rb.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
				}
			}
		}

	}

	IEnumerator ResetPlayerSpeed () {
		yield return new WaitForSeconds (2f * (gameControllerScript.GameLevel / 2));
		speed *= 2f;
		jumpForce *= 4f;
	}

	IEnumerator FinishAnimation () {
		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("Hitted", false);
	}

	public void ItemCollision (int item) {
		if (item == 0) { //Beer
			//TODO: animation
			anim.SetBool ("Hitted", true);
			speed *= 0.5f;
			jumpForce *= 0.25f;
			life -= 7.5f;
			StartCoroutine (ResetPlayerSpeed ());
		}
		if (item == 1) { //CD
			//TODO: animation
			anim.SetBool ("Hitted", true);
			life -= 10f;
		}
		if (item == 2) { //Coin
			points++;
			pointsText.GetComponent<Text> ().text = points.ToString ();
			life += 5f;
		}
		StartCoroutine (FinishAnimation ());
		if (life > 100f) life = 100f;
		if (life < 0f) {
			life = 0f;
			Destroy (gameObject);
		}
		healthSlider.value = life;
	}
}