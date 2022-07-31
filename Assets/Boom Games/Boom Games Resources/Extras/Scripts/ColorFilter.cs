using System.IO;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace BoomGames.Template.Shaders
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    [AddComponentMenu("Boom Games/Color Filter Camera")]
    public class ColorFilter : MonoBehaviour
    {
#if UNITY_EDITOR
        static readonly string MaterialPath =
            Path.Combine(Strings.GetAssetsPath(Strings.BoomGamesResourcesPath),
                "Extras/Shaders/Color Filter/ColorFilter.mat");
#endif
        static readonly int TintID = Shader.PropertyToID("_Tint");
        static readonly int TintAmountID = Shader.PropertyToID("_TintAmount");
        static readonly int SaturationID = Shader.PropertyToID("_Saturation");
        static readonly int BrightnessID = Shader.PropertyToID("_Value");

        [SerializeField, HideInInspector] Material material;
        [SerializeField] Color tint;
        [SerializeField, Range(0, 2)] float saturation = 1;
        [SerializeField, Range(0, 2)] float brightness = 1;

        public Color Tint
        {
            get => tint;
            set => tint = value;
        }

        public float Saturation
        {
            get => saturation;
            set => saturation = Mathf.Clamp(value, 0, 2);
        }

        public float Brightness
        {
            get => brightness;
            set => brightness = Mathf.Clamp(value, 0, 2);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            material.SetColor(TintID, tint);
            material.SetFloat(TintAmountID, tint.a);
            material.SetFloat(SaturationID, saturation);
            material.SetFloat(BrightnessID, brightness);
            Graphics.Blit(source, destination, material);
        }

#if UNITY_EDITOR
        void Reset()
        {
            material = AssetDatabase.LoadAssetAtPath<Material>(MaterialPath);

            if (!material)
                Debug.LogError("Material not found at path!");
            else if (!material.shader)
                Debug.LogError("Shader not found!");
            else if (!material.shader.isSupported)
                Debug.LogError("Shader not supported!");
        }
#endif
    }
}