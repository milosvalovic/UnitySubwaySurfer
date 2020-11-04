using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoroundEnviromentSpawner : MonoBehaviour
{
    
    public GameObject mountain;
    public GameObject rock;
    float timeElapsed = 0;  
    float spawnCycle = 0.5f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnCycle)
        { 
            GameObject temp;

            int random = Random.Range(0,2);
            if (random == 0)
            {
                temp = mountain;
            }
            else 
            {
                temp = rock;
            }
            int objectZ = Random.Range(-1, 1);

            spawnCycle = Random.Range(0.25f, 2f);

            GameObject spawnObject = (GameObject)Instantiate(temp);
            
            
            Vector3 pos = spawnObject.transform.position;
            spawnObject.transform.position = new Vector3(objectZ < 0? 13*-1 : 13, pos.y, transform.position.z);
            timeElapsed = 0; 
        }
    }

}
