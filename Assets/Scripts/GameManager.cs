using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {
        currentEnemyMode = EnemyMode.chase;

        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribir al evento de escena cargada
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        // Desuscribir al evento de escena cargada para evitar errores
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasignar referencias cuando se carga una nueva escena
        ReasignarReferencias();
    }

    void Start()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(false); // Me aseguro que el Canvas esté desactivado al inicio
        }
        ReasignarReferencias();

    }

    void Update()
    {
        textPoints.text = "Points:" + points.ToString(); // muestra puntuacion en UI 

        if (points >= 80 && winCanvas != null && !winCanvas.activeSelf)
        {
            winCanvas.SetActive(true); // Activa el Canvas de victoria
            Time.timeScale = 0f;
            AudioManager.Instance.PlaySFX("GameWin");
            AudioManager.Instance.StopMusic();
        }
    }

    private void ReasignarReferencias()
    {
        player = GameObject.FindWithTag("Player");
        leftWrapNode = GameObject.Find("NodeA");
        rightWrapNode = GameObject.Find("NodeB");
        textPoints = GameObject.Find("PointsText")?.GetComponent<TextMeshProUGUI>();

        // Encuentra el objeto "Canvas General" y luego busca "YouWin" dentro de él
        GameObject canvasGeneral = GameObject.Find("Canvas General");
        if (canvasGeneral != null)
        {
            winCanvas = canvasGeneral.transform.Find("YouWin")?.gameObject; // Busca "YouWin" dentro del "Canvas General"
        }

        if (winCanvas != null)
        {
            winCanvas.SetActive(false); // Asegúrate de que esté desactivado si es necesario
        }
    }

    public void ResetPoints()
    {
        points = 0; // Reinicia los puntos a cero
        if (textPoints != null)
        {
            textPoints.text = "Points: 0"; // Actualiza la UI
        }
    }
}
