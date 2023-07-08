using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{

    public class SnakeTail : MonoBehaviour
    {
        public Snake snake;
        public int direction;

        public void Awake()
        {
            if (!snake)
            {
                snake = GetComponentInParent<Snake>();
            }
        }

        //public void SetPositionAndRotation(Vector2 position, int direction, Quaternion rotation)
        //{
        //    transform.position = position;
        //    this.direction = direction;
        //    transform.rotation = rotation;
        //}

        public void SetSprite(Sprite sprite)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
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