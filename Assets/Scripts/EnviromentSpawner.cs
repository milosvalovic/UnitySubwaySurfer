using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawner : MonoBehaviour
{
    
    public GameObject flatRock1;
    public GameObject flatRock2;
    public GameObject grass;
    public GameObject plant;
    float timeElapsed = 0;  
    float spawnCycle = 0.4f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnCycle)
        { 
            GameObject temp;

            int random = Random.Range(0,4);
            if (random == 0)
            {
                temp = flatRock1;
            }
            else if (random == 1)
            {
                temp = flatRock2;
            }
            else if (random == 2)
            {
                temp = grass;
            }
            else if (random == 3)
            {
                temp = plant;
            }
            else 
            {
                temp = plant;
            }

            spawnCycle = Random.Range(0.4f, 1f);

            GameObject spawnObject = (GameObject)Instantiate(temp);
            
            
            Vector3 pos = spawnObject.transform.position;
            spawnObject.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), pos.y, transform.position.z);
            timeElapsed = 0; 
        }
    }

}
