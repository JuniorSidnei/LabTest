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

        private void OnEnable() {
            ThermometerLeftButtonController.onThermometerEnabled += ThermometerEnabled;
            ThermometerLeftButtonController.onUpdateTemperature += UpdateTemperature;
        }

        private void OnDisable() {
            ThermometerLeftButtonController.onThermometerEnabled -= ThermometerEnabled;
            ThermometerLeftButtonController.onUpdateTemperature -= UpdateTemperature;
        }

        private void Awake() {
            TemperatureStatusMode = TemperatureMode.Celsius;
            CurrentTemperature.text = string.Format("{0:0.0}", 0.0f);
            CurrentTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
            MaxTemperature.text = string.Format("{0:0.0}", 0.0f);
            CurrentThermomenterTemperatureMode.text = TemperatureStatusMode == TemperatureMode.Celsius ? "C°" : "F°";
            CurrentThermometerTemperature.text = string.Format("{0}", 0.0f);
            MaxThermometerTemperature.text = string.Format("{0}", 0.0f);
        }

        private void UpdateTemperature(float currentTemperature) {
            CurrentTemperature.text = string.Format("{0:0.0}", currentTemperature);
            CurrentThermometerTemperature.text = string.Format("{0:0.0}", currentTemperature);
        }
        
        private void ThermometerEnabled(bool enabled) {
            TemperaturePanel.SetActive(enabled);
        }
    }
    
    
}