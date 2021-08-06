using UnityEngine;

namespace Gameplay
{
    public class FollowCamera : MonoBehaviour
    {
        private float _yPosition;
        private Vector3 _angles;
        
        [SerializeField] public Transform target;
        [SerializeField] private float distance;
        
        // Start is called before the first frame update
        void Start()
        {
            _yPosition = transform.position.y;
            _angles = transform.eulerAngles;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 position = target.position + (-target.forward * distance);
            position.y = _yPosition;
            transform.position = position;

            _angles.y = target.eulerAngles.y;
            transform.eulerAngles = _angles;
        }
    }
}
