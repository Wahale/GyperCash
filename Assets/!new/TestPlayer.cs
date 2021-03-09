using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;
    public float speedPlayer = 5f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * speedPlayer;
        moveVector.z = Input.GetAxis("Vertical") * speedPlayer;

        characterController.Move(moveVector * Time.deltaTime);
    }
}
