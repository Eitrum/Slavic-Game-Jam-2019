using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Pixelate : MonoBehaviour {

    [SerializeField] private int pixelWidth = 192;

    private Material material = null;
    private int widthId;
    private int heightId;

    public Material Material {
        get {
            if(!material) {
                material = new Material(Shader.Find("Hidden/PixelateShader"));
                widthId = Shader.PropertyToID("_Width");
                heightId = Shader.PropertyToID("_Height");
            }

            return material;
        }
    }

    public int PixelWidth {
        get => pixelWidth;
        set {
            pixelWidth = value;
            UpdateMaterial();
        }
    }

    void Awake() {
        UpdateMaterial();
    }

    ~Pixelate() {
        if(material) {
            DestroyImmediate(material);
        }
    }

    void OnDestroy() {
        if(material) {
            DestroyImmediate(material);
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
#if UNITY_EDITOR
        UpdateMaterial();
#endif
        Graphics.Blit(src, dst, Material);
    }
}