using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LabTest.CameraMovement {
    
    public class CameraMovement : MonoBehaviour {
        public float MovementSpeed;
        public float RotationSpeed;
        
        private PlayerInput m_playerInput;
        private InputAction m_movement;
        private InputAction m_rotation;
        private Vector3 m_velocity;
        private bool m_introFinished;
        
        private void OnEnable() {
            MouseClickManager.onFinishIntro += FinishedIntro;
        }
        
        private void OnDisable() {
            MouseClickManager.onFinishIntro -= FinishedIntro;
        }
        
        private void Awake() {
            m_playerInput = GetComponent<PlayerInput>();
            m_movement = m_playerInput.actions["Movement"];
            m_rotation = m_playerInput.actions["Rotation"];
        }

        private void Update() {
            if (!m_introFinished) return;
            
            var rotInput = m_rotation.ReadValue<Vector2>();
            transform.eulerAngles += new Vector3(0, rotInput.x * RotationSpeed, 0);
            
            var input = m_movement.ReadValue<Vector2>();
            Debug.Log("input: " + input);
            m_velocity = new Vector3(input.x, 0, input.y) * Time.deltaTime * MovementSpeed;
            transform.Translate(m_velocity);
        }

        private void FinishedIntro() {
            m_introFinished = true;
        }
    }
}