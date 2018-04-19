using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D _rb;

	private bool _isJump;
	
	private float _jumpForce = 10f;

	private float _moveForce = 20f;

	[SerializeField]
	private float _maxSpeed = 5;
	
	void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

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
	
	void Move()
	{
		var horizontalMove = Input.GetAxis("Horizontal");
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
