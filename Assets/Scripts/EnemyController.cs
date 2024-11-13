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

    void Awake()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if (enemyNodeState == EnemyNodeStateEnum.movingInNodes)
        {

        }
        else if (enemyNodeState == EnemyNodeStateEnum.respawning)
        {

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
                    movementController.SetDirection("left");
                }
            }
        }
    }

    void DetermineMantisDirection()
    {

    }

    void DetermineBeetleDirection()
    {

    }

    void DetermineOtherDirection()
    {

    }

    void DetermineAnotherDirection()
    {

    }
}
