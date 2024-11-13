using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MovementController movementController;
    private AnimationManager animationManager;

    private Dictionary<string, string> playerAnimations = new Dictionary<string, string>
    {
        {"Idle", "isIdle"},
        {"Run", "isRunning"},
        {"Jump", "isJumping"},
        {"Attack", "isAttacking"}
    };

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



    void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    private void Start()
    {
        // Inicializar AnimationManager con el Animator del enemigo y sus animaciones
        animationManager = GetComponent<AnimationManager>();
        Animator playerAnimator = GetComponent<Animator>();
        animationManager.Initialize(playerAnimator, playerAnimations);
    }



    // Update is called once per frame
    void Update()
    {
        foreach (var entry in keyDirectionMap)
        {
            if (Input.GetKey(entry.Key))
            {
                movementController.SetDirection(entry.Value);
                break;
            }
        }
    }
}
