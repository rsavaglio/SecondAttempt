using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x, y, z);
    }
}
