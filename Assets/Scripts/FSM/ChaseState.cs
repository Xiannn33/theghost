using UnityEngine;

public class ChaseState : ISate
{
    private FSM manager;
    private float time;
    private Paramter paramter;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.paramter = manager.Param;
    }
    public void OnEnter()
    {
        paramter.Anim.Play("enemy_walk");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        manager.FlipTo(paramter.Target);

        if (paramter.Target == null ||
            manager.transform.position.x < paramter.ChasePoints[0].position.x ||
            manager.transform.position.x > paramter.ChasePoints[1].position.x)
        {
            manager.TransitionState(StateType.idle);
        }
        if (Physics2D.OverlapCircle(paramter.AttackPoint.position, paramter.AttackArea, paramter.TargetLayer))
        {
            manager.TransitionState(StateType.attack);
        }
        else
        {
            if (paramter.Target)
            {
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                    paramter.Target.position, 5 * Time.deltaTime);
            }
        }
    }
}

