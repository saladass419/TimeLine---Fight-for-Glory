using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, ENEMYTURN, GAMEOVER };
public enum ActionTypeChosen { NONE, MOVE, ATTACK, SPECIALABILITY1, SPECIALABILITY2 };


public class GridGameController : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] Material attackTileMaterial;
    [SerializeField] Material moveTileMaterial;
    [SerializeField] Material tile1Material;
    [SerializeField] Material tile2Material;

    [SerializeField] private HeroCard currentChosenHeroCard;
    [SerializeField] private GameObject currentChosenHeroModel;
    [SerializeField] private ItemCard currentChosenItemCard;
    [SerializeField] private Tile currentChosenTile;

    [SerializeField] private GridGameUIManager uiManager;

    private GameStates gameState;
    private ActionTypeChosen actionType = ActionTypeChosen.NONE;
    private int turn;
    private Camera boardGameCamera;
   
    
    private float currentDistanceFromBoard = 0;
    private bool rotatePressed = false;
    private bool antiRotatePressed = false;


    private void Awake()
    {
        boardGameCamera = Camera.allCameras[1];
    }


    private void Start()
    {
        currentChosenHeroCard.TilesToMove.Add((1, 1));
        currentChosenHeroCard.TilesToMove.Add((0, 0));
        currentChosenHeroCard.TilesToMove.Add((2, 3));
        currentChosenHeroCard.TileToAttack.Add((2, 3));
        currentChosenHeroCard.TileToAttack.Add((7, 7));
        string json = JsonUtility.ToJson(currentChosenHeroCard);
        File.WriteAllText(@"C:\Users\SteveP1\Desktop\json", json);
        board.AddMonsterToTile(currentChosenHeroCard, 0, 0);
    }



    private void Update()
    {
        Vector3 center = new Vector3(1.12f, 0f, 39.21f);
        Vector3 vectorToCenter = boardGameCamera.transform.position - center;
        vectorToCenter.Normalize();
        
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (currentDistanceFromBoard > -5f)
            {
                boardGameCamera.transform.position = new Vector3(boardGameCamera.transform.position.x + vectorToCenter.x * 0.7f, boardGameCamera.transform.position.y + vectorToCenter.y* 0.7f, boardGameCamera.transform.position.z + vectorToCenter.z * 0.7f);
                currentDistanceFromBoard -= 0.7f;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (currentDistanceFromBoard < 10.0f)
            {
                boardGameCamera.transform.position = new Vector3(boardGameCamera.transform.position.x - vectorToCenter.x * 0.7f, boardGameCamera.transform.position.y - vectorToCenter.y * 0.7f, boardGameCamera.transform.position.z - vectorToCenter.z * 0.7f);
                currentDistanceFromBoard += 0.7f;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rotatePressed = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rotatePressed = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            antiRotatePressed = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            antiRotatePressed = false;
        }

        if (rotatePressed)
        {
            boardGameCamera.transform.RotateAround(new Vector3(1.12f, 0f, 39.21f), Vector3.up, 30 * Time.deltaTime);
        }

        if(antiRotatePressed)
        {
            boardGameCamera.transform.RotateAround(new Vector3(1.12f, 0f, 39.21f), Vector3.up, 30 * -Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.allCameras[1].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "TileObject")
                {
                    Debug.Log("Gotcha bitch");
                }
                else if(hit.transform.tag == "Tile" && actionType == ActionTypeChosen.MOVE)
                {
                    Tile newTile = hit.transform.GetComponent<Tile>();
                    Tile oldTile = board.findTile(currentChosenHeroCard.InstantiatedModel.GetComponent<Model>().Position.PosX, currentChosenHeroCard.InstantiatedModel.GetComponent<Model>().Position.PosY);
                    board.MoveHeroFromTileToAnother(currentChosenHeroCard, oldTile, newTile);
                    actionType = ActionTypeChosen.NONE;
                    board.ResetBoardMaterial(tile1Material, tile2Material);
                }
            }
        }
    }

    public void MoveHero()
    {
        actionType = ActionTypeChosen.MOVE;
        HighlightPossiblePlaces(currentChosenHeroCard.Position, currentChosenHeroCard.TilesToMove, moveTileMaterial);
    }

    private void HighlightPossiblePlaces((int PosX, int PosY) currentPos, List<(int PosX, int PosY)> Pos, Material changeMaterial)
    {
        foreach((int PosX, int PosY) position in Pos)
        {
            int boardCoordinatePositionX = currentPos.PosX + position.PosX;
            int boardCoordinatePositionY = currentPos.PosY + position.PosY;
            if(RealTileOnBoard(boardCoordinatePositionX, boardCoordinatePositionY))
            {
                board.HighlightTiles(board.TileList[boardCoordinatePositionX, boardCoordinatePositionY], changeMaterial);
            }
        }
    }

    public void Attack()
    {
        actionType = ActionTypeChosen.ATTACK;
        HighlightPossiblePlaces(currentChosenHeroCard.Position, currentChosenHeroCard.TileToAttack, attackTileMaterial);
        board.ResetBoardMaterial(tile1Material, tile2Material);
    }


    private bool RealTileOnBoard(int x, int y)
    {
        if((x > -1 && x < 8) && (y > -1 && y < 8))
        {
            return true;
        }
        return false;
    }


    private void RestrictMovement()
    {

    }

    private void StartGame()
    {

    }

    private void EnableMovement()
    {

    }

    private void ResetHeroActionPoints()
    {

    }
  
    private void EndGame()
    {

    }

}
