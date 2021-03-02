using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField]
        int platformCount = 25;

        GameObject finishZone;

        PlatformPositionGenerator platformPositionGenerator;

        void Start()
        {
            finishZone = FindObjectOfType<FinishZoneLogic>().gameObject;

            platformPositionGenerator = GetComponent<PlatformPositionGenerator>();

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnReadyToPlay += SpawnRandomLevel;

            SpawnRandomLevel();
        }

        public List<Vector3> platformPositions
        {
            get; private set;
        }

        [SerializeField]
        GameObjectPool objectPool;
        void SpawnRandomLevel()
        {
            objectPool.DisableAllObjects();

            platformPositions = platformPositionGenerator.GetNewPlatformPositions(platformCount);

            GameObject lastPlatform;
            foreach (Vector3 position in platformPositions)
            {
                
                GameObject platform = objectPool.GetObject();
                platform.transform.position = position;
                platform.SetActive(true);

                lastPlatform = platform;
            }

            PlaceFinishZoneTrigger(platformPositions[platformPositions.Count-1]);
        }

        void PlaceFinishZoneTrigger(Vector3 lastPlatformPosition)
        {
            finishZone.transform.position = lastPlatformPosition;
        }

    }
}