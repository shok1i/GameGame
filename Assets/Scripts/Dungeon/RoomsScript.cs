using UnityEditor.SceneManagement;
using UnityEngine;

public class RoomsScript : MonoBehaviour
{
    public GameObject gates;
    public GameObject player;
    public GameObject enemiesContainer;
    public GameObject rewardsContainer;
    public GameObject[] enemiesPrefabs;
    public GameObject[] bossesPrefabs;
    public GameObject[] rewards;
    private RoomsManager _roomsManager;
    private GameObject weaponsContainer;
    private bool _roomCleared = false;

    void Start()
    {
        _roomsManager = GameObject.Find("RoomsManager").GetComponent<RoomsManager>();
        gates.SetActive(false);
        player = GameObject.FindWithTag("Player").gameObject;
        weaponsContainer = GameObject.FindWithTag("WeaponsContainer").gameObject;
        Debug.Log(_roomsManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_roomCleared && IsInside(player.transform, gameObject.transform, new Vector2(20, 25)) && !gates.activeSelf)
        {
            gates.SetActive(true);
            if (_roomsManager.getClearedRooms() + 1 == _roomsManager.getRoomsOnLevel()) {
                var enemies = Instantiate(bossesPrefabs[Random.Range(0, enemiesPrefabs.Length)], transform.position, Quaternion.identity, enemiesContainer.transform);
            }
            else {
                var enemies = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], transform.position, Quaternion.identity, enemiesContainer.transform);
            }
            // саунд закрытия дверей
        }
        try
        {
            if (enemiesContainer.transform.GetChild(0).transform.childCount == 0 && !_roomCleared)
            {
                _roomCleared = true;
                _roomsManager.addClearedRoom();
                Debug.Log(_roomsManager.getClearedRooms() + " " + _roomsManager.getRoomsOnLevel());
                if (_roomsManager.getClearedRooms() == _roomsManager.getRoomsOnLevel())
                {
                    _roomsManager.addLevel();
                    PlayerPrefs.SetInt("Level", _roomsManager.getLevel());
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    Debug.Log("УРА ПОБЕДА");
                    gates.SetActive(false);
                    gameObject.GetComponent<RoomsScript>().enabled = false;
                    return;
                }
                gates.SetActive(false);
                gameObject.GetComponent<RoomsScript>().enabled = false;
                // награды
                GameObject gun1 = Instantiate(rewards[Random.Range(0, rewards.Length)], rewardsContainer.transform.GetChild(0).position, Quaternion.identity, weaponsContainer.transform);
                GameObject gun2 = Instantiate(rewards[Random.Range(0, rewards.Length)], rewardsContainer.transform.GetChild(1).position, Quaternion.identity, weaponsContainer.transform);

                gun1.GetComponent<WeaponOrbit>().enabled = false;
                gun1.GetComponent<BaseWeaponsClass>().enabled = false;
                gun1.GetComponent<WeaponOutline>().enabled = true;
                gun1.GetComponent<WeaponOutline>().enableOutline();

                gun2.GetComponent<WeaponOrbit>().enabled = false;
                gun2.GetComponent<BaseWeaponsClass>().enabled = false;
                gun2.GetComponent<WeaponOutline>().enabled = true;
                gun2.GetComponent<WeaponOutline>().enableOutline();
                // саунд открытия дверей

            }
        }
        catch (System.Exception e)
        {
            
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
