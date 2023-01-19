using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : BaseState
{
    public Run(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState) : base(enemy, stateMachine, enemyData, animationState)
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
        enemy.Run();
        if (enemy.isHitted)
        {
            stateMachine.stateChange(enemy.hit);
        }
        if (enemy.isDead)
        {
            stateMachine.stateChange(enemy.dead);
        }
        if (enemy.attack && !enemyData.demon || (!enemy.isRunning && enemyData.demon && enemy.attack))
        {
            stateMachine.stateChange(enemy.simpleAttack);
        }
        if (enemy.isPatroling)
        {
            stateMachine.stateChange(enemy.patrol);
        }
      
        base.LogicUpdate();
    }
}
