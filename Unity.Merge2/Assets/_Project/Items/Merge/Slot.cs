using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Project.Items.Merge
{
    public class Slot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public event Action<Slot> OnTake;
        public event Action<Slot> OnEndTake;
        public event Action<Slot> OnPut;

        [SerializeField] private Image _itemIcon;
        private Canvas _canvas;
        private Image _dragIcon;
        private bool _isDrag;

        public Sprite Sprite 
        {
            get => _itemIcon.sprite;

            set
            {
                if (_isDrag)
                    return;

                _itemIcon.sprite = value;
                _itemIcon.gameObject.SetActive(value != null);
            }
        }

        public void Construct(Canvas canvas, Image dragIcon)
        {
            _canvas = canvas;
            _dragIcon = dragIcon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Down " + transform.GetSiblingIndex());
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Sprite == null)
                return;

            _isDrag = true;
            _itemIcon.gameObject.SetActive(false);
            _dragIcon.rectTransform.position = eventData.position;
            _dragIcon.sprite = Sprite;
            _dragIcon.gameObject.SetActive(true);
            OnTake?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Sprite == null)
                return;

            _dragIcon.rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDrag = false;
            _dragIcon.gameObject.SetActive(false);
            OnEndTake?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnPut?.Invoke(this);
        }
    }
}