using Project_PropHunt.Player;
using UnityEngine;

namespace Project_PropHunt
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        private PlayerManager m_playerManager;
        private CharacterController m_characterController;

        [Header("Player values")]
        private float m_gravityValue = -9.81f;
        private float m_playerMovementSpeed = 4f;
        private float m_playerRunSpeed = 6f;
        private float m_playerJumpHeight = 1.5f;
        private Vector3 m_playerVelocity;
        private bool m_isGrounded;

        private void Awake()
        {
            m_playerManager = GetComponent<PlayerManager>();
            m_characterController = GetComponent<CharacterController>();
        }

        public void HandleAllMovements()
        {
            HandleAllMovement();
        }

        private void HandleAllMovement()
        {
            Debug.Log(m_characterController.isGrounded);

            m_isGrounded = m_characterController.isGrounded;
            
            if(m_isGrounded && m_playerVelocity.y < 0f)
            {
                m_playerVelocity.y = 0f;
            }

            Vector3 moveVector = transform.right * m_playerManager.horizontalMovement;
            moveVector += transform.forward * m_playerManager.verticalMovement;
            moveVector.y = 0f;

            if (moveVector.magnitude != 0f)
            {
                if (m_playerManager.isSprinting)
                {
                    m_characterController.Move(Time.deltaTime * m_playerRunSpeed * moveVector);
                }
                else if (!m_playerManager.isSprinting)
                {
                    m_characterController.Move(Time.deltaTime * m_playerMovementSpeed * moveVector);
                }
            }

            if(m_playerManager.isJumping && m_isGrounded)
            {
                m_playerVelocity.y += Mathf.Sqrt(m_playerJumpHeight * -2f * m_gravityValue);
            }

            m_playerVelocity.y += m_gravityValue * Time.deltaTime;
            m_characterController.Move(m_playerVelocity * Time.deltaTime);
        }
    }
}