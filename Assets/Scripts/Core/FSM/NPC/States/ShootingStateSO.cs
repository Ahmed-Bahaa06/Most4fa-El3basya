using UnityEngine;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Shooting", fileName = "ShootingState")]
    public class ShootingStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float attackRange = 10f;
        public float fireRate = 1f;

        [Header("Transitions")]
        public NPCStateSO stateWhenPlayerLost;

        public override void OnEnter(NPCStateMachine npc)
        {
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = true; 
            }
        }

        public override void OnUpdate(NPCStateMachine npc)
        {
            if (npc.Target != null)
            {
                float distance = Vector3.Distance(npc.transform.position, npc.Target.position);
                
                if (distance > attackRange && stateWhenPlayerLost != null)
                {
                    npc.ChangeState(stateWhenPlayerLost);
                    return;
                }

                npc.transform.LookAt(new Vector3(npc.Target.position.x, npc.transform.position.y, npc.Target.position.z));

                // Using the stateless ActionTimer stored on the NPC Context
                if (npc.ActionTimer >= fireRate)
                {
                    Debug.Log($"[{npc.gameObject.name}] Shoots at the Player!");
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
