using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolder : MonoBehaviour
{
    [SerializeField] List<SingleTile> tiles;

    public List<SingleTile> tileListA;
    public List<SingleTile> tileListB;

    private void Start()
    {
        if (tiles == null)
            tiles = new List<SingleTile>();
    }

    public SingleTile GetOneTile()
    {
        int length = tiles.Count;
        int num = Random.Range(0, length);

        SingleTile singleTile = tiles[num];
        return singleTile;
    }

    public void RemoveTilesFromList(bool firstList)
    {
        if (firstList)
        {
            foreach (SingleTile tile in tileListA)
            {
                Destroy(tile.gameObject);
            }
            tileListA.Clear();
        }
        else
        {
            foreach (SingleTile tile in tileListB)
            {
                Destroy(tile.gameObject);
            }
            tileListB.Clear();

        }
    }

    public void ResetTilesInList(bool firstList)
    {
        if (firstList)
        {
            foreach (SingleTile tile in tileListA)
            {
                tile.SetWasCounted(false);
            }
            tileListA.Clear();
        }
        else
        {
            foreach (SingleTile tile in tileListB)
            {
                tile.SetWasCounted(false);
            }
            tileListB.Clear();
        }
    }
}