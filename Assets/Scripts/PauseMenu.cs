using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
           
    }

    public void Pause()
    {
        UI.SetActive(!UI.activeSelf);
        FindObjectOfType<AudioManager>().Play("PauseSound");

        if (UI.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Restart()
    {
        Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Button Pressed");
    }

    public void Menu()
    {
        Pause();
        SceneManager.LoadScene("MainMenu");
    }
}
