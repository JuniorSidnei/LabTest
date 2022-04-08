using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class BunsenController : MonoBehaviour, IClickable {
        
        
        public void OnClick()
        {
            Debug.Log("click bunsen");
        }
    }
}