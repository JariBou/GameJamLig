using System;
using System.Collections.Generic;
using _project.Scripts.EnvironmentLogic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class GroundDetector : MonoBehaviour
    {
        private Collider2D _collider;
        private int _groundTouchingCount = 0;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public bool IsOnGround()
        {
            return _groundTouchingCount > 0;
        }

        public T GetGroundComponent<T>()
        {
            List<Collider2D> results = new List<Collider2D>();
            _collider.OverlapCollider(new ContactFilter2D().NoFilter(), results);

            foreach (Collider2D result in results)
            {
                T component = result.GetComponent<T>();
                if (component != null) return component;
            }

            throw new NullReferenceException("No component of type " + typeof(T).ToString() + "was found on ground");
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