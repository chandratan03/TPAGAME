using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GenerateBSP : MonoBehaviour {

    public GameObject wall;
    public GameObject GroundObject;
    public GameObject mainSoldier = null;
    public GameObject soldier;
    public GameObject potion;
    public miniMapScript secondCam;
    //public GameObject Sphere;
    int boxX = 3, boxZ = 3;
    public CharacterCamera cam;
    Vector3 vect = new Vector3(0, 100, 0);
    public static int playerCount = 0;
    public Text playerCountTxt;
    public bool startPlaying = false;
    public GameObject winImage;
    public GameObject loseImage;
    
    public void Start()
    {
        generateMap();
    }





    public void generateMap()
    {
        Debug.Log(chooseMap.map1);
        if(chooseMap.map1 == true)
        {

            CreateCustomMap(1);
        }else if (chooseMap.map2 == true)
        {
            CreateCustomMap(2);
        }
        else if (chooseMap.map3 == true)
        {
            CreateCustomMap(3);
        }
        else if (chooseMap.map4 == true)
        {
            CreateCustomMap(4);
        }
        else
        {
            createBSPMap();
        }
    }


    public void CreateCustomMap(int number)
    {
        CreateSelectMap(number);
        PrintMap();
        PlaceCustomSoldiers();

        startPlaying = true;
    }
    void PlaceCustomSoldiers()
    {
        while (playerCount<2)
        {
            int x = Random.Range(1, size-1);
            int y = Random.Range(1, size-1);
            if(map[x,y] == ' ')
            {   
                if(playerCount == 0)
                {
                    mainSoldier = Instantiate(mainSoldier, new Vector3(x * boxX, -2, y * boxZ), Quaternion.identity);
                    Camera.main.transform.position = mainSoldier.transform.position;
                    cam.target = mainSoldier.transform;
                    secondCam.target = mainSoldier.transform;
                    playerCount++;

                }else
                {
                    Instantiate(soldier, new Vector3(x * boxX, -2, y * boxZ), Quaternion.identity);
                    playerCount++;

                }
            }
        
        }

    }

    public void CreateSelectMap(int number)
    {

        switch (number){
            case 1:
                map1();
                break;
            case 2:
                map2();
                break;
            case 3:
                map3();
                break;
            case 4:
                map4();
                break;
        }
       
    }

    public void createBSPMap()
    {
        Init(); // INIT SIDES
        CreateMap(); // CREATE THE BSP
        //GENERATE/PRINT IT INTO THE GAMES
        PrintMap();
        //Instantiate(Sphere, new Vector3(20, 10, 10), Quaternion.identity);
        placeSoldiers();
        placePotions();
        startPlaying = true;
        //cam.target.position += vect;
        //cam.transform.position += vect;
    }
    public void Update()
    {
        
        if(startPlaying == true)
        {
            
            if(mainSoldier!= null)
            {
                if(playerCount == 1) //winning
                {
                    startPlaying = false;
                    winImage.SetActive(true);
                    StartCoroutine(backToMainMenu());
                }
            }else
            {//losing
                startPlaying = false;
                loseImage.SetActive(true);
                StartCoroutine(backToMainMenu());
            }


        }
        playerCountTxt.text = playerCount+"";
    }
    IEnumerator backToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void placeSoldiers()
    {
        shuffleRoom();


        for (int i=0; i<4; i++)
        {
            int x = Random.Range(rooms[i].x + 1, rooms[i].x2);
            int y = Random.Range(rooms[i].y + 1, rooms[i].y2);

            if (i == 0)
            {
                mainSoldier = Instantiate(mainSoldier, new Vector3(x * boxX, -2, y * boxZ), Quaternion.identity);
                Camera.main.transform.position = mainSoldier.transform.position;
                cam.target = mainSoldier.transform;
                secondCam.target = mainSoldier.transform; 
            }
            else
                Instantiate(soldier, new Vector3(x * boxX, -2, y * boxZ), Quaternion.identity);



            playerCount++;
        }
       
    }
    public void placePotions()
    {
        shuffleRoom();
        for (int i = 0; i < 3; i++)
        {
            int x = Random.Range(rooms[i].x + 1, rooms[i].x2);
            int y = Random.Range(rooms[i].y + 1, rooms[i].y2);

            Instantiate(potion, new Vector3(x * boxX, 0, y * boxZ), Quaternion.identity);
                


            
        }
    }

    public void shuffleRoom()
    {
       for(int i=0; i<rooms.Count; i++)
        {
            int number = Random.Range(i, rooms.Count - 1);
            Room temp = rooms[i];
            rooms[i] = rooms[number];
            rooms[number] = temp;
        }
    }

    public void PrintMap()
    {
        
        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                if(map[i,j] == '#')
                {
                    Instantiate(wall, new Vector3(i*boxX, 0, j*boxZ), Quaternion.identity);
                }
                else if(map[i,j] == 'O')
                {
                    Instantiate(potion, new Vector3(i * boxX, 0, j * boxZ), Quaternion.identity);

                }

                Instantiate(GroundObject, new Vector3(i * boxX, -2.7f, j * boxZ), Quaternion.identity);

            }
        }
    }

    public struct Room
    {
        public int x, y, x2, y2;
        public bool soldier;

        public Room(int x, int y, int x2, int y2)
        {
            this.x = x;
            this.y = y;
            this.x2 = x2;
            this.y2 = y2;
            soldier = false;
        }
    }
    const int size = 30; // THIS IS THE SIZE OF THE MAP

    char[,] map = new char[size, size];
    char[,] ground = new char[size, size];
    List<Room> rooms = new List<Room>();

    public void Init()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
                {
                    map[i, j] = '#';
                }
                else
                {
                    map[i, j] = ' ';
                }
                //ground[i,j] = ' ';
            }
        }
    }
    public void CreateMap()
    {
        rooms.Add(new Room(0, 0, size - 1, size - 1));
        for (int i = 0; i < 4; i++) // 4x split
        {
            int manyRoom = rooms.Count;
            for (int j = 0; j < manyRoom; j++)
            {
                Room tempRoom = rooms[0];
                double a = getPercentage();


                if (i % 2 == 0) // horizontal
                {


                    int split = System.Convert.ToInt32(((tempRoom.x2 - tempRoom.x) * a + tempRoom.x));
                    int count = 0;

                    while (map[split, tempRoom.y2] == ' ' || map[split, tempRoom.y] == ' ')
                    {
                        a = getPercentage();
                        split = System.Convert.ToInt32(((tempRoom.x2 - tempRoom.x) * a + tempRoom.x));
                        //System.Console.WriteLine(split);
                        if (count == 50) break;
                        count++;
                    }

                    for (int k = tempRoom.y + 1; k < tempRoom.y2; k++)
                    {
                        map[split, k] = '#';
                    }
                    //RANDOM RANGE IF USING INT THE MAX IS EXCLUSIVE, IF FLOAT IS INCLUSIVE
                    int door = System.Convert.ToInt32(Random.Range(tempRoom.y + 1, tempRoom.y2));
                    map[split, door] = ' ';
                    rooms.Add(new Room(tempRoom.x, tempRoom.y, split, tempRoom.y2));
                    rooms.Add(new Room(split, tempRoom.y, tempRoom.x2, tempRoom.y2));

                }
                else
                {

                    int count = 0;
                    int split = System.Convert.ToInt32(((tempRoom.y2 - tempRoom.y) * a + tempRoom.y));
                    while (map[tempRoom.x2, split] == ' ' || map[tempRoom.x, split] == ' ')
                    {
                        a = getPercentage();
                        split = System.Convert.ToInt32(((tempRoom.y2 - tempRoom.y) * a + tempRoom.y));
                        if (count == 50)
                            break;
                        count++;
                    }

                    //Console.WriteLine(a);
                    //Console.WriteLine(split);
                    for (int k = tempRoom.x + 1; k < tempRoom.x2; k++)
                    {
                        map[k, split] = '#';
                    }

                    //RANDOM RANGE IF USING INT THE MAX IS EXCLUSIVE, IF FLOAT IS INCLUSIVE
                    int door = System.Convert.ToInt32(Random.Range(tempRoom.x + 1, tempRoom.x2));
                    map[door, split] = ' ';

                    rooms.Add(new Room(tempRoom.x, tempRoom.y, tempRoom.x2, split));
                    rooms.Add(new Room(tempRoom.x, split, tempRoom.x2, tempRoom.y2));

                }
                rooms.RemoveAt(0);


            }
        }
    }

    public double getPercentage()
    {
        return System.Convert.ToDouble(Random.Range(0.4f, 0.7f));
    }

    public void map1()
    {
        string path;
        StreamReader r = null;
        path = "Assets/1.txt";

        if (File.Exists(path) == true)
        {

            int index = 0;
            r = new StreamReader(path);
            while (!r.EndOfStream)
            {
                string line = r.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    map[index, j] = line[j];
                }
                index++;
            }

        }


    }
    public void map2()
    {
        string path;
        StreamReader r = null;

        path = "Assets/2.txt";

        if (File.Exists(path) == true)
        {

            int index = 0;
            r = new StreamReader(path);
            while (!r.EndOfStream)
            {
                string line = r.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    map[index, j] = line[j];
                }
                index++;
            }

        }


    }
    public void map3()
    {
        string path;
        StreamReader r = null;

        path = "Assets/3.txt";

        if (File.Exists(path) == true)
        {

            int index = 0;
            r = new StreamReader(path);
            while (!r.EndOfStream)
            {
                string line = r.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    map[index, j] = line[j];
                }
                index++;
            }

        }


    }
    public void map4()
    {
        string path;
        StreamReader r = null;

        path = "Assets/4.txt";

        if (File.Exists(path) == true)
        {

            int index = 0;
            r = new StreamReader(path);
            while (!r.EndOfStream)
            {
                string line = r.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    map[index, j] = line[j];
                }
                index++;
            }

        }


    }


}
