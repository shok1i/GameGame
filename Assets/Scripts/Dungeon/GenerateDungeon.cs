using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateDungeon : MonoBehaviour {
    [SerializeField] public int heightMatrix = 6;
    [SerializeField] public int weightMatrix = 5;
    [SerializeField] public int roomsCount = 6;

    private int[,] matrix;
    private Dictionary<int, (int, int)> roomsAddress = new Dictionary<int, (int, int)>();
    // void Start()
    // {
    //     getGeneratedMatrix();
    //     printMatrix();
    //     getDeadEnds();
    // }

    public int[,] getMatrix()
    {
        return matrix;
    }
    public Dictionary<int, (int,int)> getRoomsAdderss(){
        return roomsAddress;
    }
    public int[,] getGeneratedMatrix(){
        matrix = new int[heightMatrix,weightMatrix];
        for(int i = 0; i < heightMatrix; i++){
            for (int j = 0; j < weightMatrix; j++) {
                matrix[i,j] = -1;
            }
        }
        matrix = generateRooms();
        return matrix;
    }
    private int[,] generateRooms(){
        matrix = new int[heightMatrix,weightMatrix];
        for(int i = 0; i < heightMatrix; i++){
            for (int j = 0; j < weightMatrix; j++) {
                matrix[i,j] = -1;
            }
        }
        // generate first room
        Unity.Mathematics.Random rand = new Unity.Mathematics.Random((uint)DateTime.Now.Ticks);
        int firstX = rand.NextInt(0,weightMatrix);
        int firstY = rand.NextInt(0,heightMatrix);
        matrix[firstY, firstX] = 0;
        roomsAddress.Add(0,(firstY, firstX));
        // MAIN GEN (generating other rooms)

       for (int i = 1; i < roomsCount; i++){
            int currentRoom = i;
            (int choosenRoomX, int choosenRoomY) = roomsAddress[Mathf.Abs(currentRoom - 1)];
            
            int direction = rand.NextInt(0, 4);
            int newX = choosenRoomX;
            int newY = choosenRoomY;

            switch (direction) {
                case 0: newX += 1; break;
                case 1: newX -= 1; break;
                case 2: newY += 1; break;
                case 3: newY -= 1; break;
            }

            if (newX >= 0 && newX < heightMatrix && newY >= 0 && newY < weightMatrix && matrix[newX,newY] == -1) {
                matrix[newX, newY] = i;
                roomsAddress.Add(i,(newX,newY));
            } else {
                i--;
            }
        }
        return matrix;
    }

    public List<int> getDeadEnds(){
        List<int> deadEnds = new List<int>();
        for (int i = 0; i < heightMatrix; i++) {
            if (matrix[i,0] != -1){
                deadEnds.Add(matrix[i,0]);
            }
            if (matrix[i, weightMatrix - 1] != -1) {
                deadEnds.Add(matrix[i,weightMatrix - 1]);
            }
        }
        for (int i = 0; i < weightMatrix; i++) {
            if (matrix[0, i] != -1) {
                deadEnds.Add(matrix[0,i]);
            }
            if (matrix[heightMatrix - 1, i] != -1) {
                deadEnds.Add(matrix[heightMatrix - 1, i]);
            }
        }
        // foreach (int end in deadEnds) {
        //     Debug.Log(end);
        // }
        return deadEnds;
    }

    public void printMatrix(){
        string buffer = "";
        for (int i = 0; i < heightMatrix; i++ ) {
            for (int j = 0; j < weightMatrix; j++ ) {
                buffer += "|" + matrix[i,j] + "|";
            }
            buffer += "\n";
        }
        Debug.Log(buffer);
    }
}