#if VOODOO_SDK
using UnityEngine;
using GameAnalyticsSDK;

namespace BoomGames.Template.SDKHandler
{
    public class VoodooSDKHandler : MonoBehaviour
    {
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

        void OnStateChanged(LevelState state, LevelData data)
        {
            switch (state)
            {
                case LevelState.Started:
                    OnGameStart(data);
                    break;
                case LevelState.Ended:
                    OnGameEnd(data);
                    break;
            }
        }

        static void OnGameStart(LevelData data)
        {
            TinySauce.OnGameStarted($"{data.LevelNo}");
        }

        static void OnGameEnd(LevelData data)
        {
            var result = data.Result;
            var levelComplete = result == GameResult.Win;
            var score = data.Score ?? (levelComplete ? 100 : 0);
            var segmentName = $"Level{data.LevelNo}:Attempts";
            var attemptCount = PlayerPrefs.GetInt("AttemptCount", 1);
            
            GameAnalytics.NewDesignEvent(segmentName, attemptCount);
            TinySauce.OnGameFinished(levelComplete, score, $"{data.LevelNo}");
            PlayerPrefs.SetInt("AttemptCount", result == GameResult.Lose ? ++attemptCount : 1);
        }
    }
}
#endif