#if ZPLAY_SDK
using System.Collections.Generic;
using System.Globalization;
using Facebook.Unity;
using UnityEngine;

namespace BoomGames.Template.SDKHandler
{
    public class ZplaySDKHandler : MonoBehaviour
    {
        [SerializeField] string FLURRY_API_KEY;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void OnEnable()
        {
            Game.LevelData.StateChanged += OnStateChanged;
        }

        void OnDisable()
        {
            Game.LevelData.StateChanged -= OnStateChanged;
        }
    
        void Start()
        {
            if (!FB.IsInitialized) FB.Init();

            // Initialize Flurry.
            new Flurry.Builder()
                .WithCrashReporting()
                .WithLogEnabled()
                .WithLogLevel(Flurry.LogLevel.VERBOSE)
                .WithMessaging()
                .Build(FLURRY_API_KEY);
        }
    
        void OnStateChanged(LevelState state, LevelData data)
        {
            if (state != LevelState.Started && state != LevelState.Ended) return;
            var dic = new Dictionary<string, string>
            {
                { "Level", data.LevelNo.ToString() }
            };

            switch (state)
            {
                case LevelState.Started:
                    Flurry.LogEvent("Level_start", dic);
                    break;
                case LevelState.Ended:
                    Flurry.LogEvent(data.Result == GameResult.Win ? "Level_finished" : "Level_fail", dic);
                    dic.Add("Length", data.Playtime.ToString(CultureInfo.InvariantCulture));
                    Flurry.LogEvent("Level_length", dic);
                    break;
            }
        }
    }
}
#endif