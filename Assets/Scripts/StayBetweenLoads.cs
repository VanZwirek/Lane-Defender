using UnityEngine;

public class StayBetweenLoads : MonoBehaviour
{
     static public bool once_call;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
