using UnityEngine;

namespace _project.Scripts.PlayerBundle
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField, Range(0.001f, 1f)] private float _lerpStrength;
        [SerializeField] private GameObject _target;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 targetPosition = _target.transform.position;
            targetPosition.z = transform.position.z;

   
            // TODO improve this shit
            transform.position = Vector3.Lerp(transform.position, targetPosition, _lerpStrength);
        }
    }
}
