using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class SubSceneLoader : MonoBehaviour
    {
        private static SubSceneLoader instance;
        public static SubSceneLoader Instance => instance;

        private Transform player;
        public Transform Player => player;

        [SerializeField]
        private List<SubScene> subScenes = new List<SubScene>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            PlayerController controller = FindObjectOfType<PlayerController>();
            if(controller != null)
            {
                player = controller.transform;
            }

            SubScene[] subscenes = GetComponentsInChildren<SubScene>();
            subScenes = new List<SubScene>(subscenes);
        }

        private void Update()
        {
            for(int i = 0; i < subScenes.Count; i++)
            {
                subScenes[i].DoUpdate(player);
            }
        }
    }
}
