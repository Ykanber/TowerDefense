using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelPassedUI : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
