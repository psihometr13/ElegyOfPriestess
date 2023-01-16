using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseState currentState { get; private set; }
    
    public void Inizialization(BaseState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void stateChange(BaseState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
