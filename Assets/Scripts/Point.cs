using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	private int _id;
	private int _point = 10;
	public int Id { 
		get { return _id; } 
		set { _id = value; }
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		PointsManager.Instance.Add(this);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player != null)
		{
			PointsManager.Instance.Remove(_id);
		}
	}

	public void DestroySelf()
	{
		gameObject.SetActive(false);
	}

	
}

