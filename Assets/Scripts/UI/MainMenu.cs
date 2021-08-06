using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] public Button playButton;
        
        // Start is called before the first frame update
        void Awake()
        {
            playButton.onClick.AddListener(Play);
        }

        private void Play()
        {
            SceneManager.LoadScene("Scenes/Loading", LoadSceneMode.Single);
        }
    }
}
