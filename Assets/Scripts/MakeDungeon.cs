using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MakeDungeon : MonoBehaviour
{
    public GameObject scriptableObject;
    public GameObject[] roomsPrefabs;
    public GameObject[] doors;
    private GenerateDungeon _generateDungeonScript;

    void Start()
    {
        _generateDungeonScript = scriptableObject.GetComponent<GenerateDungeon>();
        _generateDungeonScript.getGeneratedMatrix();
        makeDungeonByMatrix(_generateDungeonScript.getMatrix());
        _generateDungeonScript.printMatrix();
    }

    private void makeDungeonByMatrix(int[,] matrix)
    {
        // generate on scene first room
        int i = 0;
        float currentX = 0.0f, currentY = 0.0f;
        int coordX = 0, coordY = 0;
        float shift = 30f;
        float newX = 0.0f, newY = 0.0f;
        var roomsAddress = _generateDungeonScript.getRoomsAdderss();
        foreach (var room in roomsAddress)
        {
            if (room.Key == 0){
                Debug.Log("0 ROOM");
                coordY = room.Value.Item1;
                coordX = room.Value.Item2;
                continue;
            } else {
                var newRoomCoordX = room.Value.Item2;
                var newRoomCoordY = room.Value.Item1;
                if (newRoomCoordX > coordX){
                    newX = currentX + shift * 2;
                } else if (newRoomCoordX < coordX){
                    newX = currentX - shift * 2;
                } 
                if (newRoomCoordY > coordY){
                    newY = currentY - shift;
                } else if (newRoomCoordY < coordY){
                    newY = currentY + shift;
                }
                coordX = newRoomCoordX;
                coordY = newRoomCoordY;
                currentX = newX;
                currentY = newY;
                GameObject makedRoom = roomsPrefabs[0];
                i++;
                makedRoom.name = "RoomaBoba" + i;
                Instantiate(makedRoom, new Vector3(currentX, currentY, 15), Quaternion.identity);
            }
        }
    }
}
