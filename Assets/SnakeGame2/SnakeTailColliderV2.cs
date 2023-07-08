using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV2
{
    public class SnakeTailColliderV2 : MonoBehaviour
    {
        SnakeControllerV2 controller;

        // Start is called before the first frame update
        void Start()
        {
            controller = FindObjectOfType<SnakeControllerV2>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            controller.OnAnyCollision(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            controller.OnAnyCollision(collision.gameObject);
        }
    }
}