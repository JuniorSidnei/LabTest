using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class ThermometerLeftButtonController : MonoBehaviour, IClickable
    {
        public Transform RayOrigin;
        public LineController LineController;
        
        private Animator m_animator;

        private bool m_isButtonPressed;
        public List<Transform> m_linePoints = new List<Transform>();
        
        private void Awake() {
            m_animator = GetComponent<Animator>();
            m_linePoints.Add(RayOrigin);
        }

        private void FixedUpdate() {
            
            if (m_isButtonPressed) {
                
                if (Physics.Raycast(RayOrigin.position, transform.right, out var hit)) {
                    m_linePoints.Add(hit.transform);
                    Debug.DrawRay(RayOrigin.position, transform.right * hit.distance, Color.yellow);
                }
                LineController.SetupLine(m_linePoints);
            }
            
        }

        public void OnClick() {
            m_isButtonPressed = true;
            m_animator.SetBool("pressed", m_isButtonPressed);
        }

        public void OnLooseClick() {
            m_isButtonPressed = false;
            m_animator.SetBool("pressed", m_isButtonPressed);
            LineController.ClearLine();
        }
    }
}