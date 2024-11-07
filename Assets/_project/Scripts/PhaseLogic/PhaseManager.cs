using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.PhaseLogic
{
    public class PhaseManager : MonoBehaviour
    {
        private List<PhaseableObject> _phaseableObjects = new();
        private PhaseState _currentState = PhaseState.Red;

        public PhaseState CurrentState => _currentState;

        private void Start()
        {
            PhaseTo(PhaseState.Red);
        }

        public void RegisterObject(PhaseableObject phaseableObject)
        {
            _phaseableObjects.Add(phaseableObject);
        }

        public void UnregisterObject(PhaseableObject phaseableObject)
        {
            _phaseableObjects.Remove(phaseableObject);
        }

        public void PhaseTo(PhaseState phaseState)
        {
            _currentState = phaseState;
            foreach (PhaseableObject phaseableObject in _phaseableObjects)
            {
                phaseableObject.PhaseChange(phaseState);
            }
        }
    }
}