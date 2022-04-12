using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LabTest.Managers {
    
    public class GameManager : Singleton<GameManager> {
        public TextMeshProUGUI WarningText;
        public TextMeshProUGUI EquipText;

        private bool m_isEpiEquipped;
        private PlayerInput m_playerInput;
        private InputAction m_equipAction;

        private void OnEnable() {
            MouseClickManager.onFinishIntro += FinishedIntro;
        }
        
        private void OnDisable() {
            MouseClickManager.onFinishIntro -= FinishedIntro;
        }

        private void FinishedIntro() {
            WarningText.gameObject.SetActive(true);
            EquipText.gameObject.SetActive(true);
        }
        
        private void Awake() {
            m_playerInput = GetComponent<PlayerInput>();
            m_equipAction = m_playerInput.actions["Equip"];
            m_equipAction.performed += _ => {
                m_isEpiEquipped = true;
                WarningText.gameObject.SetActive(false);
                EquipText.gameObject.SetActive(false);
            };

        }
        public bool IsEPIEquipped => m_isEpiEquipped;
    }
}