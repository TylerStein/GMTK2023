using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class Snake : MonoBehaviour
    {

        public bool didEat = false;
        public bool didCollide = false;
        public Vector2 direction = Vector2.right;
        public int initialLength = 1;

        public Transform head;
        public GameObject tailPrefab;
        public GameManager gameManager;
        public LinkedList<Transform> tail = new LinkedList<Transform>();

        private GameObject foodToDestroy;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            InvokeRepeating("Tick", gameManager.tickRate, gameManager.tickRate);
        }

        public void SetDirection(Vector2 dir)
        {
            Vector2 nextDirection = direction;
            if (dir.x != 0)
            {
                nextDirection = dir.x > 0 ? Vector2.right : Vector2.left;
            }
            else if (dir.y != 0)
            {
                nextDirection = dir.y > 0 ? Vector2.up : Vector2.down;
            }

            if (direction != -nextDirection)
            {
                direction = nextDirection;
            }
        }


        // Update is called once per frame
        void Tick()
        {
            if (didCollide)
            {
                ResetSnake();
                return;
            }

            Vector2 lastPosition = head.position;
            head.Translate(direction);

            bool addTailExtension = (didEat && gameManager.TryEat(foodToDestroy)) || tail.Count < initialLength;
            if (addTailExtension)
            {
                GameObject extension = Instantiate(tailPrefab, lastPosition, Quaternion.identity, transform);
                tail.AddFirst(extension.transform);
            }
            if (tail.Count > 0)
            {
                tail.Last.Value.position = lastPosition;
                tail.AddFirst(tail.Last.Value);
                tail.RemoveLast();
            }

            didEat = false;
            foodToDestroy = null;
        }

        void ResetSnake()
        {
            didCollide = false;
            didEat = false;
            direction = Vector2.right;
            foreach (Transform transform in tail)
            {
                Destroy(transform.gameObject);
            }
            tail.Clear();
            head.position = Vector3.zero;
        }

        public void OnAnyCollision(GameObject other)
        {
            if (other.tag == gameManager.TagBorder || other.tag == gameManager.TagSnake)
            {
                didCollide = true;
            }
            else if (other.tag == gameManager.TagFood)
            {
                didEat = true;
                foodToDestroy = other;
            }
        }
    }
}