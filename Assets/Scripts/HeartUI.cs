using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public LifePlayer playerHealth; // Referencia al script de vida del jugador
    public Image[] heartImages; // Array de imágenes de corazón

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHearts; // Suscribirse al evento de cambio de salud
        }
        else
        {
            Debug.LogError("PlayerHealth no está asignado en HeartUI.");
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHearts; // Desuscribirse del evento al desactivar
        }
    }

    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHealth; // Mostrar u ocultar cada corazón
        }
    }
}
