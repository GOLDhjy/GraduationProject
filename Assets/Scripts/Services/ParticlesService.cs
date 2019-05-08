using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace MyService
{
    public class ParticlesService : Singleton<ParticlesService>
    {

        public void PlayPrefabParticle(GameObject gameObject,string str)
        {
            var CuurentInstance = ResourceService.Instance.InstantiateAsset<GameObject>(GameConfigService.Instance.ParticlesPrefabPath + str);
            var scrip = CuurentInstance.GetComponent<PSMeshRendererUpdater>();
            CuurentInstance.transform.SetParent(gameObject.transform);
            scrip.UpdateMeshEffect(gameObject);
        }
        public void StopPrefabParticle(GameObject gameObject, string str)
        {
            var scrip = gameObject.GetComponentInChildren<PSMeshRendererUpdater>();
            scrip.IsActive = false;
            var CurrentInstance = gameObject.transform.Find(str+"(Clone)").gameObject;
            if (CurrentInstance != null)
            {
                GameObject.Destroy(CurrentInstance);
            }
            else
            {
                Debug.LogError("Effect gameobject is null");
            }
            
           // GameObject.Destroy(CuurentInstance);
        }
    }
}
