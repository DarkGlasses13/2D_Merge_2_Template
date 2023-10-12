using Architecture_Base.Core;
using System.Linq;

namespace Assets._Project.Items.Merge
{
    public class MergeGridController : Controller
    {
        private readonly MergeGrid _model;
        private readonly MergeGridView _view;
        private readonly ItemBase _itemBase;
        private int _fromSlot = -1, _toSlot = -1;

        public MergeGridController(MergeGrid model, MergeGridView view, ItemBase itemBase)
        {
            _model = model;
            _view = view;
            _itemBase = itemBase;
        }

        protected override void OnEnable()
        {
            _model.OnChanged += OnChanged;
            _view.OnTake += OnTake;
            _view.OnEndTake += OnEndTake;
            _view.OnPut += OnPut;
            _view.Draw(_model.Items);
        }

        private void OnTake(int from) => _fromSlot = from;

        private void OnPut(int to) => _toSlot = to;

        private void OnEndTake(int from)
        {
            if (_fromSlot == -1)
            {
                _toSlot = -1;
                _view.Draw(_model.Items);
                return;
            }

            if (_toSlot == -1)
            {
                _fromSlot = -1;
                _view.Draw(_model.Items);
                return;
            }

            if (_fromSlot == _toSlot)
            {
                _fromSlot = -1;
                _toSlot = -1;
                _view.Draw(_model.Items);
                return;
            }

            Item fromItem = _model.Items.ElementAt(_fromSlot);
            Item toItem = _model.Items.ElementAt(_toSlot);

            if (fromItem != null)
            {
                if (toItem != null)
                {
                    if (fromItem.Sprite.name == toItem.Sprite.name)
                    {
                        _model.Remove(_toSlot);
                        _model.Put(_toSlot, _itemBase.GetNewBySpriteName(fromItem.MergeResult.name));
                        _model.Remove(_fromSlot);
                        _fromSlot = -1;
                        _toSlot = -1;
                        _view.Draw(_model.Items);
                        return;
                    }
                }

                _model.Swap(_fromSlot, _toSlot);
                _fromSlot = -1;
                _toSlot = -1;
                _view.Draw(_model.Items);
            }
            else
            {
                _fromSlot = -1;
                _toSlot = -1;
                _view.Draw(_model.Items);
                return;
            }
        }

        private void OnChanged() => _view.Draw(_model.Items);

        protected override void OnDisable()
        {
            _model.OnChanged -= OnChanged;
            _view.OnTake -= OnTake;
            _view.OnEndTake -= OnEndTake;
            _view.OnPut -= OnPut;
        }
    }
}
