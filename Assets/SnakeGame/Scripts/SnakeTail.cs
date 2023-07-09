using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeTail : MonoBehaviour
    {
        public Snake snake;
        public SpriteRenderer spriteRenderer;
        public int fromDirection;
        public int toDirection;

        public void Awake()
        {
            if (!snake)
            {
                snake = GetComponentInParent<Snake>();
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void SetDirection(int from, int to)
        {
            // spriteRenderer.flipX = false;
            // spriteRenderer.flipY = false;

            fromDirection = from;
            toDirection = to;
            int rotation = to;

            if (from != to)
            {
                if (from == Direction.UP)
                {
                    rotation = to == Direction.RIGHT ? Direction.UP : Direction.RIGHT;
                    // transform.rotation = to == Direction.RIGHT ? Direction
                } else if (from == Direction.DOWN)
                {
                    rotation = to == Direction.RIGHT ? Direction.LEFT : Direction.DOWN;
                } else if (from == Direction.RIGHT)
                {
                    rotation = to == Direction.UP ? Direction.DOWN : Direction.RIGHT;
                } else if (from == Direction.LEFT)
                {
                    rotation = to == Direction.UP ? Direction.LEFT : Direction.UP;
                }
            }

            transform.rotation = Direction.GetRotation(rotation);
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            snake.OnAnyCollision(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            snake.OnAnyCollision(collision.gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector2 dir = Direction.GetVector2Direction(fromDirection);
            Gizmos.DrawRay(transform.position, dir);

            Gizmos.color = Color.green;
            Vector2 dir2 = Direction.GetVector2Direction(toDirection);
            Gizmos.DrawRay(transform.position, dir2);
        }
    }
}