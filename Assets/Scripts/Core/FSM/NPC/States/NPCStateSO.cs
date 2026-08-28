using UnityEngine;

namespace KhosaryCode.AI
{
    public abstract class NPCStateSO : ScriptableObject
    {
        public virtual void OnEnter(NPCStateMachine npc) { }
        public virtual void OnUpdate(NPCStateMachine npc) { }
        public virtual void OnExit(NPCStateMachine npc) { }
    }
}
