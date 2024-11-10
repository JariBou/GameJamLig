using _project.Scripts.PhaseLogic;
using _project.Scripts.PlayerBundle;
using UnityEngine;

namespace _project.Scripts.GameplayElements
{
    public class Totem : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private MonoBehaviour _activableObject;
        private IActivableObject<Totem> _activableScript;
        
        [SerializeField] private PhaseState _neededPhaseState;

        private void Awake()
        {
            _activableScript.RegisterActivator(this);
        }

        public void Interact(Player player)
        {
            if (player.phaseManager.CurrentState != _neededPhaseState) return;
            _activableScript?.Activate(this);
        }

        private void OnValidate()
        {
            if (_activableObject == null) return;
            if ((IActivableObject<Totem>)_activableObject == null)
            {
                _activableObject = null;
            }
            
            _activableScript = (IActivableObject<Totem>)_activableObject;
        }
    }
}