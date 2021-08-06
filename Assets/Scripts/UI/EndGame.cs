using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] public Button mainMenuButton;
        [SerializeField] public Text text;
    
        // Start is called before the first frame update
        void Awake()
        {
            if (Gameplay.EndGaneObserver.Victory)
            {
                text.text = "Victory";
            }
            else
            {
                text.text = "Game Over";
            }
            mainMenuButton.onClick.AddListener(LoadMainMenu);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        } 
    }
}
