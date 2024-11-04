using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject character;
    public float offsetY = 3.0f;
    public float smoothSpeed = 0.015f;
    private Vector3 targetPosition;
    void Update()
    {
        try
        {
            float newY = character.GetComponent<SpriteRenderer>().flipY ?
                         character.transform.position.y :
                         character.transform.position.y + offsetY;

            targetPosition = new Vector3(character.transform.position.x, newY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
        catch
        {
            character = GameObject.Find("/Character");
        }
    }
}
