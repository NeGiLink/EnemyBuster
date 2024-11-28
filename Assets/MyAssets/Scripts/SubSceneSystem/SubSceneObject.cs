using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class SubSceneObject : MonoBehaviour
    {
        private string objectName;

        public static List<GameObject> FindObjectsByName(string name)
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>(); // ���ׂẴA�N�e�B�u�ȃI�u�W�F�N�g���擾
            List<GameObject> matchingObjects = new List<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == name) // ���O�Ńt�B���^�����O
                {
                    matchingObjects.Add(obj);
                }
            }

            return matchingObjects;
        }

        void Start()
        {
            objectName = gameObject.name;
            List<GameObject> objects = FindObjectsByName(objectName);

            foreach (GameObject o in objects)
            {
                if(o != gameObject)
                {
                    Destroy(o);
                }
            }

            Destroy(this);
        }
    }
}
