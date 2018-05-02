using UnityEngine;

public class AttackState : IEnemyState
{
	private TestEnemy enemy;
	private float nextTime;
	private float waitTime = 3;
    public void Enter(TestEnemy enemy)
    {
		this.enemy = enemy;
    }

    public void Execute()
    {
			if(enemy.Target != null) {
				enemy.Idle();
				Attack();
			} else { 
				enemy.ChangeState(new IdleState());
			}
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
			enemy.Engage();
		}
	}

}
