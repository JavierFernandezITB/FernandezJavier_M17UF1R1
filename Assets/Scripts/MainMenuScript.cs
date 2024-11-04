using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameObject UICanvas;
    private CanvasGroup FadeUI;
    void Start()
    {
        UICanvas = GameObject.Find("/UICanvas");
        FadeUI = GameObject.Find("/UICanvas/Fade").GetComponent<CanvasGroup>();
        UICanvas.SetActive(false);
    }

    private IEnumerator NewGameCoroutine()
    {
        UICanvas.SetActive(true);
        for (float i = 0; i < 1; i += .1f)
        {
            FadeUI.alpha = i;
            yield return new WaitForSeconds(.1f);
        }
        FadeUI.alpha = 1;
        SceneManager.LoadScene("1");
    }

    public void NewGameFromEnd()
    {
        Destroy(GameObject.Find("/Character"));
        StartCoroutine(NewGameCoroutine());
    }

    public void NewGame()
    {
        StartCoroutine(NewGameCoroutine());
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
