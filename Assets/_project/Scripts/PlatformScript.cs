using _project.Scripts.EnvironmentLogic;
using _project.Scripts.PhaseLogic;
using UnityEngine;

namespace _project.Scripts
{
    [RequireComponent(typeof(EnvironmentObject))]
    public class PlatformScript : PhaseableObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _boxCollider2D;
        [SerializeField] private GameObject _childObject;
        [SerializeField] private PassThroughPlatformScript _passThroughPlatformScript;

        public PassThroughPlatformScript GetPassThroughScript() => _passThroughPlatformScript;

        protected override void Awake_Impl()
        {
            _boxCollider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Phase_Impl()
        {
            _spriteRenderer.enabled = true;
            _boxCollider2D.enabled = true;
            _childObject.SetActive(true);
        }

        protected override void Unphase_Impl()
        {
            _spriteRenderer.enabled = false;
            _boxCollider2D.enabled = false;
            _childObject.SetActive(false);
        }

        private void OnValidate()
        {
            if (_childObject == null) _childObject = transform.GetChild(0).gameObject;
            if (_passThroughPlatformScript == null) _passThroughPlatformScript = transform.GetChild(0).GetComponent<PassThroughPlatformScript>();
            
            GetComponent<EnvironmentObject>().AddEnvironmentType(EnvironmentType.StableGround);
        }
    }
}