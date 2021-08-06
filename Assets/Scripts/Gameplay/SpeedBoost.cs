using System;
using UnityEngine;

namespace Gameplay
{
    public class SpeedBoost : MonoBehaviour
    {
        private bool _wasBoosted = false;
        private float _initialSpeed;

        [SerializeField] public int timeBoost;
        [SerializeField] public float boostPercentage;

        public void Boost(ref float speed)
        {
            if (!_wasBoosted)
            {
                GetComponent<AudioSource>().Play();
                _wasBoosted = true;
                _initialSpeed = speed;
                speed += (speed * (boostPercentage / 100));
            }
        }

        public void ResetSpeed(ref float speed)
        {
            if (_wasBoosted)
            {
                speed = _initialSpeed;
            }
        }
    }
}
