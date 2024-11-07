using System;
using _project.Scripts.EnvironmentLogic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxHorizontalSpeed = 7f;
        [SerializeField] private float _horizontalSpeed = 3f;
        [SerializeField] private float _jumpSpeed = 10f;
        [SerializeField] private float _linearDrag = 6f;
        [SerializeField] private float _gravityStrength = 9.81f;
        
        [SerializeField] private BoxCollider2D _feetCollider;
        
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
            Collider2D[] results = new Collider2D[1];
            _feetCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);

            foreach (Collider2D col in results)
            {
                if (col.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false)
                {
                    _isGrounded = true;
                    return;
                }
            }

        }

        private void FixedUpdate()
        {
            CheckGround();
            Vector2 snapshotSpeed = _rb.velocity;
            snapshotSpeed.x = (Math.Abs(snapshotSpeed.x) - _linearDrag * Time.fixedDeltaTime) * Math.Sign(snapshotSpeed.x);
            if (!_isGrounded) snapshotSpeed.y -= _gravityStrength * Time.fixedDeltaTime;

            if (_inputVec.x > 0)
            {
                if (snapshotSpeed.x > _maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalSpeed * _inputVec.x * Time.fixedDeltaTime;
            } else if (_inputVec.x < 0)
            {
                if (snapshotSpeed.x < -_maxHorizontalSpeed) return;
                
                snapshotSpeed.x += _horizontalSpeed * _inputVec.x * Time.fixedDeltaTime;
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
            _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }
}