using UnityEngine;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Security Chase", fileName = "SecurityChaseState")]
    public class SecurityChaseStateSO : NPCStateSO
    {
        [Header("Settings")]
        public float chaseDistance = 15f;
        public float meleeAttackRange = 1.5f;
        public float meleeAttackRate = 1f;
        public float meleeDamage = 10f;

        [Header("Transitions")]
        public NPCStateSO stateWhenPlayerLost;

        public override void OnEnter(NPCStateMachine npc)
        {
            base.OnEnter(npc);
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = false;
                npc.ActionTimer = meleeAttackRate; // Ready to attack immediately when in range
            }
            
            KhosaryCode.Audio.SoundManager.Instance.PlaySound(KhosaryCode.Audio.SoundType.GuardMeleeSpotPlayer, npc.transform.position);
        }

        public override void OnUpdate(NPCStateMachine npc)
        {
            if (npc.Target != null)
            {
                float distance = Vector2.Distance(npc.transform.position, npc.Target.position);
                
                if (distance > chaseDistance && stateWhenPlayerLost != null)
                {
                    npc.ChangeState(stateWhenPlayerLost);
                    return;
                }

                if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
                {
                    // Continuously path towards the player
                    npc.Agent.destination = npc.Target.position;
                    
                    if (distance <= meleeAttackRange)
                    {
                        if (npc.ActionTimer >= meleeAttackRate)
                        {
                            PerformMeleeAttack(npc);
                            npc.ActionTimer = 0f;
                        }
                    }
                }
                
                npc.ActionTimer += Time.deltaTime;
            }
        }

        private void PerformMeleeAttack(NPCStateMachine npc)
        {
            IDamagable damagable = npc.Target.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(meleeDamage);
                KhosaryCode.VisualFeedbacks.VFXManager.Instance.PlayVFX(KhosaryCode.VisualFeedbacks.VFXType.MeleeHit, npc.Target.position);
                Debug.Log($"[{npc.gameObject.name}] melee attacked Player for {meleeDamage} damage!");

                var officerVisual = npc.GetComponentInChildren<OfficerVisual>();
                if (officerVisual != null)
                {
                    officerVisual.PlayAttackAnimation();
                }
            }
        }
    }
}
