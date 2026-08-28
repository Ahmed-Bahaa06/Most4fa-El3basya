using UnityEngine;

namespace KhosaryCode.AI
{
    [CreateAssetMenu(menuName = "AI/States/Hiding", fileName = "HidingState")]
    public class HidingStateSO : NPCStateSO
    {
        public override void OnEnter(NPCStateMachine npc)
        {
            if (npc.Agent != null && npc.Agent.isActiveAndEnabled)
            {
                npc.Agent.isStopped = true; 
            }
            Debug.Log($"[{npc.gameObject.name}] is HIDING!");
        }
    }
}
