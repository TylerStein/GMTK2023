using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV2
{
    public class SnakeControllerV2 : MonoBehaviour
    {
        public Transform borderUp;
        public Transform borderRight;
        public Transform borderDown;
        public Transform borderLeft;
        public GameObject foodPrefab;

        public PlayerInputHandler inputHandler;

        public bool didEat = false;
        public bool didCollide = false;
        public Vector2 direction = Vector2.right;

        public Transform head;
        public GameObject tailPrefab;

        public LinkedList<Transform> tail = new LinkedList<Transform>();
        public float tickRate = 2.0f;

        public List<GameObject> observedFood = new List<GameObject>();

        private GameObject foodToDestroy;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Tick", tickRate, tickRate);

            SpawnFood();
            SpawnFood();
            SpawnFood();
            SpawnFood();
        }

        void Update()
        {
            int moveX = Mathf.RoundToInt(inputHandler.move.x);
            int moveY = Mathf.RoundToInt(inputHandler.move.y);

            Vector2 nextDirection = direction;
            if (moveX != 0)
            {
                nextDirection = moveX > 0 ? Vector2.right : Vector2.left;
            }
            else if (moveY != 0)
            {
                nextDirection = moveY > 0 ? Vector2.up : Vector2.down;
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

            if (didEat)
            {
                GameObject extension = Instantiate(tailPrefab, lastPosition, Quaternion.identity, transform);
                tail.AddFirst(extension.transform);
                didEat = false;
                foodToDestroy.SetActive(false);
                Destroy(foodToDestroy);
                foodToDestroy = null;
                SpawnFood();
            }
            if (tail.Count > 0)
            {
                tail.Last.Value.position = lastPosition;
                tail.AddFirst(tail.Last.Value);
                tail.RemoveLast();
            }

            observedFood = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup"));
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

        void SpawnFood()
        {
            int x = Mathf.RoundToInt(Random.Range(borderLeft.position.x, borderRight.position.x));
            int y = Mathf.RoundToInt(Random.Range(borderDown.position.y, borderUp.position.y));
            Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }

        public void OnAnyCollision(GameObject other)
        {
            Debug.Log("Collision with object: " + other.name);
            if (other.tag == "Death" || other.tag == "Player")
            {
                didCollide = true;
            }
            else if (other.tag == "Pickup")
            {
                didEat = true;
                foodToDestroy = other;
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (GameObject food in observedFood)
            {
                Gizmos.DrawLine(head.position, food.transform.position);
            }
        }
    }
}