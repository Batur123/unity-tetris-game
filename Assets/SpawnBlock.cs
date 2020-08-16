using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{

    public GameObject[] BlockGroups;


    public void SpawnBlocks()
    {
        int i = Random.Range(0, BlockGroups.Length);

        Instantiate(BlockGroups[i], transform.position,Quaternion.identity);

    }

    void Start()
    {
        SpawnBlocks();
    }


    void Update()
    {
        
    }
}
