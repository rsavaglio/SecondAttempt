using UnityEngine;

public class AttackPositionAnimator : MonoBehaviour
{
    public float _interpolationValue;
    
    PhaseKeyframe[] _keyframeList;
    int _currentIndex;

    PhaseKeyframe _currentKeyframe;
    InterpolationType _interpolationType;
    Vector3 _interpolationEndPosition;
    Vector3 _interpolationStartPosition;
    Vector3 _bezierControlPoint;
   
    bool _enableInterpolation = false;

    Vector3 _targetPosition;

    void Update()
    {   
        float adjustedInterpolationValue = _interpolationValue - _currentIndex;

        bool resetInterpolation = false;
        
        // Next keyframe
        if(adjustedInterpolationValue >= 1.0f)
        {
            if(_keyframeList.Length <= _currentIndex + 1)
            {
                _enableInterpolation = false;
                return;
            }
            else
            {
                _currentIndex++;
                _enableInterpolation = true;
                resetInterpolation = true;
            }
        }

        // Previous keyframe
        if(adjustedInterpolationValue < 0.0f)
        {
            if(_keyframeList.Length >= _currentIndex -1)
            {
                _enableInterpolation = false;
                return;
            }
            else
            {
                _currentIndex--;
                _enableInterpolation = true;
                resetInterpolation = true;
            }
        }

        if(!_enableInterpolation)
        {
            return;
        }

        if(resetInterpolation)
        {
            adjustedInterpolationValue = _interpolationValue - _currentIndex;
            _currentKeyframe = _keyframeList[_currentIndex];
            SetInterpolationPositions();
        }

        if(_interpolationType == InterpolationType.BEZIER)
        {
            transform.parent.localPosition = (((1 - adjustedInterpolationValue) * (1 - adjustedInterpolationValue)) 
            * _interpolationStartPosition) + (((1 -  adjustedInterpolationValue) * 2.0f) 
            * adjustedInterpolationValue * _bezierControlPoint) 
            + ((adjustedInterpolationValue * adjustedInterpolationValue) * _interpolationEndPosition);
        }
        else
        {
            transform.parent.localPosition = Vector3.Lerp(_interpolationStartPosition, _interpolationEndPosition, adjustedInterpolationValue);
        }
    }

    void SetInterpolationPositions()
    {
        _interpolationStartPosition = transform.parent.localPosition;
        _interpolationType = _keyframeList[_currentIndex]._interpolationType;
        
        switch(_keyframeList[_currentIndex]._PositionType)
        {
            case PositionCalculationType.TARGET:
                _interpolationEndPosition = _targetPosition + _keyframeList[_currentIndex]._Position - transform.parent.parent.localPosition;
                _bezierControlPoint = _targetPosition + _keyframeList[_currentIndex]._bezierControlPoint - transform.parent.parent.localPosition;
                break;
            
            case PositionCalculationType.ZERO:
                _interpolationEndPosition = Vector3.zero + _keyframeList[_currentIndex]._Position;
                _bezierControlPoint = Vector3.zero + _keyframeList[_currentIndex]._bezierControlPoint;
                break; 
        }
    }

    public void SetAttackPhase(AttackPhase anAttackPhase)
    {
        _keyframeList = anAttackPhase._KeyframeList;

        _currentIndex = 0;
        _interpolationValue = 0;

        if(_keyframeList.Length != 0)
        {
            _currentKeyframe = _keyframeList[0];
            _enableInterpolation = true;
            SetInterpolationPositions();
        }
        else
        {
            _enableInterpolation = false;
        }

        Debug.Log("AttackPositionAnimator keyframe list set from " + anAttackPhase.name);
    }

    public void DisableInterpolation()
    {
        _enableInterpolation = false;
    }

    void OnTargetChange(GameObject aTarget)
    {
        _targetPosition = aTarget.transform.position;
    }
}
