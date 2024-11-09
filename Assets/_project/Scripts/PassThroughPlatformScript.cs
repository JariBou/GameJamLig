using System.Collections;
using UnityEngine;

namespace _project.Scripts
{
    public class PassThroughPlatformScript : MonoBehaviour
    {
        [SerializeField] private Collider2D _platformCollider;
        private bool _playerPassingThrough;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.tag);
            if (other.CompareTag("Player"))
            {
                _platformCollider.enabled = false;
                _playerPassingThrough = true;
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"Leaving platform {other.tag}");
            if (other.CompareTag("Player"))
            {
                _platformCollider.enabled = true;
                _playerPassingThrough = false;
            }
        }

        public void TempDisableCollision()
        {
            StartCoroutine(TemporalyDisableCollision());
        }

        public IEnumerator TemporalyDisableCollision()
        {
            _platformCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0.5f);
            if (!_playerPassingThrough) _platformCollider.enabled = true;
        }
    }
}
