using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Animation Index
    private AnimationManager animationManager;
    private Dictionary<string, string> playerAnimations = new Dictionary<string, string>
    {
        {"up", "MoveAhead"},
        {"right", "MoveRight"},
        {"down", "MoveBack"},
        {"left", "MoveLeft"}
    };


    MovementController movementController;

    Dictionary<KeyCode, string> keyDirectionMap = new Dictionary<KeyCode, string>
        {
            { KeyCode.W, "up" },
            { KeyCode.UpArrow, "up" },
            { KeyCode.S, "down" },
            { KeyCode.DownArrow, "down" },
            { KeyCode.A, "left" },
            { KeyCode.LeftArrow, "left" },
            { KeyCode.D, "right" },
            { KeyCode.RightArrow, "right" }
        };

    void Start()
    {
        // Inicializar AnimationManager con el Animator del jugador y sus animaciones
        animationManager = GetComponent<AnimationManager>();
        Animator playerAnimator = GetComponentInChildren<Animator>();
        animationManager.Initialize(playerAnimator, playerAnimations);
    }

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        movementController.lastMovingDirection = "left";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var entry in keyDirectionMap)
        {
            if (Input.GetKey(entry.Key))
            {
                string desiredDirection = entry.Value;

                // Verifica si puede moverse en la dirección deseada antes de cambiar la animación
                if (movementController.CanMoveInDirection(desiredDirection))
                {
                    movementController.SetDirection(desiredDirection);
                    // Activa la animación correspondiente a la dirección
                    animationManager.PlayAnimation(desiredDirection);
                }

                break;
            }
        }
    }
}
