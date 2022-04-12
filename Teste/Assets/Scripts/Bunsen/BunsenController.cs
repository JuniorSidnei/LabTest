using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LabTest.Cubes;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class BunsenController : MonoBehaviour, IClickable {

        public Transform ObjectHolder;
        public ParticleSystem BunsenFire;
        public float EmissionTime;
        private bool m_isObjectPlaced;

        private Animator m_animator;
        private bool m_isBunsenOn;
        private float m_elapsedTime;
        
        public bool IsBunsenOccupied => m_isObjectPlaced;
        public bool IsBunsenOn => m_isBunsenOn;
        
        private void OnEnable() {
            CubeManager.onObjectPlacedInBunsen += ObjectPlacedInBunsen;
        }

        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        private void Update() {
            if (!m_isBunsenOn) return;
            
            m_elapsedTime += Time.deltaTime;
            var percentAmount = m_elapsedTime / EmissionTime;
            var emission = BunsenFire.emission;
            emission.rateOverTime = Mathf.Lerp(0, 50, percentAmount);
        }

        private void ObjectPlacedInBunsen(bool isObjectPlaced) {
            m_isObjectPlaced = isObjectPlaced;
        }
        
        public void OnClick() {
            if (m_isBunsenOn) { 
                BunsenEnabled(false);
                BunsenFire.Stop();
                var emission = BunsenFire.emission;
                emission.rateOverTime = 0;
                m_elapsedTime = 0;
                return;
            }
            
            BunsenEnabled(true);
            BunsenFire.Play();
        }

        public void OnLooseClick() { }

        private void BunsenEnabled(bool enabled) {
            m_isBunsenOn = enabled;
            m_animator.SetBool("bunsen_on", m_isBunsenOn);
        }
    }
}