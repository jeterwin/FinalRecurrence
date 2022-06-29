using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheckController : MonoBehaviour
{
    public RenderTexture lightCheckTexture;
    public float LightLevel;
    public int Light;
    private void FixedUpdate()
    {
        RenderTexture tmpTexture = RenderTexture.GetTemporary(lightCheckTexture.width, lightCheckTexture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(lightCheckTexture, tmpTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmpTexture;

        Texture2D temp2DTexture = new Texture2D(lightCheckTexture.width, lightCheckTexture.height);
        temp2DTexture.ReadPixels(new Rect(0, 0, tmpTexture.width, tmpTexture.height), 0 , 0);
        temp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmpTexture);

        Color32[] colors = temp2DTexture.GetPixels32();
        Destroy(temp2DTexture);
        LightLevel = 0;
        for(int i = 0; i < colors.Length; i++)
        {
            LightLevel += (0.2126f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b);
        }
        LightLevel -= 259330;
        LightLevel = LightLevel / colors.Length;
        Light = Mathf.RoundToInt(LightLevel);
    }
}
