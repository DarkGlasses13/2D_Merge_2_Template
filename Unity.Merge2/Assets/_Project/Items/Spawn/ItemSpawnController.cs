using Architecture_Base.Core;
using Assets._Project;
using Assets._Project.Items;
using Assets._Project.Items.Merge;
using Assets._Project.Money;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawnController : Controller
{
    private readonly ItemBase _itemBase;
    private readonly GameConfigLoader _configLoader;
    private readonly Player _player;
    private readonly MergeGrid _mergeGrid;
    private readonly Slider _cooldownBar;
    private readonly ResourceEventController _resourceController;
    private GameConfig _config;
    private float _cooldown;

    public ItemSpawnController(ItemBase itemBase, GameConfigLoader configLoader,
        Player player, MergeGrid itemField, Slider cooldownBar,
        ResourceEventController resourceController)
    {
        _itemBase = itemBase;
        _configLoader = configLoader;
        _player = player;
        _mergeGrid = itemField;
        _cooldownBar = cooldownBar;
        _resourceController = resourceController;
    }

    public override async Task InitializeAsync()
    {
        _config = await _configLoader.LoadAsync();
    }

    public override void Tick()
    {
        if (_resourceController.IsEventActive)
            return;

        _cooldownBar.value = _cooldown / (_config.SpawnCooldown / _player.SpawnCooldownModifire);

        if (_cooldown >= _config.SpawnCooldown / _player.SpawnCooldownModifire)
        {
            if (_mergeGrid.HasEmptySlots)
            {
                Item item = _itemBase.GetByMergeLevel(_player.SpawnItemMergeLevel);

                if (_mergeGrid.TryAdd(item))
                    item.IsInUse = true;

                _cooldown = 0;
            }

            return;
        }

        _cooldown += Time.deltaTime;
    }
}
