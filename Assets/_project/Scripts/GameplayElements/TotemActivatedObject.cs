using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.GameplayElements
{
    public class TotemActivatedObject : MonoBehaviour, IActivableObject<Totem>
    {
        private int _neededTotems;
        private List<Totem> _activatedTotems = new();
        
        public void Activate(Totem activator)
        {
            if (_activatedTotems.Contains(activator)) return;
            _activatedTotems.Add(activator);
            if (_activatedTotems.Count >= _neededTotems)
            {
                Destroy(gameObject);
            }
        }

        public void RegisterActivator(Totem activator)
        {
            _neededTotems++;
        }
    }
}