using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assignment
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] GameObject menuPanel;

        public bool IsLocked { get; set; }

        private const string GAME_OVER_TEXT = "You died";
        private const string VICT0ORY_TEXT = "You are victorious";

        private void Update()
        {
            if (!IsLocked && Input.GetKeyDown(KeyCode.Escape))
            {
                menuPanel.SetActive(!menuPanel.activeInHierarchy);
            }
        }

        public void OnLoadSceneButtonClicked()
        {
            SceneManager.LoadScene(1);
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}