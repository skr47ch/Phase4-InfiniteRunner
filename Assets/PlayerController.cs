using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 speed = new Vector2(2f, 5f);
	int direction = 1;
	
	void Start () {
	
	}

	void Update () {
		float moveDirection = Input.GetAxisRaw("Horizontal");
		Debug.Log(moveDirection);
		transform.Translate(Vector2.right * speed.x * moveDirection * Time.deltaTime);
		transform.Translate(Vector2.up * speed.y * direction * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D collison) {
		if(collison.gameObject.CompareTag("Walls")) {
			direction *= -1;
			Debug.Log("Collisio");
		}
	}
}
