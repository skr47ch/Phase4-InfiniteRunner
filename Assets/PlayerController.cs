using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform groundCheck;
	public LayerMask mask;

	public float speed = 2f;
	int direction = 1;
	public float jumpForce;
	Rigidbody2D body;
	Vector2 lastFrameVelocity;
	bool isGrounded = false;

	void Start () {
		body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {

		RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.01f, mask);
		Debug.DrawRay(groundCheck.position, Vector2.down * 0.01f, Color.red);
		if(hit) isGrounded = true;


		Debug.Log(isGrounded);

//		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1<< LayerMask.NameToLayer("Ground"));
		if(isGrounded) {

			isGrounded = false;
			body.AddForce(Vector2.up * jumpForce);
		}
		float moveDirection = Input.GetAxisRaw("Horizontal");
//		Debug.Log(moveDirection);
		transform.Translate(Vector2.right * speed * moveDirection * Time.deltaTime);
		//transform.Translate(Vector2.up * jumpVelocity * direction * Time.deltaTime);
//		lastFrameVelocity = body.velocity;
	}

//	void Jump() {
//		body.AddForce(Vector2.up * jumpForce);
//		isGrounded = false;
//	}

//	void OnCollisionEnter2D(Collision2D collision) {
//		if(collision.gameObject.CompareTag("Walls")) {
//			isGrounded = true;	
//		}
//	}

//	void OnCollisionExit2D() {
//		isGrounded = false;
//	}
}
