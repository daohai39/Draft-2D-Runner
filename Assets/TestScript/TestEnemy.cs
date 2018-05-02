using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : TestCharacter {
	
	private IEnemyState currentState;

    public GameObject bulletPrefab; 
    public Transform shotSpawn;

	public GameObject Target {get; set;}

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
			LockOnTarget();
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

	public void Engage()
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

	private void LockOnTarget()
	{
		if (Target != null)
		{
			var xDir = Target.transform.position.x - transform.position.x;
			if (xDir > 0 && !isFacingRight || xDir < 0 && isFacingRight)
			{
				Flip();
			}
		}
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
