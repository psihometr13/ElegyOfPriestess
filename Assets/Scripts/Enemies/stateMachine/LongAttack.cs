using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAttack : BaseState
{
    public LongAttack(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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
        if (enemy.isHitted)
        {
            Debug.Log("hit");
            stateMachine.stateChange(enemy.hit);
        }
        if (enemy.isDead)
        {
            stateMachine.stateChange(enemy.dead);
        }
        base.LogicUpdate();
    }
}
