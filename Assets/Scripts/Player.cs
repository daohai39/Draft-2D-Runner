using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


	private bool _isJump;
	
	private float _jumpForce = 10f;

	private float _moveForce = 20f;

	[SerializeField]
	private float _maxSpeed = 5;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
			_isJump = true;
	}
	void FixedUpdate()
	{
		Move();
		if (_isJump) Jump();
	}
	
	protected override void Move()
	{
		var horizontalMove = Input.GetAxis("Horizontal");
		if (horizontalMove  > 0 && !isFacingRight || horizontalMove < 0 && isFacingRight)
			ChangeDirection();
		_rb.AddForce(horizontalMove * Vector2.right * _moveForce);
		if (Mathf.Abs(_rb.velocity.x) > _maxSpeed )
			_rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxSpeed, _rb.velocity.y);
	}

	void Jump()
	{
		_rb.AddForce(new Vector2(0f, _jumpForce),ForceMode2D.Impulse);
		_isJump = false;
	}
}
