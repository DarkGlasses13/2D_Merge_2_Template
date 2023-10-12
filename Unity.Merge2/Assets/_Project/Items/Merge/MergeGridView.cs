using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Items.Merge
{
    public class MergeGridView : MonoBehaviour
    {
        public event Action<int> OnTake;
        public event Action<int> OnEndTake;
        public event Action<int> OnPut;

        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Image _dragItemIcon;
        [SerializeField] private GridLayoutGroup _layoutGroup;
        private Slot[] _slots;

        public int Capacity => _layoutGroup.transform.childCount;

        private void OnValidate() => UpdateDragingItemIconSize();

        private void Awake()
        {
            _slots = _layoutGroup.GetComponentsInChildren<Slot>();
            Array.ForEach(_slots, slot => slot.Construct(_mainCanvas, _dragItemIcon));
            UpdateDragingItemIconSize();
        }

        private void OnEnable()
        {
            foreach (Slot slot in _slots)
            {
                slot.OnTake += Take;
                slot.OnPut += Put;
                slot.OnEndTake += EndTake;
            }
        }

        private void Take(Slot slot) => OnTake?.Invoke(slot.transform.GetSiblingIndex());

        private void Put(Slot slot) => OnPut?.Invoke(slot.transform.GetSiblingIndex());

        private void EndTake(Slot slot) => OnEndTake?.Invoke(slot.transform.GetSiblingIndex());

        public void Draw(IReadOnlyCollection<Item> items)
        {
            for (int i = 0; i < Capacity; i++)
            {
                _slots[i].Sprite = items.ElementAt(i)?.Sprite;
            }
        }

        private void UpdateDragingItemIconSize()
        {
            if (_dragItemIcon && _layoutGroup)
            {
                _dragItemIcon.rectTransform.sizeDelta = _layoutGroup.cellSize;
            }
        }

        private void OnDisable()
        {
            foreach (Slot slot in _slots)
            {
                slot.OnTake -= Take;
                slot.OnPut -= Put;
                slot.OnEndTake -= EndTake;
            }
        }
    }
}