using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Degrees to rotate per second
    public float x = 0;
    public float y = 0;
    public float z = 0;

    void Update()
    {
        transform.Rotate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
    }
}