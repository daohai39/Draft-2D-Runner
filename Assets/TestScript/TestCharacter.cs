using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour {
	[SerializeField]
	protected float speed;

	protected bool isFacingRight;

	protected Animator animator;

	public bool Attack {get; set;}

	public Rigidbody2D Rigidbody {get; set;}

	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	protected void ChangeDirection(float horizontal)
	{
		if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
		{
			isFacingRight = !isFacingRight;
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
		}
	}
}