using Architecture_Base.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets._Project.Upgrade
{
    public class UpgradeController : Controller
    {
        private readonly GameConfigLoader _configLoader;
        private readonly Player _player;
        private readonly UpgradePopup _popup;
        private readonly List<StatUpgrader> _upgraders = new();

        public UpgradeController(GameConfigLoader configLoader, Player player, UpgradePopup popup) 
        {
            _configLoader = configLoader;
            _player = player;
            _popup = popup;
        }

        public override async Task InitializeAsync()
        {
            GameConfig config = await _configLoader.LoadAsync();
            _upgraders.AddRange(config.GetStatUpgraders(_player));
            await _popup.InitializeAsync(_upgraders);
        }

        protected override void OnEnable() => _popup.OnUpgrade += OnUpgrade;

        private void OnUpgrade(string title)
        {
            StatUpgrader upgrader = _upgraders.SingleOrDefault(upgrader => upgrader.Title == title);

            if (upgrader != null)
            {
                _player.Money -= (ulong)upgrader.Price;
                upgrader.Upgrade(_player);
            }
        }

        public override void FixedTick()
        {
            _upgraders
                .ForEach(upgrader => _popup
                .SwitchInteraction(upgrader.Title, _player.Money >= upgrader.Price));
        }

        protected override void OnDisable() => _popup.OnUpgrade -= OnUpgrade;
    }
}
