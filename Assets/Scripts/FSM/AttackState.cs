using UnityEngine;

public class AttackState : ISate
{
    private FSM manager;
    private Paramter paramter;
    private AnimatorStateInfo info;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.paramter = manager.Param;
    }
    public void OnEnter()
    {
        paramter.Anim.Play("enemy_attack");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        info = paramter.Anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > 0.95f)
        {
            manager.TransitionState(StateType.chase);
        }
    }
}
