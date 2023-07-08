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

        public Transform head;
        public GameObject tailPrefab;
        public GameManager manager;

        public LinkedList<Transform> tail = new LinkedList<Transform>();
        public float tickRate = 2.0f;

        public List<GameObject> observedFood = new List<GameObject>();

        private GameObject foodToDestroy;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Tick", tickRate, tickRate);
        }

        public void SetDirection(Vector2 target)
        {
            Vector2 nextDirection = direction;
            if (target.x != 0)
            {
                nextDirection = target.x > 0 ? Vector2.right : Vector2.left;
            }
            else if (target.y != 0)
            {
                nextDirection = target.y > 0 ? Vector2.up : Vector2.down;
            }

            if (direction != -nextDirection)
            {
                direction = nextDirection;
            }
        }

        void Update()
        {
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
                manager.OnSnakeEat();
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