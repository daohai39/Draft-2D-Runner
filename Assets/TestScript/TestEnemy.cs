using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour {
	protected Rigidbody2D _rb;
	protected Animator _anim;
	[SerializeField]
	protected float _speed;
	protected bool _isFacingRight;

	protected virtual void Awake() 
	{
		_isFacingRight = true;
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
	}

	protected virtual void ChangeDirection(float horizontal) 
	{
		if (horizontal > 0 && !_isFacingRight || horizontal < 0 && _isFacingRight)
		{
			_isFacingRight = !_isFacingRight;
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
		}
	}

}

public class TestEnemy : TestCharacter {
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = 0.0f;
		HandleMovement(x);	
	}

	private void HandleMovement(float horizontal)
	{

	}

	private void HandleAttack()
	{

	}


}
