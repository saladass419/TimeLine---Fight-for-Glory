using System;
using System.Collections;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, ENEMYTURN, GAMEOVER }; 

public class GridGameController : MonoBehaviour
{
    private GameStates gameState;
    private int turn;
    [SerializeField] private Board board;
    private Camera boardGameCamera;
    [SerializeField] private HeroCard currentChosenHeroCard;
    private ItemCard currentChosenItemCard;
    private float currentDistanceFromBoard = 0;
    private bool rotatePressed = false;
    private bool antiRotatePressed = false;

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && !rotatePressed)
        {
            Vector3 center = new Vector3(1.12f, 0f, 39.21f);
            Debug.Log(boardGameCamera.transform.position.x);
            Debug.Log(boardGameCamera.transform.position.y);
            Debug.Log(boardGameCamera.transform.position.z);
            Vector3 vectorToCenter = boardGameCamera.transform.position - center;
            Debug.Log(vectorToCenter.x);
            Debug.Log(vectorToCenter.y);
            Debug.Log(vectorToCenter.z);
            vectorToCenter.Normalize();
            Debug.Log("Vector to center:");
            Debug.Log(vectorToCenter.y * 0.3f);
            if(currentDistanceFromBoard > -7.5f)
            {
                boardGameCamera.transform.position = new Vector3(boardGameCamera.transform.position.x, boardGameCamera.transform.position.y + vectorToCenter.y*0.3f, boardGameCamera.transform.position.z + vectorToCenter.z * 0.3f);
                currentDistanceFromBoard -= 0.3f;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && !rotatePressed)
        {
            Vector3 center = new Vector3(1.12f, 0f, 39.21f);
            Vector3 vectorToCenter = boardGameCamera.transform.position - center;
            vectorToCenter.Normalize();
            if (currentDistanceFromBoard < 5)
            {
                boardGameCamera.transform.position = new Vector3(boardGameCamera.transform.position.x, boardGameCamera.transform.position.y - vectorToCenter.y * 0.3f, boardGameCamera.transform.position.z - vectorToCenter.z * 0.3f);
                currentDistanceFromBoard += 0.3f;
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
    }


    private void Awake()
    {
        boardGameCamera = Camera.allCameras[1];
    }

    private void Start()
    {
        board.AddMonsterToTile(currentChosenHeroCard, 0, 0);
    }

    private void RotateCamera()
    {
        boardGameCamera.gameObject.transform.position = new Vector3();
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
