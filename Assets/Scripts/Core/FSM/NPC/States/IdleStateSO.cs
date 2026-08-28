using UnityEngine;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Idle", fileName = "IdleState")]
    public class IdleStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float idleDuration = 2f;
        public float sightRange = 10f;

        [Header("Transitions")]
        public NPCStateSO stateWhenFinished;
        public NPCStateSO stateWhenPlayerSpotted;

        public override void OnEnter(NPCStateMachine npc)
        {
            base.OnEnter(npc);
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = true;
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

            if (npc.StateTimer >= idleDuration && stateWhenFinished != null)
            {
                npc.ChangeState(stateWhenFinished);
            }
        }
    }
}
