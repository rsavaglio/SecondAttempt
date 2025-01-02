using UnityEngine;

[CreateAssetMenu]
public class AttackSequence : ScriptableObject
{
    public bool _isMultiHit;
    public bool _repeatLastAttackPhase;
    
    public AttackPhase _SetupPhase;
    public AttackPhase[] _AttackPhases;
    public AttackPhase _OnHitPhase;
    public AttackPhase _OnMissPhase;
    public AttackPhase _ResetPhase;
}