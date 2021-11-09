using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Spawners
{
    public class CarSpawner : MonoBehaviour
    {
        public float spawnFrequency = 1.5f;
        public List<GameObject> prefabs;
        public bool generateObstacles = true;
        public float objectSpeed = 4f;
        public bool objectMovingRight = false;
    
        // Start is called before the first frame update
        protected void Start()
        {
            StartCoroutine(SpawnItem());
        }

        private IEnumerator SpawnItem()
        {
            while (generateObstacles)
            {
                // Do at a defined frequency.
                yield return new WaitForSeconds(spawnFrequency);

                // Create the GameObject.
                var go = Instantiate(ChoosePrefab());
                // Set the position.
                go.transform.position = transform.position;
                // Propagate managed components. 
                var carController = go.GetComponent<CarController>();
                carController.speed = objectSpeed;
                carController.movementDirection = objectMovingRight ? Vector2.right : Vector2.left;
            }
        }

        private GameObject ChoosePrefab()
        {
            return prefabs[Random.Range(0, prefabs.Count)];
        }
    }
}
