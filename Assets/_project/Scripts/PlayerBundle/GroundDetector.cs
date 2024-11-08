using System;
using System.Collections.Generic;
using _project.Scripts.EnvironmentLogic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class GroundDetector : MonoBehaviour
    {

        private List<GameObject> _stableGrounds = new();

        public bool IsOnGround()
        {
            return _stableGrounds.Count > 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false) _stableGrounds.Add(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<EnvironmentObject>()?.IsOfType(EnvironmentType.StableGround) ?? false) _stableGrounds.Remove(other.gameObject);
        }
    }
}