using UnityEditor.SceneManagement;
using UnityEngine;

public class RoomsScript : MonoBehaviour
{
    public GameObject gates;
    public GameObject player;
    public GameObject enemiesContainer;
    public GameObject[] enemiesPrefabs;
    private bool _roomCleared = false;

    void Start()
    {
        gates.SetActive(false);
        player = GameObject.FindWithTag("Player").gameObject;
        Debug.Log(player.transform.position + " " + gameObject.transform.position);
        Debug.Log("Room " + gameObject + " " + IsInside(player.transform, gameObject.transform, new Vector2(20,36)));
    }

    // Update is called once per frame
    void Update()
    {
        if (!_roomCleared && IsInside(player.transform, gameObject.transform, new Vector2(20, 36)) && !gates.activeSelf)
        {
            gates.SetActive(true);
            // саунд закрытия дверей
            var enemies = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], transform.position, Quaternion.identity, enemiesContainer.transform);
        }
        if (enemiesContainer.transform.GetChild(0).transform.childCount == 0)
        {
            _roomCleared = true;
            gates.SetActive(false);
            // награды
            // саунд открытия дверей

        }        
    }
    
    bool IsInside(Transform inner, Transform outer, Vector2 outerSize)
    {
    Vector2 innerPos = inner.position;
    Vector2 outerPos = outer.position;

    float left = outerPos.x - outerSize.x / 2f;
    float right = outerPos.x + outerSize.x / 2f;
    float bottom = outerPos.y - outerSize.y / 2f;
    float top = outerPos.y + outerSize.y / 2f;

    return innerPos.x >= left && innerPos.x <= right &&
           innerPos.y >= bottom && innerPos.y <= top;
    }
}
