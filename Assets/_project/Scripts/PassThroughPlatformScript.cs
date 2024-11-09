using UnityEngine;

namespace _project.Scripts
{
    public class PassThroughPlatformScript : MonoBehaviour
    {
        [SerializeField] private Collider2D _platformCollider;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.tag);
            if (other.CompareTag("Player")) _platformCollider.enabled = false;
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"Leaving platform {other.tag}");
            if (other.CompareTag("Player")) _platformCollider.enabled = true;
        }
    }
}
