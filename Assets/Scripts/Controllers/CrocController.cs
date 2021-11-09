using UnityEngine;

namespace Controllers
{
    public class CrocController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _screenBounds;
        
        public Vector2 movementDirection;
        public float speed = 4f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _screenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        }

        private void Update()
        {
            _rb.velocity = movementDirection * speed;
        
            if (movementDirection == Vector2.left && (transform.position.x < _screenBounds.x * -1.5))
                Destroy(gameObject);
            if (movementDirection == Vector2.right && (transform.position.x > _screenBounds.x * 1.5))
                Destroy(gameObject);
        
        }

    }
}
