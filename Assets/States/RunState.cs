using UnityEngine;

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
