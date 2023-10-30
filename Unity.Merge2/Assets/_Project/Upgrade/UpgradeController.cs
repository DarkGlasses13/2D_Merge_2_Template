using Architecture_Base.Core;
using Assets._Project.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets._Project.Upgrade
{
    public class UpgradeController : Controller
    {
        public event Action OnUpgrade;

        private readonly GameConfigLoader _configLoader;
        private readonly Player _player;
        private readonly UpgradePopup _popup;
        private readonly MoneyUICounter _moneyCounter;
        private readonly List<StatUpgrader> _upgraders = new();

        public UpgradeController(GameConfigLoader configLoader, Player player,
            UpgradePopup popup, MoneyUICounter moneyCounter) 
        {
            _configLoader = configLoader;
            _player = player;
            _popup = popup;
            _moneyCounter = moneyCounter;
        }

        public override async Task InitializeAsync()
        {
            GameConfig config = await _configLoader.LoadAsync();
            _upgraders.AddRange(config.GetStatUpgraders(_player));
            await _popup.InitializeAsync(_upgraders);
        }

        protected override void OnEnable() => _popup.OnUpgrade += Upgrade;

        private void Upgrade(string title)
        {
            StatUpgrader upgrader = _upgraders.SingleOrDefault(upgrader => upgrader.Title == title);

            if (upgrader != null)
            {
                _player.Money -= (ulong)upgrader.Price;
                _moneyCounter.Set(_player.Money);
                upgrader.Upgrade(_player);
                OnUpgrade?.Invoke();
            }
        }

        public override void Tick()
        {
            _upgraders
                .ForEach(upgrader => _popup
                .UpdateData(upgrader.Title, upgrader.Price, _player.Money >= upgrader.Price));
        }

        protected override void OnDisable() => _popup.OnUpgrade -= Upgrade;
    }
}
