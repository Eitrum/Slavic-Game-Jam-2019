using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example {
    public class PixelateRotateExample : MonoBehaviour {

        public Vector3 rotationSpeed = new Vector3(0, 45, 0);

        void Update() {
            this.transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
        }

    }
}