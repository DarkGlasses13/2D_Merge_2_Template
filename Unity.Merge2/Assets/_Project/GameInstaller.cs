using Assets._Project;
using Assets._Project.Items;
using Assets._Project.Items.Collection;
using Assets._Project.Items.Merge;
using Assets._Project.Money;
using Assets._Project.Upgrade;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[CreateAssetMenu(fileName = "Game Installer", menuName = "Installers/Game Installer")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        BindRunner();
        BindConfigLoader();
        BindItemBase();
        BindSpawnCooldownBar();
        BindMergeGrid();
        BindItemsCollectionGrid();
        BindLowerPanel();
        BindUpgradePopup();
        BindMoneyFormater();
        BindMoneyUICounter();
        BindControllers();
    }

    private void BindControllers()
    {
        Container
            .Bind<ItemSpawnController>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<MergeGridController>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<ItemCollectController>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<UpgradeController>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<MoneyEarnController>()
            .FromNew()
            .AsSingle();
    }

    private void BindMoneyUICounter()
    {
        Container
            .Bind<MoneyUICounter>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindMoneyFormater()
    {
        Container
            .Bind<MoneyFormater>()
            .FromNew()
            .AsSingle();
    }

    private void BindUpgradePopup()
    {
        Container
            .Bind<UpgradePopup>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindLowerPanel()
    {
        Container
            .Bind<LowerPanel>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindItemsCollectionGrid()
    {
        Container
            .Bind<ItemsCollectionGrid>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindMergeGrid()
    {
        Container
            .Bind<MergeGrid>()
            .FromFactory<MergeGridFactory>()
            .AsSingle();

        Container
            .Bind<MergeGridView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindSpawnCooldownBar()
    {
        Container
            .Bind<Slider>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindItemBase()
    {
        Container
            .Bind<ItemBase>()
            .FromNew()
            .AsSingle();
    }

    private void BindConfigLoader()
    {
        Container
            .Bind<GameConfigLoader>()
            .FromNew()
            .AsSingle();
    }

    private void BindRunner()
    {
        Container
            .BindInterfacesTo<GameRunner>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}