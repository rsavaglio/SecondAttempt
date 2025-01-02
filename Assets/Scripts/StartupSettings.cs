using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor;

public class StartupSettings : MonoBehaviour
{
    public int myFrameRate = 60;

    void Awake()
    {
        Application.targetFrameRate = myFrameRate;
    }

}
