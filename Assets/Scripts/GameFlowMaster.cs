using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFlowMaster : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI pointsTextEndGame;
    [SerializeField] TextMeshProUGUI turnsText;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] int playerMovesThisLevel = 10;

    TileHolder tileHolder;

    public float points;

    bool gameStarted;

    Vector2 initialMousePos;
    Vector2 releasedMousePos;

    SingleTile tileA;
    SingleTile tileB;


    void Start()
    {
        points = 0;
        Waiting();
        gameStarted = true;
        tileHolder = FindObjectOfType<TileHolder>();
        //pointsText.text = points.ToString();
        UpdatePointsUI();
    }

    void Update()
    {
        if (!gameStarted) return;

        if(playerMovesThisLevel <= 0)
        {
            //Display game over text and score
            gameOverCanvas.SetActive(true);
            UpdatePointsUI();

        }

        //Debug.Log("Game is running");
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.GetRayIntersection(ray);

            initialMousePos = hit.point;

            if(hit)
            {
                //Debug.Log("Clicked down on: " + hit.transform.GetComponent<SingleTile>().GetTileType());
                tileA = hit.transform.GetComponent<SingleTile>();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hit = Physics2D.GetRayIntersection(ray);

            releasedMousePos = hit.point;


            if (hit && DistanceCheck() && playerMovesThisLevel > 0)
            {
                //Debug.Log("Clicked down on: " + hit.transform.GetComponent<SingleTile>().GetTileType());

                tileB = hit.transform.GetComponent<SingleTile>();
                if(tileA.GetTileType() != tileB.GetTileType())
                {
                    //Debug.Log("Swap tiles!");
                    playerMovesThisLevel--;
                    SwapTiles(tileA, tileB);
                }
            }
        }
    }

    private void SwapTiles(SingleTile tileA, SingleTile tileB)
    {
        Vector2 tempPosition = tileA.transform.position;
        tileA.transform.position = tileB.transform.position;
        tileB.transform.position = tempPosition;
        CheckForPoints();
    }

    private void CheckForPoints()
    {
        float pointsA = tileA.CheckNeighbors(true);
        float pointsB = tileB.CheckNeighbors(false);

        if (pointsA >= 30)
        {
            points += pointsA;
            tileHolder.RemoveTilesFromList(true);
        }
        else
        {
            tileHolder.ResetTilesInList(true);
        }
        if (pointsB >= 30)
        {
            points += pointsB;
            tileHolder.RemoveTilesFromList(false);
        }
        else
        {
            tileHolder.ResetTilesInList(false);
        }
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        //Debug.Log("Points: " + points);
        pointsText.text = points.ToString();
        pointsTextEndGame.text = points.ToString();
        turnsText.text = playerMovesThisLevel.ToString("D2");
    }

    private bool DistanceCheck()
    {
        float xDist, yDist;
        xDist = Mathf.Abs( releasedMousePos.x - initialMousePos.x);
        yDist = Mathf.Abs(releasedMousePos.y - initialMousePos.y);

        if(xDist <= 1.5 && yDist <= 0.5)
        {
            return true;
        }

        if(yDist <= 1.5 && xDist <= 0.5)
        {
            return true;
        }

        return false;
    }

    IEnumerable Waiting()
    {
        yield return new WaitForSeconds(20f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
