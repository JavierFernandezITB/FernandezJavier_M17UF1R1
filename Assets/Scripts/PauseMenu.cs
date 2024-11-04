using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private CanvasGroup FadeUI;
    private bool isRestarting = false;

    private void Start()
    {
        if (!(FindObjectsOfType<PauseMenu>().Count() > 1))
        {
            DontDestroyOnLoad(transform);
        }
        else
        { 
            Destroy(gameObject);
        }
        FadeUI = GameObject.Find("/UICanvas/Fade").GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private IEnumerator RestartCoroutine()
    {
        isRestarting = true;
        Resume();
        for (float i = 0; i < 1; i += .1f)
        {
            FadeUI.alpha = i;
            yield return new WaitForSeconds(.1f);
            Debug.Log("yeet");
        }
        FadeUI.alpha = 1;
        Destroy(GameObject.Find("/Character"));
        isRestarting = false;
        SceneManager.LoadScene("1");
    }

    public void Restart()
    {
        if (!isRestarting)
            StartCoroutine(RestartCoroutine());
    }

    public void Quit()
    {
        // Directivas de preprocesado be like :P
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
