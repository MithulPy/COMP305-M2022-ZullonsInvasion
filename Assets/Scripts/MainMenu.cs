using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void Menu()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene("StartMenu");
    }

    public void Level1()
    {
        Debug.Log("Level1");
        GameManager.score = 0;
        SceneManager.LoadScene("Level1");

    }

    public void Level2()
    {
        Debug.Log("Level2");
        GameManager.score = 0;
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        Debug.Log("Level3");
        GameManager.score = 0;
        SceneManager.LoadScene("Level3");
    }
}
