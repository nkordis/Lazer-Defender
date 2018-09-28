using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 10f;
    [NonSerialized] float padding = 1f;
    float minX;
    float maxX;
    float minY;
    float maxY;

    // Use this for initialization
    void Start () {
        setUpMoveBoundaries();
	}

    private void setUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update () {
        Move();
	}

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var playerXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var playerYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector2(playerXPos, playerYPos );
    }
}
