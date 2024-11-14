
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public GameObject currentNode;
    [SerializeField] float speed = 4.0f;

    [SerializeField] string direction = "";
    public string lastMovingDirection = "";

    public bool canWrap = true;

    public bool isEnemy = false;


    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        NodeController currentNodeController = currentNode.GetComponent<NodeController>();

        MoveTowardsCurrentNode();

        if (IsAtCenterOfNode() || IsReversingDirection())
        {
            if (isEnemy)
            {
                GetComponent<EnemyController>().ReachedCenterOfNode(currentNodeController);
            }

            HandleWrappingOrMovement(currentNodeController);
        }
        else
        {
            canWrap = true;
        }
    }

    // Moves object to the current node
    private void MoveTowardsCurrentNode()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);
    }

    // Checks if object is at the center of node
    private bool IsAtCenterOfNode()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        float nodeX = currentNode.transform.position.x;
        float nodeY = currentNode.transform.position.y;

        return currentX == nodeX && currentY == nodeY;
    }

    // Checks if current direction equals the oposite to the last movement direction 
    private bool IsReversingDirection()
    {
        return (direction == "left" && lastMovingDirection == "right")
            || (direction == "right" && lastMovingDirection == "left")
            || (direction == "up" && lastMovingDirection == "down")
            || (direction == "down" && lastMovingDirection == "up");
    }

    // Attempt to move the object to the next node in the desired direction
    private void TryMoveToNextNode(NodeController currentNodeController)
    {
        GameObject newNode = currentNodeController.getNodeFromDirection(direction);

        if (newNode != null)
        {
            currentNode = newNode;
            lastMovingDirection = direction;
        }
        else
        {
            direction = lastMovingDirection;
            newNode = currentNodeController.getNodeFromDirection(direction);
            if (newNode != null)
            {
                currentNode = newNode;
            }
        }
    }


    private void HandleWrappingOrMovement(NodeController currentNodeController)
    {
        if (ShouldWrapLeft(currentNodeController))
        {
            WrapToRight();
        }
        else if (ShouldWrapRight(currentNodeController))
        {
            WrapToLeft();
        }
        else
        {
            TryMoveToNextNode(currentNodeController);
        }
    }

    private bool ShouldWrapLeft(NodeController currentNodeController)
    {
        return currentNodeController.isWrapLeftNode && canWrap;
    }

    private bool ShouldWrapRight(NodeController currentNodeController)
    {
        return currentNodeController.isWrapRightNode && canWrap;
    }

    // If wereached the center of the left wrap, warp the the right warp
    private void WrapToRight()
    {
        currentNode = gameManager.rightWrapNode;
        direction = "left";
        lastMovingDirection = "left";
        transform.position = currentNode.transform.position;
        canWrap = false;
    }

    // If wereached the center of the right wrap, warp the the left warp
    private void WrapToLeft()
    {
        currentNode = gameManager.leftWrapNode;
        direction = "right";
        lastMovingDirection = "right";
        transform.position = currentNode.transform.position;
        canWrap = false;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
    public bool CanMoveInDirection(string newDirection)
    {
        NodeController currentNodeController = currentNode.GetComponent<NodeController>();
        GameObject newNode = currentNodeController.getNodeFromDirection(newDirection);
        return newNode != null;
    }
}
