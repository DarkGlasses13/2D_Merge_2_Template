using Architecture_Base.Core;
using Architecture_Base.Scene_Switching;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Assets._Project
{
    public class ProjectRunner : Runner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly ISceneSwitcher _sceneSwitcher;

        public ProjectRunner(ISceneSwitcher sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        public void Initialize() => RunAsync();

        protected override Task CreateControllers()
        {
            _controllers = new IController[] 
            {
            };

            return Task.CompletedTask;
        }

        protected override void OnControllersInitialized()
        {
        }

        protected override void OnControllersEnabled()
        {
            _sceneSwitcher.ChangeAsync("Game");
        }

        public void Dispose()
        {

        }
    }
}
