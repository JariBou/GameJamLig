using System;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        
        [SerializeField] private float _maxHorizontalSpeed = 7f;
        [SerializeField] private float _horizontalSpeed = 3f;
        [SerializeField] private float _jumpSpeed = 10f;
        [SerializeField] private float _linearDrag = 6f;
        [SerializeField] private float _gravityStrength = 9.81f;
        
        [SerializeField] private PlayerInputHandler _inputHandler;

        private void FixedUpdate()
        {
            Vector2 snapshotSpeed = _rb.velocity;
            snapshotSpeed.x = (Math.Abs(snapshotSpeed.x) - _linearDrag * Time.fixedDeltaTime) * Math.Sign(snapshotSpeed.x);
            snapshotSpeed.y -= _gravityStrength * Time.fixedDeltaTime;

            if (_inputHandler.InputVec.x > 0)
            {
                if (snapshotSpeed.x > _maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalSpeed * _inputHandler.InputVec.x * Time.fixedDeltaTime;
            } else if (_inputHandler.InputVec.x < 0)
            {
                if (snapshotSpeed.x < -_maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalSpeed * _inputHandler.InputVec.x * Time.fixedDeltaTime;
            }
            
            snapshotSpeed.x = Mathf.Clamp(snapshotSpeed.x, -_maxHorizontalSpeed, _maxHorizontalSpeed);
            
            _rb.velocity = snapshotSpeed;
        }

        private void OnValidate()
        {
            if (_rb == null) _rb = GetComponent<Rigidbody2D>();
            //_rb.drag = _linearDrag;
        }
    }
}