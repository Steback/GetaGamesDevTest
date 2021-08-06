using UnityEngine;
using UnityEngine.AI;

namespace Gameplay
{
    public class Movement : MonoBehaviour
    {
        private Transform _transform;
        private float _yPosition;
        private bool _penaltyActivated = false;
        private float _penaltyTimeRemaining;
        private bool _jumpActivated = false;
        private float _currentYPosition;
        private float _distanceJump;

        [SerializeField] public float speed;
        [SerializeField] public float angularVelocity;
        
        // Start is called before the first frame update
        void Awake()
        {
            _transform = GetComponent<Transform>();
            _yPosition = _transform.position.y;
            _currentYPosition = _yPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (_penaltyActivated)
            {
                if (_penaltyTimeRemaining <= 0)
                {
                    _penaltyActivated = false;
                }
                else
                {
                    _penaltyTimeRemaining -= Time.deltaTime;
                    return;
                }
            }
            
            Vector3 rotation = Vector3.zero;
            Vector3 position = _transform.position;
            rotation.y = _transform.eulerAngles.y;

            KeyInput(ref position, ref rotation);
            HeightDistance(ref position);
            
            _transform.eulerAngles = rotation;
            _transform.position = position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_penaltyActivated)
            {
                Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
                
                if (obstacle)
                {
                    _penaltyActivated = true;
                    _penaltyTimeRemaining = obstacle.penaltyTime;
                    Object.Destroy(other.gameObject);
                }
            }

            if (!_jumpActivated)
            {
                 JumpBoost jumpBoost = other.gameObject.GetComponent<JumpBoost>();

                if (jumpBoost)
                {
                    _jumpActivated = true;
                    _distanceJump = jumpBoost.distanceJump;
                    Object.Destroy(other.gameObject);
                }
            }
        }

        private void KeyInput(ref Vector3 position, ref Vector3 rotation)
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                position += _transform.forward * (speed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.S))
            { 
                position -= _transform.forward * (speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rotation.y -= angularVelocity * Time.deltaTime;
            }
            
            if (Input.GetKey(KeyCode.D))
            { 
                rotation.y += angularVelocity * Time.deltaTime;
            }
        }

        private void HeightDistance(ref Vector3 position)
        {
            if (_jumpActivated)
            {
                if (_currentYPosition < _distanceJump)
                {
                    print("Jump!");
                    _currentYPosition += speed * Time.deltaTime;
                }
                else
                {
                    _jumpActivated = false;
                }
            }
            else
            {
                if (_currentYPosition > _yPosition)
                {
                    _currentYPosition -= speed * Time.deltaTime;
                }
                else
                {
                    _currentYPosition = _yPosition;
                }
            }
            
            position.y = _currentYPosition;
        }
    }
}
