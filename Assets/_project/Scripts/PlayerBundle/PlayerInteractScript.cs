using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class PlayerInteractScript : MonoBehaviour
    {
        private Collider2D _collider;
        private Player _player;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        public void TryInteract()
        {
            List<Collider2D> results = new List<Collider2D>();
            _collider.GetContacts(new ContactFilter2D().NoFilter(), results);

            foreach (Collider2D result in results)
            {
                IInteractableObject interactableObject = result.GetComponent<IInteractableObject>();
                if (interactableObject != null)
                {
                    interactableObject.Interact(_player);
                    return;
                }
            }
        }
    }
}