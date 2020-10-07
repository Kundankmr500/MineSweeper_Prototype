using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameService GameService;


    private void Update()
    {
        CheckUserInput();
    }


    // checking for User Input and Process these
    private void CheckUserInput()
    {
        // Mouse left click check
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int xPos = Mathf.RoundToInt(mousePos.x);
            int yPos = Mathf.RoundToInt(mousePos.y);

            GameService.MouseLeftClickProcess(xPos, yPos);
        }

        // Mouse right click check for flag tile place.
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int xPos = Mathf.RoundToInt(mousePos.x);
            int yPos = Mathf.RoundToInt(mousePos.y);

            GameService.MouseRightClickProcess(xPos, yPos);
        }
    }
}
