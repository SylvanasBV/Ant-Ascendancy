using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private bool canMoveLeft = false;
    [SerializeField] private bool canMoveRight = false;
    [SerializeField] private bool canMoveUp = false;
    [SerializeField] private bool canMoveDown = false;

    [SerializeField] private GameObject nodeLeft;
    [SerializeField] private GameObject nodeRight;
    [SerializeField] private GameObject nodeUp;
    [SerializeField] private GameObject nodeDown;


    public bool isWrapRightNode = false;
    public bool isWrapLeftNode = false;

    // If the node contains a pellet when the game starts 
    public bool isPelletNode = false;
    // If the node still has a pellet
    public bool hasPellet = false;
    //Distance

    Animator animate;

    void Awake()
    {
        if (transform.childCount > 0)
        {
            hasPellet = true;
            isPelletNode = true;
            animate = GetComponent<Animator>();
        }

        hitsDown();
        hitsUp();
        hitsRight();
        hitsLeft();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Allows to check the raycast down direction
    public void hitsDown()
    {
        RaycastHit2D[] hitsDown;
        // Shoot raycast (line) going down
        hitsDown = Physics2D.RaycastAll(transform.position, -Vector2.up);

        // Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);
            // Hay que modificar la distancia dependiendo de que distancia estan los nodos dentro del juego
            if (distance < 0.4f && hitsDown[i].collider.tag == "Node")
            {
                canMoveDown = true;
                nodeDown = hitsDown[i].collider.gameObject;
            }
        }
    }

    // Allows to check the raycast up direction
    public void hitsUp()
    {
        RaycastHit2D[] hitsUp;
        // Shoot raycast (line) going up
        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        // Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);
            // Hay que modificar la distancia dependiendo de que distancia estan los nodos dentro del juego
            if (distance < 0.4f && hitsUp[i].collider.tag == "Node")
            {
                canMoveUp = true;
                nodeUp = hitsUp[i].collider.gameObject;
            }
        }
    }

    // Allows to check the raycast right direction
    public void hitsRight()
    {
        RaycastHit2D[] hitsRight;
        // Shoot raycast (line) going right
        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        // Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);
            // Hay que modificar la distancia dependiendo de que distancia estan los nodos dentro del juego
            if (distance < 0.4f && hitsRight[i].collider.tag == "Node")
            {
                canMoveRight = true;
                nodeRight = hitsRight[i].collider.gameObject;
            }
        }
    }

    // Allows to check the raycast left direction
    public void hitsLeft()
    {
        RaycastHit2D[] hitsLeft;
        // Shoot raycast (line) going left
        hitsLeft = Physics2D.RaycastAll(transform.position, -Vector2.right);

        // Loop through all of the gameobjects that the raycast hits
        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);
            // Hay que modificar la distancia dependiendo de que distancia estan los nodos dentro del juego
            if (distance < 0.4f && hitsLeft[i].collider.tag == "Node")
            {
                canMoveLeft = true;
                nodeLeft = hitsLeft[i].collider.gameObject;
            }
        }
    }

    public GameObject getNodeFromDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        }
        else if (direction == "right" && canMoveRight)
        {
            return nodeRight;
        }
        else if (direction == "up" && canMoveUp)
        {
            return nodeUp;
        }
        else if (direction == "down" && canMoveDown)
        {
            return nodeDown;
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");

        if (collision.tag == "Player" && isPelletNode)
        {
            hasPellet = false;
            animate.SetBool("DestroyObject", true);
        }
    }
}
