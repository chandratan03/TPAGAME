using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEmptyMap : MonoBehaviour {

    const int size = 30;
    char[,] map = new char[size+5, size+5];
    int boxSize = 3;
    public GameObject wall;
    public GameObject groundObject;

    public GameObject[,] walls = new GameObject[size+5,size+5];
    public GameObject[,] grounds = new GameObject[size + 5, size + 5];
    //public ChooseChoice cc;
    void Start () {
        Init();
        PrintMap();
        //cc.walls = walls;
	}
    private void Init()
    {
        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                if (i == 0 || j == 0 || j == size - 1|| i == size - 1)
                {
                    map[i, j] = '#';
                }else
                    map[i, j] = ' '; 
            }
        }
    }
    private void PrintMap()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[i,j] == '#')
                {
                    walls[i,j]= Instantiate(wall, new Vector3(i * boxSize, 0, j * boxSize), Quaternion.identity);

                }
                grounds[i,j]= Instantiate(groundObject, new Vector3(i * boxSize, -2.7f, j * boxSize), Quaternion.identity);
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
