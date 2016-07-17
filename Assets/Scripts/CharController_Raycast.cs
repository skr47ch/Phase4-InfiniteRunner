using UnityEngine;
using System.Collections;

public class CharController_Raycast : MonoBehaviour {

	public LayerMask collisionMask;
	public int rayCountX = 3;
	public int rayCountY = 3;
	public float speedX = 5;
	public float speedY = 5;
	public float gravity = 5;

//	PlayerController player;
	Rigidbody2D rigidBdy;
	Vector2 centre;
	Vector2 extent;
	Vector2 topLeft, topRight, bottomLeft, bottomRight;
	float raySpacingX, raySpacingY;
	float rayLength;

	bool isFacingRight = true;
	bool isJumping = false;

	float hVelocity;

	void Start () {
//		player = FindObjectOfType<PlayerController>();
		rigidBdy = GetComponent<Rigidbody2D>();
		CalculateBounds();
	}
		
	void Update () {
		CalculateBounds();
		GetDirection();
		CreateRayHorizonal();
		CreateRayVertical();
	}

	void GetDirection() {
		hVelocity = Input.GetAxis("Horizontal");
//		rigidBdy.velocity = new Vector2(hVelocity * speed * Time.deltaTime, rigidBdy.velocity.y);

//		anim.SetFloat("hSpeed", Mathf.Abs(hVelocity));
//		anim.SetFloat("vSpeed", rigidBdy.velocity.y);

		if(hVelocity > 0 && !isFacingRight) Flip();
		else if(hVelocity < 0 && isFacingRight) Flip();
	}

	void Flip() {
		isFacingRight = !isFacingRight;
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void CreateRayHorizonal() {
		// generate ray here
		Vector2 rayOrigin =  isFacingRight ? topRight : topLeft;
		rayLength = speedX * Time.deltaTime;
		int direction = isFacingRight ? 1 : -1;
		for(int i = 0; i < rayCountX; i ++) {
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * direction * rayLength, Color.green);

			if(hit.fraction > 0) {
				direction = 0;
				break;
			}
			rayOrigin.y += raySpacingX;
		}
		transform.Translate(Vector2.right * direction * speedX * Time.deltaTime);
	}


	void CreateRayVertical () {
		// generate ray here
		Vector2 rayOrigin =  isJumping ? topLeft : bottomLeft;
		rayLength = speedY * Time.deltaTime;
		int direction = isJumping ? 1 : -1;
		for(int i = 0; i < rayCountX; i ++) {
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * direction, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.up * direction * rayLength, Color.green);

			if(hit.fraction > 0) {
				direction = 0;
				break;
			}
			rayOrigin.x += raySpacingY;
		}
		speedY += gravity * Time.deltaTime;
		transform.Translate(Vector2.up * direction * speedY * Time.deltaTime);	
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

	void OnDrawGizmos(){
		Debug.DrawRay(topLeft, Vector2.up * -1, Color.red);
		Debug.DrawRay(topLeft, Vector2.left * -1, Color.red);

		Debug.DrawRay(topRight, Vector2.up * -1, Color.red);
		Debug.DrawRay(topRight, Vector2.right * -1, Color.red);

		Debug.DrawRay(bottomLeft, Vector2.up * 1, Color.red);
		Debug.DrawRay(bottomLeft, Vector2.left * -1, Color.red);

		Debug.DrawRay(bottomRight, Vector2.up * 1, Color.red);
		Debug.DrawRay(bottomRight, Vector2.right * -1, Color.red);
	}
}
