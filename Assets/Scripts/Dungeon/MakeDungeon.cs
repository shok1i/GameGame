using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MakeDungeon : MonoBehaviour
{
    public GameObject scriptableObject;
    public GameObject[] roomsPrefabs;
    public GameObject[] doorPrefabs;
    public GameObject roomsObject;
    private GenerateDungeon _generateDungeonScript;
    private int _roomsCount = 0;

    void Start()
    {
        _generateDungeonScript = scriptableObject.GetComponent<GenerateDungeon>();
        _generateDungeonScript.getGeneratedMatrix();
        MakeDungeonByMatrix(_generateDungeonScript.getMatrix());
        _generateDungeonScript.printMatrix();
        MakeDoors(_generateDungeonScript.getMatrix());
    }

    private void MakeDungeonByMatrix(int[,] matrix)
    {
        // generate on scene first room
        float currentX = 0.0f, currentY = 0.0f;
        int coordX = 0, coordY = 0;
        float shift = 30f;
        float newX = 0.0f, newY = 0.0f;
        var roomsAddress = _generateDungeonScript.getRoomsAdderss();
        foreach (var room in roomsAddress)
        {
            if (room.Key == 0){
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
                _roomsCount++;
                makedRoom.name = "Room-" + _roomsCount;
                Instantiate(makedRoom, new Vector3(currentX, currentY, 15), Quaternion.identity, parent:roomsObject.transform);
            }
        }
    }
    private void MakeDoors(int[,] matrix){
        var roomsAddress = _generateDungeonScript.getRoomsAdderss();
        for (int i = 0; i <= _roomsCount; i++){
            int currentRoomX = roomsAddress[i].Item2;
            int currentRoomY = roomsAddress[i].Item1;
            foreach (var room in roomsAddress){
                var nextRoomX = room.Value.Item2;
                var nextRoomY = room.Value.Item1;
                if (currentRoomX == nextRoomX && currentRoomY == nextRoomY){
                    continue;
                } else if (nextRoomX - currentRoomX == 1 && currentRoomY == nextRoomY){
                    var currentRoom = roomsObject.transform.Find("Room-" + i + "(Clone)"); //("Room-" + i);
                    var distRoom = roomsObject.transform.Find("Room-" + room.Key + "(Clone)");
                    // right wall -> left wall
                    Transform startWall = currentRoom.Find("Wall Right");
                    Transform distWall = distRoom.Find("Wall Left");
                    if (currentRoomX < nextRoomX) { // left wall -> right wall
                        startWall = distRoom.Find("Wall Left");
                        distWall = currentRoom.Find("Wall Right");
                    }
                    var doorObject = doorPrefabs[1];
                    var distance = Mathf.Abs(distWall.transform.position.x - startWall.transform.position.x);
                    var posX = (distWall.transform.position.x + startWall.transform.position.x) / 2 + 0.5f;
                    doorObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(8f, 3f);
                    bool startedCollider = true;
                    for (float j = distWall.transform.position.x; j <= startWall.transform.position.x;){
                        GameObject door = Instantiate(doorObject, new UnityEngine.Vector3(j, distWall.transform.position.y - 0.2f, 13), Quaternion.identity);
                        if(!startedCollider){
                            Transform child = door.transform.Find("Collider");
                            Destroy(child.GetComponent<BoxCollider2D>());
                        }
                        startedCollider = false;
                        j += 4f;
                    }
                    Instantiate(doorObject, new UnityEngine.Vector3(startWall.transform.position.x,distWall.transform.position.y-0.2f,13), Quaternion.identity);
                } else if (nextRoomY - currentRoomY == 1 && currentRoomX == nextRoomX){
                    var currentRoom = roomsObject.transform.Find("Room-" + i + "(Clone)"); //("Room-" + i);
                    var distRoom = roomsObject.transform.Find("Room-" + room.Key + "(Clone)");
                    Debug.Log(room.Key);
                    // down wall -> upper wall
                    Transform startWall = currentRoom.Find("Wall Bottom");
                    Transform distWall = distRoom.Find("Wall Top");
                    if (currentRoomY > nextRoomY){
                        startWall = distRoom.Find("Wall Bottom");
                        distWall = currentRoom.Find("Wall Top");
                    }
                    var doorObject = doorPrefabs[0];
                    var distance = Mathf.Abs(distWall.transform.position.y - startWall.transform.position.y);
                    var posY = (distWall.transform.position.y + startWall.transform.position.y) / 2;
                    doorObject.GetComponentInChildren<SpriteRenderer>().size = new Vector2(1f, distance + 1f);
                    doorObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(4f, distance+2.5f);
                    Instantiate(doorObject, new Vector3(distWall.transform.position.x, posY, 13), Quaternion.identity);
                }
            }
        }
    }
}
