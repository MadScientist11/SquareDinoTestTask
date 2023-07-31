using System.IO;
using UnityEngine;

namespace Game.Source.Services
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
    }
 
    public class AssetProvider : IAssetProvider
    {
        public T LoadAsset<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);

            if (asset == null)
            {
                throw new FileLoadException($"The asset at path \"{path}\" doesn't exist or type mismatch");
            }

            return asset;
        }
        
    }
}