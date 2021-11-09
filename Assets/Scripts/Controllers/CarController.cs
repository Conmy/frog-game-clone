using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class CarController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Vector2 _screenBounds;
        
        public Vector2 movementDirection;
        public float speed = 4f;
        public List<Sprite> sprites;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _sr.sprite = ChooseSprite();
            
            _screenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            // Debug.Log("Start CarController moving: " + _movementDirection + " with speed: " + speed);
            _rb.velocity = movementDirection * speed;

            if (movementDirection == Vector2.left && (transform.position.x < _screenBounds.x * -1.5))
                Destroy(gameObject);
            if (movementDirection == Vector2.right && (transform.position.x > _screenBounds.x * 1.5))
                Destroy(gameObject);
        }

        private Sprite ChooseSprite()
        {
            return sprites[Random.Range(0, sprites.Count)];
        }
    }
}
