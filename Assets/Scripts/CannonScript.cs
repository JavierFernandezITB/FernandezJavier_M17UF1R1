using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CannonScript : MonoBehaviour
{
    public float time = 1.5f;
    public bool cannonEnabled = false;
    public bool left = false;
    public float projectileSpeed = 5f;
    public GameObject CannonBall;

    void Start()
    {
        if (cannonEnabled)
            StartCoroutine(SpawnCannonBall());
    }

    private void Update()
    {
        if (!cannonEnabled)
        {
            RaycastHit2D[] raycastHitUp = Physics2D.RaycastAll(transform.position, new Vector2(left == false ? 1 : -1, 0));
            Debug.DrawRay(transform.position, new Vector2(left == false ? 1 : -1, 0), Color.red);
            if (Array.Exists(raycastHitUp, x => x.collider.gameObject.name.Equals("Character")))
            {
                StartCoroutine(SpawnCannonBall());
                cannonEnabled = true;
            }
        }
    }

    private IEnumerator SpawnCannonBall()
    {
        GameObject projectile = Instantiate(CannonBall, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(left == false ? projectileSpeed : -projectileSpeed, 0);
        yield return new WaitForSeconds(time);
        yield return SpawnCannonBall();
    }
}
