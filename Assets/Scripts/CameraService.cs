using UnityEngine;


public class CameraService : MonoBehaviour
{
    public GameService GameService;

    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = GameService.RowSize / GameService.ColSize;

        Camera.main.transform.position = new Vector3(GameService.RowSize / 2, GameService.ColSize / 2,
                                                                    Camera.main.transform.position.z);

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = (GameService.ColSize / 2) + 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = GameService.ColSize / 2 * differenceInSize + 2;
        }
    }

}
