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
        [SerializeField] private float _ladderSpeed = 350f;
        [SerializeField] private float _linearDrag = .05f;
        [SerializeField] private float _gravityStrength = 9.81f;

        [SerializeField] private GroundDetector _groundDetector;
        [SerializeField] private Collider2D _bodyCollider;
        
        // If set to false this fuck up when detection happens when jumping so might go to a OnTriggerOverlap thing on a separate script maybe
        [SerializeField] private bool _fallingRemovesJump = true; 
        
        private Rigidbody2D _rb;
        private bool _isGrounded;
        private bool _isOnLadder;
        private bool _canMove = true;
        private Vector2 _inputVec;
        private bool _collidesWithLadder;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void PassInputVec(Vector2 inputVec)
        {
            _inputVec = inputVec;
        }

        private void FixedUpdate()
        {
            _isGrounded = _groundDetector.IsOnGround();
            Vector2 snapshotSpeed = _rb.velocity;
            if (_isOnLadder)
            {
                snapshotSpeed.x = Math.Abs(snapshotSpeed.x) * (1 - _linearDrag)*3/4 * Math.Sign(snapshotSpeed.x);
            } else
            {
                snapshotSpeed.x = Math.Abs(snapshotSpeed.x) * (1 - _linearDrag) * Math.Sign(snapshotSpeed.x);
            }
            
            LadderCheck(ref snapshotSpeed);
            if (!_isGrounded && !_isOnLadder) snapshotSpeed.y -= _gravityStrength * Time.fixedDeltaTime;
            
            float speedMult = _isGrounded ? 1f : .6f;
            
            if (_canMove)
            {
                snapshotSpeed.x += _horizontalAcceleration * speedMult * _inputVec.x * Time.fixedDeltaTime;
            }
           
            snapshotSpeed.x = Mathf.Clamp(snapshotSpeed.x, -_maxHorizontalSpeed, _maxHorizontalSpeed);
            
            _rb.velocity = snapshotSpeed;
        }

        private void LadderCheck(ref Vector2 snapshotSpeed)
        {
            if (_isOnLadder && _collidesWithLadder)
            {
                if (_inputVec.y < 0 && _isGrounded)
                {
                    _isOnLadder = false;
                }
                else
                {
                    snapshotSpeed.y = _ladderSpeed * Time.fixedDeltaTime * Math.Sign(_inputVec.y);
                }
            }
            else if (_collidesWithLadder)
            {
                switch (_isGrounded)
                {
                    case true when _inputVec.y > 0:
                        _isOnLadder = true;
                        snapshotSpeed.y = _ladderSpeed * Time.fixedDeltaTime * Math.Sign(_inputVec.y);
                        break;
                    case false when Math.Abs(_inputVec.y) > 0:
                        _isOnLadder = true;
                        break;
                }
            }
            else
            {
                _isOnLadder = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("trigger entered");
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.Ladder) ?? false)
            {
                Debug.Log("Component found");
                _collidesWithLadder = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("trigger exited");
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.Ladder) ?? false)
            {
                Debug.Log("Component found");
                _collidesWithLadder = false;
            }
        }

        private void CheckForLadder()
        {
            if (!_isOnLadder & Math.Abs(_inputVec.y) > 0)
            {
                List<Collider2D> results = new List<Collider2D>();
                _bodyCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results);
                
                foreach (Collider2D col in results)
                {
                    if (col.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.Ladder) ?? false)
                    {
                        _isGrounded = true;
                        break;
                    }
                }
            }
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