using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class PlatformPositionGenerator : MonoBehaviour
    {

        [SerializeField]
        GameObject startingPoint;

        [SerializeField]
        float minDistance = 3f;

        [SerializeField]
        float maxDistance = 3.5f;

        public List<Vector3> GetNewPlatformPositions(int ammount)
        {
            List<Vector3> positions = new List<Vector3>();
            positions.Add(startingPoint.transform.position);

            if (ammount <= 1)
            {
                return positions;
            }

            for (int i = 0; i < ammount - 1; i++)
            {
                positions.Add(GetNextPlatformPosition(positions[i]));
            }

            return positions;
        }

        Vector3 GetNextPlatformPosition(Vector3 currentPosition)
        {
            float randomX = Random.Range(startingPoint.transform.position.x - maxDistance, startingPoint.transform.position.x + maxDistance);
            float randomZ = Random.Range(currentPosition.z + minDistance, currentPosition.z + maxDistance);
            
            return new Vector3(randomX, currentPosition.y, randomZ);
        }
    }
}