using UnityEngine;

public class GameBoyFilter : MonoBehaviour 
{
    private RenderTexture _downscaledRenderTexture;
    [SerializeField] int height = 144;
    public Material identityMaterial;
    public Material gameboyMaterial;



    private void OnEnable()
    {
        // To reduce screen size, made like this to preserve aspect. height is editable in editor
        Camera camera = GetComponent<Camera>();
        int width     = Mathf.RoundToInt(camera.aspect * height);
        _downscaledRenderTexture = new RenderTexture(width, height, 16);
        _downscaledRenderTexture.filterMode = FilterMode.Point;
    }

    private void OnDisable()
    {
        Destroy(_downscaledRenderTexture);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, _downscaledRenderTexture, gameboyMaterial);
        Graphics.Blit(_downscaledRenderTexture, dst, identityMaterial);
    }
}