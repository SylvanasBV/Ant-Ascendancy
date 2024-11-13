using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnNodes : MonoBehaviour
{   

    int numToSpawn =17;
    public float currentSpawnOffset;
    public float spawnOffset =1.0f;
    // Start is called before the first frame update
    void Start()
    {   
     /*  gameObject.name ="Node";
       return;
        if (gameObject.name == "Node")
        {
            currentSpawnOffset=spawnOffset;
           for (int i =0; i<numToSpawn;i++ ) 
           {
            //Clone a new node 
            GameObject clone = Instantiate(gameObject,new Vector3(transform.position.x  , transform.position.y + currentSpawnOffset,0 ), Quaternion.identity);
            currentSpawnOffset += spawnOffset;
           }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.tag == "Node")
        {

            Destroy( collision.gameObject);
        }

    }
}
