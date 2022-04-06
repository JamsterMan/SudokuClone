using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript: MonoBehaviour
{
    private int MainMenuBulidIndex = 0;

    //moves to the game scene
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //quits application
    public void ExitGame()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    //goes to the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuBulidIndex);
    }
}
