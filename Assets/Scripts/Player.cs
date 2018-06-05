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
	public GameObject gameOverText; //UI 3d text object

	[Header ("Physics Settings")]
	float floor;
	public float jumpForce;
	public float speed;
	bool isGrounded;

	Rigidbody2D rb;
	GameObject gameController;
	GameController gameControllerScript;
	SpriteRenderer spriteRenderer;
	Animator anim;

	private float level = 1f;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
		gameController = GameObject.Find ("GameController");
		gameControllerScript = gameController.GetComponent<GameController> ();
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		floor = Mathf.RoundToInt (transform.position.y);
		healthSlider.value = life;
		gameControllerScript.OnVariableChange += InitPlayer;
	}

	public void InitPlayer () {
		anim.enabled = true;
	}

	public void UpdateLevel (float value) {
		level = value;
	}

	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Wall") {
			transform.position = new Vector3 (-1 * transform.position.x, transform.position.y, transform.position.z);
			spriteRenderer.flipX = !spriteRenderer.flipX;

		}

	}

	void FixedUpdate () {
		if (anim.enabled && gameControllerScript.GameStarted) {

			Vector3 move = new Vector3 (Input.GetAxis ("Horizontal") * speed, rb.velocity.y, 0f);
			bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));

			if (flipSprite) {
				spriteRenderer.flipX = !spriteRenderer.flipX;
			}

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
		yield return new WaitForSeconds (2f * (level / 2));
		speed *= 2f;
		jumpForce *= 4f;
	}

	IEnumerator FinishAnimation () {
		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("Hitted", false);
	}

	public void ItemCollision (int item) {
		if (item == 0) { //Beer
			anim.SetBool ("Hitted", true);
			speed *= 0.5f;
			jumpForce *= 0.25f;
			life -= 7.5f;
			StartCoroutine (ResetPlayerSpeed ());
		}
		if (item == 1) { //CD
			anim.SetBool ("Hitted", true);
			life -= 101f;
		}
		if (item == 2) { //Coin
			points++;
			pointsText.GetComponent<Text> ().text = points.ToString ();
			life += 2.5f;
		}

		StartCoroutine (FinishAnimation ());

		if (life > 100f) life = 100f;
		healthSlider.value = life;

		if (life < 0f) {
			life = 0f;
			gameOverText.GetComponent<Text> ().text = "Game Over\n" + points.ToString () + " Puntos Conseguidos";
			gameControllerScript.EndGame ();
		}
	}
}