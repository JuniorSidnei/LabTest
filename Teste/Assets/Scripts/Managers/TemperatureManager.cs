using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Controllers;
using TMPro;
using UnityEngine;

namespace LabTest.Managers {

    public class TemperatureManager : MonoBehaviour {
        
        public enum TemperatureMode {
            Celsius, Fahrenheit    
        }
        
        public GameObject TemperaturePanel;
        public TextMeshProUGUI CurrentTemperatureMode;
        public TextMeshProUGUI CurrentTemperature;
        public TextMeshProUGUI MaxTemperature;
        
        public TextMeshProUGUI CurrentThermomenterTemperatureMode;
        public TextMeshProUGUI CurrentThermometerTemperature;
        public TextMeshProUGUI MaxThermometerTemperature;

        public TemperatureMode TemperatureStatusMode;

        private bool m_isMaxTemperatureRegistered;
        
        private void OnEnable() {
            ThermometerTriggerButtonController.onThermometerEnabled += ThermometerEnabled;
            ThermometerTriggerButtonController.onUpdateTemperature += UpdateTemperature;
            ThermometerMiddleButtonController.onRegisterMaxTemperature += RegisterMaxTemperature;
            ThermometerModeController.onChangeTemperatureMode += UpdateTemperatureMode;
        }

        private void OnDisable() {
            ThermometerTriggerButtonController.onThermometerEnabled -= ThermometerEnabled;
            ThermometerTriggerButtonController.onUpdateTemperature -= UpdateTemperature;
            ThermometerMiddleButtonController.onRegisterMaxTemperature -= RegisterMaxTemperature;
            ThermometerModeController.onChangeTemperatureMode -= UpdateTemperatureMode;
        }

        private void Awake() {
            TemperatureStatusMode = TemperatureMode.Celsius;
            var temperature = string.Format("{0:0.0}", 0.0f);
            CurrentTemperature.text = temperature;
            CurrentTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
            MaxTemperature.text = string.Format("Max: {0:0.0}", 0.0f);
            CurrentThermomenterTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
            CurrentThermometerTemperature.text = temperature;
            MaxThermometerTemperature.text = string.Format("Max: {0:0.0}", 0.0f);
        }

        private void UpdateTemperature(float currentTemperature) {
            var temperature = string.Format("{0:0.0}", currentTemperature);
            CurrentTemperature.text = temperature;
            CurrentThermometerTemperature.text = temperature;

            if (m_isMaxTemperatureRegistered) return;
            
            MaxTemperature.text = string.Format("Max: {0:0.0}", currentTemperature);
            MaxThermometerTemperature.text = string.Format("Max: {0:0.0}", currentTemperature);
        }

        private void RegisterMaxTemperature() {
            m_isMaxTemperatureRegistered = !m_isMaxTemperatureRegistered;
        }
        
        private void ThermometerEnabled(bool enabled) {
            TemperaturePanel.SetActive(enabled);
        }

        private void UpdateTemperatureMode(TemperatureMode temperatureMode) {
            TemperatureStatusMode = temperatureMode;
            CurrentTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
            CurrentThermomenterTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
        }
    }
    
    
}