using System;
using _project.Scripts.PhaseLogic;
using Unity.VisualScripting;
using UnityEngine;

namespace _project.Scripts
{
    public class PhaseObjectTest : PhaseableObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private GameObject _childObject;
        
        protected override void Phase_Impl()
        {
            _childObject.SetActive(true);
        }

        protected override void Unphase_Impl()
        {
            _childObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (_childObject == null) _childObject = transform.GetChild(0).gameObject;
        }
    }
}