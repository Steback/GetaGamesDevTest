using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TimeCounter : MonoBehaviour
    {
        private readonly string defaultText = "Tiempo: ";
        private Collider _collider;
        
        [SerializeField] public float timeRemaining;
        [SerializeField] public Text text;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        // Update is called once per frame
        void Update()
        {
            timeRemaining -= Time.deltaTime;
            text.text = $"<b>{defaultText} {((int)timeRemaining).ToString()}</b>";
        }

        private void OnCollisionEnter(Collision other)
        {
            TimeBoost timeBoost = other.gameObject.GetComponent<TimeBoost>();
            
            if (timeBoost) timeBoost.IncrementTime(ref timeRemaining);
        }
    }
}
