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
        private GameObject m_lastObjectSelected;

        public delegate void OnFinishIntro();
        public static event OnFinishIntro onFinishIntro;
        
        private void Awake() {
            m_camera = GetComponent<Camera>();
            m_playerInput = GetComponent<PlayerInput>();
            m_leftClick = m_playerInput.actions["LeftClick"];
            
            m_leftClick.performed += ctx => {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                if (Physics.Raycast(m_camera.ScreenPointToRay(mousePos), out var hit)) {
                    m_lastObjectSelected = hit.collider.gameObject;
                    hit.collider.GetComponent<IClickable>()?.OnClick();
                }
            };

            m_leftClick.canceled += ctx => {
                m_lastObjectSelected.GetComponent<IClickable>()?.OnLooseClick();
                m_lastObjectSelected = null;
            };
        }

        public void OnFinishedIntro() => onFinishIntro?.Invoke();
    }
}