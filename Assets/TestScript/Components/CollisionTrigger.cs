using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {

	private BoxCollider2D _player;

	[SerializeField]
	private BoxCollider2D _collider;

	[SerializeField]
	private BoxCollider2D _trigger;
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Test Player").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(_collider, _trigger, true);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Test Player") {
			Physics2D.IgnoreCollision(_player, _collider, true);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Test Player") {
			Physics2D.IgnoreCollision(_player, _collider, false);
		}
	}
}
