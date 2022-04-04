using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class KaguyaBamboo : GenericBamboo
{
    readonly float DelayTime = 1500;

    InGameViewPresenter presenter;
    private void Awake()
    {
        presenter = FindObjectOfType<InGameViewPresenter>();
    }

    public override void AttackAction()
    {
        presenter.GamePreClearEvent.Invoke();

        var animator = GetComponent<Animator>();
        animator.SetTrigger("Break");

        Observable.Timer(TimeSpan.FromMilliseconds(DelayTime))
            .Subscribe(_ =>
            {
                presenter.GameClearEvent.Invoke();

                Destroy(transform.parent.gameObject);
            });
    }

    public override int CalcScore(int current)
    {
        return current * BambooInfo.KaguyaScoreMagnification;
    }

    public override BambooInfo.BambooType GetBambooType()
    {
        return BambooInfo.BambooType.Kaguya;
    }
}
