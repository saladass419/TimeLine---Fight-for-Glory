using System;
using System.Collections;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, ENEMYTURN, GAMEOVER }; 

public class GridGameController : MonoBehaviour
{
    private GameStates gameState;
    private int turn;
    private Board board;
    private Camera boardGameCamera;
    private HeroCard currentChosenHeroCard;
    private ItemCard currentChosenItemCard;


    private void Awake()
    {
        boardGameCamera = Camera.allCameras[1];
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
