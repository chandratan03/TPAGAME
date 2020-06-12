using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ChooseChoice : MonoBehaviour
{

    // Use this for initialization
    const int size = 30;

    char[,] map = new char[size + 5, size + 5];
    char[,] tempMap = new char[size + 5, size + 5];
    char[,] redMap = new char[size + 5, size + 5];
    const int boxSize = 3;
    
    // ARRAY OF GAME OBJECT
    public GameObject[,] walls = new GameObject[size + 5, size + 5];
    public GameObject[,] grounds = new GameObject[size + 5, size + 5];
    
    
    //GAMEobjects
    public GameObject wallObject;
    public GameObject groundObject;
    public GameObject potionObject;
    //Material
    public Material redMaterial;
    public Material defaultMaterial;
    public Material greenMaterial;

    private Transform selectedGround;
    //private bool test= true;
    private bool wallSelect = true;
    private bool potionSelect = false;
    private bool removeSelect = false;

    public GameObject savedText;

    public Texture2D DeleteTexture;

    public GameObject RedObject;
    public GameObject GreenObject;
    public GameObject pinnedRed;

    public GameObject pinned;
    float xbef=0;
    float zbef=0;
    void Start()
    {
        Init();
        PrintMap();
        savedText.SetActive(false);
        //Cursor.SetCursor();
    }


    void Update()
    {
        hitted();
        //grounds = emptyMap.grounds;

    }
    private void resetMaterialColor(Transform selection)
    {
        if (selectedGround != null && selection != selectedGround)
        {
            var render = selectedGround.GetComponent<Renderer>(); // get the component of the block
            //Debug.Log(render);
            render.material.color = defaultMaterial.color; // change the material to default( CANT CHANGE THE MATERIAL ONLY CAN CHANGE THE MATERIAL COLOR)
            //Debug.Log(render.material);
            selectedGround = null; // set selected ground to null
            //test = false;
        }


    }

    private void createObject()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {   
                
                Transform press = hit.transform;
                var render = press.GetComponent<Renderer>();//the component
                Vector3 pos = new Vector3(0, 0, 0);
                float xPos = render.transform.localPosition.x;
                //float yPos = render.transform.localPosition.x;
                float zPos = render.transform.localPosition.z;


                if (xPos <= 0 || zPos <= 0 || xPos / boxSize == size-1 || zPos / boxSize == size-1) return;


                if (wallSelect == true)
                {



                    Instantiate(wallObject, hit.transform.position + pos, Quaternion.identity);
                    map[(int)xPos / boxSize, (int)zPos / boxSize] = '#';
                    //Debug.Log(render.transform.localPosition);
                }
                else if (potionSelect == true)
                {
                    Instantiate(potionObject, hit.transform.position + pos, Quaternion.identity);
                    map[(int)xPos / boxSize, (int)zPos / boxSize] = 'O';

                }
            }
        }


    }

    private void removeObject()
    {
        if (!removeSelect) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform press = hit.transform;
                var render = press.GetComponent<Renderer>();//the component
                float xPos = render.transform.localPosition.x;
                float zPos = render.transform.localPosition.z;
                if (xPos <= 0 || zPos <= 0 || (xPos / boxSize) == size-1 || (zPos / boxSize) == size-1) return;

                //Debug.Log("true");
                if (render.CompareTag("Wall") || render.CompareTag("Potion"))
                {
                    Destroy(press.gameObject);
                    Debug.Log(render.transform.localPosition);
                    map[(int)xPos / boxSize, (int)zPos / boxSize] = ' ';

                }
            }
        }
    }

    private void hitted()
    {
        //if (test == false) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // if a block is hitted
        {
            //Debug.Log(hit);

            Transform selection = hit.transform; // point the ground // get the block
            float xPos = hit.transform.localPosition.x;
            float zPos = hit.transform.localPosition.z;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }


            //======================================================================first one
            //resetMaterialColor(selection);
            //if (selection.CompareTag("Ground")) // if hitted point is ground
            //{
            //    if (pinnedRed != null) Destroy(pinnedRed);
            //    var render = selection.GetComponent<Renderer>();


            //    if (render != null)
            //    {
            //        //if (xbef != xPos || zPos != zbef || pinnedRed == null)
            //        //{

            //        //    if (xPos <= 0 || zPos <= 0 || xPos / boxSize >= size - 1 || zPos / boxSize >= size - 1)
            //        //        return;
            //        //    //redMap[(int)xPos / boxSize, (int)zPos / boxSize] = 'R';
            //        //    if (pinned != null) Destroy(pinnedRed);
            //        //    Vector3 pos = hit.transform.position;

            //        //    if (wallSelect == true)
            //        //    {
            //        //        pos.y = -1.7f;
            //        //        pinned = Instantiate(wallObject, pos, Quaternion.identity);
            //        //    }
            //        //    else if (potionSelect == true)
            //        //    {
            //        //        pos.y = -1f;
            //        //        pinned = Instantiate(potionObject, pos, Quaternion.identity);
            //        //    }
            //        //    xbef = xPos;
            //        //    zbef = zPos;

            //        //}

            //        //Vector3 pos = hit.transform.position;
            //        //pos.y = -2.7f;

            //        //if (pinned != null) Destroy(pinned);

            //        //if (wallSelect == true && map[(int)(xPos / boxSize), (int)(zPos / boxSize)] == ' ')
            //        //{   
            //        //    pinned = Instantiate(wallObject, pos, Quaternion.identity);
            //        //}
            //        //else if (potionSelect == true && map[(int)(xPos / boxSize), (int)(zPos / boxSize)] == ' ')
            //        //{
            //        //    pinned = Instantiate(potionObject,  pos, Quaternion.identity);
            //        //}else
            //        //{
            //        //    Destroy(pinned);
            //        //}

            //        createObject();

            //        render.material.color = greenMaterial.color;


            //    }
            //    // save the grounds
            //    selectedGround = selection;
            //}else if (selection.CompareTag("Wall") || selection.CompareTag("Potion"))
            //{

            //    if (pinned != null) Destroy(pinned);

            //    if (xbef != xPos || zPos != zbef || pinnedRed == null)
            //    {

            //        if (xPos <= 0 || zPos <= 0 || xPos / boxSize >= size - 1 || zPos / boxSize >= size - 1)
            //            return;
            //        //redMap[(int)xPos / boxSize, (int)zPos / boxSize] = 'R';
            //        if (pinnedRed != null) Destroy(pinnedRed);

            //        Vector3 pos = hit.transform.position;
            //        pos.y = -2.7f;


            //        pinnedRed = Instantiate(RedObject, pos, Quaternion.identity);
            //        xbef = xPos;
            //        zbef = zPos;

            //    }


            //    //Destroy(RedObject);


            //    if(map[(int)xPos/boxSize, (int)zPos / boxSize] != ' ')
            //    {
            //        removeObject();

            //    }else
            //    {
            //        createObject();
            //    }
            //}
            //==========================================================
            ///SECOND
            ///

            if (xPos <= 0 || zPos <= 0 || xPos / boxSize >= size - 1 || zPos / boxSize >= size - 1)return;

            float befX=0;
            float befZ=0;
            if (map[(int)xPos/boxSize, (int)zPos/boxSize] != ' ' && pinned!=null)
            {
                Destroy(pinned);
            }

            if (map[(int)xPos/boxSize, (int)zPos/boxSize] == ' ')
            {
                if (pinnedRed != null) Destroy(pinnedRed);
                Vector3 pos = hit.transform.position;
                pos.y = 0f;
                Vector3 pos2 = hit.transform.position;
                pos2.y = -2.7f;
                if (xPos!= befX || zPos!= befZ)
                {   if (pinned) Destroy(pinned);
                    if (wallSelect == true && map[(int)(xPos / boxSize), (int)(zPos / boxSize)] == ' ')
                    {
                        pinned = Instantiate(wallObject, pos, Quaternion.identity);
                        pinnedRed = Instantiate(GreenObject, pos2, Quaternion.identity);
                    }
                    else if (potionSelect == true && map[(int)(xPos / boxSize), (int)(zPos / boxSize)] == ' ')
                    {
                        pinned = Instantiate(potionObject, pos, Quaternion.identity);
                        pinnedRed = Instantiate(GreenObject, pos2, Quaternion.identity);
                    }
                    else
                    {
                        Destroy(pinned);
                    }
                    befX = xPos;
                    befZ = zPos;
                }
                createObject();
            }else
            {

                if (pinned != null) Destroy(pinned);

                if (xbef != xPos || zPos != zbef || pinnedRed == null)
                {

                    if (xPos <= 0 || zPos <= 0 || xPos / boxSize >= size - 1 || zPos / boxSize >= size - 1)
                        return;
                    if (pinnedRed != null) Destroy(pinnedRed);

                    Vector3 pos = hit.transform.position;
                    pos.y = -2.7f;


                    pinnedRed = Instantiate(RedObject, pos, Quaternion.identity);
                    xbef = xPos;
                    zbef = zPos;

                }
                removeObject();
            }









        }

    }

    public void OnClickWall()
    {
        wallSelect = true;
        potionSelect = false;
        removeSelect = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void OnClickPotion()
    {

        wallSelect = false;
        potionSelect = true;
        removeSelect = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void OnClickDelete()
    {
        wallSelect = false;
        potionSelect = false;
        removeSelect = true;
        Cursor.SetCursor(DeleteTexture, Vector2.zero, CursorMode.Auto);
    }
    

    public void floodFill(int y, int x)
    {
        if(tempMap[y,x] == '#' || tempMap[y, x] == 'F')
        {
            return;
        }
        if(tempMap[y,x] == ' ' || tempMap[y,x] == 'O')
        {
            tempMap[y, x] = 'F'; 
        }
        floodFill(y - 1, x);//top
        floodFill(y, x + 1);//right
        floodFill(y + 1, x);//down
        floodFill(y, x - 1);//left


    }
    public bool checkMapIsCanSave()
    {
        //for(int i=1; i<size-1; i++)
        //{
        //    for(int j=1; j<size-1; j++)
        //    {
        //        if (map[i, j - 1] == '#' && // kiri
        //            map[i - 1, j] == '#' && // atas 
        //            map[i, j + 1] == '#' && // kanan
        //            map[i+1, j ] == '#' ) // bawah
        //        {
        //            Debug.Log("tidak bisa");
        //            //Debug.Log("hello");
        //            //savedText.SetActive(true);
        //            //Debug.Log(savedText.SetActive(true));
        //            return false;
        //        }
        //    }
        //}
        //Debug.Log("bisa");
        //return true;
        fillTempMap();

        floodFill(1,1);
        
        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                if(tempMap[i,j] == ' ')
                {
                    return false;
                }
            }
        }

        return true;
    }
    void fillTempMap()
    {
        for (int i = 0; i < size ; i++)
        {
            for (int j = 0; j < size ; j++)
            {
                tempMap[i, j] = map[i, j];
            }

        }

    }

    public void OnClickSave()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        if (checkMapIsCanSave())
        {
            if (writeFile() == true)
            {
                Debug.Log("write success");

            }
            else
            {
                Debug.Log("write fail");
            }
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }else if (checkMapIsCanSave()==false)
        {
            Debug.Log("hello");
            savedText.SetActive(true);
            
            //savedText.SetActive(true);
        }



    }

    public bool writeFile()
    {
        string path;
        StreamWriter w = null;
        for(int i=1; i<=4; i++)
        {
            path = "Assets/" + i + ".txt";

            if (File.Exists(path) == false)
            {
                
                w = new StreamWriter(path, true);
                for (int j=0; j<size; j++)
                {
                    string line="";
                    for(int k=0; k < size; k++)
                    {
                        
                           line += map[j, k];
                    }
                    //Debug.Log(line);
                    w.WriteLine(line);
                }
                w.Close();
                return true;
            }
        }

        return false;
    }


    private void Init()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == 0 || j == 0 || j == size - 1 || i == size - 1)
                {
                    map[i, j] = '#';

                }
                else
                {
                    map[i, j] = ' ';

                }
                walls[i, j] = null;
                grounds[i, j] = null; 
            }
        }
    }

    private void PrintMap()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[i, j] == '#')
                {
                    walls[i, j] = Instantiate(wallObject, new Vector3(i * boxSize, 0, j * boxSize), Quaternion.identity);

                }
                grounds[i, j] = Instantiate(groundObject, new Vector3(i * boxSize, -3, j * boxSize), Quaternion.identity);
            }
        }



    }
}
