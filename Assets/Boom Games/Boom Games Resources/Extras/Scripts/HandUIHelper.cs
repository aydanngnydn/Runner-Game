using UnityEngine;

namespace BoomGames.Template.Extras
{
    public class HandUIHelper : MonoBehaviour
    {
        [SerializeField] bool _active;
        [SerializeField] RectTransform _handRectTransform;

        GameObject _go;
        GameObject HandGo
        {
            get
            {
                if (!_go)
                    _go = _handRectTransform.gameObject;

                return _go;
            }
        }
        
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (_active) return;
                if (HandGo.activeSelf)
                    HandGo.SetActive(false);
            }
        }

        void LateUpdate()
        {
            if (!Active)
            {
                if (HandGo.activeSelf)
                    HandGo.SetActive(false);
                return;
            }
#if ENABLE_LEGACY_INPUT_MANAGER
            if (Input.GetMouseButtonDown(0))
            {
                _handRectTransform.position = Input.mousePosition;
                HandGo.SetActive(true);
            }
            else if (Input.GetMouseButton(0)) _handRectTransform.position = Input.mousePosition;
            else if (Input.GetMouseButtonUp(0)) HandGo.SetActive(false);
#endif
        }
    }
}