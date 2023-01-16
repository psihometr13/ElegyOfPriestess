using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    public Idle(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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
        if (enemy.attack)
        {
            stateMachine.stateChange(enemy.simpleAttack);
        }
        if ((enemyData.rooted || enemyData.mosquito) && enemy.attack && enemy.currentHP <= enemyData.maxHealth / 2)
        {
            stateMachine.stateChange(enemy.longAttack);
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
