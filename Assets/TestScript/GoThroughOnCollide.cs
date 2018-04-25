using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughOnCollide : MonoBehaviour {
	[SerializeField]
	private Collider2D collider;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true);
	}

}
