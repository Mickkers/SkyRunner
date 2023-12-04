using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.GameplayActions gameplay;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        gameplay = playerInput.Gameplay;
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

        gameplay.Jump.performed += ctx => playerMovement.Jump();
        gameplay.Attack.performed += ctx => playerAttack.Attack();
        gameplay.Reload.performed += ctx => playerAttack.Reload();
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Movement(gameplay.Move.ReadValue<float>());
    }
}
