using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Project Installer", menuName = "Installers/Project Installer")]
public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {

    }
}