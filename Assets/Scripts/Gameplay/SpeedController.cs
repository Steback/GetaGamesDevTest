using UnityEngine;

namespace Gameplay
{
    public class SpeedController : MonoBehaviour
    {
        private Movement _movement;
        private SpeedBoost _boost;
        private float _timeBoost;
        private bool _isBoostActivated = false;

        // Start is called before the first frame update
        void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isBoostActivated)
            {
                if (_timeBoost <= 0)
                {
                    _isBoostActivated = false;
                    _timeBoost = 0;
                    _boost.ResetSpeed(ref _movement.speed);
                    Object.Destroy(_boost.gameObject);
                }
                else
                {
                    _timeBoost -= Time.deltaTime;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_isBoostActivated) return;
            _boost = other.gameObject.GetComponent<SpeedBoost>();
                
            if (_boost)
            {
                _boost.Boost(ref _movement.speed);
                _timeBoost = _boost.timeBoost;
                _isBoostActivated = true;
            }
        }
    }
}
