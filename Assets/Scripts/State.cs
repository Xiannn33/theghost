using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    private Animator anim;
    public State(Animator anim)
    {
        this.anim = anim;
    }
    /// <summary>
    /// 角色状态
    /// </summary>
    public enum PlayerAnimState
    {
        idle,
        walk,
        jump,
        fall,
        attack,
        hurt
    }
    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="state">状态</param>
    public void ChangeState(PlayerAnimState state)
    {
        switch (state)
        {
            case PlayerAnimState.idle:
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Jump", false);
                //anim.SetBool("Kick", false);
                //anim.SetBool("Crouch", false);
                //anim.SetBool("Punch", false);
                //anim.SetBool("Hurt", false);
                break;
            case PlayerAnimState.walk:
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", false);
                anim.SetBool("Attack", false);
                break;
            case PlayerAnimState.jump:
                anim.SetBool("Idle",false);
                anim.SetBool("Walk",false);
                anim.SetBool("Fall",false);
                anim.SetBool("Jump",true);
                break;
            case PlayerAnimState.fall:
                anim.SetBool("Idle", false);
                anim.SetBool("Jump", false);
                anim.SetBool("Walk",false);
                anim.SetBool("Fall", true);
                break;
            case PlayerAnimState.attack:
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", false);
                anim.SetBool("Attack", true);
                break;
                //case PlayerAnimState.punch:
                //    anim.SetBool("Idle", false);
                //    anim.SetBool("Walk", false);
                //    //anim.SetBool("Punch", true);
                //    break;
                //case PlayerAnimState.crouch:
                //    anim.SetBool("Idle", false);
                //    //anim.SetBool("Crouch", true);
                //    break;
                //case PlayerAnimState.crouch_kick:
                //    anim.SetBool("Idle", false);
                //    //anim.SetBool("Crouch", true);
                //    //anim.SetBool("Kick", true);
                //    break;
                //case PlayerAnimState.hurt:
                //    anim.SetBool("Idle", false);
                //    //anim.SetBool("Hurt", true);
                //    break;
        }
    }
}
