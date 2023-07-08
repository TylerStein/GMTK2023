using UnityEngine;

namespace SnakeGame
{
    public class WorldObject : MonoBehaviour
    {
        protected GameManager gameManager;
        protected int priority = 0;
        public void SetupTickReceiver(int priority)
        {
            this.priority = priority;
            if (!gameManager)
            {
                gameManager = FindObjectOfType<GameManager>();
            }

            gameManager.RegisterTickReceiver(this, priority);
        }

        public void SetTickReceiverPriority(int priority)
        {
            gameManager.ChangeTickReceiverPriority(this, priority);
        }

        protected virtual void OnDestroy()
        {
            if (gameManager)
            {
                gameManager.UnRegisterTickReceiver(this);
            }
        }

        public virtual void OnTick(float deltaTime) { }

        public virtual Vector2 GetProjectedPosition(int steps) {
            return Vector2.zero;
        }
    }
}