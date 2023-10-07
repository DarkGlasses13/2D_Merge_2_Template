using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.UI_Helpers
{
    public class GridScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private Vector2 _cellSize, _spacing;

        private void OnValidate() => UpdateScale();

        [ContextMenu("Update Scale")]
        public void UpdateScale()
        {
            if (_rectTransform == null || _gridLayoutGroup == null)
                return;

            float scaleFactor = _rectTransform.rect.width / _rectTransform.rect.height;
            _gridLayoutGroup.cellSize = _cellSize / scaleFactor;
            _gridLayoutGroup.spacing = _spacing / scaleFactor;
        }
    }
}