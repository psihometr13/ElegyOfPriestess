using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : BaseState
{
    public Hit(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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

        enemy.StartCoroutine("WaitAfterHit");
        if (!enemy.isHitted && !enemy.attack && enemyData.rooted)
        {
            stateMachine.stateChange(enemy.idle);
        }
        if (!enemy.isHitted && enemy.attack)
        {
            stateMachine.stateChange(enemy.simpleAttack);
        }
        if (!enemy.isHitted && enemy.attack && (enemyData.rooted || enemyData.mosquito)
            && enemy.currentHP <= enemyData.maxHealth / 2)
        {
            stateMachine.stateChange(enemy.longAttack);
        }
        if (enemy.isDead)
        {
            stateMachine.stateChange(enemy.dead);
        }
        if (!enemy.isHitted && !enemy.attack && enemy.isPatroling)
        {
            stateMachine.stateChange(enemy.patrol);
        }
        if (!enemy.isHitted && !enemy.attack && enemyData.demon && enemy.isRunning)
        {
            stateMachine.stateChange(enemy.run);
        }
        if (!enemy.isHitted && enemy.isDing)
        {
            stateMachine.stateChange(enemy.ding);
        }
        base.LogicUpdate();
    }


}
