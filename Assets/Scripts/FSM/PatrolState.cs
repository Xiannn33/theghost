using UnityEngine;

public class PatrolState : ISate
{
    private FSM manager;
    private int patrolPosition;
    private Paramter paramter;
    public PatrolState(FSM manager)
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
        patrolPosition++;
        if (patrolPosition >= paramter.PatrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }

    public void OnUpdate()
    {
        manager.FlipTo(paramter.PatrolPoints[patrolPosition]);
        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            paramter.PatrolPoints[patrolPosition].position, 5*Time.deltaTime);
        if (Vector3.Distance(manager.transform.position, paramter.PatrolPoints[patrolPosition].position)<10f)
        {
            
            manager.TransitionState(StateType.idle);
        }
        if (paramter.Target != null && paramter.Target.position.x >= paramter.ChasePoints[0].position.x &&
    paramter.Target.position.x <= paramter.ChasePoints[1].position.x)
        {
            manager.TransitionState(StateType.react);
        }
    }
}
