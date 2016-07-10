using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public LayerMask whatIsGround;

	public float speed = 5.0f;
	public float groundRadius = 0.2f;
	public float jumpForce = 500f;
	public float dieForce = 500f;

	Transform checkGround;
	Rigidbody2D rigidBdy;
	Animator anim;

	bool isFacingRight = true;
	bool isGrounded = false;
	bool isAlive = true;
	bool isRestart = false;
	bool isSliding = false;

	int direction;
	int health = 100;


	void Start () {
		anim = GetComponent<Animator>();
		rigidBdy = GetComponent<Rigidbody2D>();
		checkGround = GameObject.Find("CheckGround").GetComponent<Transform>();
		Debug.Log(checkGround.name);
	}

	void FixedUpdate() {
		Movement();
	}

	void Update () {
		//Jump();
		if (isGrounded && (Input.GetKeyDown(KeyCode.Space))) {
			Jump();
		}
		if (Input.GetKey(KeyCode.LeftShift)) {
			isSliding = true;
		}
		else isSliding = false;

		SetAnimationParameters();
	}

	void SetAnimationParameters() {
		anim.SetBool("isSliding", isSliding);
		anim.SetBool("isGrounded", isGrounded);
	}

	void Movement() {
		if(health < 1) return;

		if(Physics2D.OverlapCircle(checkGround.position, groundRadius, whatIsGround)) {
			Debug.Log("Touch");
		}

		float hVelocity = Input.GetAxis("Horizontal");
		rigidBdy.velocity = new Vector2(hVelocity * speed * Time.deltaTime, rigidBdy.velocity.y);

		anim.SetFloat("hSpeed", Mathf.Abs(hVelocity));
		anim.SetFloat("vSpeed", rigidBdy.velocity.y);

		if(hVelocity > 0 && !isFacingRight) Flip();
		else if(hVelocity < 0 && isFacingRight) Flip();
	}

	void Flip() {
		isFacingRight = !isFacingRight;
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void Jump() {
		if(health < 1) return;
		isGrounded = false;
		rigidBdy.AddForce(new Vector2(0, jumpForce));
//		if(isGrounded && (Input.GetKeyDown(KeyCode.Space))) {
//			anim.SetBool("isGrounded", false);
//			rigidBdy.AddForce(new Vector2(0, jumpForce));
//		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
	}

	void OnCollisionStay2D(Collision2D collision) {
		if(collision.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if(collision.gameObject.CompareTag("Ground")) {
			isGrounded = false;
		}
	}

	void Dead() {
		anim.SetTrigger("Dead");
		Debug.Log("Dead");
		health = 0;
	}
}
