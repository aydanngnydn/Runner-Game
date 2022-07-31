using BoomGames.Template.Editor;
using UnityEditor;

namespace BoomGames.SDKHandler
{
    public static class SDKHandlerDefine
    {
        [InitializeOnLoadMethod]
        static void Init()
        {
            EditorApplication.delayCall += () => DefineHelper.AddDefine("SDK_HANDLER");
        }
    }
}