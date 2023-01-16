using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : BaseState
{
    public Dead(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Check()
    {
        base.Check();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        enemy.StartCoroutine("Res");
        enemy.isDead = false;
        base.LogicUpdate();
    }
}
