using UnityEngine;

namespace SnakeGame
{
    public class MovingFood : WorldObject
    {
        public Vector2 direction;

        public void Start()
        {
            SetupTickReceiver(128);
        }

        public override void OnTick(float deltaTime)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x + direction.x), Mathf.Round(transform.position.y + direction.y), transform.position.z); 
            if (!gameManager.IsInBounds(transform.position))
            {
                gameManager.RemoveFood(this);
            }
        }

        public override Vector2 GetProjectedPosition(int steps)
        {
            return (Vector2)transform.position + (direction * steps);
        }
    }
}