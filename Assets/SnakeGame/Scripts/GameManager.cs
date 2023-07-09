using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI highScoreText;
        public float timer;
        public float highScore;

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

        public List<WorldObject> availableFood = new List<WorldObject>();
        public PrioritizedList<WorldObject> tickReceivers = new PrioritizedList<WorldObject>();

        public Snake snake;
        public CharacterPlayer player;

        public float tickRate = 0.15f;

        // Start is called before the first frame update
        void Start()
        {
            snake = FindObjectOfType<Snake>();
            player = FindObjectOfType<CharacterPlayer>();

            SpawnFood();
            SpawnFood();
            SpawnFood();    
            SpawnFood();
            InvokeRepeating("OnTick", tickRate, tickRate);

            timer = 0;
            timerText.text = Mathf.Round(timer).ToString();
            highScoreText.text = Mathf.Round(highScore).ToString();
        }

        public void OnTick()
        {
            for (int i = 0; i < tickReceivers.Count; i++)
            {
                tickReceivers.ValueAt(i).OnTick(tickRate);
            }

            timer += tickRate;
            if (timer > highScore)
            {
                highScore = timer;
            }
            timerText.text = Mathf.Round(timer).ToString();
            highScoreText.text = Mathf.Round(highScore).ToString();
        }

        public void RegisterTickReceiver(WorldObject receiver, int priority)
        {
            if (!tickReceivers.ContainsValue(receiver))
            {
                tickReceivers.Add(receiver, priority);
            }
        }

        public void ChangeTickReceiverPriority(WorldObject receiver, int priority)
        {
            tickReceivers.UpdatePriority(receiver, priority);
        }

        public void UnRegisterTickReceiver(WorldObject receiver)
        {
            tickReceivers.Remove(receiver);
        }

        public void RegisterPlayer(CharacterPlayer player)
        {
            availableFood.Add(player.character);
        }

        public bool TryEat(WorldObject food)
        {
            if (!food)
            {
                return false;
            }

            if (food.name == "Player")
            {
                player.shouldReset = true;
                snake.shouldReset = true;
                timer = 0;


                return false;
            }

            RemoveFood(food);
            SpawnFood();
            return true;
        }

        public void RemoveFood(WorldObject food)
        {
            int index = availableFood.IndexOf(food);
            if (index >= 0)
            {
                availableFood.RemoveAt(index);
                food.gameObject.SetActive(false);
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
            availableFood.Add(movingFood.GetComponent<WorldObject>());
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
            availableFood.Add(instance.GetComponent<WorldObject>());
        }

    }
}