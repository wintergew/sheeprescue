using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy; 

    void Start()
    {
        Destroy(gameObject, timeToDestroy); 
    }
}