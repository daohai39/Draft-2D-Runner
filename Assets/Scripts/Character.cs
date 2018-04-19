using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
	private int _id;

	[SerializeField]
	protected int _hp = 3;

	[SerializeField]
	protected float _speed = 0.05f;

	protected bool _isFacingRight = true;

	protected Rigidbody2D _rb;

    public int Id {
        get {
            return _id;
        }
        set {
            _id = value;
        }
    }

	public virtual void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}


    protected virtual void Move()
    {
        if (_isFacingRight) {
            _rb.MovePosition(_rb.position + Vector2.right * _speed * Time.deltaTime);
        } else {
            _rb.MovePosition(_rb.position + Vector2.left * _speed * Time.deltaTime);
        }
    }


    protected virtual void ChangeDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
        Debug.Log("Change Direction");
    }
    public virtual void DestroySelf() {
        gameObject.SetActive(false);
    }
}