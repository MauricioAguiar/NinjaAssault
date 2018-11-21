using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiTextureCam :MonoBehaviour {

        public Transform target; 
        
        private void Update() {        
    
        var wantedPos = Camera.main.WorldToViewportPoint(target.position);
        transform.position = wantedPos;
    }
}
