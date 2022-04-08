using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LabTest.Managers {
    
    public class MouseClickManager : MonoBehaviour {

        private Camera m_camera;
        private PlayerInput m_playerInput;

        private InputAction m_leftClick;
        
        private void Awake() {
            m_camera = GetComponent<Camera>();
            m_playerInput = GetComponent<PlayerInput>();
            m_leftClick = m_playerInput.actions["LeftClick"];
            
            m_leftClick.performed += ctx => {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                if (Physics.Raycast(m_camera.ScreenPointToRay(mousePos), out var hit)) {
                    hit.collider.GetComponent<IClickable>()?.OnClick();
                }
            };
        }
    }
}