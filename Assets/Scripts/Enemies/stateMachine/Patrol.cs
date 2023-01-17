using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseState
{
    public Patrol(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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
        if (!enemyData.rooted)
        {
            enemy.Patroler();
        }
        if (enemy.isHitted)
        {
            stateMachine.stateChange(enemy.hit);
        }
        if (enemy.isDead)
        {
            stateMachine.stateChange(enemy.dead);
        }
        if (enemy.isRunning && enemyData.demon)
        {
            stateMachine.stateChange(enemy.run);
        }

        if (enemy.isDing)
        {
            enemy.Dinging();
            stateMachine.stateChange(enemy.ding);
        }
        base.LogicUpdate();
    }
}
