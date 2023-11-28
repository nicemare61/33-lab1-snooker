using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    //                            int point
    private void OnTriggerEnter(Collider other)
    {
       Ball b = other.gameObject.GetComponent<Ball>();

       if (b != null) //ถ้า B มีอยู่จริง
       {
           GameManager.instance.PlayerScore += b.Point; //+ b.point เข้าไปใน PlayerScore
           GameManager.instance.UpdateScoreText();
           Destroy(b.gameObject);
       }
    }
}
