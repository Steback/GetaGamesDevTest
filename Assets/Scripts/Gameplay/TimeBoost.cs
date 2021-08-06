using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class TimeBoost : MonoBehaviour
    {
        private bool _timeIncremented = false;
        
        public void IncrementTime(ref float timeRemaining)
        {
            if (!_timeIncremented)
            {
                _timeIncremented = true;
                timeRemaining += 5f;
                Object.Destroy(gameObject);
            } 
        }
    }
}
