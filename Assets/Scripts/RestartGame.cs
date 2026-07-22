/*using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Time.timeScale = 1; 
    }
}*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        Time.timeScale = 1f; // oyunu tekrar başlatırken zamanı düzelt
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // bulunduğun sahneyi yeniden yükle
    }
}