using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    //Nodes Variables
    public GameObject leftWrapNode;
    public GameObject rightWrapNode;

    public enum EnemyMode
    {
        chase, scatter
    }

    public EnemyMode currentEnemyMode;

    //UI Variables
    public int points = 0;
    public TextMeshProUGUI textPoints;
    public GameObject winCanvas;

    void Start()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(false); // Me aseguro que  el Canvas esté desactivado al inicio
        }
    }


    private void Awake()
    {
        currentEnemyMode = EnemyMode.chase;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        textPoints.text = "Points:" + points.ToString(); // muestra puntuacion en UI 

        if (points >= 80 && winCanvas != null && !winCanvas.activeSelf)
        {
            winCanvas.SetActive(true); // Activa el Canvas de victoria
        }

    }
}
