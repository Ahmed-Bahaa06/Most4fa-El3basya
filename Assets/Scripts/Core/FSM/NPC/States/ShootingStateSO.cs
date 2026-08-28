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
                // Use Vector2 for 2D distance
                float distance = Vector2.Distance(npc.transform.position, npc.Target.position);
                
                if (distance > attackRange && stateWhenPlayerLost != null)
                {
                    npc.ChangeState(stateWhenPlayerLost);
                    return;
                }

                // Removed the 3D LookAt() because it rotates the sprite along the Y/X axis, 
                // making it invisible to a 2D Orthographic camera!

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
