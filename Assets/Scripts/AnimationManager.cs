using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private Dictionary<string, string> animationStates;

    // Inicializaci�n del manager para un personaje espec�fico
    public void Initialize(Animator characterAnimator, Dictionary<string, string> states)
    {
        animator = characterAnimator;
        animationStates = states;
    }

    // M�todo para reproducir la animaci�n
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
            Debug.LogWarning("La animaci�n no existe en el diccionario: " + animationName);
        }
    }
}

