using System;
using System.Collections.Generic;
using _project.Scripts.EnvironmentLogic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class GroundDetector : MonoBehaviour
    {

        private int _groundTouchingCount = 0;

        public bool IsOnGround()
        {
            return _groundTouchingCount > 0;
        }

        public void ResetGroundTouching()
        {
            _groundTouchingCount = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false) _groundTouchingCount++;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false) _groundTouchingCount--;
        }
    }
}