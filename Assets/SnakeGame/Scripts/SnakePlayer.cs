using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakePlayer : MonoBehaviour
    {
        public PlayerInputHandler inputHandler;
        public Snake snake;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int moveX = Mathf.RoundToInt(inputHandler.move.x);
            int moveY = Mathf.RoundToInt(inputHandler.move.y);
            snake.SetDirection(new Vector2(moveX, moveY));
        }
    }
}