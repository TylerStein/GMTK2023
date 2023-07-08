using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class MovingFood : MonoBehaviour
    {
        public GameManager gameManager;
        public Vector2 direction;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            InvokeRepeating("Tick", gameManager.tickRate, gameManager.tickRate);
        }

        void Tick()
        {
            transform.Translate(direction);
            if (!gameManager.IsInBounds(transform.position))
            {
                gameManager.RemoveFood(gameObject);
            }
        }
    }
}