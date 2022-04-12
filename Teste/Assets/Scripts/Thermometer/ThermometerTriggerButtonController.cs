using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using LabTest.Managers;
using UnityEditorInternal;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class ThermometerTriggerButtonController : MonoBehaviour, IClickable {
        
        public GameObject ThermometerPanel;
        public Transform RayOrigin;
        public TemperatureManager.TemperatureMode TemperatureStatusMode;
        
        private Animator m_animator;

        private bool m_isButtonPressed;
        private bool m_isThermometerOn;
        private bool m_isTemperatureUpdated;

        public delegate void OnThermometerEnabled(bool enabled);
        public static event OnThermometerEnabled onThermometerEnabled;
        
        public delegate void OnUpdateTemperature(float currentTemperature);
        public static event OnUpdateTemperature onUpdateTemperature;
        
        private void OnEnable() {
            ThermometerModeController.onChangeTemperatureMode += UpdateTemperatureMode;
        }

        private void OnDisable() {
            ThermometerModeController.onChangeTemperatureMode -= UpdateTemperatureMode;
        }
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            if (!m_isButtonPressed && !m_isThermometerOn) return;
            
            ThermometerPanel.SetActive(true);
            onThermometerEnabled?.Invoke(true);
            if (!Physics.Raycast(RayOrigin.position, transform.right, out var hit)) return;
            
            var tempController = hit.collider.GetComponent<TemperatureController>();
            if (m_isTemperatureUpdated) return;
            m_isTemperatureUpdated = true;
            StartCoroutine(UpdateTemperature(tempController));
            
            Debug.DrawRay(RayOrigin.position, transform.right * hit.distance, Color.yellow);
        }

        private void UpdateTemperatureMode(TemperatureManager.TemperatureMode temperatureMode) {
            TemperatureStatusMode = temperatureMode;
        }
        
        public void OnClick() {
            m_isButtonPressed = true;
            m_isThermometerOn = true;
            m_animator.SetBool("pressed", m_isButtonPressed);
        }

        public void OnLooseClick() {
            m_isButtonPressed = false;
            m_animator.SetBool("pressed", m_isButtonPressed);
            StartCoroutine(nameof(ThermometerOff));
        }

        private IEnumerator UpdateTemperature(TemperatureController tempController) {
            yield return new WaitForSeconds(0.9f);
            if (tempController == null) {
                onUpdateTemperature?.Invoke(0.0f);    
            }
            else {
                onUpdateTemperature?.Invoke(TemperatureStatusMode == TemperatureManager.TemperatureMode.Celsius ? tempController.TemperatureCelsius : tempController.TemperatureFahrenheit);    
            }
            
            m_isTemperatureUpdated = false;
        }
        
        private IEnumerator ThermometerOff() {
            yield return new WaitForSeconds(15);
            ThermometerPanel.SetActive(false);
            m_isThermometerOn = false;
            onThermometerEnabled?.Invoke(false);
        }
    }
}