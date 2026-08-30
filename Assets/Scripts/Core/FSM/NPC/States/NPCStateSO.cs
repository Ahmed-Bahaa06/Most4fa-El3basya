using UnityEngine;

namespace KhosaryCode.AI
{
    public abstract class NPCStateSO : ScriptableObject
    {
        [Header("State Visuals")]
        public Color stateColor = Color.white;

        public virtual void OnEnter(NPCStateMachine npc) 
        { 
            if (npc.SpriteRenderer != null)
            {
                //npc.SpriteRenderer.color = stateColor;
            }
        }
        public virtual void OnUpdate(NPCStateMachine npc) { }
        public virtual void OnExit(NPCStateMachine npc) { }
    }
}
