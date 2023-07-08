using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public string TagSnake = "Snake";
        public string TagFood = "Food";
        public string TagPlayer = "Player";
        public string TagBorder = "Border";

        public Transform borderUp;
        public Transform borderRight;
        public Transform borderDown;
        public Transform borderLeft;

        public GameObject foodPrefab;
        public GameObject movingFoodPrefab;

        public List<GameObject> availableFood;

        public float tickRate = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
            SpawnFood();
            SpawnFood();
            SpawnFood();
            SpawnFood();
            availableFood = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagFood));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool TryEat(GameObject food)
        {
            if (!food)
            {
                return false;
            }

            if (food.name == "Player")
            {
                // RespawnPlayer();
                return false;
            }

            RemoveFood(food);
            SpawnFood();
            return true;
        }

        public void RemoveFood(GameObject food)
        {
            int index = availableFood.IndexOf(food);
            if (index >= 0)
            {
                availableFood.RemoveAt(index);
                food.SetActive(false);
                Destroy(food);
            }
        }

        public Vector2 GetRandomInBounds()
        {
            return new Vector2(Random.Range(borderLeft.position.x + 2, borderRight.position.x - 2), Random.Range(borderDown.position.y + 2, borderUp.position.y - 2));
        }

        public Vector2Int GetIntRandomInBounds()
        {
            Vector2 random = GetRandomInBounds();
            return new Vector2Int(Mathf.RoundToInt(random.x), Mathf.RoundToInt(random.y));
        }

        public void SpawnMovingFood(Vector2 origin, Vector2 direction)
        {
            GameObject movingFood = Instantiate(movingFoodPrefab, origin, Quaternion.identity);
            movingFood.GetComponent<MovingFood>().direction = direction;
            availableFood.Add(movingFood);
        }

        public bool IsInBounds(Vector2 point)
        {
            return
                point.y < borderUp.position.y &&
                point.x < borderRight.position.x &&
                point.y > borderDown.position.y &&
                point.x > borderLeft.position.x;
        }

        void SpawnFood()
        {
            Vector2Int random = GetIntRandomInBounds();
            GameObject instance = Instantiate(foodPrefab, new Vector3(random.x, random.y, 0), Quaternion.identity);
            availableFood.Add(instance);
        }

    }
}