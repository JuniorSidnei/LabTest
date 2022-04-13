using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LabTest.Cubes;
using LabTest.Interface;
using LabTest.Managers;
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
            if (m_isBunsenOn) {
                AnimateBunsenParticle(0, 50);
            }
            else {
                AnimateBunsenParticle(50, 0);
            }
        }

        private void ObjectPlacedInBunsen(bool isObjectPlaced) {
            m_isObjectPlaced = isObjectPlaced;
        }
        
        public void OnClick() {
            if (!GameManager.Instance.IsEPIEquipped) return;
            
            if (m_isBunsenOn) { 
                BunsenEnabled(false);
                m_elapsedTime = 0;
                return;
            }

            m_elapsedTime = 0;
            BunsenEnabled(true);
            BunsenFire.Play();
        }

        public void OnLooseClick() { }

        public void OnFinishedClose() {
            BunsenFire.Stop();
        }

        private void BunsenEnabled(bool enabled) {
            m_isBunsenOn = enabled;
            m_animator.SetBool("bunsen_on", m_isBunsenOn);
        }

        private void AnimateBunsenParticle(float initial, float final) {
            m_elapsedTime += Time.deltaTime;
            var percentAmount = m_elapsedTime / EmissionTime;
            var emission = BunsenFire.emission;
            emission.rateOverTime = Mathf.Lerp(initial, final, percentAmount);
        }
    }
}