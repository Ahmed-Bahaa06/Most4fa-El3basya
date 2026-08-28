using UnityEngine;
using Pathfinding;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Running", fileName = "RunningState")]
    public class RunningStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float safeDistance = 15f;
        public float updateInterval = 0.5f;
        public float fleeLookAhead = 10f;

        [Header("Transitions")]
        public NPCStateSO stateWhenPlayerLost;

        public override void OnEnter(NPCStateMachine npc)
        {
            base.OnEnter(npc);
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = false;
                npc.ActionTimer = updateInterval; // Force immediate update
            }
        }

        public override void OnUpdate(NPCStateMachine npc)
        {
            if (npc.Target != null)
            {
                // Use Vector2 to prevent Z-axis issues in 2D
                float distance = Vector2.Distance(npc.transform.position, npc.Target.position);
                
                if (distance > safeDistance && stateWhenPlayerLost != null)
                {
                    npc.ChangeState(stateWhenPlayerLost);
                    return;
                }

                if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
                {
                    if (npc.ActionTimer >= updateInterval)
                    {
                        // Calculate flee direction strictly on X and Y
                        Vector2 fleeDirection = ((Vector2)npc.transform.position - (Vector2)npc.Target.position).normalized;
                        Vector3 desiredDestination = (Vector2)npc.transform.position + fleeDirection * fleeLookAhead;
                        
                        // Ensure the destination is constrained to the walkable graph bounds
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

                        npc.ActionTimer = 0f;
                    }
                    else
                    {
                        npc.ActionTimer += Time.deltaTime;
                    }
                }
            }
        }
    }
}
