using UnityEngine;

public class FinishListener : MonoBehaviour
{
    public GameObject lastSceneUI;

    void Start()
    {
        lastSceneUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lastSceneUI.SetActive(true);
        }
    }
}