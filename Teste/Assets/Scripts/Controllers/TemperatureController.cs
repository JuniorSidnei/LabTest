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
        private float m_constantCA;
        private float m_constantCR;
        private bool m_hasInitialHeatTemperature;
        private bool m_hasInitialColdTemperature;
        private float m_elapsedTime;
        private float m_initialHeatTemperature;
        private const float m_constantEuler = 2.71f;
        private BunsenController m_bunsenController;

        public void SetBunsen(BunsenController bunsenController) {
            m_bunsenController = bunsenController;
        }
        
        public void SetInitialTemperatureAndConstants(float initialTemperature, float constantCA, float constantCR,
            float minimumTemperature, float maximumTemperature) {
            m_initialTemperature = initialTemperature;
            m_constantCA = constantCA;
            m_constantCR = constantCR;
            m_minimumTemperature = minimumTemperature;
            m_maxTemperature = maximumTemperature;
            m_currentTemperatureCelsius = m_initialTemperature;
            m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
        }

        private void Update() {
            if (m_bunsenController.IsBunsenOn) {
                m_hasInitialColdTemperature = false;
                if (!m_hasInitialHeatTemperature) {
                    m_initialHeatTemperature = m_currentTemperatureCelsius;
                    m_hasInitialHeatTemperature = true;
                    m_elapsedTime = 0;
                }
                
                m_elapsedTime += Time.deltaTime;
                var currentTemp = m_initialHeatTemperature + (m_maxTemperature - m_initialHeatTemperature) * (1 - Mathf.Pow(m_constantEuler, m_constantCA * m_elapsedTime));
                if (currentTemp >= m_maxTemperature) {
                    currentTemp = m_maxTemperature;
                }

                m_currentTemperatureCelsius = currentTemp;
                m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
            } else {
                m_hasInitialHeatTemperature = false;
                if (!m_hasInitialColdTemperature) {
                    m_initialHeatTemperature = m_currentTemperatureCelsius;
                    m_hasInitialColdTemperature = true;
                    m_elapsedTime = 0;
                }

                m_elapsedTime += Time.deltaTime;
                var currentTemp = m_initialHeatTemperature + (m_minimumTemperature - m_initialHeatTemperature) * (1 - Mathf.Pow(m_constantEuler, m_constantCR * m_elapsedTime));
                if (currentTemp <= m_minimumTemperature) {
                    currentTemp = m_minimumTemperature;
                }

                m_currentTemperatureCelsius = currentTemp;
                m_currentTemperatureFahrenheit = (m_currentTemperatureCelsius * 9/5) + 32;
            }
        }
    }
}