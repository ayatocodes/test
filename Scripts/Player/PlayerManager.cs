using UnityEngine;
using Unity.Netcode;
using JetBrains.Annotations;

namespace Project_PropHunt.Player
{
    public class PlayerManager : NetworkBehaviour
    {
        private PlayerInputManager m_inputManager;
        private PlayerLocomotionManager m_locomotionManager;

        [SerializeField] private Transform m_playerDefaultTransform;
        [SerializeField] private Transform m_playerPropTransform;

        public float horizontalMovement;
        public float verticalMovement;
        public bool isSprinting;
        public bool isCrouching;
        public bool isJumping;
        public bool isInteracting;

        private void Awake()
        {
            m_inputManager = GetComponent<PlayerInputManager>();
            m_locomotionManager = GetComponent<PlayerLocomotionManager>();
            m_playerDefaultTransform = transform.GetChild(0);
        }

        private void Update()
        {
            if (!IsOwner) return;

            if(isInteracting)
            {
                ChangeToPropRpc();
            }

            UpdateInputValues();
            m_locomotionManager.HandleAllMovements();
        }
        [Rpc(SendTo.Everyone)]
        private void ChangeToPropRpc()
        {

            m_playerDefaultTransform.gameObject.SetActive(!m_playerDefaultTransform.gameObject.activeSelf);
            m_playerPropTransform.gameObject.SetActive(!m_playerPropTransform.gameObject.activeSelf);
        }
        private void UpdateInputValues()
        {
            horizontalMovement = m_inputManager.playerMovementInput.x;
            verticalMovement = m_inputManager.playerMovementInput.y;
            isCrouching = m_inputManager.isCrouching;
            isJumping = m_inputManager.isJumping;
            isSprinting = m_inputManager.isSprinting;
            isInteracting = m_inputManager.isInteracting;
        }
    }
}
