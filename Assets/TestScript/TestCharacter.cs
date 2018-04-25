using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour {
	[SerializeField]
	protected float speed;

	[HideInInspector]
	public bool isFacingRight;

	public Animator Animator;

	public bool Attack {get; set;}

	public Rigidbody2D Rigidbody {get; set;}

	protected virtual void Awake()
	{
		isFacingRight = true;
		Animator = GetComponent<Animator>();
		Rigidbody = GetComponent<Rigidbody2D>();
	}

	protected void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
	}
}