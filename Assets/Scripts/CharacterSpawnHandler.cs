using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSpawnHandler : MonoBehaviour
{
    private GameManagerScript _gameManagerScript;
    // Start is called before the first frame update

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (!(FindObjectsOfType<CharacterSpawnHandler>().Count() > 1))
        {
            DontDestroyOnLoad(transform);
        }
        else
        { 
            Destroy(transform.gameObject);
        }

        _gameManagerScript = GameObject.Find("/GameManagerService").GetComponent<GameManagerScript>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            if (_gameManagerScript.isSwitchingLevel)
            {
                Debug.Log("Spawn handle");
                if (_gameManagerScript.isPreviousLevel)
                    transform.position = GameObject.Find("/ExitDoor").transform.position;
                else
                    transform.position = GameObject.Find("/GoBackSign").transform.position;
                GameObject TempCamera = GameObject.Find("/Main Camera");
                TempCamera.transform.position = new Vector3(transform.position.x, transform.position.y, TempCamera.transform.position.z);
                _gameManagerScript.isSwitchingLevel = false;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Hello, I am a nice and beautiful error that shows up when the first scene loads, please, bear with me (oso conmigo).");
        }
        
    }
}
