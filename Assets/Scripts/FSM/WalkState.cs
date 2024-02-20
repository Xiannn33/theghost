using UnityEngine;

public class WalkState : ISate
{
    private FSM manager;
    private Paramter paramter;
    private AnimatorStateInfo info;
    public WalkState(FSM manager)
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
    }
}
