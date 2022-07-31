#if AZUR_SDK
using System.Collections.Generic;
using UnityEngine;

namespace BoomGames.Template.SDKHandler
{
    [DisallowMultipleComponent]
    public class AzurSDKHandler : MonoBehaviour
    {
        void Awake() => DontDestroyOnLoad(this);

        void OnEnable() => Game.LevelData.StateChanged += OnLevelStateChange;

        void OnDisable() => Game.LevelData.StateChanged -= OnLevelStateChange;

        void OnLevelStateChange(LevelState state, LevelData levelData)
        {
            switch (state)
            {
                case LevelState.Started:
                    OnGameStart(levelData);
                    break;
                case LevelState.Ended:
                    OnGameEnd(levelData);
                    break;
            }
        }

        void OnGameStart(LevelData levelData)
        {
            var data = new Dictionary<string, object> {["level"] = levelData.LevelNo};
            AppMetrica.Instance.ReportEvent("level_start", data);
        }

        void OnGameEnd(LevelData levelData)
        {
            var data = new Dictionary<string, object>
            {
                ["level"] = levelData.LevelNo,
                ["progress"] = levelData.Progress,
                ["result"] = levelData.Result,
                ["time"] = levelData.Playtime
            };
            
            AppMetrica.Instance.ReportEvent("level_finish", data);
        }
    }
}
#endif