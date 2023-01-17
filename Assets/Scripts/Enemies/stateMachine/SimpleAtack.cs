using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAtack : BaseState
{
    public SimpleAtack(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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

        enemy.Attack();
        if (!enemy.attack)
        {
            stateMachine.stateChange(enemy.idle);
        }
        if ((enemyData.rooted || enemyData.mosquito) && enemy.currentHP <= enemyData.maxHealth / 2)
        {
            stateMachine.stateChange(enemy.longAttack);
        }
        if (enemy.isHitted)
        {
            stateMachine.stateChange(enemy.hit);
        }
        if (enemy.isDead)
        {
            stateMachine.stateChange(enemy.dead);
        }
        if (!enemy.attack && enemyData.demon && enemy.isRunning)
        {
            stateMachine.stateChange(enemy.run);
        }
        if (!enemy.attack && enemyData.demon && enemy.isPatroling)
        {
            stateMachine.stateChange(enemy.patrol);
        }
        base.LogicUpdate();
    }
}
