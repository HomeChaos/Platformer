using Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace EnemyScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyVision : MonoBehaviour
    {
        public event UnityAction Detection;

        private void Start()
        {
            var collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                Detection?.Invoke();
            }
        }
    }
}