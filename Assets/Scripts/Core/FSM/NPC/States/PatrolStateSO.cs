using UnityEngine;

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
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = false;
            }
        }

        public override void OnUpdate(NPCStateMachine npc)
        {
            if (npc.Target != null)
            {
                float distance = Vector3.Distance(npc.transform.position, npc.Target.position);
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
