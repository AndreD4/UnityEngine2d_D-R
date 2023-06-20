using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRb;



    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRb.velocity = new Vector2 (moveSpeed, 0f);
    }
}
