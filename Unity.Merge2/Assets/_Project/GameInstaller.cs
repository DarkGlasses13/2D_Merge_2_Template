using Assets._Project;
using Assets._Project.Items;
using Assets._Project.Items.Collection;
using Assets._Project.Items.Merge;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Game Installer", menuName = "Installers/Game Installer")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        BindRunner();
        BindConfigLoader();
        BindItemBase();
        BindMergeGrid();
        BindItemsCollection();
        BindLowerPanel();
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
    }

    private void BindLowerPanel()
    {
        Container
            .Bind<LowerPanel>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindItemsCollection()
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