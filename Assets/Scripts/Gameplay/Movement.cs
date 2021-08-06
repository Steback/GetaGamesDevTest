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

        [SerializeField] public float speed;
        [SerializeField] public float angularVelocity;
        
        // Start is called before the first frame update
        void Awake()
        {
            _transform = GetComponent<Transform>();
            _yPosition = _transform.position.y;
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

            _transform.eulerAngles = rotation;
            position.y = _yPosition;
            _transform.position = position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_penaltyActivated) return;

            Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();

            if (obstacle)
            {
                _penaltyActivated = true;
                _penaltyTimeRemaining = obstacle.penaltyTime;
                Object.Destroy(other.gameObject);
            }
        }
    }
}
