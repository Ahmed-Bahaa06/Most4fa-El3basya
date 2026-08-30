using UnityEngine;

namespace KhosaryCode.AI
{
    public abstract class NPCStateSO : ScriptableObject
    {
        [Header("State Settings")]
        public Color stateColor = Color.white;
        [Tooltip("The speed the NPC will move at while in this state.")]
        public float movementSpeed = 3f;

        public virtual void OnEnter(NPCStateMachine npc) 
        { 
            if (npc.SpriteRenderer != null)
            {
                //npc.SpriteRenderer.color = stateColor;
            }

            if (npc.Agent != null)
            {
                npc.Agent.maxSpeed = movementSpeed;
            }
        }
        public virtual void OnUpdate(NPCStateMachine npc) { }
        public virtual void OnExit(NPCStateMachine npc) { }
    }
}
