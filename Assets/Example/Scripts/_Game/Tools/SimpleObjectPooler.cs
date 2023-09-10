using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPooler : MonoBehaviour
{
    public GameObject prefabToPool;
    public int poolSize;
    private List<GameObject> objectPool;
    private GameObject poolObject;

    private void Awake()
    {
        objectPool = new List<GameObject>();

        poolObject = new GameObject();
        poolObject.name = "Projectile Pool";

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool, poolObject.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject newPooledGameObject = Instantiate(prefabToPool, poolObject.transform);
        objectPool.Add(newPooledGameObject);
        return newPooledGameObject;
    }
}
