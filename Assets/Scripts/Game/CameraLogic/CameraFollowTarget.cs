using Game.Player;
using UnityEngine;

namespace Game.CameraLogic //can't use 'Game.Camera' as it CONFUSES VS and hides the Unity Camera component script... 
{
    public class CameraFollowTarget : MonoBehaviour
    {
        GameObject target;
        Vector3 offSet;

        private void Start()
        {
            target = FindObjectOfType<BallController>().gameObject;
            offSet = transform.position - target.transform.position;
        }

        private void Update()
        {
            Vector3 nextPosition = target.transform.position + offSet;
            nextPosition.y = transform.position.y;
            nextPosition.x = transform.position.x;
        
            transform.position = nextPosition;
        }

    }
}