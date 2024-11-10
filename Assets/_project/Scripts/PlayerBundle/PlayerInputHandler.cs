using System;
using _project.Scripts.PhaseLogic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _project.Scripts.PlayerBundle
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PhaseManager _phaseManager;
        [SerializeField] private PlayerInteractScript _playerInteractScript;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _phaseManager = GameObject.FindGameObjectWithTag("PhaseManager").GetComponent<PhaseManager>();
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            Vector2 move = ctx.ReadValue<Vector2>();
            _playerMovement.PassInputVec(move);
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            if(!ctx.performed) return;
            _playerMovement.Jump();
        }

        public void Phase(InputAction.CallbackContext ctx)
        {
            if(!ctx.performed) return;
            _phaseManager.PhaseTo(_phaseManager.CurrentState == PhaseState.Blue ? PhaseState.Red : PhaseState.Blue);
        }

        public void Interact(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            _playerInteractScript.TryInteract();
        }
    }
}
