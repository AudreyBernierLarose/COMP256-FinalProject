using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("FinalProjectGame");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR //if in the unity editor and want to quit
        UnityEditor.EditorApplication.isPlaying = false;
#endif //if in the application itself and want to quit
        Application.Quit();
    }
}
