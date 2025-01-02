using UnityEngine;
using UnityEngine.UIElements;

public enum InterpolationType
{
    LERP,
    BEZIER
}

public enum PositionCalculationType
{
    TARGET,
    ZERO
}

[System.Serializable]
public struct PhaseKeyframe
{ 
    public InterpolationType _interpolationType;
    public PositionCalculationType _PositionType;
    public Vector3 _Position;
    public Vector3 _bezierControlPoint;
}

[CreateAssetMenu]
public class AttackPhase : ScriptableObject
{
    public AnimationClip _AnimationClip;
    public PhaseKeyframe[] _KeyframeList;
}
