using System;
using UnityEngine;

namespace _project.Scripts.PlayerBundle 
{
    public class PlayerAnimationScript : MonoBehaviour
    {
        private static readonly int XInput = Animator.StringToHash("XInput");
        private static readonly int YSpeed = Animator.StringToHash("YSpeed");
        private static readonly int Jump = Animator.StringToHash("Jump");

        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private PlayerMovement _playerMovement;
        
        private bool _isMirrored;

        

        // public void PassInputValue(Vector2 value)
        // {
        //     if (value.x < 0) _isMirrored = true;
        //     _spriteRenderer.flipX = _isMirrored;
        //     _animator.SetFloat(Speed, value.x);
        // }
        

        private void TriggerJump()
        {
            _animator.SetTrigger(Jump);
        }
        
        private void OnInputVecChanged(Vector2 value)
        {
            _isMirrored = value.x < 0;
            _spriteRenderer.flipX = _isMirrored;

            _animator.speed = 1;
            if (_playerMovement.IsOnLadder && !(Math.Abs(value.y) > 0))
            {
                _animator.speed = 0;
            }
            
            _animator.SetFloat(XInput, Math.Abs(value.x));
        }
        
        private void OnVelocityChanged(Vector2 value)
        {
            _animator.SetFloat(YSpeed, Math.Abs(value.y));
        }
        
        private void OnEnable()
        {
            _playerMovement.InputVecChanged += OnInputVecChanged;
            _playerMovement.JumpEvent += TriggerJump;
            _playerMovement.VelocityChanged += OnVelocityChanged;
        }

        

        private void OnDisable()
        {
            _playerMovement.InputVecChanged -= OnInputVecChanged;
            _playerMovement.JumpEvent -= TriggerJump;
            _playerMovement.VelocityChanged -= OnVelocityChanged;
        }
    }
}