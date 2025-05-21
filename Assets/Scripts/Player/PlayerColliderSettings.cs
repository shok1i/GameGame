using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerCollisionSettings : MonoBehaviour
{
    //public Collider2D wallCollider;
    private GameObject[] _walls;
    private GameObject _player;
    public GameObject roomsObject;
    //public Collider2D playerCollider;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _walls = GameObject.FindGameObjectsWithTag("Wall");
        Debug.Log("ENTER" + _walls.Length);
        Collider2D playerCollider = _player.GetComponent<Collider2D>();
        foreach (GameObject wall in _walls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, wallCollider, true);
        }
        //Physics2D.IgnoreCollision(playerCollider, wallCollider, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT");
        Collider2D playerCollider = _player.GetComponent<Collider2D>();
        foreach (GameObject wall in _walls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, wallCollider, false);
        }
    }
}