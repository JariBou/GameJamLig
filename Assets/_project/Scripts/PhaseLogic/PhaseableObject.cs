using System;
using UnityEngine;

namespace _project.Scripts.PhaseLogic
{
    public abstract class PhaseableObject : MonoBehaviour
    {
        protected PhaseManager _phaseManager;
        [field: SerializeField] public PhaseState PhaseType { get; protected set; }
        public bool IsPhased { get; protected set; }

        private void Awake()
        {
            _phaseManager = GameObject.FindGameObjectWithTag("PhaseManager").GetComponent<PhaseManager>();
            if (_phaseManager == null) throw new Exception("PhaseManager is null");
            _phaseManager.RegisterObject(this);
            
            IsPhased = true;
            
            Awake_Impl();
        }
        
        protected virtual void Awake_Impl(){}

        private void OnDestroy()
        {
            _phaseManager?.UnregisterObject(this);
        }
        
        protected virtual void OnDestroy_Impl(){}


        public virtual void PhaseChange(PhaseState newPhaseState)
        {
            if (newPhaseState == PhaseType)
            {
                PhaseDaddy();
            }
            else
            {
                UnphaseDaddy();
            }
        }

        private void PhaseDaddy()
        {
            if (IsPhased) return;
            IsPhased = true;
            Phase_Impl();
        }
        protected abstract void Phase_Impl();

        private void UnphaseDaddy()
        {
            if (!IsPhased) return;
            IsPhased = false;
            Unphase_Impl();
        }
        
        protected abstract void Unphase_Impl();
    }
}