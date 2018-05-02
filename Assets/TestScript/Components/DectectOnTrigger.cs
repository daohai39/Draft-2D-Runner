using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectectOnTrigger : MonoBehaviour {
	[SerializeField]
	private TestEnemy enemy;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			enemy.Target = other.gameObject;
		}
		else return;
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")  {
			enemy.Target = null;
		}
		else return;
	}
}
