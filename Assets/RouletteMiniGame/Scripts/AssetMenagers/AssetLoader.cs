using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RouletteMiniGame.AssetManagement
{
    public static class AssetLoader
    {
        public static void LoadAsset<T>(string address, Action<T> callback)
        {
            Addressables.LoadAssetAsync<T>(address).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    callback?.Invoke(handle.Result);
                else
                {
                    throw new Exception($"Failed to load asset: {address}");
                }
            };
        }

        public static async Task<T> LoadAssetAsync<T>(string address)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                throw new Exception($"Failed to load asset: {address}");
            }
        }
    }
}