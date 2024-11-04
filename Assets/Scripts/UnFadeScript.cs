using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UnFadeScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool fading = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha == 1 && !fading)
        {
            fading = true;
            StartCoroutine(UnFade());
        }
    }

    private IEnumerator UnFade()
    {
        CharacterMovement charMovementTemp = GameObject.Find("/Character").GetComponent<CharacterMovement>();
        yield return new WaitForSeconds(1);
        charMovementTemp.SetDeadState(false);
        for (float i = 1; i > 0; i -= 0.1f)
        {
            canvasGroup.alpha = Mathf.Clamp(i, 0, 1);
            yield return new WaitForSeconds(0.1f);
        }
        canvasGroup.alpha = 0;
        fading = false;
    }
}
