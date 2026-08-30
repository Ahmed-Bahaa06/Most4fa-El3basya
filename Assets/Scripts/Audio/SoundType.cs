namespace KhosaryCode.Audio
{
    public enum SoundType
    {
        // Player
        PlayerFootstep,
        PlayerKnockoutHit,
        PlayerHurt,
        PlayerDie,

        // NPC General
        NPCIdleStep,
        NPCRunStep,
        
        // NPC Male
        MaleNPCHurt,
        MaleNPCDie,
        DoctorSpotPlayer,
        GuardMeleeSpotPlayer,
        GuardRangedSpotPlayer,
        
        // NPC Female
        FemaleNPCHurt,
        FemaleNPCDie,
        FemaleDoctorSpotPlayer,

        // UI
        UIButtonClick,
        
        // General
        VFX
    }
}
