using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Managers;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class TemperatureController : MonoBehaviour {
        
        private float m_currentTemperatureCelsius;
        private float m_currentTemperatureFahrenheit;

        public float TemperatureCelsius => m_currentTemperatureCelsius;
        public float TemperatureFahrenheit => m_currentTemperatureFahrenheit;

        private float m_initialTemperature;
        private float m_minimumTemperature;
        private float m_maxTemperature;
        private float m_heatingConstant;
        private float m_coolingConstant;
        private bool m_hasInitialTemperature;
        //private bool m_hasInitialColdTemperature;
        private float m_elapsedTime;
        private float m_initialHeatTemperature;
        private const float m_constantEuler = 2.71828f;
        private float m_maxTemperatureRegistered;
        private BunsenController m_bunsenController;

        private void OnEnable() {
            ThermometerMiddleButtonController.onRegisterMaxTemperature += RegisterMaxTemperature;
        }

        private void OnDisable() {
            ThermometerMiddleButtonController.onRegisterMaxTemperature += RegisterMaxTemperature;
        }

        private void RegisterMaxTemperature() {
            m_maxTemperatureRegistered = 0.0f;
        }
        
        public void SetBunsen(BunsenController bunsenController) {
            m_bunsenController = bunsenController;
        }
        
        public void SetInitialTemperatureAndConstants(float initialTemperature, float heatingConstant,
            float coolingConstant,
            float minimumTemperature, float maximumTemperature) {
            m_initialTemperature = initialTemperature;
            m_heatingConstant = heatingConstant;
            m_coolingConstant = coolingConstant;
            m_minimumTemperature = minimumTemperature;
            m_maxTemperature = maximumTemperature;
            m_currentTemperatureCelsius = m_initialTemperature;
            m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
        }

        private void Update() {
            if (m_bunsenController.IsBunsenOn) {
                
                if (!m_hasInitialTemperature) {
                    m_initialHeatTemperature = m_currentTemperatureCelsius;
                    m_hasInitialTemperature = true;
                    m_elapsedTime = 0;
                }
      
                m_elapsedTime += Time.deltaTime;
                var currentTemp = m_initialHeatTemperature + (m_maxTemperature - m_initialHeatTemperature) * (1 - Mathf.Pow(m_constantEuler, m_heatingConstant * m_elapsedTime)); 
                if (currentTemp >= m_maxTemperature) {
                     currentTemp = m_maxTemperature;
                }
                
                m_currentTemperatureCelsius = currentTemp;
                m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
            } else {
                
                if (m_hasInitialTemperature) {
                    m_initialHeatTemperature = m_currentTemperatureCelsius;
                    m_hasInitialTemperature = false;
                    m_elapsedTime = 0;
                }
                
                m_elapsedTime += Time.deltaTime;
                var currentTemp = m_initialHeatTemperature + (m_minimumTemperature - m_initialHeatTemperature) * (1 - Mathf.Pow(m_constantEuler, m_coolingConstant * m_elapsedTime));
                if (currentTemp <= m_minimumTemperature) {
                    currentTemp = m_minimumTemperature;
                }
                
                m_currentTemperatureCelsius = currentTemp;
                m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
            }
        }
    }
}