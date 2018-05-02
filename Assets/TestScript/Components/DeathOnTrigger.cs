using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTrigger : MonoBehaviour {
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			TestPlayer.Instance.Animator.SetTrigger("die");
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		other.gameObject.SetActive(false);
	}
}
