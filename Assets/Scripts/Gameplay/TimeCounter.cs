using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TimeCounter : MonoBehaviour
    {
        private readonly string defaultText = "Tiempo: ";
        
        [SerializeField] public float timeRemaining;
        [SerializeField] public Text text;

        // Update is called once per frame
        void Update()
        {
            timeRemaining -= Time.deltaTime;
            text.text = $"<b>{defaultText} {((int)timeRemaining).ToString()}</b>";
        }
    }
}
