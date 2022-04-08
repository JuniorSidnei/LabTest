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
            Resting, OnBunsen, MovingToPosition
        }

        public Transform BunsenHolder;
        public CubeState State;

        private Vector3 m_originalPosition;

  
        private void Awake() {
            m_originalPosition = transform.position;
        }

        public void OnClick() {
            var Sequence = DOTween.Sequence();
            
            switch (State) {
                case CubeState.MovingToPosition:
                    return;
                case CubeState.OnBunsen:
                    State = CubeState.MovingToPosition;
                    Sequence.Append(transform.DOMoveX(m_originalPosition.x, 1.0f));
                    Sequence.Append(transform.DOMoveY(m_originalPosition.y, 1.0f));
                    Sequence.OnComplete(() => {
                        transform.DOMoveZ(m_originalPosition.z, 0.1f);
                        State = CubeState.Resting;
                    });
                    break;
                default:
                    
                    State = CubeState.MovingToPosition;
                    Sequence.Append(transform.DOMoveY(BunsenHolder.position.y, 1.0f));
                    Sequence.Append(transform.DOMoveX(BunsenHolder.position.x, 1.0f));
                    Sequence.OnComplete(() => {
                        transform.DOMoveZ(BunsenHolder.position.z, 0.1f);
                        State = CubeState.OnBunsen;
                    });
                    break;
            }
        }
    }
}