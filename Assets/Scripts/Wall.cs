using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wall : MonoBehaviour{
	private int _id;

	public int Id {
		get {
			return _id;
		} 
		set {
			_id = value;
		}
	}

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        WallManager.Instance.Add(this);
    }

	public void DestroySelf()
	{
		gameObject.SetActive(false);
	}
}