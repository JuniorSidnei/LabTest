using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabTest.Managers
{


    public class TimerManager : MonoBehaviour
    {
        public Button StartButton;
        public Button StopButton;
        public Button ResetButton;
        public TextMeshProUGUI TimerText;
        private float m_timer;
        private bool m_isTimerStarted;
        private bool m_isTimerStopped;

        public float CurrentTimeMinutes => m_timer / 60;
        public float CurrentTimeSeconds => m_timer % 60;
        
        private void Awake() {
            
            StartButton.onClick.AddListener(() => {
                m_isTimerStarted = true;
            });
            
            StopButton.onClick.AddListener(() => {
                m_isTimerStarted = false;
            });
            
            ResetButton.onClick.AddListener(() => {
                m_isTimerStarted = false;
                m_timer = 0;
            });
        }

        private void Update() {
            float minutes = Mathf.FloorToInt(m_timer / 60);
            float seconds = Mathf.FloorToInt(m_timer % 60);

            TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            
            if (m_isTimerStarted) {
                m_timer += Time.deltaTime;
            }
        }
    }
}