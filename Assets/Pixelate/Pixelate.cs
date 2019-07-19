using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Pixelate : MonoBehaviour {

    [SerializeField] private int pixelWidth = 192;

    private Material material = null;
    private int widthId;
    private int heightId;

    public int PixelWidth {
        get => pixelWidth;
        set {
            pixelWidth = Mathf.Clamp(value, 10, Screen.width);
            UpdateMaterial();
        }
    }

    void Awake() {
        material = new Material(Shader.Find("Hidden/PixelateShader"));
        widthId = Shader.PropertyToID("_Width");
        heightId = Shader.PropertyToID("_Height");
        UpdateMaterial();
    }

    void OnDestroy() {
        if(material) {
            Destroy(material);
        }
    }

    private void UpdateMaterial() {
        if(!material)
            return;
        var width = Screen.width;
        var height = Screen.height;
        var ratio = (float)width / (float)height;
        material.SetFloat(widthId, pixelWidth);
        material.SetFloat(heightId, pixelWidth * ratio);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src, dst, material);
    }
}