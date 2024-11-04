using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb2d;
    private bool moveRight = false;
    private bool moveLeft = false;
    public bool isGravitySwitched = false;
    public bool isDead = false;
    private Animator characterAnimator;
    private SpriteRenderer characterSprite;
    private AudioManagerService audioManagerService;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterSprite = GetComponent<SpriteRenderer>();
        audioManagerService = GameObject.Find("/AudioManagerService").GetComponent<AudioManagerService>();
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.D)) moveRight = true;
        if (Input.GetKeyUp(KeyCode.D)) moveRight = false;

        if (Input.GetKeyDown(KeyCode.A)) moveLeft = true;
        if (Input.GetKeyUp(KeyCode.A)) moveLeft = false;

        Vector2 positionRight = new Vector2(transform.position.x + 0.25f, transform.position.y);
        Vector2 positionLeft = new Vector2(transform.position.x - 0.25f, transform.position.y);

        RaycastHit2D[] raycastHitUp = Physics2D.RaycastAll(positionRight, new Vector2(0, .5f), 1.0f);
        RaycastHit2D[] raycastHitDown = Physics2D.RaycastAll(positionRight, new Vector2(0, -.5f), 1.0f);
        RaycastHit2D[] raycastHitUp2 = Physics2D.RaycastAll(positionLeft, new Vector2(0, .5f), 1.0f);
        RaycastHit2D[] raycastHitDown2 = Physics2D.RaycastAll(positionLeft, new Vector2(0, -.5f), 1.0f);

        #if UNITY_EDITOR
            Debug.DrawRay(positionRight, new Vector2(0, .5f), Color.red);
            Debug.DrawRay(positionRight, new Vector2(0, -.5f), Color.green);
            Debug.DrawRay(positionLeft, new Vector2(0, .5f), Color.red);
            Debug.DrawRay(positionLeft, new Vector2(0, -.5f), Color.green);
        #endif

        if (Input.GetKeyDown(KeyCode.Space) && (
            (Array.Exists(raycastHitUp, x => x.collider.gameObject.name.Equals("Terrain")) || Array.Exists(raycastHitDown, x => x.collider.gameObject.name.Equals("Terrain"))) ||
            (Array.Exists(raycastHitUp2, x => x.collider.gameObject.name.Equals("Terrain")) || Array.Exists(raycastHitDown2, x => x.collider.gameObject.name.Equals("Terrain")))
            ) 
           )
        {
            audioManagerService.switchGravSource.Play();
            isGravitySwitched = !isGravitySwitched;
        }
    }


    void FixedUpdate()
    {
        if (isDead) return;


        if (moveRight || moveLeft)
        {
            if (!audioManagerService.moveAudioSource.isPlaying)
            {
                audioManagerService.moveAudioSource.Play();
            }
            characterAnimator.SetBool("isWalking", true);
        }
        else
        {
            if (audioManagerService.moveAudioSource.isPlaying)
            {
                audioManagerService.moveAudioSource.Pause();
            }
            characterAnimator.SetBool("isWalking", false);
        }

        if (moveRight)
        {
            characterSprite.transform.localScale = new Vector3(1, 1, 1);
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
        else if (moveLeft)
        {
            characterSprite.transform.localScale = new Vector3(-1, 1, 1);
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (isGravitySwitched)
        {
            characterSprite.flipY = !characterSprite.flipY;
            rb2d.velocity = new Vector2(0, rb2d.gravityScale * 10);
            rb2d.gravityScale = -rb2d.gravityScale;
            isGravitySwitched = false;
        }
    }


    public void SetDeadState(bool dead)
    {
        isDead = dead;
        audioManagerService.moveAudioSource.Pause();
        moveRight = false;
        moveLeft = false;
        rb2d.velocity = Vector2.zero;
    }
}
