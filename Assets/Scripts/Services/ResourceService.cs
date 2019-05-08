using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public class ResourceService : Singleton<ResourceService>
    {
        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            T obj = Resources.Load<T>(assetName);
            if (obj == null)
            {
               Debug.LogError("Can't Load Asset:"+ assetName + " as "+typeof(T));
            }
            return obj;
        }

        public T InstantiateAsset<T>(string assetName, Vector3 position, Quaternion rotation) where T : UnityEngine.Object
        {
            T obj = LoadAsset<T>(assetName);
            return obj == null ? null : UnityEngine.Object.Instantiate(obj, position, rotation);
        }

        public T InstantiateAsset<T>(string assetName) where T : UnityEngine.Object
        {
            T obj = LoadAsset<T>(assetName);
            return obj == null ? null : UnityEngine.Object.Instantiate(obj);
        }

        public T InstantiateAsset<T>(string assetName, Transform parent) where T : UnityEngine.Object
        {
            T obj = LoadAsset<T>(assetName);
            return obj == null ? null : UnityEngine.Object.Instantiate(obj, parent);
        }
    }
}
