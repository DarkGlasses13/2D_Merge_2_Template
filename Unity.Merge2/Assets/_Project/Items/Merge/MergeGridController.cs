using Architecture_Base.Core;
using Assets._Project.Upgrade;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets._Project.Items.Merge
{
    public class MergeGridController : Controller
    {
        private readonly MergeGrid _model;
        private readonly MergeGridView _view;
        private readonly ItemBase _itemBase;
        private readonly GameConfigLoader _configLoader;
        private readonly UpgradeController _upgradeController;
        private readonly Player _player;
        private int _fromSlot = -1, _toSlot = -1;
        private GameConfig _config;

        public MergeGridController(MergeGrid model, MergeGridView view,
            ItemBase itemBase, GameConfigLoader configLoader,
            UpgradeController upgradeController, Player player)
        {
            _model = model;
            _view = view;
            _itemBase = itemBase;
            _configLoader = configLoader;
            _upgradeController = upgradeController;
            _player = player;
        }

        public override async Task InitializeAsync()
        {
            _config = await _configLoader.LoadAsync();
        }

        protected override void OnEnable()
        {
            _model.OnChanged += OnChanged;
            _view.OnTake += OnTake;
            _view.OnEndTake += OnEndTake;
            _view.OnPut += OnPut;
            _upgradeController.OnUpgrade += OnUpgrade;
            _view.Draw(_model.Items);
        }

        private void OnUpgrade()
        {
            for (int i = 0; i < _model.Items.Count; i++)
            {
                if (_model.Items.ElementAt(i) != null)
                {
                    if (_model.Items.ElementAt(i).MergeLevel < _player.SpawnItemMergeLevel)
                    {
                        _model.Remove(i);
                        _model.Put(i, _itemBase.GetByMergeLevel(_player.SpawnItemMergeLevel));
                    }
                }
            }
        }

        public void OnEarn()
        {
            List<ulong> earns = new();

            foreach (var item in _model.Items)
            {
                earns.Add(item != null 
                    ? _config.GetEarn(_itemBase.Count, item.MergeLevel) 
                    : 0);
            }
            
            _view.ShowEarn(earns);
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
            _upgradeController.OnUpgrade -= OnUpgrade;
        }
    }
}
