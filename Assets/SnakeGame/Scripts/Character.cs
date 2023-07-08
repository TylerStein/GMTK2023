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
    }
}
