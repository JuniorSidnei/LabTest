using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class ThermometerMiddleButtonController : MonoBehaviour, IClickable {
        
        private Animator m_animator;

        public delegate void OnRegisterMaxTemperature();
        public static event OnRegisterMaxTemperature onRegisterMaxTemperature;
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        public void OnClick() {
            m_animator.SetBool("pressed", true);
            onRegisterMaxTemperature?.Invoke();
        }

        public void OnLooseClick() {
            m_animator.SetBool("pressed", false);
        }
    }
}