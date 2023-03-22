using UnityEngine;
using UnityEngine.Events;

namespace Scripts.PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        private const string _horizontalAxis = "Horizontal";
        private const string _buttonJump = "Jump";
        private const string _buttonAttack = "Fire1";
        
        public event UnityAction<float> Movement;
        public event UnityAction Jump; 
        public event UnityAction Attack;

        private void Update()
        {
            Movement?.Invoke(Input.GetAxis(_horizontalAxis));
            
            if(Input.GetButtonDown(_buttonJump))
                Jump?.Invoke();
            
            if(Input.GetButtonDown(_buttonAttack))
                Attack?.Invoke();
        }
    }
}