#if CRAZY_LABS_SDK
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;

namespace BoomGames.Template.SDKHandler
{
    [DisallowMultipleComponent]
    public class CrazyLabsSDKHandler : MonoBehaviour
    {
        void Awake()
        {
            TTPCore.Setup();
            
            DontDestroyOnLoad(this);
        }
        
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
            var levelNo = levelData.LevelNo;

            if (levelNo == 1)
            {
                TTPGameProgression.FirebaseEvents.LevelUp(levelNo, new Dictionary<string, object>());
            }

            TTPGameProgression.FirebaseEvents.MissionStarted(levelNo, new Dictionary<string, object>());
        }

        void OnGameEnd(LevelData levelData)
        {
            var (levelNo, result) = (levelData.LevelNo, levelData.Result);

            switch (result)
            {
                case GameResult.Win:
                    TTPGameProgression.MissionComplete(new Dictionary<string, object>());
                    TTPGameProgression.FirebaseEvents.LevelUp(levelNo + 1, new Dictionary<string, object>());
                    break;
                case GameResult.Lose:
                    TTPGameProgression.FirebaseEvents.MissionFailed(new Dictionary<string, object>());
                    break;
            }
        }
    }
}
#endif