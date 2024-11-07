using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.PhaseLogic
{
    public class PhaseManager : MonoBehaviour
    {
        private List<PhaseableObject> _phaseableObjects = new();

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
            foreach (PhaseableObject phaseableObject in _phaseableObjects)
            {
                phaseableObject.PhaseChange(phaseState);
            }
        }
    }
}