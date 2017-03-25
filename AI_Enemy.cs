using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour {


    public enum ENEMY_STATE {PATROL, CHASE, ATTACK};

    public ENEMY_STATE CurrentState
    {
        get { return currentState; }

        set
        {
            currentState = value;

            StopAllCoroutines();

            switch (currentState)
            {
                case ENEMY_STATE.PATROL:

                    StartCoroutine(AIPatrol());

                    break;

                case ENEMY_STATE.CHASE:

                    StartCoroutine(AIChase());

                    break;

                case ENEMY_STATE.ATTACK:

                    StartCoroutine(AIAttack());

                    break;

            }
        }
    }

    public ENEMY_STATE currentState = ENEMY_STATE.PATROL;

    private LineSight ThisLineSight = null;

    private NavMeshAgent ThisAgent = null;

    private Transform ThisTransform = null;

    public Health SnekHealth = null;

    private Transform SnekTransform = null;

    public Transform PatrolDestination = null;

    public float MaxDamage = 10f;

    void Awake()
    {
        ThisLineSight = GetComponent<LineSight>();

        ThisAgent = GetComponent<NavMeshAgent>();

        ThisTransform = GetComponent<Transform>();

        SnekTransform = GetComponent<Transform>();

        SnekHealth = GameObject.FindGameObjectWithTag("Snek").GetComponent<Health>();
    }

     void Start()
    {
       CurrentState = ENEMY_STATE.PATROL;

   }

   

    public IEnumerator AIPatrol()
    {
        while (currentState == ENEMY_STATE.PATROL)
        {
            ThisLineSight.Sensitity = LineSight.SightSensitivity.STRICT;

            ThisAgent.Resume();

            ThisAgent.SetDestination(PatrolDestination.position);

            while (ThisAgent.pathPending)
            {
                yield return null;
            }

            if (ThisLineSight.CanSeeTarget)
            {
                ThisAgent.Stop();

                CurrentState = ENEMY_STATE.CHASE;

                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator AIChase()
    {
        while (currentState == ENEMY_STATE.CHASE)
        {
            ThisLineSight.Sensitity = LineSight.SightSensitivity.LOOSE;

            ThisAgent.Resume();

            ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);

            while (ThisAgent.pathPending)
            {
                yield return null;
            }

            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance + 0.2 )
            {
                ThisAgent.Stop();

                CurrentState = ENEMY_STATE.ATTACK;

                yield break;
            }

            else

            {

                ThisAgent.Resume();

                if (!ThisLineSight.CanSeeTarget)

                {
                    CurrentState = ENEMY_STATE.PATROL;

                }

                yield return null;
            }
        }
    }

    public IEnumerator AIAttack()
    {
        while (currentState == ENEMY_STATE.ATTACK)
        {

            ThisAgent.Resume();

            ThisAgent.SetDestination(SnekTransform.position);

            while (ThisAgent.pathPending)
            {
                yield return null;
            }

            if (ThisAgent.remainingDistance  > ThisAgent.stoppingDistance + 0.3)
            {
               CurrentState = ENEMY_STATE.CHASE;

            }

            else
            {
                SnekHealth.healthpoints -= MaxDamage;

               Debug.Log(ThisAgent.remainingDistance);
            }

            yield return new WaitForSeconds(1);

        }

    }

}
