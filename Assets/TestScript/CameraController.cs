using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private float _xMin;

	[SerializeField]
	private float _xMax;

	[SerializeField]
	private float _yMin;

	[SerializeField]
	private float _yMax;

	[SerializeField]
	private Transform _player;
	
	private Vector3 _offset;
	void Start () {
		_offset = _player.position - transform.position;
		_offset = new Vector3(_offset.x, _offset.y/2, _offset.z);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = _player.position - _offset;
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, _xMin, _xMax),
			Mathf.Clamp(transform.position.y, _yMin, _yMax),
			transform.position.z
		);
	}
}
