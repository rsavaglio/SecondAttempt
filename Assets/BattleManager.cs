using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] _enemyList;
    public GameObject[] _allyList;
    
    GameObject _currentTarget;

    int _enemyIndex;
    
    void Start()
    {
        _currentTarget = _enemyList[0];
        SendMessage("OnTargetChange", _currentTarget);
    }

    void IncrementTarget()
    {
        _enemyIndex++;
        
        if(_enemyIndex >= _enemyList.Length)
        {
            _enemyIndex = 0;
        }

        _currentTarget = _enemyList[_enemyIndex];
        
        SendMessage("OnTargetChange", _currentTarget);

        Debug.Log("Increment Target!");
    }

    void SetFirstTarget()
    {
        _enemyIndex = 0;
        _currentTarget = _enemyList[_enemyIndex];
        SendMessage("OnTargetChange", _currentTarget);
    }
}
