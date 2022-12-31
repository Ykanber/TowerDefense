using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadButton()
    {
        PlayerData data = SaveSystem.LoadGame();
        if (data != null)
        {
            if (SaveSystem.saveOnNextLevelScreen == true)
            {
                SceneManager.LoadScene(data.LevelIndex + 1);
            }
            else
            {
                SaveSystem.isLoad = true;
                SceneManager.LoadScene(data.LevelIndex);
            }
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
