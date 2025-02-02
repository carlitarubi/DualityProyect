using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    public int winpoints = 5;
    public TMP_Text pointsText;

    private void Awake()
    {
        _instance = this;
        

    }


    public void PointsUp(int gain)
    {
        points += gain;
        pointsText.text = "Medicines " + points.ToString() + "/" + winpoints.ToString();
    }

   


}
