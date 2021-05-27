using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void Settings()
    {
        Debug.Log("Settings Menu");
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
