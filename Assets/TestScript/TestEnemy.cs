using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : TestCharacter {
	
	private IEnemyState currentState;

	[HideInInspector]
	public bool IsLockOn;
    public GameObject bulletPrefab; 
    public Transform shotSpawn;

    public override bool IsDead 
	{
		get {
			return health <= 0;
		}	
	}

    protected override void Awake() {
		base.Awake();
		ChangeState(new IdleState());
	}
	
	void Update () {
		if (!IsDead) {
			if (!TakeDamage) {
				currentState.Execute();
			}
			TargetPlayer();
		}
	}

	private Vector2 GetDirection()
	{
		return isFacingRight ? Vector2.right : Vector2.left;
	}
 
	public void ChangeState(IEnemyState state)
	{
		//if valid state
		if (currentState!=null)
		{
			currentState.Exit();
		}
		//Exit current state
		//Swap state
		currentState = state;
		//Enter new state
		currentState.Enter(this);
	}
	
	public void Idle()
	{
		Animator.SetBool("idle", true);
		Rigidbody.velocity = Vector2.zero;
	}

	public void Run()
	{
		Animator.SetBool("idle", false);
		transform.Translate(speed * GetDirection() * Time.deltaTime);
	}

	public void Shoot()
	{
		if (isFacingRight)
        {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        } else if (!isFacingRight) {
            GameObject tmp = Instantiate(bulletPrefab, shotSpawn.position, Quaternion.identity);
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
	}
	public void TargetPlayer()
	{
		// declare min distance between player and enemy
		Vector2 minDistance = new Vector2(10,0);
		// calculate distance between player and enenmy
		Vector2 distance = TestPlayer.Instance.transform.position - transform.position;
		// if player within distance && facing each other 
		// attack
		if (minDistance.x >= Mathf.Abs(distance.x) && minDistance.y <= Mathf.Abs(distance.y) - 1)
			IsLockOn = true;
		// else reset
		else 
			IsLockOn = false;
	}

	public void ChangeDirection() 
	{
		Flip();
	}

    public override IEnumerator TakingDamage()
    {
		health -= 10;
		if (!IsDead) {
			Animator.SetTrigger("damage");
		} else {
			Animator.SetTrigger("die");
		}
			yield return null;
    }
	
}
