﻿using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 jump;
    public float jumpForce = 2.0f;
    private bool isGrounded = true;

    public float movementSpeed = 10;
    public float turningSpeed = 60;
    [SerializeField]
    private string jumpKeyBind = "Jump";

    void Start() {
        jump = new Vector3(0.0f, jumpForce, 0.0f);
    }

    void Update()
    {

        if (Input.GetButtonDown(jumpKeyBind) && isGrounded)
        {
            //transform.Translate(Vector3.up * 100 * Time.deltaTime, Space.World);
            isGrounded = false;
            gameObject.GetComponent<Rigidbody>().AddForce(jump , ForceMode.Impulse);

        }
    }

    void OnCollisionEnter()
    {
        isGrounded = true;
    }

}
