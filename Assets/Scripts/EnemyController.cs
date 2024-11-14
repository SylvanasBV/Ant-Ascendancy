using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AnimationManager animationManager;

    private Dictionary<string, string> playerAnimations = new Dictionary<string, string>
    {
        {"up", "MoveAhead"},
        {"right", "MoveRight"},
        {"down", "MoveBack"},
        {"left", "MoveLeft"}
    };

    public enum EnemyNodeStateEnum
    {
        respawning,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }

    public enum EnemyType
    {
        mantis,
        beetle
    }

    public EnemyNodeStateEnum enemyNodeState;
    public EnemyType enemyType;

    public GameObject enemyNodeLeft;
    public GameObject enemyNodeRight;
    public GameObject enemyNodeCenter;
    public GameObject enemyNodeStart;

    public MovementController movementController;
    public GameObject startingNode;
    public bool readyToLeaveHome = false;
    public GameManager gameManager;
    public bool isFrightened = false;
    public GameObject[] scatterNodes;
    public int scatterNodeIndex;

    void Awake()
    {
        scatterNodeIndex = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementController = GetComponent<MovementController>();
        animationManager = GetComponent<AnimationManager>(); // Asegúrate de que AnimationManager está en el mismo GameObject
        startingNode = enemyNodeStart;

        if (enemyType == EnemyType.mantis)
        {
            enemyNodeState = EnemyNodeStateEnum.leftNode;
            startingNode = enemyNodeLeft;
        }
        else if (enemyType == EnemyType.beetle)
        {
            enemyNodeState = EnemyNodeStateEnum.startNode;
            startingNode = enemyNodeStart;
        }
        movementController.currentNode = startingNode;
        transform.position = startingNode.transform.position;

        // Inicializar el AnimationManager con el animator del enemigo y las animaciones
        Animator animator = GetComponent<Animator>();
        if (animationManager != null && animator != null)
        {
            animationManager.Initialize(animator, playerAnimations);
        }
    }

    void Update()
    {
        if (movementController.currentNode.GetComponent<NodeController>().isSideNode)
        {
            movementController.SetSpeed(1);
        }
        else
        {
            movementController.SetSpeed(3);
        }
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if (enemyNodeState == EnemyNodeStateEnum.movingInNodes)
        {
            if (gameManager.currentEnemyMode == GameManager.EnemyMode.scatter)
            {
                if (transform.position.x == scatterNodes[scatterNodeIndex].transform.position.x && transform.position.y == scatterNodes[scatterNodeIndex].transform.position.y)
                {
                    scatterNodeIndex++;
                    if (scatterNodeIndex == scatterNodes.Length - 1)
                    {
                        scatterNodeIndex = 0;
                    }
                }

                string direction = GetClosestDirection(scatterNodes[scatterNodeIndex].transform.position);
                movementController.SetDirection(direction);
                animationManager.PlayAnimation(direction); // Cambiar animación
            }
            else if (isFrightened)
            {
                // Modo asustado (lógica pendiente de implementar)
            }
            else
            {
                if (enemyType == EnemyType.mantis)
                {
                    DetermineMantisDirection();
                }
                else if (enemyType == EnemyType.beetle)
                {
                    DetermineBeetleDirection();
                }
            }
        }
        else
        {
            if (readyToLeaveHome)
            {
                if (enemyNodeState == EnemyNodeStateEnum.leftNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.centerNode;
                    movementController.SetDirection("right");
                    animationManager.PlayAnimation("right");
                }
                else if (enemyNodeState == EnemyNodeStateEnum.rightNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.centerNode;
                    movementController.SetDirection("left");
                    animationManager.PlayAnimation("left");
                }
                else if (enemyNodeState == EnemyNodeStateEnum.centerNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.startNode;
                    movementController.SetDirection("up");
                    animationManager.PlayAnimation("up");
                }
                else if (enemyNodeState == EnemyNodeStateEnum.startNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.movingInNodes;
                    string initialDirection = (enemyType == EnemyType.mantis) ? "left" : "right";
                    movementController.SetDirection(initialDirection);
                    animationManager.PlayAnimation(initialDirection);
                }
            }
        }
    }

    void DetermineMantisDirection()
    {
        string direction = GetClosestDirection(gameManager.player.transform.position);
        movementController.SetDirection(direction);
        animationManager.PlayAnimation(direction);
    }

    void DetermineBeetleDirection()
    {
        string playersDirection = gameManager.player.GetComponent<MovementController>().lastMovingDirection;
        float distanceBetweenNodes = 2.45f;

        Vector2 target = gameManager.player.transform.position;
        if (playersDirection == "left")
        {
            target.x -= distanceBetweenNodes * 2;
        }
        else if (playersDirection == "right")
        {
            target.x += distanceBetweenNodes * 2;
        }
        else if (playersDirection == "up")
        {
            target.y += distanceBetweenNodes * 2;
        }
        else if (playersDirection == "down")
        {
            target.y -= distanceBetweenNodes * 2;
        }

        string direction = GetClosestDirection(target);
        movementController.SetDirection(direction);
        animationManager.PlayAnimation(direction);
    }

    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        // Revisa cada dirección posible y elige la más cercana
        if (nodeController.canMoveUp && lastMovingDirection != "down")
        {
            GameObject nodeUp = nodeController.nodeUp;
            float distance = Vector2.Distance(nodeUp.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.canMoveDown && lastMovingDirection != "up")
        {
            GameObject nodeDown = nodeController.nodeDown;
            float distance = Vector2.Distance(nodeDown.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            GameObject nodeLeft = nodeController.nodeLeft;
            float distance = Vector2.Distance(nodeLeft.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (nodeController.canMoveRight && lastMovingDirection != "left")
        {
            GameObject nodeRight = nodeController.nodeRight;
            float distance = Vector2.Distance(nodeRight.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        return newDirection;
    }
}