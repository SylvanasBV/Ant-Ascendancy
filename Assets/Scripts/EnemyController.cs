using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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
    }

    // Update is called once per frame
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
            // Scatter mode
            if (gameManager.currentEnemyMode == GameManager.EnemyMode.scatter)
            {

                // If we reached the scatter node, add one to the scatter node index
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

            }
            else if (isFrightened)
            {

            }

            // Chase mode
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
            // If enemy is ready to leave home
            if (readyToLeaveHome)
            {
                // If enemy is in the left home node, move to the center
                if (enemyNodeState == EnemyNodeStateEnum.leftNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.centerNode;
                    movementController.SetDirection("right");
                }
                // If enemy is in the left home node, move to the center
                else if (enemyNodeState == EnemyNodeStateEnum.rightNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.centerNode;
                    movementController.SetDirection("left");
                }
                // If enemy is in the center home node, move to the start
                else if (enemyNodeState == EnemyNodeStateEnum.centerNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.startNode;
                    movementController.SetDirection("up");
                }
                // If enemy is in the startNode home node, move around in the game
                else if (enemyNodeState == EnemyNodeStateEnum.startNode)
                {
                    enemyNodeState = EnemyNodeStateEnum.movingInNodes;
                    // Depending on the enemy it will go either left or right
                    if (enemyType == EnemyType.mantis)
                    {
                        movementController.SetDirection("left");
                    }
                    else
                    {
                        movementController.SetDirection("right");
                    }
                }
            }
        }
    }

    void DetermineMantisDirection()
    {
        string direction = GetClosestDirection(gameManager.player.transform.position);
        movementController.SetDirection(direction);
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

    }



    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementController.lastMovingDirection;
        string newDirection = "";

        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        // If we can move up and we aren't reversing
        if (nodeController.canMoveUp && lastMovingDirection != "down")
        {
            // Get the node above
            GameObject nodeUp = nodeController.nodeUp;
            // Get the distance between the top node and the player
            float distance = Vector2.Distance(nodeUp.transform.position, target);

            // If this is the shortest distance, set our direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }


        // If we can move down and we aren't reversing
        if (nodeController.canMoveDown && lastMovingDirection != "up")
        {
            // Get the node below
            GameObject nodeDown = nodeController.nodeDown;
            // Get the distance between the bottom node and the player
            float distance = Vector2.Distance(nodeDown.transform.position, target);

            // If this is the shortest distance, set our direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        // If we can move left and we aren't reversing
        if (nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            // Get the left node 
            GameObject nodeLeft = nodeController.nodeLeft;
            // Get the distance between the left node and the player
            float distance = Vector2.Distance(nodeLeft.transform.position, target);

            // If this is the shortest distance, set our direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        // If we can move right and we aren't reversing
        if (nodeController.canMoveRight && lastMovingDirection != "left")
        {
            // Get the right node 
            GameObject nodeRight = nodeController.nodeRight;
            // Get the distance between the left node and the player
            float distance = Vector2.Distance(nodeRight.transform.position, target);

            // If this is the shortest distance, set our direction
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        return newDirection;
    }

}
