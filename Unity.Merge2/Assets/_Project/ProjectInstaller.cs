using Architecture_Base.Scene_Switching;
using Assets._Project;
using Assets._Project.Scene_Switch;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Project Installer", menuName = "Installers/Project Installer")]
public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        BindPlayer();
        BindRunner();
        BindRandom();
        BindSceneSwitcher();
    }

    private void BindSceneSwitcher()
    {
        Container
            .Bind<ISceneSwitcher>()
            .To<SceneSwitcher>()
            .FromNew()
            .AsSingle();
    }

    private void BindRandom()
    {
        Container
            .Bind<FastRandom>()
            .FromNew()
            .AsSingle();
    }

    private void BindRunner()
    {
        Container
            .BindInterfacesTo<ProjectRunner>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindPlayer()
    {
        Container
            .Bind<Player>()
            .FromNew()
            .AsSingle();
    }
}