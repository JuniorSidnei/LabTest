using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LabTest.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LabTest.Controllers {
    
    public class ThermometerController : MonoBehaviour, IClickable {

        public GameObject ThermometerPanel;
        public Camera Camera;
        private bool m_isEquipped;
        private BoxCollider m_boxCollider;

        private void Awake() {
            m_boxCollider = GetComponent<BoxCollider>();
        }

        public void OnClick() {
            if (m_isEquipped) return;
            
            m_isEquipped = true;
            m_boxCollider.enabled = false;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(1f, 0.5f));
            sequence.Append(transform.DOLocalRotate(new Vector3(0f, -90, 0f), 0.5f));
            transform.SetParent(Camera.transform);
            sequence.Append(transform.DOLocalMove(new Vector3(0.5f, -0.5f, 1f), 0.5f));
            sequence.OnComplete(() => {
                ThermometerPanel.SetActive(true);
            });
        }

        public void OnLooseClick() { }
    }
}