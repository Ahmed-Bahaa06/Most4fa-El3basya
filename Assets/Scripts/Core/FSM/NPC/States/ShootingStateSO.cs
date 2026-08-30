using UnityEngine;
using KhosaryCode.Combat;

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
            
            KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.GuardRangedSpotPlayer, npc.transform.position);
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

                // Using the stateless ActionTimer stored on the NPC Context
                if (npc.ActionTimer >= fireRate)
                {
                    Vector2 direction = ((Vector2)npc.Target.position - (Vector2)npc.transform.position).normalized;
                    
                    if (npc is SecurityStateMachine guard && guard.Weapon != null)
                    {
                        guard.Weapon.Fire(direction);
                    }
                    else
                    {
                        Debug.LogWarning($"[{npc.gameObject.name}] Cannot shoot: Missing ProjectileWeapon on SecurityStateMachine!");
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
