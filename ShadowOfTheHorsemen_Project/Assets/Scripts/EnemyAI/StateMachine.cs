using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentlyActiveState;
    private IState previouslyActiveState;


	public void ChangeState(IState newState)
    {
        if (this.currentlyActiveState != null)                        //Ensures there is an active state.
        {
            this.previouslyActiveState = this.currentlyActiveState;
            this.currentlyActiveState.Exit();
        }
        this.currentlyActiveState = newState;
        this.currentlyActiveState.Enter();
    }
    public void ExecuteStateUpdate()                             //State Machine provides a way to execute what's inside IState objects.
    {
        var runningState = this.currentlyActiveState;
        if (runningState != null)                               //Making sure it's not empty
        {
            runningState.Execute();
        }
    }
    public void SwitchToPreviousState()                         
    {
        this.currentlyActiveState.Exit();
        this.currentlyActiveState = this.previouslyActiveState;
        this.currentlyActiveState.Enter();
    }
}
