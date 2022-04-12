using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using LabTest.Managers;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class ThermometerModeController : MonoBehaviour, IClickable {
        public TemperatureManager.TemperatureMode TemperatureMode;
        
        private Animator m_animator;

        public delegate void OnChangeTemperatureMode(TemperatureManager.TemperatureMode temperatureMode);
        public static event OnChangeTemperatureMode onChangeTemperatureMode;
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }
        
        public void OnClick() {
            m_animator.SetBool("pressed", true);
            onChangeTemperatureMode?.Invoke(TemperatureMode);
        }

        public void OnLooseClick() {
            m_animator.SetBool("pressed", false);
        }
    }
}