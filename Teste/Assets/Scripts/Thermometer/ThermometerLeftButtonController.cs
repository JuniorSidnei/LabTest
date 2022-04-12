using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using LabTest.Managers;
using UnityEditorInternal;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class ThermometerLeftButtonController : MonoBehaviour, IClickable {
        
        public GameObject ThermometerPanel;
        public Transform RayOrigin;
        public LineController LineController;
        
        private Animator m_animator;

        private bool m_isButtonPressed;
        private bool m_isThermometerOn;
        public List<Transform> m_linePoints = new List<Transform>();

        public delegate void OnThermometerEnabled(bool enabled);
        public static event OnThermometerEnabled onThermometerEnabled;
        
        public delegate void OnUpdateTemperature(float currentTemperature);
        public static event OnUpdateTemperature onUpdateTemperature;
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            
            if (m_isButtonPressed || m_isThermometerOn) {
                ThermometerPanel.SetActive(true);
                onThermometerEnabled?.Invoke(true);
                if (Physics.Raycast(RayOrigin.position, transform.right, out var hit)) {
                    //m_linePoints.Add(hit.transform);
                    var tempController = hit.collider.GetComponent<TemperatureController>();
                    if (tempController) {
                        onUpdateTemperature?.Invoke(tempController.TemperatureCelsius);
                    }
                    else {
                        onUpdateTemperature?.Invoke(0.0f);
                    }
                    Debug.DrawRay(RayOrigin.position, transform.right * hit.distance, Color.yellow);
                }
                //LineController.SetupLine(m_linePoints);
            }
            
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
            //LineController.ClearLine();
        }

        private IEnumerator ThermometerOff() {
            yield return new WaitForSeconds(15);
            ThermometerPanel.SetActive(false);
            m_isThermometerOn = false;
            onThermometerEnabled?.Invoke(false);
        }
    }
}