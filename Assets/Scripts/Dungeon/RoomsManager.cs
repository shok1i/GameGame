using UnityEngine;
using UnityEngine.UIElements;

public class RoomsManager : MonoBehaviour
{
    private int _currentRoom = 0;
    private int _roomsCleared = 0;
    private int _level = 1; // FLOOR
    private int _roomsOnLevel;

    void Start()
    {
        _roomsOnLevel = transform.parent.gameObject.GetComponentInChildren<GenerateDungeon>().roomsCount;
    }

    public void addClearedRoom()
    {
        _roomsCleared += 1;
    }
    public int getClearedRooms()
    {
        return _roomsCleared;
    }
    public void addLevel()
    {
        _level += 1;
    }
    public int getLevel()
    {
        return _level;
    }
    public void addCurrentRoom()
    {
        _currentRoom += 1;
    }
    public int getCurrentRoom()
    {
        return _currentRoom;
    }
    public int getRoomsOnLevel()
    {
        return _roomsOnLevel;
    }
}
