using Architecture_Base.Core;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Money
{
    public class ResourceEventController : Controller
    {
        private readonly GameConfigLoader _configLoader;
        private readonly Player _player;
        private readonly ResourceUICounter _uiCounter;
        private readonly ResourceEventScreen _screen;
        private GameConfig _config;
        private float _time;
        private bool _isWait;

        public bool IsEventActive { get; private set; }

        public ResourceEventController(GameConfigLoader configLoader, Player player,
            ResourceUICounter uiCounter, ResourceEventScreen screen)
        {
            _configLoader = configLoader;
            _player = player;
            _uiCounter = uiCounter;
            _screen = screen;
        }

        public override async Task InitializeAsync()
        {
            _config = await _configLoader.LoadAsync();
        }

        protected override void OnEnable()
        {
            _screen.OnCatch += OnCatch;
        }

        private void OnCatch()
        {
            _player.Resource++;
            _uiCounter.Set(_player.Resource);
        }

        public override void Tick()
        {
            if (_isWait == false)
            {
                if (IsEventActive)
                {
                    if (_time >= _config.ResourceEventTime * _player.ResourceEventTimeModifire)
                    {
                        _time = 0;
                        IsEventActive = false;
                        _screen.Stop();
                        _isWait = true;
                        _screen.Hide(OnScreenHiden);
                        return;
                    }
                }
                else
                {
                    if (_time >= _config.ResourceEventCooldown / _player.ResourceEventCooldownModifire)
                    {
                        _time = 0;
                        IsEventActive = true;
                        _isWait = true;
                        _screen.Show(OnScreenShown);
                        return;
                    }
                }

                _time += Time.deltaTime;
            }
        }

        private void OnScreenShown()
        {
            _screen.Emit();
            _isWait = false;
        }

        private void OnScreenHiden() => _isWait = false;

        protected override void OnDisable()
        {
            _screen.OnCatch -= OnCatch;
        }
    }
}
