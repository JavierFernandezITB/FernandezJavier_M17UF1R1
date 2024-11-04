using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool isPreviousLevel = false;
    public bool isSwitchingLevel = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!(FindObjectsOfType<GameManagerScript>().Count() > 1))
        {
            DontDestroyOnLoad(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
    