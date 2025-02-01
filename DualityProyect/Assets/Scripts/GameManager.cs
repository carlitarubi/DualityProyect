using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    { get
        {
            if (_instance == null)
            {
                Debug.Log("Game Manager is null");
            }
            return _instance;
        } 
    }

    //Variables
    public int points;
    public int Winpoints;


    private void Awake()
    {
        _instance = this;
        
    }

    public void PointsUp(int gain)
    {
        points += gain;
    }

}
