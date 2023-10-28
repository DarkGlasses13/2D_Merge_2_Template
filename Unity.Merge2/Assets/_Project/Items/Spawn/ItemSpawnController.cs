using Architecture_Base.Core;
using Assets._Project;
using Assets._Project.Items;
using Assets._Project.Items.Merge;
using System.Threading.Tasks;
using UnityEngine;

public class ItemSpawnController : Controller
{
    private readonly ItemBase _itemBase;
    private readonly GameConfigLoader _configLoader;
    private readonly Player _player;
    private readonly MergeGrid _mergeGrid;
    private GameConfig _config;
    private float _cooldown;

    public ItemSpawnController(ItemBase itemBase, GameConfigLoader configLoader, Player player, MergeGrid itemField)
    {
        _itemBase = itemBase;
        _configLoader = configLoader;
        _player = player;
        _mergeGrid = itemField;
    }

    public override async Task InitializeAsync()
    {
        _config = await _configLoader.LoadAsync();
    }

    public override void Tick()
    {
        if (_cooldown >= 1)
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

        _cooldown += _config.CooldownSpeed * _player.CooldownSpeedModifire * Time.deltaTime;
    }
}
