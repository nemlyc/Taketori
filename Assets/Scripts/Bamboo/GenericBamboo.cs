using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class GenericBamboo : MonoBehaviour, IBamboo
{
    readonly float DelayTime = 1000f;

    public virtual void AttackAction()
    {
        /*
         * -> Split Bamboo
         * -> Animation
         * -> Destroy
         */
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Break");

        Observable.Timer(TimeSpan.FromMilliseconds(DelayTime))
            .Subscribe(_ =>
            {
                Destroy(transform.parent.gameObject);
            });
    }

    public abstract int CalcScore(int current);

    public abstract BambooInfo.BambooType GetBambooType();
}
