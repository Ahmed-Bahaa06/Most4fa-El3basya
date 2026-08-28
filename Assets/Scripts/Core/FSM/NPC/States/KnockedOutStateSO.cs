using UnityEngine;
using Pathfinding;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(fileName = "KnockedOutState", menuName = "KhosaryCode/FSM/NPC States/KnockedOut State")]
    public class KnockedOutStateSO : NPCStateSO
    {
        public override void OnEnter(NPCStateMachine stateMachine)
        {
            if (stateMachine.Agent != null)
            {
                stateMachine.Agent.isStopped = true;
            }

            // Play Knockout Animation here if you have one on the Animator
            var animator = stateMachine.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("KnockedOut");
            }

            // Disable all colliders on the NPC so they can't be hit again or block the path
            Collider2D[] colliders = stateMachine.GetComponentsInChildren<Collider2D>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }

            // Change color to black to visually indicate they are knocked out
            if (stateMachine.SpriteRenderer != null)
            {
                stateMachine.SpriteRenderer.color = Color.black;
            }
        }

        public override void OnUpdate(NPCStateMachine stateMachine)
        {
            // Do nothing, they are unconscious
        }

        public override void OnExit(NPCStateMachine stateMachine)
        {
            // Typically shouldn't exit this state unless revived, but we can reset if needed
        }
    }
}
