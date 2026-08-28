using UnityEngine;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Running", fileName = "RunningState")]
    public class RunningStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float safeDistance = 15f;

        [Header("Transitions")]
        public NPCStateSO stateWhenPlayerLost;

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
                
                if (distance > safeDistance && stateWhenPlayerLost != null)
                {
                    npc.ChangeState(stateWhenPlayerLost);
                    return;
                }

                if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
                {
                    Vector3 fleeDirection = (npc.transform.position - npc.Target.position).normalized;
                    npc.Agent.destination = npc.transform.position + fleeDirection * 5f;
                }
            }
        }
    }
}
