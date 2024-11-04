using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public float time = 2f;
    public bool cannonEnabled = false;
    public bool left = false;
    public float projectileSpeed = 5f;
    public GameObject CannonBall;
    public int poolSize = 5;

    private Queue<GameObject> projectilePool;

    void Awake()
    {
        InitializeProjectilePool();
    }

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

    private void InitializeProjectilePool()
    {
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(CannonBall);
            projectile.name = "Cannon Ball (Queued)";
            projectile.GetComponent<SpriteRenderer>().enabled = false;
            projectile.GetComponent<BoxCollider2D>().enabled = false;
            projectilePool.Enqueue(projectile);
        }
    }

    private GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.name = "Cannon Ball (Active)";
            projectile.GetComponent<SpriteRenderer>().enabled = true;
            projectile.GetComponent<BoxCollider2D>().enabled = true;
            return projectile;
        }
        return null;
    }

    private void ReturnProjectile(GameObject projectile)
    {
        projectile.name = "Cannon Ball (Queued)";
        projectile.GetComponent<SpriteRenderer>().enabled = false;
        projectile.GetComponent<BoxCollider2D>().enabled = false;
        projectilePool.Enqueue(projectile);
    }

    private IEnumerator SpawnCannonBall()
    {
        GameObject projectile = GetProjectile();
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(left == false ? projectileSpeed : -projectileSpeed, 0);
            yield return new WaitForSeconds(time);
            ReturnProjectile(projectile);
        }
        yield return SpawnCannonBall();
    }
}
