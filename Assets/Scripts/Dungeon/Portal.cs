using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int level = PlayerPrefs.GetInt("Level");
            PlayerPrefs.SetInt("Level", level + 1);
            SceneManager.LoadScene("SceneForRooms");
        }
    }
}
