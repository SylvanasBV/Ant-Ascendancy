using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject menuPusa;
    private bool pauseGame = false;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(pauseGame)
            {
                Resume();
            } else
                {
                    Pause();
                }
        }
    }
 
    public void Pause()
    {
        pauseGame = true;
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        menuPusa.SetActive(true);   
    }

    public void Resume()
    {
        pauseGame = false;
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        menuPusa.SetActive(false);

    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }


}
