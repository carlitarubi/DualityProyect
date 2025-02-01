using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public float raiseY = 0.2f;
    public float speed = 1.5f;
    Vector3 posicion;

    private void Start()
    {
        posicion = transform.localPosition;
    }
    void Update()
    {
        transform.position = new Vector3(posicion.x, posicion.y + raiseY * Mathf.Sin(Time.time * speed), posicion.z);
    }
}
