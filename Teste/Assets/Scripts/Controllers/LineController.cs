using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LabTest.Controllers {
    
    public class LineController : MonoBehaviour {
        
        private LineRenderer m_lineRend;
        private List<Transform> m_points = new List<Transform>();

        private void Awake() {
            m_lineRend = GetComponent<LineRenderer>();
        }

        private void Update() {
            for (var i = 0; i < m_points.Count; i++) {
                m_lineRend.SetPosition(i, m_points[i].position);
            }
        }

        public void SetupLine(List<Transform> points) {
            m_lineRend.positionCount = points.Count;
            m_points = points;
        }

        public void ClearLine() {
            m_lineRend.positionCount = 0;
            m_points = new List<Transform>();
        }
    }
}