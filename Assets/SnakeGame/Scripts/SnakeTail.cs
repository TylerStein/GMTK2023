using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeTail : MonoBehaviour
    {
        Snake snake;

        // Start is called before the first frame update
        void Start()
        {
            snake = FindObjectOfType<Snake>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            snake.OnAnyCollision(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            snake.OnAnyCollision(collision.gameObject);
        }
    }
}