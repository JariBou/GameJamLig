using _project.Scripts.PhaseLogic;
using UnityEngine;

namespace _project.Scripts
{
    public class PhaseObjectTest : PhaseableObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _boxCollider2D;
        protected override void Phase_Impl()
        {
            _boxCollider2D.enabled = true;
            var color = _spriteRenderer.color;
            color.a = 1;
            _spriteRenderer.color = color;
        }

        protected override void Unphase_Impl()
        {
            _boxCollider2D.enabled = false;
            var color = _spriteRenderer.color;
            color.a = 0;
            _spriteRenderer.color = color;
        }
    }
}