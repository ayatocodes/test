using UnityEngine;
using Project_PropHunt.Input;

namespace Project_PropHunt
{
    public class PlayerInputManager : MonoBehaviour
    {
        private GameInputActionsMap m_gameInputActionsMap;

        public Vector2 playerMovementInput;
        public bool isSprinting;
        public bool isCrouching;
        public bool isJumping;
        public bool isInteracting;

        private void OnEnable()
        {
            if(m_gameInputActionsMap == null)
            {
                m_gameInputActionsMap = new GameInputActionsMap();

                AddInputEvents();
            }

            m_gameInputActionsMap.Enable();
        }

        private void AddInputEvents()
        {
            m_gameInputActionsMap.Player.Movement.performed += i => playerMovementInput = i.ReadValue<Vector2>();
            m_gameInputActionsMap.Player.Sprint.performed += i => isSprinting = true;
            m_gameInputActionsMap.Player.Sprint.canceled += i => isSprinting = false;
            m_gameInputActionsMap.Player.Crouch.performed += i => isCrouching = true;
            m_gameInputActionsMap.Player.Crouch.canceled += i => isCrouching = false;
            m_gameInputActionsMap.Player.Jump.performed += i => isJumping = true;
            m_gameInputActionsMap.Player.Jump.canceled += i => isJumping = false; 
            m_gameInputActionsMap.Player.Interact.performed += i => isInteracting = true;
            m_gameInputActionsMap.Player.Interact.canceled += i => isInteracting = false;
        }

        private void OnDisable()
        {
            m_gameInputActionsMap.Disable();
        }
    }
}
