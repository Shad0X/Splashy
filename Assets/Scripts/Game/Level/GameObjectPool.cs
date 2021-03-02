using UnityEngine;

namespace Game.Level
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        int startingSize = 10;       

        void Start()
        {
            for (int i = 0; i < startingSize; i++)
            {
                AddNewObjectToPool();
            }
        }

        public GameObject GetObject()
        {
            foreach (Transform obj in gameObject.transform)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    return obj.gameObject;
                }
            }
            return AddNewObjectToPool();
        }


        [SerializeField]
        GameObject objectToPool;

        GameObject AddNewObjectToPool()
        {
            GameObject obj = Instantiate(objectToPool, gameObject.transform);
            obj.SetActive(false);
            return obj;
        }

        public void DisableAllObjects()
        {
            foreach (Transform obj in transform)
            {
                obj.gameObject.SetActive(false);
            }
        }

    }
}