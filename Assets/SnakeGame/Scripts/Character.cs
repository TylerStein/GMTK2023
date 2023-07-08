using SnakeGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameManager gameManager;
    public Vector2 move;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("Tick", gameManager.tickRate, gameManager.tickRate);
    }

    public void SetMove(Vector2 move)
    {
        this.move = move;
    }

    // Update is called once per frame
    void Tick()
    {
        transform.Translate(move * speed);
        move = Vector2.zero;
        ClampPosition();
    }

    void ClampPosition()
    {
        float clampedX = transform.position.x;
        float clampedY = transform.position.y;
        if (clampedY >= gameManager.borderUp.position.y)
        {
            clampedY = gameManager.borderUp.position.y - 1;
        } else if (clampedY <= gameManager.borderDown.position.y)
        {
            clampedY = gameManager.borderDown.position.y + 1;
        }

        if (clampedX >= gameManager.borderRight.position.x)
        {
            clampedX = gameManager.borderRight.position.x - 1;
        } else if (clampedX <= gameManager.borderLeft.position.x)
        {
            clampedX = gameManager.borderLeft.position.x + 1;
        }

        transform.position = new Vector2(clampedX, clampedY);
    }
}
