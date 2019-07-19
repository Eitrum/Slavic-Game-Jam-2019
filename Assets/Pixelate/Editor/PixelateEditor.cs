using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pixelate))]
public class PixelateInspector : Editor {
    public override void OnInspectorGUI() {
        SerializedObject so = new SerializedObject(target);
        var pixelate = (Pixelate)target;
        pixelate.PixelWidth = EditorGUILayout.IntSlider("Pixel Width", pixelate.PixelWidth, 10, 1920);
        if(so.hasModifiedProperties) {
            so.ApplyModifiedProperties();
        }
    }
}