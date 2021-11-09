using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Controllers
{
    public class FrogController : MonoBehaviour
    {
        [SerializeField] private SceneController sceneController;
        [SerializeField] private GameObject splatPrefab;
    
        private SpriteRenderer _sr;
        
        private Vector2 _facing = Vector2.up;
        private bool _inWater = false;
        private Vector3 _startingPosition;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _startingPosition = transform.position;
        }

        private void Update()
        {
            ButtonInputLogic();

            if (_inWater && transform.parent == null)
            {
                Debug.Log("You died in water");
                CreateSplatObject();
                ResetToStartingPosition();
                sceneController.SetScore(0);
            } 
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Goal"))
            {
                Debug.Log("Winner!!");
                // Add score to Scene Controller.
                sceneController.AddScore(100);
                ResetToStartingPosition();
            }
            else if (other.CompareTag("Water"))
            {
                Debug.Log("In Water Now");
                _inWater = true;
            }
            else if (other.CompareTag("Car") || other.CompareTag("Croc"))
            {
                CreateSplatObject();
                ResetToStartingPosition();
                // Add Score.
                sceneController.SetScore(0);

            }
            else if (other.CompareTag("Log"))
            {
                gameObject.transform.parent = other.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Water"))
            {
                Debug.Log("Exiting Water");
                _inWater = false;
            }
            else if (other.CompareTag("Log"))
            {
                Debug.Log("Hopping from log");
                gameObject.transform.parent = null;
                if (_inWater) Debug.Log("SPLASH");
            }
        }

        private void ButtonInputLogic()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var horizontalInput = Input.GetAxis("Horizontal");
                
                if (horizontalInput > 0) Move(Vector2.right);
                else if (horizontalInput < 0) Move(Vector2.left);
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                var verticalInput = Input.GetAxis("Vertical");

                if (verticalInput > 0) Move(Vector2.up);
                else if (verticalInput < 0) Move(Vector2.down);
            }
        }

        private void CreateSplatObject()
        {
            // Create a splat object.
            var splatGo = Instantiate(splatPrefab);
            splatGo.transform.position = transform.position;
            sceneController.AddSplat(splatGo);
        }
        
        private void ResetToStartingPosition()
        {
            // Remove the parent transform if one exists.
            if (transform.parent != null) transform.parent = null;
            
            var trans = transform;
            trans.position = _startingPosition;
            RotateSpriteToFacing(Vector2.up);
        }
        
        private void Move(Vector2 direction)
        {
            RotateSpriteToFacing(direction);
            transform.Translate(Vector3.up);
        }
        
        private void RotateSpriteToFacing(Vector2 newFacing)
        {
            //Debug.Log($"Facing: {_facing}, New Facing: {newFacing}");
            if (newFacing == _facing) 
                return;
    
            if (_facing == Vector2.up)
            {
                if (newFacing == Vector2.left) transform.Rotate(0, 0, 90f);
                if (newFacing == Vector2.right) transform.Rotate(0, 0, -90f);
                if (newFacing == Vector2.down) transform.Rotate(0, 0, 180f);
            }

            if (_facing == Vector2.down)
            {
                if (newFacing == Vector2.left) transform.Rotate(0, 0, -90f);
                if (newFacing == Vector2.right) transform.Rotate(0, 0, 90f);
                if (newFacing == Vector2.up) transform.Rotate(0, 0, 180f);
            }

            if (_facing == Vector2.left)
            {
                if (newFacing == Vector2.right) transform.Rotate(0, 0, 180f);
                if (newFacing == Vector2.up) transform.Rotate(0,0, -90f);
                if (newFacing == Vector2.down) transform.Rotate(0,0, 90f);
            }
            if (_facing == Vector2.right)
            {
                if (newFacing == Vector2.left) transform.Rotate(0, 0, 180f);
                if (newFacing == Vector2.up) transform.Rotate(0, 0, 90f);
                if (newFacing == Vector2.down) transform.Rotate(0, 0, -90f);
            }

            _facing = newFacing;
        }
    }
}
