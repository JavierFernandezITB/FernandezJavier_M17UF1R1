using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigScript : MonoBehaviour
{
    public float speed = 1.5f;
    public float movingTime = 2.0f;
    private bool isAlive = true;
    public bool startLeft = false;
    private Rigidbody2D rb2d;
    private SpriteRenderer rbSprite;
    private Animator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(CharacterAI());
    }

    void MoveRight()
    {
        characterAnimator.SetBool("isWalking", true);
        rbSprite.transform.localScale = new Vector3(-1, rbSprite.transform.localScale.y, 1);
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    void MoveLeft()
    {
        characterAnimator.SetBool("isWalking", true);
        rbSprite.transform.localScale = new Vector3(1, rbSprite.transform.localScale.y, 1);
        rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
    }

    void Stop()
    {
        characterAnimator.SetBool("isWalking", false);
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }

    private IEnumerator CharacterAI()
    {
        if (startLeft)
        {
            MoveRight();
            yield return new WaitForSeconds(movingTime);
            Stop();
            yield return new WaitForSeconds(movingTime);
        }
        while (isAlive)
        {
            MoveLeft();
            yield return new WaitForSeconds(movingTime);
            Stop();
            yield return new WaitForSeconds(movingTime);
            MoveRight();
            yield return new WaitForSeconds(movingTime);
            Stop();
            yield return new WaitForSeconds(movingTime);
        }
        yield return CharacterAI();
    }
}
