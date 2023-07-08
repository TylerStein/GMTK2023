using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeV1
{
    public class FoodPlayerController : MonoBehaviour
    {
        public PlayerInputHandler inputHandler;
        public float speed = 6.0f;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler.AddInputListener(gameObject, 0);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInput();
        }

        void UpdateInput()
        {
            if (!inputHandler.IsActiveListener(gameObject))
            {
                return;
            }

            transform.Translate(speed * inputHandler.move * Time.deltaTime);
        }

        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
        }

        public Vector2 GetPosition()
        {
            return (Vector2)transform.position;
        }
    }
}