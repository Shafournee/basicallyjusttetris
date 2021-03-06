﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Platformer : MonoBehaviourPun
{
    enum Direction { left, right, none }

    Direction dir;

    Rigidbody2D rigidBody;

    float speed = 5f;

    bool isGrounded;
    bool recentlyJumped;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetKey(KeyCode.A))
        {
            dir = Direction.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = Direction.right;
        }
        else
        {
            dir = Direction.none;
        }

        if (Physics2D.Raycast(new Vector2(transform.position.x, GetComponent<BoxCollider2D>().bounds.min.y - .01f), Vector2.down, .1f))
        {
            if(!recentlyJumped)
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        if (dir == Direction.right)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else if(dir == Direction.left)
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
        }
        else if(isGrounded)
        {
            rigidBody.velocity = new Vector2(0f, 0f);
        }
        else
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(JumpingCoroutine());
        }
    }

    IEnumerator JumpingCoroutine()
    {
        recentlyJumped = true;
        rigidBody.AddForce(new Vector2(0f, 300f));
        yield return new WaitForSeconds(.8f);
        recentlyJumped = false;
    }
}
