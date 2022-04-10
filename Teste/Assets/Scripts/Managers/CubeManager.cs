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

        private Vector3 m_originalPosition;

        public delegate void OnObjectPlacedInBunsen(bool isObjectPlaced);
        public static event OnObjectPlacedInBunsen onObjectPlacedInBunsen;
        
        private void Awake() {
            m_originalPosition = transform.position;
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