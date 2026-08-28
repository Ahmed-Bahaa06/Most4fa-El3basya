using UnityEngine;
using Pathfinding;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Patrol", fileName = "PatrolState")]
    public class PatrolStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float patrolDuration = 4f; 
        public float sightRange = 10f;

        [Header("Transitions")]
        public NPCStateSO stateWhenFinished;
        public NPCStateSO stateWhenPlayerSpotted;

        public override void OnEnter(NPCStateMachine npc)
        {
            base.OnEnter(npc);
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = false;
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector3 desiredDestination = (Vector2)npc.transform.position + randomDirection * 5f;
                
                if (AstarPath.active != null)
                {
                    NNInfo nnInfo = AstarPath.active.GetNearest(desiredDestination, NNConstraint.Default);
                    if (nnInfo.node != null)
                    {
                        npc.Agent.destination = (Vector3)nnInfo.position;
                    }
                    else
                    {
                        npc.Agent.destination = desiredDestination;
                    }
                }
                else
                {
                    npc.Agent.destination = desiredDestination;
                }
            }
        }

        public override void OnUpdate(NPCStateMachine npc)
        {
            if (npc.Target != null)
            {
                float distance = Vector2.Distance(npc.transform.position, npc.Target.position);
                if (distance <= sightRange && stateWhenPlayerSpotted != null)
                {
                    npc.ChangeState(stateWhenPlayerSpotted);
                    return;
                }
            }

            if (npc.StateTimer >= patrolDuration && stateWhenFinished != null)
            {
                npc.ChangeState(stateWhenFinished);
            }
        }
    }
}
