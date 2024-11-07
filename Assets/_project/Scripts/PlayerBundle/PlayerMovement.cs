using System;
using System.Collections.Generic;
using _project.Scripts.EnvironmentLogic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.PlayerBundle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxHorizontalSpeed = 25f;
        [FormerlySerializedAs("_horizontalSpeed")] [SerializeField] private float _horizontalAcceleration = 30f;
        [SerializeField] private float _jumpSpeed = 10f;
        [SerializeField] private float _linearDrag = .05f;
        [SerializeField] private float _gravityStrength = 9.81f;

        [SerializeField] private BoxCollider2D _feetCollider;
        
        // If set to false this fuck up when detection happens when jumping so might go to a OnTriggerOverlap thing on a separate script maybe
        [SerializeField] private bool _fallingRemovesJump = true; 
        
        private Rigidbody2D _rb;
        private bool _isGrounded;
        private Vector2 _inputVec;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void PassInputVec(Vector2 inputVec)
        {
            _inputVec = inputVec;
        }

        private void CheckGround()
        {
            List<Collider2D> results = new List<Collider2D>();
            _feetCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);

            foreach (Collider2D col in results)
            {
                if (col.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false)
                {
                    _isGrounded = true;
                    return;
                }
            }
            
            if (_fallingRemovesJump) _isGrounded = false;
        }

        private void FixedUpdate()
        {
            CheckGround();
            Vector2 snapshotSpeed = _rb.velocity;
            snapshotSpeed.x = Math.Abs(snapshotSpeed.x) * (1 - _linearDrag) * Math.Sign(snapshotSpeed.x);
            if (!_isGrounded) snapshotSpeed.y -= _gravityStrength * Time.fixedDeltaTime;

            if (_inputVec.x > 0)
            {
                if (snapshotSpeed.x > _maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalAcceleration * _inputVec.x * Time.fixedDeltaTime;
            } else if (_inputVec.x < 0)
            {
                if (snapshotSpeed.x < -_maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalAcceleration * _inputVec.x * Time.fixedDeltaTime;
            }
            
            snapshotSpeed.x = Mathf.Clamp(snapshotSpeed.x, -_maxHorizontalSpeed, _maxHorizontalSpeed);
            
            _rb.velocity = snapshotSpeed;
        }

        private void OnValidate()
        {
            if (_rb == null) _rb = GetComponent<Rigidbody2D>();
            //_rb.drag = _linearDrag;
        }

        public void Jump()
        {
            if (!_isGrounded) return;
            _isGrounded = false;
            Vector2 vector2 = _rb.velocity;
            vector2.y = _jumpSpeed;
            _rb.velocity = vector2;
        }
    }
}