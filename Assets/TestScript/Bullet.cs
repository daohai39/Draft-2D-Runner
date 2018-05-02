using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class Bullet : MonoBehaviour {
	private Vector2 direction;

	[SerializeField]
	private float speed;
	
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		myRigidbody.velocity = speed * direction;
	}


	public void Initialize(Vector2 direction)
	{
		this.direction = direction;
	}
}
