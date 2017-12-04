using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ApplyTorchEffect : MonoBehaviour
{

    public Material shader;
    public Camera cam;
    public Transform playerTransform;

    // used for configuring the flash light apperance
    public float[] flashLightRadius = new float[] { 100f, 100f };
    public float[] flashLightBorder = new float[] { 0f, 0f };
    public Vector2[] flashLightSkew = new Vector2[] { new Vector2(), new Vector2() };
    public Vector2[] flashLightOffset = new Vector2[] { new Vector2(), new Vector2() };
    public float[] flashLightStrength = new float[] { 1f, 1f };

    // the actual data
    private float[] lightRadius = new float[] { 0f, 0f };
    private Vector4[] lightSkew = new Vector4[] { new Vector4(), new Vector4() };
    private Vector4[] lightOffset = new Vector4[] { new Vector4(), new Vector4() };
    private float[] lightBorder = new float[] { 0f, 0f };

    public float radius = 1f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        lightRadius[0] = flashLightRadius[0] * radius;
        lightRadius[1] = flashLightRadius[1] * radius;

        lightSkew[0] = flashLightSkew[0];
        lightSkew[1] = flashLightSkew[1];

        lightOffset[0] = flashLightOffset[0] * radius;
        lightOffset[1] = flashLightOffset[1] * radius;

        float screenPixelsPerUnit = (cam.WorldToScreenPoint(Vector3.one) -
            cam.WorldToScreenPoint(Vector3.zero)).magnitude;

        lightBorder[0] = flashLightBorder[0] * screenPixelsPerUnit;
        lightBorder[1] = flashLightBorder[1] * screenPixelsPerUnit;

        var projPos = cam.WorldToScreenPoint(playerTransform.position);
        shader.SetVector("_PlayerPos", new Vector2(projPos.x, projPos.y));
        shader.SetVector("_CamSize", new Vector2(cam.pixelWidth, cam.pixelHeight));

        shader.SetFloatArray("_BrightRadius", lightRadius);
        shader.SetFloatArray("_Border", lightBorder);
        shader.SetVectorArray("_Skew", lightSkew);
        shader.SetVectorArray("_Offset", lightOffset);
        shader.SetFloatArray("_Strength", flashLightStrength);
        Graphics.Blit(source, destination, shader);
    }
}
