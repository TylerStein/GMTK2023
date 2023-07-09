using UnityEngine;

namespace SnakeGame
{
    public class CharacterPlayer : MonoBehaviour
    {
        public PlayerInputHandler inputHandler;
        public GameManager gameManager;
        public Character character;
        public GameObject movingFoodPrefab;

        public float fireCooldown = 3;
        public float fireTimer = 0;

        public bool enableFire = true;
        public bool shouldReset = false;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.RegisterPlayer(this);
            fireTimer = fireCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            if (shouldReset)
            {
                ResetPlayer();
                return;
            }

            if (fireTimer < fireCooldown)
            {
                fireTimer = Mathf.Clamp(fireTimer + Time.deltaTime, 0, fireCooldown);
            }

            Vector2 movement = inputHandler.move;
            Vector2 adjustedMovement = SnapInput.GetSnapInput(movement, SnapInput.EInputSnap.FOUR);
            character.SetMove(adjustedMovement);

            bool fire = enableFire && inputHandler.fire.downThisFrame;
            if (fire && fireTimer >= fireCooldown)
            {
                Vector3 pointerWorld = Camera.main.ScreenToWorldPoint(inputHandler.pointer);
                Vector2 direction = SnapInput.GetSnapInput((pointerWorld - transform.position).normalized, SnapInput.EInputSnap.FOUR);
                gameManager.SpawnMovingFood((Vector2)transform.position + direction, direction);
                fireTimer = 0;
            }
        }

        private void ResetPlayer()
        {
            fireTimer = 0;
            transform.position = Vector2.zero;
            character.SetMove(Vector2.zero);
            shouldReset = false;
        }
    }
}