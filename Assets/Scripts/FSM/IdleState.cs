using UnityEngine;

public class IdleState : ISate
{
    private FSM manager;
    private float time;
    private Paramter paramter;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.paramter = manager.Param;
    }
    public void OnEnter()
    {
        paramter.Anim.Play("enemy_idle");
    }

    public void OnExit()
    {
        time = 0;
    }

    public void OnUpdate()
    {
        time += Time.deltaTime;
        if (time >= 1.0f)
        {
            manager.TransitionState(StateType.patrol);
        }
        if(paramter.Target!=null && paramter.Target.position.x>=paramter.ChasePoints[0].position.x &&
            paramter.Target.position.x <= paramter.ChasePoints[1].position.x)
        {
            manager.TransitionState(StateType.react);
        }
    }
}
