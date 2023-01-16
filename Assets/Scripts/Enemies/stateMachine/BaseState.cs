using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState 
{
    protected Enemy enemy;
    protected StateMachine stateMachine;
    protected EnemyData enemyData;
    protected string animationState;
    protected bool animationFinish;
    public BaseState(Enemy enemy, StateMachine stateMachine, EnemyData enemyData, string animationState)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animationState = animationState;

    }
    public virtual void Check()
    {

    }
    public virtual void Enter()
    {
        animationFinish = false;
        Check();
        enemy.anim.SetBool(animationState, true);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animationState, false);
    }

    public virtual void LogicUpdate() 
    {
        Check();
    }

    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinishTrigger()
    {
        animationFinish = true;
    }
}
