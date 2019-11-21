using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : MonoBehaviour, IState   //added monobehaviour!
{
    //private NavMeshAgent navAgent;
    //private float stunDuration;   //can make this int sometime. Its float for reusability.

    //public StunnedState(NavMeshAgent navAgent, float stunDuration)
    //{
    //    this.navAgent = navAgent;
    //    this.stunDuration = stunDuration;
    //}

    public void Enter()
    {
        //Stun(stunDuration);
    }

    public void Execute()
    {
        //TODO!!!!!
        //throw new System.NotImplementedException();
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }

    //IEnumerator Stun(float stunDuration)     // NOT WORKING
    //{
    //    navAgent.speed = 0;

    //    Debug.Log("Stunned " + Time.time);

    //    yield return new WaitForSeconds(stunDuration);

    //    Debug.Log("UnStunned " + Time.time);

    //    navAgent.speed = 10;   //Also can set this to imported speed and use returnToPreviousSpeed;
    //}
}

