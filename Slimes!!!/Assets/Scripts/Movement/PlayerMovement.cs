using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_MoveSpeed = 7.5f;

    [SerializeField]
    float m_SmoothInputSpeed = 0.2f;

    [SerializeField]
    float m_JumpHeight = 5f;

    [SerializeField]
    Rigidbody2D m_MainRB;

    [SerializeField]
    BoxCollider2D m_FeetCollider;

    private float m_MoveInput;
    private float m_CurrentMoveInput;
    private float m_SmoothInputVelocity;
    private PlayerInput m_PlayerInput;
    private PlayerInputActions m_PlayerInputActions;
    private PlayerJumpChecker m_PlayerJumpChecker;

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerInputActions = new PlayerInputActions();
        m_PlayerInputActions.Player.Enable();     // Only want to enable the Player action map

        m_PlayerJumpChecker = new PlayerJumpChecker();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (m_PlayerJumpChecker.CanJump(m_FeetCollider))
            {
                m_MainRB.AddForce(Vector3.up * m_JumpHeight, ForceMode2D.Impulse);
            }
        }
    }

    private void MovePlayer()
    {
        m_MoveInput = m_PlayerInputActions.Player.Move.ReadValue<float>();
        m_CurrentMoveInput = Mathf.SmoothDamp(m_CurrentMoveInput, m_MoveInput, ref m_SmoothInputVelocity, m_SmoothInputSpeed);

        m_MainRB.velocity = new Vector2(Mathf.Clamp(m_CurrentMoveInput * m_MoveSpeed, -1 * m_MoveSpeed, m_MoveSpeed), m_MainRB.velocity.y);
    }
}
