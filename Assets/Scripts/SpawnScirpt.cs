using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScirpt : MonoBehaviour
{
   [SerializeField] TileHolder tileHolder;

    [SerializeField] float spawnTimer = 2f;

    float detectionDistance = 1.1f;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnTimer && CheckCanSpawn())
        {
            //spawn a tile
            //Debug.Log("Spawn a new random tile from the list of tiles.");
            
            SpawnATile();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void SpawnATile()
    {
        SingleTile myTile = tileHolder.GetOneTile();
        Instantiate(myTile.gameObject, transform.position, Quaternion.identity, this.transform);
    }

    private bool CheckCanSpawn()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionDistance);

        if (hit.collider != null)
        {
            return false;
        }
        else return true;
    }
}
