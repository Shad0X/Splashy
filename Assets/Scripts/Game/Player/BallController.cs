using UnityEngine;

namespace Game.Player
{
    public class BallController : MonoBehaviour
    {
        Vector3 startPosition;
        void Start()
        {
            startPosition = transform.position;

            GameLogic gameLogic = FindObjectOfType<GameLogic>();
            gameLogic.OnReadyToPlay += ResetStats;
            gameLogic.OnGameOver += OnGameFailed;
        }

        void Update()
        {
            #if UNITY_STANDALONE
                HandleMouseInput();
            #endif

            #if UNITY_IOS || UNITY_ANDROID
                HandleTouchInput();
            #endif
        }

        void ResetStats()
        {
            playing = false;
            transform.position = startPosition;
        }

        void OnGameFailed()
        {
            playing = false;
        }

        bool playing = false;

        float worldSpaceDistanceX;
        const int LeftMouseButton = 0;

        #if UNITY_STANDALONE
        void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(LeftMouseButton))
            {
                Vector3 touchInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 12));
                worldSpaceDistanceX = transform.position.x - touchInWorldSpace.x;
                playing = true;
            }

            if (playing && Input.GetMouseButton(LeftMouseButton))
            {
                MoveBallBasedOnInputPosition(Input.mousePosition);
            }
        }
        #endif

        void MoveBallBasedOnInputPosition(Vector3 screenPosition)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 12));
            transform.position = new Vector3(newPos.x + worldSpaceDistanceX, transform.position.y, transform.position.z);
        }

        #if UNITY_IOS || UNITY_ANDROID
        void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 12));
                    worldSpaceDistanceX = transform.position.x - touchInWorldSpace.x;
                    playing = true;
                }

                if (playing && touch.phase == TouchPhase.Moved)
                {
                    MoveBallBasedOnInputPosition(touch.position);
                }
            }
        }
        #endif

    }
}