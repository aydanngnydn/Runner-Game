using BoomGames.Template;
using UnityEngine;

namespace BoomGames
{
    public class SplashTimeline : MonoBehaviour
    {
        SceneActivator _activator;

        public void LoadLevel()
        {
            _activator = new SceneActivator();
            SceneHandler.LoadLevel(_activator);
        }
        
        public void ActivateLevel()
        {
            _activator.Activate();
        }
    }
}