using UnityEngine;
using System.Collections;

public class RayCastControl : MonoBehaviour {

	public LayerMask collisionMask;
	public int rayCountX = 3;
	public int rayCountY = 3;
	public float speedX = 5;
	public float speedY = 5;
	public float gravity = 15.0f;
	public float jumpSpeed = -5.0f;
	Vector2 velocity = Vector2.zero;

	//	PlayerController player;
	Rigidbody2D rigidBdy;
	Vector2 centre;
	Vector2 extent;
	Vector2 topLeft, topRight, bottomLeft, bottomRight;
	float raySpacingX, raySpacingY;
	float rayLength;

	bool isFacingRight = true;
	bool isJumping = false;
	bool isGrounded = false;

	float hVelocity;

	void Start () {
		CalculateBounds();
	}

	void Update () {
		CalculateBounds();
		ApplyPhysics();
		if(Input.anyKeyDown) {
			Jump();
		}

		Debug.Log("IsJumping : " + isJumping);
		Debug.Log("IsGrounded : " + isGrounded);
	}


	void ApplyPhysics() {
		CreateRayVertical();
	}

	void Jump(){

		velocity.y = jumpSpeed;
		isGrounded = false;
		isJumping = true;
	}

	void CreateRayVertical () {
		// generate ray here
		Vector2 rayOrigin =  (isJumping) ? topLeft : bottomLeft;
		rayLength = speedY * Time.deltaTime;
		int direction = (isJumping) ? 1 : -1;
		for(int i = 0; i < rayCountX; i ++) {
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down * direction, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.down * direction * rayLength * 5, Color.green);

			if(hit.fraction > 0) {
//				direction = 0;
				isGrounded = true;
				isJumping = false;
				break;
			}
			rayOrigin.x += raySpacingY;
		}
//		speedY = (isGrounded) ? 0 : speedY + gravity * Time.deltaTime;
		velocity.y =  direction * speedY * Time.deltaTime;
		transform.Translate(velocity);	
	}

	void CalculateBounds() {
		// Calculates the bounds of hibox based on current sprite
		Bounds bound = GetComponent<SpriteRenderer>().bounds;
		centre = bound.center;
		extent = bound.extents;
		topLeft = new Vector2(centre.x - extent.x, centre.y + extent.y);
		topRight = centre + extent;
		bottomLeft = centre - extent;
		bottomRight = new Vector2(centre.x + extent.x, centre.y - extent.y);

		raySpacingX = (bound.max.y - bound.min.y) / (rayCountX - 1);
		raySpacingY = (bound.max.x - bound.min.x) / (rayCountY - 1);

		//		Debug.Log(extent + " " + (bound.max.y - bound.min.y));

	}
}
