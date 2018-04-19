using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    private int _id;
    
    [SerializeField]
    private int _point = 20;

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
        EnemyManager.Instance.Add(this);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player !=null)
        {
            EnemyManager.Instance.Remove(_id);
        }
    }

    public void DestroySelf() {
        gameObject.SetActive(false);
    }
}

