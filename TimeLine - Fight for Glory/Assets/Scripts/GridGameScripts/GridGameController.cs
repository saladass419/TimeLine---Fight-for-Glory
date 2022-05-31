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
    [SerializeField] private Material attackTileMaterial;
    [SerializeField] private Material moveTileMaterial;
    [SerializeField] private Material tile1Material;
    [SerializeField] private Material tile2Material;

    [SerializeField] private HeroCard currentChosenHeroCard;
    [SerializeField] private GameObject currentChosenHeroModel;
    [SerializeField] private ItemCard currentChosenItemCard;
    [SerializeField] private Tile currentChosenTile;

    [SerializeField] private GridGameUIManager uiManager;

    [SerializeField] private GameObject testPrefab1;
    [SerializeField] private GameObject testPrefab2;

    [SerializeField] private Profile player;

    private GameStates gameState;
    private ActionTypeChosen actionType = ActionTypeChosen.NONE;
    private int turn;
    private Camera cam;
   
    
    private float currentDistanceFromBoard = 0;
    private bool rotatePressed = false;
    private bool antiRotatePressed = false;


    private void Awake()
    {
       cam = Camera.main;
    }
    private void Start()
    {
        //string json = JsonUtility.ToJson(currentChosenHeroCard);
        //File.WriteAllText(@"C:\Users\SteveP1\Desktop\json", json);

        player.AddCard(CardFactory.CreateCard(0));
        player.AddCard(CardFactory.CreateCard(0));
        player.AddCard(CardFactory.CreateCard(1));
        player.AddCard(CardFactory.CreateCard(1));
        player.AddCard(CardFactory.CreateCard(1));
        player.AddCard(CardFactory.CreateCard(1));
        player.AddCard(CardFactory.CreateCard(2));
        player.AddCard(CardFactory.CreateCard(3));

        gameState = GameStates.START;
        SetUpGame();
    }
    private void Update()
    {
        Vector3 center = new Vector3(1.12f, 0f, 39.21f);
        Vector3 vectorToCenter = cam.transform.position - center;
        vectorToCenter.Normalize();
        
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (currentDistanceFromBoard > -5f)
            {
               cam.transform.position = new Vector3(cam.transform.position.x + vectorToCenter.x * 0.7f, cam.transform.position.y + vectorToCenter.y* 0.7f, cam.transform.position.z + vectorToCenter.z * 0.7f);
                currentDistanceFromBoard -= 0.7f;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (currentDistanceFromBoard < 10.0f)
            {
               cam.transform.position = new Vector3(cam.transform.position.x - vectorToCenter.x * 0.7f, cam.transform.position.y - vectorToCenter.y * 0.7f, cam.transform.position.z - vectorToCenter.z * 0.7f);
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
            cam.transform.RotateAround(new Vector3(1.12f, 0f, 39.21f), Vector3.up, 30 * Time.deltaTime);
        }

        if(antiRotatePressed)
        {
            cam.transform.RotateAround(new Vector3(1.12f, 0f, 39.21f), Vector3.up, 30 * -Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("HeroOnTile"))
                {
                    currentChosenHeroCard = hit.transform.GetComponent<Model>().Hero;
                    currentChosenHeroModel = hit.transform.gameObject;
                    uiManager.RefreshUI(currentChosenHeroCard);
                }
                else if (hit.transform.CompareTag("Tile") && actionType == ActionTypeChosen.MOVE)
                {
                    Tile newTile = hit.transform.GetComponent<Tile>();
                    if (newTile.Occupied != true)
                    {
                        Tile oldTile = board.findTile(currentChosenHeroCard.InstantiatedModel.GetComponent<Model>().Position.PosX, currentChosenHeroCard.InstantiatedModel.GetComponent<Model>().Position.PosY);
                        board.MoveHeroFromTileToAnother(currentChosenHeroCard, oldTile, newTile);
                        actionType = ActionTypeChosen.NONE;
                        board.ResetBoardMaterial(tile1Material, tile2Material);
                    }
                }
            }
        }
    }

    private void SetUpGame()
    {
        GameObject[] heroAreaSlots = GameObject.FindGameObjectsWithTag("HeroAreaSlot");
        if(gameState == GameStates.START)
        {
            int i = 0;
            foreach (Card card in player.Deck.CardsInDeck)
            {
                if(card.CardType == CardType.HERO)
                {
                    HeroCard heroCard = (HeroCard) card;

                    if(i%2 == 0)
                        heroCard.ModelPrefab = testPrefab1;
                    else
                        heroCard.ModelPrefab = testPrefab2;

                    GameObject instantiatedHeroPrefab = Instantiate(heroCard.ModelPrefab, heroAreaSlots[i].transform.position, Quaternion.identity);
                    instantiatedHeroPrefab.AddComponent<DragAndDrop>();
                    instantiatedHeroPrefab.AddComponent<Model>();
                    heroCard.InstantiatedModel = instantiatedHeroPrefab;
                    instantiatedHeroPrefab.GetComponent<Model>().Hero = heroCard;
                    i++;
                }
            }
            gameState = GameStates.PLAYERTURN;
        }
    }



    public void MoveHero()
    {
        actionType = ActionTypeChosen.MOVE;
        Debug.Log(currentChosenHeroCard.TilesToMove);
        Debug.Log(currentChosenHeroCard.Position);
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
        HighlightPossiblePlaces(currentChosenHeroCard.Position, currentChosenHeroCard.TilesToAttack, attackTileMaterial);
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
