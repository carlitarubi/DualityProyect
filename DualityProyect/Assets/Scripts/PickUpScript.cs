using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PointsUp(1);
            gameObject.SetActive(false);
        }
    }
    
}
