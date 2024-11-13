using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private Dictionary<string, string> animationStates;

    // Inicialización del manager para un personaje específico
    public void Initialize(Animator characterAnimator, Dictionary<string, string> states)
    {
        animator = characterAnimator;
        animationStates = states;
    }

    // Método para reproducir la animación
    public void PlayAnimation(string animationName)
    {
        foreach (var state in animationStates)
        {
            animator.SetBool(state.Value, false);
        }

        if (animationStates.ContainsKey(animationName))
        {
            animator.SetBool(animationStates[animationName], true);
        }
        else
        {
            Debug.LogWarning("La animación no existe en el diccionario: " + animationName);
        }
    }
}

