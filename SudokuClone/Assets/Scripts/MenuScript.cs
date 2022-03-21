using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript: MonoBehaviour
{
    private int MainMenuBulidIndex = 0;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuBulidIndex);
    }
}
