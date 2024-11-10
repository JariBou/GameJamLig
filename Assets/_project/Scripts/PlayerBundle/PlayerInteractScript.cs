using _project.Scripts.GameplayElements;
using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class PlayerInteractScript : MonoBehaviour
    {
        [SerializeField, HideInInspector]private Player _player;
        [SerializeField] private LayerMask _interactableLayerMask;
        [SerializeField] private Vector2 _interactionBoxSize;

        public void TryInteract()
        {
            // List<Collider2D> results = new List<Collider2D>();
            // ContactFilter2D contactFilter2D = new ContactFilter2D().NoFilter();
            // // contactFilter2D.layerMask = LayerMask.GetMask("Interactables");
            // // contactFilter2D.useLayerMask = true;
            // _collider.GetContacts(contactFilter2D, results);

            
             Collider2D[] results = Physics2D.OverlapBoxAll(_player.transform.position, _interactionBoxSize, 0, _interactableLayerMask);

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

        private void OnValidate()
        {
            _player = GetComponentInParent<Player>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_player.transform.position, _interactionBoxSize);
        }
    }
}