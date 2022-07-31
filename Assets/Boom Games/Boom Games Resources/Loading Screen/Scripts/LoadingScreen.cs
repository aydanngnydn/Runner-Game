using BoomGames.Template;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoomGames
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] string loadingText = "Loading...";

        void OnEnable()
        {
            SceneHandler.LoadingProgress.ProgressChanged += OnProgressChanged;
        }

        void OnDisable()
        {
            SceneHandler.LoadingProgress.ProgressChanged -= OnProgressChanged;
        }

        void OnProgressChanged(object sender, int progress)
        {
            slider.value = progress / 100f;
            text.SetText($"{loadingText}{progress}%");
        }
    }
}