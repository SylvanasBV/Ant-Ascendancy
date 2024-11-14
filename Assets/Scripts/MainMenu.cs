using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Play()
    {
        Time.timeScale = 1f; // Asegura que el juego esté en escala de tiempo normal
        SceneManager.LoadScene(1);
    }

   
}
