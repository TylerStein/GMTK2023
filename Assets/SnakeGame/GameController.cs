using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV1
{
    public class GameController : MonoBehaviour
    {
        // Start is called before the first frame update
        public FoodPlayerController playerController;
        public SnakeController snakeController;
        public GridController grid;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 playerPosition = playerController.GetPosition();
            if (snakeController.IsCollidingWith(playerPosition))
            {
                Debug.Log("Collision with player!");
                OnSnakeCollideWithPlayer();
            }
        }

        public void OnSnakeCollideWithPlayer()
        {
            Vector2 snakePosition = (Vector2)snakeController.transform.position;
            Vector2 respawnPosition = new Vector2();
            float quadrantWidth = (grid.width / 2) * grid.tileSize.x;
            float quadrantHeight = (grid.height / 2) * grid.tileSize.y;
            if (snakePosition.x > 0 && snakePosition.y > 0)
            {
                // upper-right quadrant
                respawnPosition.x = Random.Range(0, quadrantWidth);
                respawnPosition.y = Random.Range(0, quadrantHeight);

            }
            else if (snakePosition.x > 0 && snakePosition.y < 0)
            {
                // lower-right quadrant
                respawnPosition.x = Random.Range(0, quadrantWidth);
                respawnPosition.y = quadrantHeight - Random.Range(0, quadrantHeight);

            }
            else if (snakePosition.x < 0 && snakePosition.y > 0)
            {
                // upper-left quadrant
                respawnPosition.x = quadrantWidth - Random.Range(0, quadrantWidth);
                respawnPosition.y = Random.Range(0, quadrantHeight);
            }
            else
            {
                // lower-left quadrant
                respawnPosition.x = quadrantWidth - Random.Range(0, quadrantWidth);
                respawnPosition.y = quadrantHeight - Random.Range(0, quadrantHeight);
            }

            playerController.SetPosition(respawnPosition);
            snakeController.Grow();
        }
    }
}