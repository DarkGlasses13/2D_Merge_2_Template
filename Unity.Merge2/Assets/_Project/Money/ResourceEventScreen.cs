using Architecture_Base.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets._Project.Money
{
    public class ResourceEventScreen : MonoBehaviour, IUIElement
    {
        public event Action OnCatch;

        private ResourceSpriteLoader _spriteLoader;
        private GameConfigLoader _configLoader;
        private GameConfig _config;
        private Sprite _sprite;
        private ObjectPool<Image> _resources;
        private bool _isEmmit;
        private float _time;
        private readonly List<Image> _emittingResources = new();

        [Inject]
        public void Construct(ResourceSpriteLoader spriteLoader, GameConfigLoader configLoader)
        {
            _spriteLoader = spriteLoader;
            _configLoader = configLoader;
        }

        private async void Start()
        {
            _config = await _configLoader.LoadAsync();
            _sprite = await _spriteLoader.LoadAsync();
            _resources = new(Create, OnGet, OnRelease);
        }

        public void Emit()
        {
            _isEmmit = true;
        }

        private void Update()
        {
            if (_isEmmit)
            {
                if (_time >= 1 /_config.ResourceRate)
                {
                    Image resource = _resources.Get();
                    resource.rectTransform.position = new(Random.Range(0, Screen.width), Screen.height);
                    _emittingResources.Add(resource);
                    _time = 0;
                }

                _time += Time.deltaTime;
            }

            for (int i = 0; i < _emittingResources.Count; i++)
            {
                Rect screenRect = new(0, 0, Screen.width + 10, Screen.height + 10);

                if (screenRect.Contains(_emittingResources[i].rectTransform.position) == false)
                {
                    Remove(_emittingResources[i]);
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 position = _emittingResources[i].rectTransform.position;
                    Vector3 size = _emittingResources[i].rectTransform.rect.size;
                    Vector2 ofsetPosition = new(position.x - size.x / 2, position.y - size.y / 2);
                    Rect resourceRect = new(ofsetPosition, size);

                    if (resourceRect.Contains(Input.mousePosition))
                    {
                        Remove(_emittingResources[i]);
                        OnCatch?.Invoke();
                        continue;
                    }
                }

                _emittingResources[i].rectTransform
                    .Translate(_config.ResourceSpeed * Time.deltaTime * Vector3.down);
            }
        }

        private void Remove(Image resource)
        {
            resource.gameObject.SetActive(false);
            _resources.Release(resource);
            _emittingResources.Remove(resource);
        }

        public void Stop()
        {
            _isEmmit = false;
        }

        public void Show(Action callback = null)
        {
            gameObject.SetActive(true);
            callback?.Invoke();
        }

        private void OnRelease(Image image) => image.gameObject.SetActive(false);

        private void OnGet(Image image) => image.gameObject.SetActive(true);

        private Image Create()
        {
            Image resource = new GameObject("Resource").AddComponent<Image>();
            resource.rectTransform.SetParent(transform);
            resource.sprite = _sprite;
            return resource;
        }

        public void Hide(Action callback = null)
        {
            gameObject.SetActive(false);
            _emittingResources.ForEach(resource => _resources.Release(resource));
            _emittingResources.Clear();
            callback?.Invoke();
        }
    }
}
