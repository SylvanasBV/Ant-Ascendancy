using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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



    void Awake()
    {
        movementController = GetComponent<MovementController>();
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
