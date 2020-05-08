using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardUpdates : MonoBehaviour
{

    public GameObject gameLogicManager;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public GameObject monster5;
    public GameObject monster6;
    public GameObject monster7;
    public GameObject monster8;
    // Start is called before the first frame update
    void Start()
    {
        //will hold the gameobject positions 
        Transform[] monsterPrefabs = GetComponentsInChildren<Transform>();
        NetworkManagerScript.OnBoardUpdate += updateBoard;
        
    }

    void updateBoard(){
       Monster[] board =  NetworkManagerScript.getBoardState();
       //Update the board
    }

    // Update is called once per frame
    void Update()

    {
        //get the board state and update the players and positions accordingly
        gameLogicManager.GetComponent<GameManager>();

    }
}
