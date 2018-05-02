using UnityEngine;

public class IdleState : IEnemyState
{
	private TestEnemy enemy;
	private float waitTime;
	private float time;
	public void Enter(TestEnemy enemy)
	{
		this.enemy = enemy;	
		time = Time.time;
	}

    public void Execute()
    {
			enemy.Idle();
			waitTime = Random.Range(1.0f,10.0f);
			Debug.Log(waitTime);
			if (Time.time - time >= waitTime)
			{
				enemy.ChangeState(new RunState());
			} else if (enemy.Target != null)
			{
				enemy.ChangeState(new AttackState());
			}

	}

    public void Exit()
    {
		time = 0;
    }

}
