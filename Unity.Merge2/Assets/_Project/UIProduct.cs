using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project
{
    public class UIProduct : MonoBehaviour
    {
        public event Action<string> OnBuyButtonClick;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _title, _description, _price;
        [SerializeField] private Image _icon;

        public string Title { get => _title.text; set => _title.text = value; }
        public string Price { get => _price.text; set => _price.text = value; }
        public string Description { get => _description.text; set => _description.text = value; }
        public Sprite Icon { get => _icon.sprite; set => _icon.sprite = value; }
        public bool IsInteractable { get => _button.interactable; set => _button.interactable = value; }

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnClick() => OnBuyButtonClick?.Invoke(Title);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);
    }
}