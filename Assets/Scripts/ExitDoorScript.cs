using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTombScript : MonoBehaviour
{
    public int currentLevel;
    public bool isBackTomb = false;
    private bool isPlayerTouchingTomb = false;
    private TMP_Text PressEUI;
    private CanvasGroup FadeUI;
    private GameManagerScript gameManager;
    private Animator animator;
    private CharacterMovement charMovement;

    // Start is called before the first frame update
    void Start()
    {
        charMovement = GameObject.Find("/Character").GetComponent<CharacterMovement>();
        gameManager = GameObject.Find("/GameManagerService").GetComponent<GameManagerScript>();
        PressEUI = GameObject.Find("/UICanvas/PressEText").GetComponent<TMP_Text>();
        FadeUI = GameObject.Find("/UICanvas/Fade").GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTouchingTomb && Input.GetKeyDown(KeyCode.E) && !charMovement.isDead)
        {
            StartCoroutine(SwitchLevel());   
        }
    }

    private IEnumerator SwitchLevel()
    {
        charMovement.SetDeadState(true);
        for (float i = 0; i < 1; i += .1f)
        {
            FadeUI.alpha = i;
            yield return new WaitForSeconds(.1f);
        }
        FadeUI.alpha = 1;
        if (isBackTomb)
        {
            Debug.LogWarning("Previous level!");
            gameManager.isSwitchingLevel = true;
            gameManager.isPreviousLevel = true;
            SceneManager.LoadScene((currentLevel - 1).ToString());
        }
        else
        {
            Debug.LogWarning("Next level!");
            gameManager.isSwitchingLevel = true;
            gameManager.isPreviousLevel = false;
            SceneManager.LoadScene((currentLevel + 1).ToString());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isOpening", true);
            isPlayerTouchingTomb = true;
            PressEUI.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isOpening", false);
            isPlayerTouchingTomb = false;
            try
            {
                PressEUI.enabled = false;
            }
            catch { }
        }
    }
}
