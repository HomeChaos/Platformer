using UnityEngine;

namespace Components
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.15f;
        [SerializeField] private LayerMask _groundLayer;

        public bool IsGrounded => Physics2D.OverlapCircle(transform.position, _radius, _groundLayer);

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
    }
}