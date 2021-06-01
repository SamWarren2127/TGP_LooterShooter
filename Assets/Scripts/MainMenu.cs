using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
