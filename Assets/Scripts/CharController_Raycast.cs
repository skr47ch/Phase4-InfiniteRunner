using UnityEngine;
using System.Collections;

public class CharController_Raycast : MonoBehaviour {

	public LayerMask collisionMask;
	public int rayCountX = 3;
	public int rayCountY = 3;

	PlayerController player;
	Vector2 centre;
	Vector2 extent;
	Vector2 topLeft, topRight, bottomLeft, bottomRight;
	float raySpacingX, raySpacingY;
	float rayLength = 2.0f;

	void Start () {
		player = FindObjectOfType<PlayerController>();
		CalculateBounds();
	}
		
	void Update () {
		CalculateBounds();


		// generate ray here
		Vector2 rayOrigin =  player.isFacingRight ? topRight : topLeft;
		int direction = player.isFacingRight ? 1 : -1;
		for(int i = 0; i < rayCountX; i ++) {
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, collisionMask);
			Debug.DrawRay(rayOrigin, Vector2.right * direction * rayLength, Color.green);
			rayOrigin.y += raySpacingX;

			Debug.Log(rayOrigin + "    " + i);
		}
	}

	void CalculateBounds() {
		// Calculates the bounds of hibox based on current sprite
		Bounds bound = GetComponent<SpriteRenderer>().bounds;
		centre = bound.center;
		extent = bound.extents;
		topLeft = centre - extent;
		topRight = new Vector2(centre.x + extent.x, centre.y - extent.y);
		bottomRight = centre + extent;
		bottomLeft = new Vector2(centre.x - extent.x, centre.y + extent.y);

		raySpacingX = (bound.max.y - bound.min.y) / (rayCountX - 1);
		raySpacingY = (bound.max.x - bound.min.x) / (rayCountY - 1);

//		Debug.Log(extent + " " + (bound.max.y - bound.min.y));

	}
}
