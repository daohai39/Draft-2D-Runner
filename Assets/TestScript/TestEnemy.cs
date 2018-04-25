using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState 
{
	void Execute();
	void Enter(TestEnemy enemy);
	void Exit();
}

public class AttackState : IEnemyState
{
	private TestEnemy enemy;
	private float nextTime;
	private float waitTime = 5;
    public void Enter(TestEnemy enemy)
    {
		this.enemy = enemy;
    }

    public void Execute()
    {
		if(enemy.IsLockOn)
		{
			enemy.Idle();
			Attack();
		}
		else 
			enemy.ChangeState(new IdleState());
    }

    public void Exit()
    {
		enemy.Animator.ResetTrigger("attack");
    }

	private void Attack()
	{
		if (Time.time >= nextTime) {
			nextTime = Time.time + waitTime;
			enemy.Animator.SetTrigger("attack");
			enemy.Shoot();
		}
	}

}

public class IdleState : IEnemyState
{
	private TestEnemy enemy;
	private float waitTime = 3;
	private float time;
    public void Enter(TestEnemy enemy)
    {
		this.enemy = enemy;	
		time = Time.time;
    }

    public void Execute()
    {
		enemy.Idle();
		if (Time.time - time >= waitTime)
		{
			enemy.ChangeState(new RunState());
		} else if (enemy.IsLockOn)
		{
			enemy.ChangeState(new AttackState());
		}

	}

    public void Exit()
    {
		time = 0;
    }

}

public class RunState : IEnemyState
{
	private TestEnemy enemy;
	private float waitTime = 10;
	private float time;
    public void Enter(TestEnemy enemy)
    {
		this.enemy = enemy;
		time = Time.time;
    }

    public void Execute()
    {
		enemy.Run();
		if (Time.time - time >= waitTime)
		{
			enemy.ChangeState(new IdleState());
		} else if (enemy.IsLockOn)
		{
			enemy.ChangeState(new AttackState());
		}
    }

    public void Exit()
    {
		time = 0;
    }

}

public class TestEnemy : TestCharacter {
	
	private IEnemyState currentState;

	[HideInInspector]
	public bool IsLockOn = false;
    public GameObject bulletPrefab; 
    public Transform shotSpawn;

	protected override void Awake() {
		base.Awake();
		ChangeState(new IdleState());
	}
	
	void Update () {
		currentState.Execute();
		
		TargetPlayer();
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
}
