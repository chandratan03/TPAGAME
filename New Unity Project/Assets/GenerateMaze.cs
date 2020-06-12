using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateMaze : MonoBehaviour {

    // Use this for initialization
    public GameObject wall;
    public GameObject GroundObject;
    public GameObject potion;

    const int size = 31;
    char[,] map = new char[size + 5, size + 5];
    public List<Edge> edges = new List<Edge>();


    void Start () {
        InitMap();
        //CreateMap();
        chooseMap();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }

    // Update is called once per frame
    public struct Edge
    {
        public int x;
        public int y;
        public int dx;
        public int dy;
        public Edge(int x, int y, int dx, int dy)
        {
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
        }
    }


    public void chooseMap()
    {

        string path;
        StreamReader r = null;
        bool hasOtherMap = false;
        for (int i = 1; i <= 4; i++)
        {
            path = "Assets/" + i + ".txt";

            if (File.Exists(path) == true)
            {
                
                hasOtherMap = true;
                int index = 0;
                r = new StreamReader(path);
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    for(int j=0; j<line.Length; j++)
                    {
                        map[index, j] = line[j];
                    }
                    index++;
                }
                
            }
        }
       
        if(hasOtherMap == false)
        {
            CreateMap();
        }else
            PrintMap();
    }




    public void CreateMap()
    {
        map[1, 1] = ' ';
        checkFront(1, 1);
        while (edges.Count != 0)
        {
            //Console.WriteLine("SIZE " + edge.Count);
            ShuffleList();
            Edge temp = edges[0];
            //Console.WriteLine("AFTER SIZE " + edge.Count);

            digEdge(temp);
            edges.RemoveAt(0);
        }
        PrintMap();
    }

    public void enqueueEdge(int x, int y, int dx, int dy)
    {

        if (x > 0 && y > 0 && x < size - 1 && y < size - 1)
        {
            int nx = x + dx;
            int ny = y + dy;

            if (map[nx, ny] == '#')
            {
                edges.Add(new Edge(x, y, nx, ny));

                //Console.WriteLine(nx + ": " + ny);
                //Console.WriteLine("HEHE");
            }

        }
    }
    public void checkFront(int x, int y)
    {

        enqueueEdge(x, y, 2, 0); // UP
        enqueueEdge(x, y, 0, 2);  // RIGHT
        enqueueEdge(x, y, 2, 0); // DOWN
        enqueueEdge(x, y, 0, 2); // LEFT
                                 //for(int i=0; i<edge.Count; i++) { 
                                 //     Console.WriteLine(edge.ToList()[i].dy);
                                 //}
                                 //Console.ReadLine();
    }
    public void digEdge(Edge edge)
    {
        int midx = edge.x + ((edge.dx - edge.x) / 2);
        int midy = edge.y + ((edge.dy - edge.y) / 2);
        //Console.WriteLine("Halo " + edge.dx + " " + edge.dy);

        if (map[edge.dx, edge.dy] == '#')
        {
            map[midx, midy] = ' ';
            map[edge.dx, edge.dy] = ' ';
            checkFront(edge.dx, edge.dy);
        }

    }
    public void InitMap()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = '#';
            }
        }
    }

    public void ShuffleList()
    {
        for (int i = 0; i < edges.Count; i++)
        {
            int number = Random.Range(i, edges.Count - 1);
            Edge temp = edges[i];
            edges[i] = edges[number];
            edges[number] = temp;

        }
    }



    public void PrintMap()
    {

        //int c =0;
        int boxX = 3, boxZ = 3;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[i, j] == '#')
                {
                    Instantiate(wall, new Vector3((i*boxX)-45, 20 , j*boxZ+100), Quaternion.identity);
                    //c++;
                    //Debug.Log(c + " "+ i +  " "+ j);     
                }
                if(map[i, j] == 'O')
                {
                    Instantiate(potion, new Vector3((i * boxX) - 45, 20, j * boxZ + 100), Quaternion.identity);

                }

                Instantiate(GroundObject, new Vector3(i * boxX-45   , 18, j * boxZ+100), Quaternion.identity);


            }
        }
    }

}
