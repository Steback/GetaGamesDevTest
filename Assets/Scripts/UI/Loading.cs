using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class Loading : MonoBehaviour
    {
        private bool _loadScene = true;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_loadScene)
            {
                _loadScene = false;
                StartCoroutine(Waiter());
            }
        }
        
        IEnumerator Waiter()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Scenes/TrackScene");
        }
    }
}
