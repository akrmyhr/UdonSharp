using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

namespace UdonSharp.Examples.Utilities
{
    /// <summary>
    /// Spawns random objects within a defined area and drops them using gravity.
    /// Objects are spawned via Networking.Instantiate so they appear for all users.
    /// </summary>
    [AddComponentMenu("Udon Sharp/Utilities/Random Object Rain Spawner")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public class RandomObjectRainSpawner : UdonSharpBehaviour
    {
        [Tooltip("Prefabs that can be spawned")] public GameObject[] spawnPrefabs;
        [Tooltip("Size of the spawn area (local space)")] public Vector3 areaSize = new Vector3(5f, 1f, 5f);
        [Tooltip("Time between spawns in seconds")] public float spawnInterval = 1f;
        [Tooltip("Number of objects to spawn each interval")] public int spawnCount = 1;

        private float _nextSpawnTime;

        private void Update()
        {
            if (spawnPrefabs == null || spawnPrefabs.Length == 0) return;
            if (Time.time < _nextSpawnTime) return;
            _nextSpawnTime = Time.time + spawnInterval;

            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f),
                    Random.Range(0f, areaSize.y),
                    Random.Range(-areaSize.z * 0.5f, areaSize.z * 0.5f));

                Vector3 spawnPos = transform.TransformPoint(randomOffset);
                GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
                if (prefab == null) continue;

                GameObject obj = Networking.Instantiate(VRC.SDKBase.VRC_EventHandler.VrcBroadcastType.Always, prefab.name, spawnPos, Quaternion.identity);
                // Ensure spawned object has rigidbody for physics
                if (obj.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = obj.AddComponent<Rigidbody>();
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
            }
        }
    }
}
