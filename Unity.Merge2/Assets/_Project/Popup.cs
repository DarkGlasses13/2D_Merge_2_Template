using Architecture_Base.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project
{
    public abstract class Popup : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _closeButton;
        private IUIElement _showHidePattern;

        public void Construct(IUIElement showHidePattern)
        {
            _showHidePattern = showHidePattern;
        }

        public void Show(Action callback = null)
        {
            if (gameObject.activeInHierarchy)
                return;

            if (_showHidePattern != null)
            {
                _showHidePattern.Show(callback);
                return;
            }

            gameObject.SetActive(true);
            callback?.Invoke();
        }

        protected virtual void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButtonClicked);
        }

        private void CloseButtonClicked() => Hide();

        public void Hide(Action callback = null)
        {
            if (gameObject.activeInHierarchy == false)
                return;

            if (_showHidePattern != null)
            {
                _showHidePattern.Hide(callback);
                return;
            }

            gameObject.SetActive(false);
            callback?.Invoke();
        }

        protected virtual void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButtonClicked);
        }
    }
}
