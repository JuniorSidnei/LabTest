using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LabTest.Controllers;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Cubes {
    
    public class CubeManager : MonoBehaviour, IClickable {

        public enum CubeState {
            Resting, OnBunsen, MovingToBunsen
        }

        public BunsenController BunsenController;
        public CubeState State;

        [SerializeField] private float m_initialTemperature;
        [SerializeField] private float m_constantCA;
        [SerializeField] private float m_constantCR;
        [SerializeField] private float m_minimumTemperature;
        [SerializeField] private float m_maximumTemperature;
        private Vector3 m_originalPosition;

        private TemperatureController m_temperatureController;
        
        public delegate void OnObjectPlacedInBunsen(bool isObjectPlaced);
        public static event OnObjectPlacedInBunsen onObjectPlacedInBunsen;
        
        private void Awake() {
            m_originalPosition = transform.position;
            m_temperatureController = GetComponent<TemperatureController>();
            m_temperatureController.SetBunsen(BunsenController);
            m_temperatureController.SetInitialTemperatureAndConstants(m_initialTemperature, m_constantCA, m_constantCR, m_minimumTemperature, m_maximumTemperature);
        }

        public void OnClick() {
            var Sequence = DOTween.Sequence();
            
            switch (State) {
                case CubeState.MovingToBunsen:
                    return;
                case CubeState.OnBunsen:
                    State = CubeState.MovingToBunsen;
                    Sequence.Append(transform.DOMoveX(m_originalPosition.x, 1.0f));
                    Sequence.Append(transform.DOMoveY(m_originalPosition.y, 1.0f));
                    Sequence.OnComplete(() => {
                        transform.DOMoveZ(m_originalPosition.z, 0.1f);
                        State = CubeState.Resting;
                        onObjectPlacedInBunsen?.Invoke(false);
                    });
                    break;
                default:
                    //maybe show a warning message?
                    if (BunsenController.IsBunsenOccupied) return;
                    
                    onObjectPlacedInBunsen?.Invoke(true);
                    State = CubeState.MovingToBunsen;
                    var bunsenHolder = BunsenController.ObjectHolder.position;
                    Sequence.Append(transform.DOMoveY(bunsenHolder.y, 1.0f));
                    Sequence.Append(transform.DOMoveX(bunsenHolder.x, 1.0f));
                    Sequence.OnComplete(() => {
                        transform.DOMoveZ(bunsenHolder.z, 0.1f);
                        State = CubeState.OnBunsen;
                    });
                    break;
            }
        }

        public void OnLooseClick() { }
    }
}