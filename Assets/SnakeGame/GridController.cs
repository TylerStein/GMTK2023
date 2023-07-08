using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeV1
{
    public class GridController : MonoBehaviour
    {
        public int width = 12;
        public int height = 12;

        public Vector2 tileSize = Vector2.one;
        public GameObject tilePrefab;

        private List<GameObject> tileInstances = new List<GameObject>();


        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector2(-width / 2, -height / 2);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject tileInstance = Instantiate(tilePrefab);
                    tileInstance.transform.position = new Vector3(transform.position.x + (tileSize.x * i), transform.position.y + (tileSize.y * j), transform.position.z);
                    tileInstance.transform.SetParent(gameObject.transform, true);
                    tileInstances.Add(tileInstance);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}