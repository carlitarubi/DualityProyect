using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text pointsText;

    public int currentPoints;
    public int lifePoints;
    public int winPoints = 5;
    public GameObject winGoal;

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
    private void Update()
    {
        if (currentPoints < 0) { currentPoints = 0; }
        
        UIUpdate();
    }

    public void PointsUp(int gain)
    {
        points += gain;
    }

    void UIUpdate()
    {
        pointsText.text = "Medicines " + currentPoints.ToString() + "/" + winPoints.ToString();

    }

}
