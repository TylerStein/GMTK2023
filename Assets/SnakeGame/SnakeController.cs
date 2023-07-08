using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace SnakeV1
{
    public class SnakeController : MonoBehaviour
    {
        public GameObject food;
        public GameObject snakePrefab;

        public List<GameObject> snakeInstances = new List<GameObject>();
        public List<Vector2> lastPositions = new List<Vector2>();

        public Vector2 currentVelocity = Vector2.zero;
        public Vector2 targetVelocity = Vector2.zero;

        public float acceleration = 1;
        public float maxSpeed = 1;

        public int maxSize = 12;

        public float maxCollisionCooldown = 3;
        public float currentCooldown = 0;

        public float updateTimer = 0;
        public float tickSeconds = 0.5f;


        // Start is called before the first frame update
        void Start()
        {
            Grow();

            Vector2 currentPosition = snakeInstances[0].transform.position;
            lastPositions.Add(currentPosition);
            for (int i = 0; i < maxSize; i++)
            {
                lastPositions.Add(currentPosition + Vector2Int.left * (i + 1));
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (updateTimer < tickSeconds)
            {
                updateTimer += Time.deltaTime;
            }
            else
            {
                updateTimer = 0;
                Tick(tickSeconds);
            }


            if (currentCooldown < maxCollisionCooldown)
            {
                currentCooldown += Time.deltaTime;
            }
        }

        public void Tick(float deltaTime)
        {

            Vector2 currentPosition = snakeInstances[0].transform.position;
            lastPositions.Insert(0, currentPosition);
            if (lastPositions.Count > maxSize)
            {
                lastPositions.RemoveAt(maxSize);
            }

            targetVelocity = ((Vector2)food.transform.position - currentPosition).normalized * maxSpeed;
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * deltaTime);
            currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);
            snakeInstances[0].transform.position += (Vector3)(currentVelocity * deltaTime);

            for (int i = 1; i < snakeInstances.Count; i++)
            {
                snakeInstances[i].transform.position = lastPositions[i - 1];
            }
        }

        public void Grow()
        {
            GameObject instance = Instantiate(snakePrefab);
            instance.transform.SetParent(transform, true);
            SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new UnityEngine.Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            snakeInstances.Add(instance);
            if (lastPositions.Count > 0)
            {
                instance.transform.position = (Vector3)lastPositions[1];
            }
            else
            {
                instance.transform.position = transform.position;
            }
        }

        public bool IsCollidingWith(Vector2 pos, bool ignoreCooldown = false)
        {
            if (!ignoreCooldown && currentCooldown < maxCollisionCooldown)
            {
                return false;
            }

            for (int i = 0; i < snakeInstances.Count; i++)
            {
                if (Vector2.Distance(pos, snakeInstances[i].transform.position) < 0.75)
                {
                    return true;
                }
            }

            return false;
        }
    }
}