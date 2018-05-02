using UnityEngine;

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
