using Architecture_Base.Asset_Loading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._Project.Local_Asset_Load
{
    public abstract class LocalMultipleAssetLoader<T> : MultipleAssetLoader<T> where T : class
    {
        public override IList<T> Load()
        {
            if (_assets != null)
                return (IList<T>)_assets;

            AsyncOperationHandle<IList<T>> loading = Addressables.LoadAssetsAsync<T>(Key, null);
            loading.WaitForCompletion();
            _assets = new List<object>(loading.Result);
            return loading.Result;
        }

        public override async Task<IList<T>> LoadAsync()
        {
            if (_assets != null)
                return (IList<T>)_assets;

            IList<T> assets = await Addressables.LoadAssetsAsync<T>(Key, null).Task;
            _assets = new List<object>(assets);
            return assets;
        }

        public override IList<T> LoadAndInstantiate(Transform parent, bool isActive = true)
        {
            AsyncOperationHandle<IList<GameObject>> loading = Addressables.LoadAssetsAsync<GameObject>(Key, null);
            loading.WaitForCompletion();
            IList<T> components = new List<T>();

            foreach (GameObject asset in loading.Result)
            {
                if (asset.TryGetComponent(out T component) == false)
                {
                    throw new NullReferenceException(
                        $"There is no such component on the {asset.name}");
                }

                components.Add(component);
            }

            _assets = new List<object>(components);
            return components;
        }

        public override async Task<IList<T>> LoadAndInstantiateAsync(Transform parent, bool isActive = true)
        {
            if (_assets != null)
                return (IList<T>)_assets;

            IList<T> components = new List<T>();
            IList<GameObject> assets = await Addressables.LoadAssetsAsync<GameObject>(Key, null).Task;

            foreach (GameObject asset in assets)
            {
                if (asset.TryGetComponent(out T component) == false)
                {
                    throw new NullReferenceException(
                        $"There is no such component on the {asset.name}");
                }

                components.Add(component);
            }

            _assets = new List<object>(components);
            return components;
        }
    }
}
