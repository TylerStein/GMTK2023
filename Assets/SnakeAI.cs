using SnakeGame;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public GameManager gameManager;
    public Snake snake;

    public List<GameObject> observedFood = new List<GameObject>();
    public GameObject closestFood = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("Tick", gameManager.tickRate, gameManager.tickRate);
    }

    void Tick()
    {
        observedFood = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup"));
        if (observedFood.Count > 0)
        {
            Vector2 headPosition = snake.head.position;
            observedFood.Sort((a, b) => Vector2.Distance(headPosition, a.transform.position) < Vector2.Distance(headPosition, b.transform.position) ? -1 : 1);
            closestFood = observedFood[0];
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < observedFood.Count; i++)
        {
            Gizmos.color = observedFood[i] == closestFood ? Color.blue : Color.grey;
            Gizmos.DrawLine(snake.head.position, observedFood[i].transform.position);
        }
    }
}
