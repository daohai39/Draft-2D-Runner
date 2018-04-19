using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character{

    [SerializeField]
    private int _point = 20;

    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        EnemyManager.Instance.Add(this);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Move();
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
            if (_hp > 0) _hp--;
            else 
                EnemyManager.Instance.Remove(Id);
        }
        Edge edge = other.GetComponent<Edge>();
        if (edge != null)
        {
            ChangeDirection();
        }
    }
}

