﻿using UnityEngine;

namespace PreciseEditor
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class PreciseEditor : MonoBehaviour
    {
        public static PreciseEditor Instance { get; private set; }

        public PartEditionWindow partEditionWindow = null;
        public VesselWindow vesselWindow = null;

        public void Start()
        {
            Instance = this;
            partEditionWindow = this.gameObject.AddComponent<PartEditionWindow>();
            vesselWindow = this.gameObject.AddComponent<VesselWindow>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Part part = this.GetPartUnderCursor();
                if (part)
                {
                    partEditionWindow.Show(part);
                }
            }
        }

        private Part GetPartUnderCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            EditorLogic editorLogic = EditorLogic.fetch;
            if (editorLogic && Physics.Raycast(ray, out RaycastHit rayCastHit))
            {
                return editorLogic.ship.Parts.Find(p => p.gameObject == rayCastHit.transform.gameObject);
            }
            return null;
        }
    }
}