using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LabTest.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LabTest.Controllers {
    
    public class ThermometerController : MonoBehaviour, IClickable {
        
        public Camera Camera;
        private bool m_isEquipped;
        private BoxCollider m_boxCollider;

        private void OnEnable() {
            ThermometerTriggerButtonController.onThermometerEnabled += ThermometerEnabled;
        }

        private void OnDisable() {
            ThermometerTriggerButtonController.onThermometerEnabled -= ThermometerEnabled;
        }

        private void ThermometerEnabled(bool enabled) {
            switch (enabled) {
                case true:
                    transform.DOLocalRotate(new Vector3(0, -90, 0), 0.5f);
                    break;
                case false:
                    transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);
                    break;
            }
        }
        
        private void Awake() {
            m_boxCollider = GetComponent<BoxCollider>();
        }
        
        public void OnClick() {
            if (m_isEquipped) return;
            
            m_isEquipped = true;
            m_boxCollider.enabled = false;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(1.0f, 1f));
            sequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1f));
            transform.SetParent(Camera.transform);
            sequence.Append(transform.DOLocalMove(new Vector3(0.5f, -0.5f, 1f), 1f));
        }

        public void OnLooseClick() { }
    }
}