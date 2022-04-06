using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController
{
    Animator animator;

    enum Param
    {
        Attack,
        IsMove,
    }

    enum State
    {
        Attack,
        Run,
        Idle
    }

    readonly int BaseLayer = 0;
    readonly int UpperLayer = 1;
    readonly int StartTime = 0;

    public PlayerAnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void Attack()
    {
        animator.Play(State.Attack.ToString(), UpperLayer, StartTime);
    }

    public float GetAnimState(int layer)
    {
        return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;
    }

    public void Move()
    {
        animator.Play(State.Run.ToString());
    }

    public void SetMove(bool isMove)
    {
        animator.SetBool(Param.IsMove.ToString(), isMove);
    }
}
