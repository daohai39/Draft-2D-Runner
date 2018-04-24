using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestEnemy : TestCharacter {
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovement();	
	}

	private void HandleMovement()
	{	
		animator.SetFloat("speed",1);
		transform.Translate(GetDirection() * speed);
	}

	private Vector2 GetDirection()
	{
		return isFacingRight ? Vector2.right : Vector2.left;
	}

}
