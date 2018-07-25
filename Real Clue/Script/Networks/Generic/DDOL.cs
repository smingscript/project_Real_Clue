using UnityEngine;

public class DDOL : MonoBehaviour
{
    private static DDOL playerInstance;

    public void Awake()
    {
        //DontDestroyOnLoad(this);

        //if (FindObjectsOfType(GetType()).Length > 1)
        //{
        //    Destroy(this);
        //}
    }
}
