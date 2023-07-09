using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace SnakeGame
{
    public class Snake : WorldObject
    {

        public bool didEat = false;
        public bool didCollide = false;
        public Vector2 direction = Vector2.right;
        public int lastDirectionValue = Direction.RIGHT;
        public int initialLength = 1;
        public bool shouldReset = false;

        public Transform head;
        public GameObject tailPrefab;
        public LinkedList<SnakeTail> tail = new LinkedList<SnakeTail>();

        public Sprite tailSprite;
        public Sprite bodySprite;
        public Sprite turnSprite;

        private WorldObject foodToDestroy;

        public void Start()
        {
            Vector3 startPosition = transform.position;
            transform.position = Vector3.zero;
            head.transform.position = startPosition;

            SetupTickReceiver(256);
        }

        public void SetDirection(Vector2 dir)
        {
            Vector2 nextDirection = SnapInput.GetSnapInput(dir, SnapInput.EInputSnap.FOUR);
            if (direction != -nextDirection)
            {
                direction = nextDirection;
            }
        }


        // Update is called once per frame
        public override void OnTick(float deltaTime)
        {
            if (didCollide || shouldReset)
            {
                ResetSnake();
                return;
            }

            int directionValue = Direction.GetDirection(direction);

            Vector2 lastPosition = head.position;
            Quaternion lastRotation = head.rotation;

            Quaternion directionRotation = Direction.GetRotation(directionValue);
            head.position = head.position + (Vector3)direction;
            head.rotation = directionRotation;

            bool addTailExtension = (didEat && gameManager.TryEat(foodToDestroy)) || tail.Count < initialLength;
            if (addTailExtension)
            {
                GameObject sectionObj = Instantiate(tailPrefab, lastPosition, lastRotation, transform);
                SnakeTail section = sectionObj.GetComponent<SnakeTail>();
                if (tail.Count == 0) {
                    section.SetSprite(tailSprite);
                }
                tail.AddFirst(section);

                section.snake = this;
                int fromDirection = tail.Count > 1
                    ? tail.First.Next.Value.toDirection
                    : lastDirectionValue;
                int toDirection = directionValue;
                if (fromDirection != toDirection)
                {
                    section.SetSprite(turnSprite);
                } else
                {
                    section.SetSprite(bodySprite);
                }
                section.SetDirection(fromDirection, toDirection);
            }
            else if (tail.Count > 0)
            {
                tail.AddFirst(tail.Last.Value);
                tail.RemoveLast();

                // configure last piece behind head
                LinkedListNode<SnakeTail> first = tail.First;

                int fromDirection = tail.Count > 1
                    ? first.Next.Value.toDirection
                    : lastDirectionValue;
                int toDirection = directionValue;
                if (fromDirection != toDirection)
                {
                    first.Value.SetSprite(turnSprite);
                }
                else
                {
                    first.Value.SetSprite(bodySprite);
                }
                first.Value.transform.position = lastPosition;
                first.Value.SetDirection(fromDirection, toDirection);

                // tail copies direction of last sprite it's attached to
                LinkedListNode<SnakeTail> last = tail.Last;
                fromDirection = tail.Count > 1 ? last.Previous.Value.fromDirection : directionValue;
                last.Value.SetSprite(tailSprite);
                last.Value.SetDirection(fromDirection, fromDirection);
            }

            didEat = false;
            foodToDestroy = null;
            lastDirectionValue = directionValue;
        }

        private void ResetSnake()
        {
            didCollide = false;
            didEat = false;
            shouldReset = false;
            direction = Vector2.right;
            foreach (SnakeTail snakeTail in tail)
            {
                Destroy(snakeTail.gameObject);
            }
            tail.Clear();
            head.position = (Vector2)gameManager.GetIntRandomInBounds();
        }

        public void OnAnyCollision(GameObject other)
        {
            if (/*other.tag == gameManager.TagBorder || */other.tag == gameManager.TagSnake)
            {
                didCollide = true;
            }
            else if (other.tag == gameManager.TagFood || other.tag == gameManager.TagPlayer)
            {
                didEat = true;
                foodToDestroy = other.GetComponent<WorldObject>();
            }
        }
    }
}