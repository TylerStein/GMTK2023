using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class Snake : WorldObject
    {

        public bool didEat = false;
        public bool didCollide = false;
        public Vector2 direction = Vector2.right;
        public int initialLength = 1;

        public Transform head;
        public GameObject tailPrefab;
        public LinkedList<SnakeTail> tail = new LinkedList<SnakeTail>();

        public Sprite tailSprite;
        public Sprite bodySprite;

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
            if (didCollide)
            {
                ResetSnake();
                return;
            }

            int directionValue = Direction.GetDirection(direction);

            Vector2 lastPosition = head.position;
            Quaternion lastRotation = head.rotation;
            // TODO: Track last direction
            int lastDirectionValue = directionValue;

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
                section.direction = lastDirectionValue;
            }
            else if (tail.Count > 0)
            {
                tail.Last.Value.transform.position = lastPosition;
                tail.Last.Value.transform.rotation = lastRotation;
                tail.Last.Value.direction = lastDirectionValue;
                tail.Last.Value.SetSprite(bodySprite);

                tail.AddFirst(tail.Last.Value);
                tail.RemoveLast();

                tail.Last.Value.SetSprite(tailSprite);
            }

            didEat = false;
            foodToDestroy = null;
        }

        void ResetSnake()
        {
            didCollide = false;
            didEat = false;
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
            else if (other.tag == gameManager.TagFood)
            {
                didEat = true;
                foodToDestroy = other.GetComponent<WorldObject>();
            }
        }
    }
}