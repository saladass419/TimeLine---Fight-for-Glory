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

    [SerializeField] private GameObject currentChosenHeroModel;
    [SerializeField] private ItemCard currentChosenItemCard;
    [SerializeField] private Tile currentChosenTile;

    [SerializeField] private GridGameUIManager uiManager;

    [SerializeField] private GameObject testPrefab1;

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

        player.AddCardToCollection(CardFactory.CreateCard(0));
        player.AddCardToCollection(CardFactory.CreateCard(0));
        player.AddCardToCollection(CardFactory.CreateCard(1));
        player.AddCardToCollection(CardFactory.CreateCard(1));
        player.AddCardToCollection(CardFactory.CreateCard(1));
        player.AddCardToCollection(CardFactory.CreateCard(1));
        player.AddCardToCollection(CardFactory.CreateCard(2));
        player.AddCardToCollection(CardFactory.CreateCard(3));

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
                    currentChosenHeroModel = hit.transform.gameObject;
                    uiManager.RefreshUI(currentChosenHeroModel.GetComponent<Model>().Hero);
                }
                else if (hit.transform.CompareTag("Tile") && actionType == ActionTypeChosen.MOVE)
                {
                    Tile newTile = hit.transform.GetComponent<Tile>();
                    List<(int, int)> boardPositions = board.LocalHeroPositionsToBoardPositions(currentChosenHeroModel.GetComponent<Model>().Direction, currentChosenHeroModel.GetComponent<Model>().Position, currentChosenHeroModel.GetComponent<Model>().Hero.TilesToMove);
                    if (newTile.Occupied != true && board.ChoosenTileInMovementRange(newTile, boardPositions))
                    {
                        Tile oldTile = board.FindTile(currentChosenHeroModel.GetComponent<Model>().Position.PosX, currentChosenHeroModel.GetComponent<Model>().Position.PosY);
                        board.MoveHeroFromTileToAnother(currentChosenHeroModel, oldTile, newTile);
                        currentChosenHeroModel.transform.position = newTile.transform.position;
                        currentChosenHeroModel.GetComponent<Model>().Position = newTile.Position;
                        currentChosenHeroModel.GetComponent<Model>().RotateModel(RotateDirection.LEFT, 90);
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

                    CreateHeroModelOnPlayGround(heroCard, heroAreaSlots[i]);

                    i++;
                }
            }
            gameState = GameStates.PLAYERTURN;
        }
    }


    private void CreateHeroModelOnPlayGround(HeroCard card, GameObject slotToPlace)
    {
        card.ModelPrefab = testPrefab1;
        GameObject instantiatedHeroPrefab = Instantiate(card.ModelPrefab, slotToPlace.transform.position, Quaternion.identity);
        Model createdHeroModel = instantiatedHeroPrefab.GetComponent<Model>();
        createdHeroModel.Hero = card;
        card.HeroAttributes.HeroAttributesList[HeroAttributeType.MAXHEALTH] = 100f;
        card.CurrentHealth = card.HeroAttributes.HeroAttributesList[HeroAttributeType.MAXHEALTH];
        createdHeroModel.Direction = CardinalDirection.NORTH;
        createdHeroModel.IsEnemy = false;
        createdHeroModel.GetComponentInChildren<HealthBar>().SubscribeToHealthChanged();
    }


    public void MoveHero()
    {
        actionType = ActionTypeChosen.MOVE;
        HighlightPossiblePlaces(currentChosenHeroModel.GetComponent<Model>().Direction, currentChosenHeroModel.GetComponent<Model>().Position, currentChosenHeroModel.GetComponent<Model>().Hero.TilesToMove, moveTileMaterial);
    }

    private void HighlightPossiblePlaces(CardinalDirection direction, (int PosX, int PosY) currentPos, List<(int PosX, int PosY)> Pos, Material changeMaterial)
    {
        foreach((int PosX, int PosY) position in Pos)
        {
            (int PosX, int PosY) boardCoordinates = board.PositionInBoardCoordinate(direction, currentPos, position);
            if(RealTileOnBoard(boardCoordinates))
            {
                board.HighlightTiles(board.TileList[boardCoordinates.PosX, boardCoordinates.PosY], changeMaterial);
            }
        }
    }

    public void Attack()
    {
        actionType = ActionTypeChosen.ATTACK;
        HighlightPossiblePlaces(currentChosenHeroModel.GetComponent<Model>().Direction, currentChosenHeroModel.GetComponent<Model>().Position, currentChosenHeroModel.GetComponent<Model>().Hero.TilesToAttack, attackTileMaterial);
        board.ResetBoardMaterial(tile1Material, tile2Material);
    }


    private bool RealTileOnBoard((int PosX, int PosY) position)
    {
        if((position.PosX > -1 && position.PosX < 8) && (position.PosY > -1 && position.PosY < 8))
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
