using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] float fireworkSpeed;

    private float currentSpeed = 0;

    private void Update()
    {
        gameObject.transform.position = new Vector2(
            gameObject.transform.position.x,
            gameObject.transform.position.y + (currentSpeed * Time.deltaTime)); 
    }

    public void FireworkEventMove()
    {
        currentSpeed = fireworkSpeed;
    }

    public void FireworkEventStop()
    {
        currentSpeed = 0;
    }

    public void FireworkEventDetroy()
    {
        Destroy(gameObject);
    }
}
