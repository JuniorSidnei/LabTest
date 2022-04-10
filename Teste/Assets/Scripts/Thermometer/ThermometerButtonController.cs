using System;
using System.Collections;
using System.Collections.Generic;
using LabTest.Interface;
using UnityEngine;

namespace LabTest.Controllers
{


    public class ThermometerButtonController : MonoBehaviour, IClickable
    {
        private Animator m_animator;

        private void Awake() {
            m_animator = GetComponent<Animator>();
        }

        public void OnClick()
        {
            m_animator.SetBool("pressed", true);
        }

        public void OnLooseClick()
        {
            m_animator.SetBool("pressed", false);
        }
    }
}