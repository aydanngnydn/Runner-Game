#if MOONEE_SDK
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

namespace BoomGames.Template.SDKHandler
{
    [DisallowMultipleComponent]
    public class MooneeSDKHandler : MonoBehaviour
    {
        void Awake()
        {
            FB.Init();
            GameAnalytics.Initialize();

            DontDestroyOnLoad(this);
        }
    }
}
#endif