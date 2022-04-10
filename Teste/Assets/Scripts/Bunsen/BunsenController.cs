using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Cubes;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class BunsenController : MonoBehaviour, IClickable
    {

        public Transform ObjectHolder;
        private bool m_isObjectPlaced;

        public bool IsBunsenOccupied => m_isObjectPlaced;
        
        private void OnEnable() {
            CubeManager.onObjectPlacedInBunsen += ObjectPlacedInBunsen;
        }

        private void ObjectPlacedInBunsen(bool isObjectPlaced) {
            m_isObjectPlaced = isObjectPlaced;
        }
        
        public void OnClick()
        {
            Debug.Log("click bunsen");
        }

        public void OnLooseClick() { }
    }
}