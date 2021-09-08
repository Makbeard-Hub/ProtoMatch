using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Apple,
    Lemon,
    Cherry,
    Strawberry
}

public class SingleTile : MonoBehaviour, ITile
{
    [SerializeField] TileType tileType;

    [SerializeField] float slideDownTimer = 1f;
    [SerializeField][Range(0,10)] float pointValue = 10f;

    //List<GameObject> tilesCounted = new List<GameObject>();
    TileHolder holder;

    float detectionDistance = 1.1f;
    float timer = 0;

    bool wasCounted = false;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        holder = FindObjectOfType<TileHolder>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        //check for tile below
        if(timer >= slideDownTimer)
        {
            //Debug.Log("Time to check below");
            CheckBelow();
            timer = 0;
        }
        // if empty space, move down. Else dont move
    }

    void CheckBelow()
    {
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, detectionDistance);
        
        if(hit.collider == null)
        {
            MoveOnce();
        }
    }

    void MoveOnce()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 1);
    }

    public float CheckNeighbors(bool firstList)
    {
        float newPoints = 0;
        newPoints += pointValue;
        wasCounted = true;
        //tilesCounted.Add(gameObject);
        if (firstList)
        {
            holder.tileListA.Add(this);
        }
        else
        {
            holder.tileListB.Add(this);
        }

        //Check Up
        newPoints += CheckDirection(Vector2.up, firstList);

        //Check Left
        newPoints += CheckDirection(Vector2.left, firstList);

        //Check Down
        newPoints += CheckDirection(Vector2.down, firstList);

        //Check Right
        newPoints += CheckDirection(Vector2.right, firstList);

        return newPoints;
    }

    private float CheckDirection(Vector2 direction, bool firstList)
    {
        float tempPoints = 0;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance);
        if (hit == false || hit.transform.GetComponent<SingleTile>() == null) return 0;
        SingleTile myTile = hit.transform.GetComponent<SingleTile>();
        if (myTile.GetTileType() == tileType && !myTile.GetWasCounted())
        {
            tempPoints += hit.transform.GetComponent<SingleTile>().CheckNeighbors(firstList);
        }
        return tempPoints;
    }

    public bool GetWasCounted()
    {
        return wasCounted;
    }

    public void SetWasCounted(bool state)
    {
        wasCounted = state;
    }

    public TileType GetTileType()
    {
        return tileType;
    }
}