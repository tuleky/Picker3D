using System;
using System.Collections.Generic;
using UnityEngine;

namespace Object_Pool
{
	public class ObjectPooler : MonoBehaviour
	{
		public static ObjectPooler Instance;

		public List<Pool> pools;
		Dictionary<string, Queue<GameObject>> _poolDictionary;

		void Awake()
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(gameObject);
		}

		void Start()
		{
			_poolDictionary = new Dictionary<string, Queue<GameObject>>();

			foreach (Pool pool in pools)
			{
				var objectPool = new Queue<GameObject>();

				for (int i = 0; i < pool.size; i++)
				{
					GameObject obj = Instantiate(pool.prefab, transform);
					obj.SetActive(false);
					objectPool.Enqueue(obj);
				}

				_poolDictionary.Add(pool.tag, objectPool);
			}
		}

		public GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion rotation)
		{
			if (!_poolDictionary.ContainsKey(poolTag))
			{
				Debug.LogWarning("Pool with tag " + poolTag + " doesn't exist.");
				return null;
			}

			GameObject objectToSpawn = _poolDictionary[poolTag].Dequeue();

			objectToSpawn.SetActive(true);
			objectToSpawn.transform.position = position;
			objectToSpawn.transform.rotation = rotation;

			_poolDictionary[poolTag].Enqueue(objectToSpawn);

			return objectToSpawn;
		}

		[Serializable]
		public struct Pool
		{
			public string tag;
			public GameObject prefab;
			public int size;
		}
	}
}
