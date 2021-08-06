using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay
{
    public class TimeCounter : MonoBehaviour
    {
        private readonly string defaultText = "Tiempo: ";

        [SerializeField] public GameObject endGameObserver;
        [SerializeField] public float timeRemaining;
        [SerializeField] public Text text;

        // Update is called once per frame
        void Update()
        {
            if (timeRemaining <= 0)
            {
                EndGaneObserver.Victory = false;
                EndScene();
            }
            else
            {
                timeRemaining -= Time.deltaTime;
                text.text = $"<b>{defaultText} {((int)timeRemaining).ToString()}</b>";
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Goal")
            {
                EndGaneObserver.Victory = true;
                EndScene();
            } 
            
            TimeBoost timeBoost = other.gameObject.GetComponent<TimeBoost>();
            
            if (timeBoost) timeBoost.IncrementTime(ref timeRemaining);
        }

        private void EndScene()
        {
            DontDestroyOnLoad(endGameObserver);
            SceneManager.LoadScene("Scenes/EndGame", LoadSceneMode.Single);
        }
    }
}
