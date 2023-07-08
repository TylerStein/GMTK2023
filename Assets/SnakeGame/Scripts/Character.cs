using UnityEngine;

namespace SnakeGame
{
    public class Character : WorldObject
    {
        public SpriteRenderer spriteRenderer;
        public Vector2 move;
        public float speed;

        public void Start()
        {
            SetupTickReceiver(1024);
        }

        public void SetMove(Vector2 move)
        {
            this.move = move;
            int direction = Direction.GetDirection(move);
            transform.rotation = Direction.GetRotation(direction);
        }

        public override void OnTick(float deltaTime)
        {
            transform.position = transform.position + (Vector3)(move * speed);
            move = Vector2.zero;
            ClampPosition();
        }

        void ClampPosition()
        {
            float clampedX = transform.position.x;
            float clampedY = transform.position.y;
            if (clampedY >= gameManager.borderUp.position.y)
            {
                clampedY = gameManager.borderUp.position.y - 1;
            }
            else if (clampedY <= gameManager.borderDown.position.y)
            {
                clampedY = gameManager.borderDown.position.y + 1;
            }

            if (clampedX >= gameManager.borderRight.position.x)
            {
                clampedX = gameManager.borderRight.position.x - 1;
            }
            else if (clampedX <= gameManager.borderLeft.position.x)
            {
                clampedX = gameManager.borderLeft.position.x + 1;
            }

            transform.position = new Vector2(Mathf.Round(clampedX), Mathf.Round(clampedY));
        }
    }
}