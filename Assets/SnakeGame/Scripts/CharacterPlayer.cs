using UnityEngine;

namespace SnakeGame
{
    public class CharacterPlayer : MonoBehaviour
    {
        public PlayerInputHandler inputHandler;
        public GameManager gameManager;
        public Character character;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 movement = inputHandler.move;
            Vector2 adjustedMovement = SnapInput.GetSnapInput(movement, SnapInput.EInputSnap.DPAD);
            character.SetMove(adjustedMovement);
        }
    }
}