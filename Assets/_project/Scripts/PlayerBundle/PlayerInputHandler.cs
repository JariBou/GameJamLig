using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _project.Scripts.PlayerBundle
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            Vector2 move = ctx.ReadValue<Vector2>();
            _playerMovement.PassInputVec(move);
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            _playerMovement.Jump();
        }
    }
}
