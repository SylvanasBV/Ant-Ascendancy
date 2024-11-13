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

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        startingNode = enemyNodeStart;

        if (enemyType == EnemyType.mantis)
        {
            enemyNodeState = EnemyNodeStateEnum.centerNode;
        }
        else if (enemyType == EnemyType.beetle)
        {
            enemyNodeState = EnemyNodeStateEnum.startNode;
            startingNode = enemyNodeStart;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
