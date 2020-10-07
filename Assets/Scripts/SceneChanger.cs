using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void LoadNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
