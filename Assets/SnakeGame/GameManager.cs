using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class GameManager : MonoBehaviour
    {
        public Transform borderUp;
        public Transform borderRight;
        public Transform borderDown;
        public Transform borderLeft;
        public GameObject foodPrefab;

        // Start is called before the first frame update
        void Start()
        {
            SpawnFood();
            SpawnFood();
            SpawnFood();
            SpawnFood();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSnakeEat()
        {
            SpawnFood();
        }

        void SpawnFood()
        {
            int x = Mathf.RoundToInt(Random.Range(borderLeft.position.x, borderRight.position.x));
            int y = Mathf.RoundToInt(Random.Range(borderDown.position.y, borderUp.position.y));
            Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }

    }
}