﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject rock;
    public GameObject ufo;
    float timeElapsed = 0;
    float spawnCycle = 0.5f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnCycle)
        {
            GameObject temp;
            for (int i = 0; i <= 2; i++)
            {
                int random = Random.Range(0, 4);
                if (random == 0)
                {
                    temp = rock;
                }
                else if (random == 1)
                {
                    temp = ufo;
                }
                else
                {
                    temp = new GameObject();
                }

                

                GameObject spawnObject = (GameObject)Instantiate(temp);


                Vector3 pos = spawnObject.transform.position;
                float posX = -2.5f;
                if (i == 0)
                {
                    posX = -2.5f;
                }
                else if (i == 1)
                {
                    posX = 0;
                }
                else
                {
                    posX = 2.5f;
                }

                spawnObject.transform.position = new Vector3(posX, pos.y, transform.position.z);
            }
            spawnCycle = Random.Range(0.5f, 2f);
            timeElapsed = 0;
        }
    }
}