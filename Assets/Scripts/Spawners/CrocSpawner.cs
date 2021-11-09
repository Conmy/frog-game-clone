using System.Collections;
using Controllers;
using UnityEngine;

namespace Spawners
{
    public class CrocSpawner : MonoBehaviour
    {
        public GameObject crocPrefab;
        public float spawnFrequency = 1.5f;
        public bool generateObstacles = true;
        public float objectSpeed = 4f;
        public bool objectMovingRight = false;
        
        void Start()
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
                var go = Instantiate(crocPrefab);
                // Set the position.
                go.transform.position = transform.position;
                // Propagate managed components. 
                var crocController = go.GetComponent<CrocController>();
                crocController.speed = objectSpeed;
                crocController.movementDirection = objectMovingRight ? Vector2.right : Vector2.left;
                // Control the facing direction of the croc sprite.
                var crocSpriteRenderer = go.GetComponent<SpriteRenderer>();
                crocSpriteRenderer.flipX = !objectMovingRight;
            }
        }
    }
}
