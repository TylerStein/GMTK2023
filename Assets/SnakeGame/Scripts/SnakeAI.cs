using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeAI : WorldObject
    {
        public Snake snake;

        public WorldObject closestFood = null;
        public Vector2 moveToTarget = Vector2.zero;
        public ContactFilter2D selfCollisionFilter;
        public List<RaycastHit2D> raycastHits = new List<RaycastHit2D>(5);
        public float raycastDistance = 5f;

        public void Start()
        {
            SetupTickReceiver(512);
        }

        public override void OnTick(float deltaTime)
        {
            Vector2 headPosition = snake.head.position;
            if (gameManager.availableFood.Count > 0)
            {
                gameManager.availableFood.Sort((a, b) => Vector2.Distance(headPosition, a.transform.position) < Vector2.Distance(headPosition, b.transform.position) ? -1 : 1);
                closestFood = gameManager.availableFood[0];
                // Vector2 foodPosition = closestFood.transform.position;
                // float manhattanDistance = Distance.ManhattanDistance(foodPosition.x, headPosition.x, foodPosition.y, headPosition.y);
                //Vector2 projectedPosition = closestFood.GetProjectedPosition(Mathf.RoundToInt(manhattanDistance));
                moveToTarget = closestFood.transform.position;
            }
            else
            {
                closestFood = null;
                moveToTarget = gameManager.GetIntRandomInBounds();
            }


            // TODO: Smarter movement, use vector maths
            Vector2Int targetOffset = new Vector2Int(Mathf.RoundToInt(moveToTarget.x - headPosition.x), Mathf.RoundToInt(moveToTarget.y - headPosition.y));
            if (snake.direction.y > 0)
            {
                // moving up
                if (targetOffset.y > 0)
                {
                    // in front of snake
                    CheckAndSetDirection(Vector2.up);
                }
                else if (targetOffset.y < 0)
                {
                    if (targetOffset.x < 0)
                    {
                        // behind snake, turn ccw
                        CheckAndSetDirection(Vector2.left);
                    }
                    else
                    {
                        // behind snake, turn cw (default)
                        CheckAndSetDirection(Vector2.right);
                    }
                }
                else if (targetOffset.x > 0)
                {
                    // to the right of snake
                    CheckAndSetDirection(Vector2.right);
                }
                else
                {
                    // to the left of snake
                    CheckAndSetDirection(Vector2.left);
                }
            }
            else if (snake.direction.y < 0)
            {
                // moving down
                if (targetOffset.y < 0)
                {
                    // in front of snake
                    CheckAndSetDirection(Vector2.down);
                }
                else if (targetOffset.y > 0)
                {
                    if (targetOffset.x > 0)
                    {
                        // behind snake, turn ccw
                        CheckAndSetDirection(Vector2.right);
                    }
                    else
                    {
                        // behind snake, turn cw (default)
                        CheckAndSetDirection(Vector2.left);
                    }
                }
                else if (targetOffset.x > 0)
                {
                    // to the right of the snake
                    CheckAndSetDirection(Vector2.right);
                }
                else
                {
                    // to the left of the snake
                    CheckAndSetDirection(Vector2.left);
                }
            }
            else if (snake.direction.x > 0)
            {
                // moving right
                if (targetOffset.y < 0)
                {
                    // beneath snake
                    CheckAndSetDirection(Vector2.down);
                }
                else if (targetOffset.y > 0)
                {
                    // above snake
                    CheckAndSetDirection(Vector2.up);
                }
                else if (targetOffset.x > 0)
                {
                    // in front of snake
                    CheckAndSetDirection(Vector2.right);
                }
                else
                {
                    if (targetOffset.y > 0)
                    {
                        // behind snake, turn ccw
                        CheckAndSetDirection(Vector2.up);
                    }
                    else
                    {
                        // behind snake, turn cw (default)
                        CheckAndSetDirection(Vector2.down);
                    }
                }
            }
            else
            {
                // moving left
                if (targetOffset.y < 0)
                {
                    // beneath snake
                    CheckAndSetDirection(Vector2.down);
                }
                else if (targetOffset.y > 0)
                {
                    // above snake
                    CheckAndSetDirection(Vector2.up);
                }
                else if (targetOffset.x > 0)
                {
                    if (targetOffset.y > 0)
                    {
                        // behind snake, turn ccw
                        CheckAndSetDirection(Vector2.down);
                    }
                    else
                    {
                        // behind snake, turn cw (default)
                        CheckAndSetDirection(Vector2.up);
                    }
                }
                else
                {
                    // in front of snake
                    CheckAndSetDirection(Vector2.left);
                }
            }
        }

        private void CheckAndSetDirection(Vector2 dir)
        {
            int hits = Physics2D.Raycast(snake.head.transform.position, dir, selfCollisionFilter, raycastHits, raycastDistance);
            bool hitSelf = false;
            for (int i = 0; i < hits; i++)
            {
                if (raycastHits[i].collider.tag == gameManager.TagSnake)
                {
                    if (raycastHits[i].collider.gameObject != snake.head.gameObject)
                    {
                        hitSelf = true;
                        break;
                    }
                }
            }

            if (!hitSelf)
            {
                snake.SetDirection(dir);
            }

        }

        private void OnDrawGizmosSelected()
        {
            if (!gameManager)
            {
                return;
            }

            for (int i = 0; i < gameManager.availableFood.Count; i++)
            {
                Gizmos.color = gameManager.availableFood[i] == closestFood ? Color.blue : Color.grey;
                Gizmos.DrawLine(snake.head.position, gameManager.availableFood[i].transform.position);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(snake.head.position, moveToTarget);
        }
    }
}