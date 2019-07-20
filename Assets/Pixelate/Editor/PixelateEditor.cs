using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pixelate))]
public class PixelateInspector : Editor {
    public override void OnInspectorGUI() {
        var so = serializedObject;

        var pixelate = (Pixelate)target;
        var oldPixelWidth = pixelate.PixelWidth;
         var newPixelWidth = EditorGUILayout.IntSlider("Pixel Width", oldPixelWidth, 10, 1920);

        if(newPixelWidth != oldPixelWidth) {
            so.FindProperty("pixelWidth").intValue = newPixelWidth;
            so.ApplyModifiedProperties();
            so.Update();
            pixelate.PixelWidth = newPixelWidth;
        }
    }
}