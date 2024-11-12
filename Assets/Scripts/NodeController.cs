using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    void Start()
    {
        RaycastHit2D[] hitsDown;
        // Shoot raycast (line) going down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);

        // Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            // Hay que modificar la distancia dependiendo de que distancia estan los nodos dentro del juego
            if (distance < 0.4f)
            {
                canMoveDown = true;
                nodeDown = hitsDown[i].collider.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
