using System;
using UnityEngine;
using System.Collections; 
public class LifePlayer : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public event Action<int> OnHealthChanged; // Evento para notificar el cambio de salud

    public GameObject overCanvas;

    private Collider2D playerCollider;// Collider del jugador
    private bool wasAttack=false;
    private void Start()
    {
        currentHealth = maxHealth; // Iniciar con salud completa
        OnHealthChanged?.Invoke(currentHealth); // Notificar el valor inicial
        playerCollider = GetComponent<Collider2D>(); 
    }

    // Método para reducir vida
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Limitar entre 0 y maxHealth

        OnHealthChanged?.Invoke(currentHealth); // Notificar el cambio en la salud

        if (currentHealth <= 0)
        {
            Die();
           
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");

         overCanvas.SetActive(true);
        AudioManager.Instance.PlaySFX("GameOver");
         Time.timeScale = 0f;
        // Lógica adicional al morir (reiniciar nivel, game over, etc.)
    }

    // Método para detectar colisiones con el enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&& !wasAttack) // Verifica si el objeto es un enemigo
        {
            AudioManager.Instance.PlaySFX("Damage");
            TakeDamage(1); // Reducir vida en 1 al tocar el enemigo
            StartCoroutine(TemporaryInvincibility());
        }
    }

     private IEnumerator TemporaryInvincibility()
    {   
        
        wasAttack = true; // Desactivar el collider (trigger) para evitar más daño
        yield return new WaitForSeconds(3f); // Esperar 3 segundos
        wasAttack= false; // Reactivar el collider
    }
}
