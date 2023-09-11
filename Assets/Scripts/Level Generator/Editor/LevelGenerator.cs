using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    string[,] playfield = new string[6, 6]
    {
        {"0","0","0","0","0","0"}
        ,{"0","0","0","0","0","0"}
        ,{"0","0","0","0","0","0"}
        ,{"0","0","0","0","0","0"}
        ,{"0","0","0","0","0","0"}
        ,{"0","0","0","0","0","0"}
    };
    [Range(0,8)]
    [SerializeField] int twoSizeCarsAmount = 3;
    [Range(0, 8)]
    [SerializeField] int threeSizeCarsAmount = 3;
    [Space]
    [Header("Prefabs")]
    [SerializeField] GameObject redCarPrefab;
    [SerializeField] GameObject[] carPrefabs;
    [SerializeField] GameObject[] truckPrefabs;
    public Transform playfieldReferencePoint;

    [HideInInspector]
    [SerializeField]List<GameObject> allCarsCreated = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateThePlayfield();
    }

    string DebugBoard(string[,] field)
    {
        string s = "";

        for (int y = 5; y >= 0; y--)
        {
            for (int x = 0; x <= 5; x++)
            {
                s += field[x, y];
                if (x == 5)
                {
                    s += "\n";
                }
            }
        }

        return s;
    }

    void PlaceInBoard(int x, int y, string identifier)
    {
        playfield[x, y] = identifier;
    }

    void PlaceRedInBoard()
    {
        //Column
        int x1 = Random.Range(0,3); // 0,1,2
        int x2 = x1 + 1;
        //Row
        int y = 3;
        //Place in Playfield
        PlaceInBoard(x1, y, "R");
        PlaceInBoard(x2, y, "R");
        //Debug
        print(DebugBoard(playfield));
    }

    void PlaceEnemyInBoard(int length, string identifier)
    {
        bool posFound = false;
        int maxSearches = 20; //Max trys to find valid pos
        int currentSearches = 0;

        while (!posFound && currentSearches < maxSearches)
        {
            //Choose Random Pos
            int x = Random.Range(0, 6);
            int y = Random.Range(0, 6);
            List<Vector2Int[]> possiblePlaces = new List<Vector2Int[]>();
            List<Vector2Int[]> validPlaces = new List<Vector2Int[]>();
            //is the current spot free
            if (playfield[x,y] == "0")
            {
                switch (length)
                {
                    case 2:
                        if (y != 3)//Red Lane Prevention
                        {
                            //can also check for left and right
                            Vector2Int[] left2 = { new Vector2Int(x, y), new Vector2Int(x - 1, y) };
                            possiblePlaces.Add(left2);
                            Vector2Int[] right2 = { new Vector2Int(x, y), new Vector2Int(x + 1, y) };
                            possiblePlaces.Add(right2);
                        }
                        Vector2Int[] up2 = { new Vector2Int(x, y), new Vector2Int(x, y + 1) };
                        possiblePlaces.Add(up2);
                        Vector2Int[] down2 = { new Vector2Int(x, y), new Vector2Int(x, y - 1) };
                        possiblePlaces.Add(down2);

                        break;
                    case 3:
                        if (y != 3)//Red Lane Prevention
                        {
                            //can also check for left and right
                            Vector2Int[] left3 = { new Vector2Int(x, y), new Vector2Int(x - 1, y), new Vector2Int(x - 2, y) };
                            possiblePlaces.Add(left3);
                            Vector2Int[] right3 = { new Vector2Int(x, y), new Vector2Int(x + 1, y), new Vector2Int(x + 2, y) };
                            possiblePlaces.Add(right3);
                        }
                        Vector2Int[] up3 = { new Vector2Int(x, y), new Vector2Int(x, y + 1), new Vector2Int(x, y + 2) };
                        possiblePlaces.Add(up3);
                        Vector2Int[] down3 = { new Vector2Int(x, y), new Vector2Int(x, y - 1), new Vector2Int(x, y - 2) };
                        possiblePlaces.Add(down3);

                        break;
                }

                // Loop through the possible places list
                for (int i = 0; i < possiblePlaces.Count; i++)
                {
                    for (int j = 0; j < possiblePlaces[i].Length; j++)
                    {
                        // Check if all single positions are free or valid
                        if (PositionIsFree(possiblePlaces[i]))
                        {
                            validPlaces.Add(possiblePlaces[i]);
                        }
                    }
                }

                //Loop through valid places - if there any
                if(validPlaces.Count > 0)
                {
                    int randomIndex = Random.Range(0, validPlaces.Count);
                    for (int i = 0; i < validPlaces[randomIndex].Length; i++)
                    {
                        //place all positions in the board
                        PlaceInBoard(validPlaces[randomIndex][i].x, validPlaces[randomIndex][i].y, identifier);
                    }
                    posFound = true;
                }
                else
                {
                    currentSearches++;
                }
            }
        }
        print(DebugBoard(playfield));
    }

    bool PositionIsFree(Vector2Int[] allPositions)
    {
        for(int i = 0; i < allPositions.Length; i++)
        {
            if (SinglePositionFree(allPositions[i]) == false)
            {
                return false;
            }
        }
        return true;
    }

    bool SinglePositionFree(Vector2Int position)
    {
        // Inside boundarys of the board
        if(position.x >= 0 && position.x < 6 && position.y >= 0 && position.y < 6)
        {
            if (playfield[position.x, position.y] == "0")//empty space found
            {
                return true;
            }
        }
        return false;
    }

    public void CreateThePlayfield()
    {
        //Delete all first
        DeletePlayfield();

        //Place Red
        PlaceRedInBoard();

        //Create 2 size cars
        CreateTwoSizeCars();

        //Create 3 size cars
        CreateThreeSizeCars();

        //Create the physical Playfield
        CreatePhysicalPlayfield();
    }

    public void DeletePlayfield()
    {
        //Empty string board
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                playfield[x, y] = "0";
            }
        }

        //Delete the physical cars
        foreach (GameObject car in allCarsCreated)
        {
            DestroyImmediate(car);
        }
        allCarsCreated.Clear();
    }

    void CreateTwoSizeCars()
    {
        // 8 identifiers for 2-size cars
        List<string> identifiersList = new List<string>(){"S", "T", "U", "V", "W", "X", "Y", "Z" };
        for (int i = 1; i <= twoSizeCarsAmount; i++)
        {
            string identifier = identifiersList[Random.Range(0, identifiersList.Count)];
            PlaceEnemyInBoard(2, identifier);
            identifiersList.Remove(identifier);
        }
    }

    void CreateThreeSizeCars()
    {
        // 8 identifiers for 3-size cars
        List<string> identifiersList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
        for (int i = 0; i < threeSizeCarsAmount; i++)
        {
            string identifier = identifiersList[Random.Range(0, identifiersList.Count)];
            PlaceEnemyInBoard(3, identifier);
            identifiersList.Remove(identifier);
        }
    }

    void CreatePhysicalPlayfield()
    {
        //Place red car in the correct rotation
        Vector3[] rotations = {Vector3.zero, new Vector3(0,90,0), new Vector3(0, 180, 0), new Vector3(0, 270, 0)};

        //Red car
        for (int x = 0; x < 6; x++)
        {
            if (playfield[x, 3] == "R")
            {
                Vector3 pos = new Vector3(playfieldReferencePoint.position.x + x + 1
                                        , playfieldReferencePoint.position.y
                                        , playfieldReferencePoint.position.z + 3);
                GameObject redCar = UnityEditor.PrefabUtility.InstantiatePrefab(redCarPrefab) as GameObject;
                redCar.transform.position = pos;
                redCar.transform.rotation = Quaternion.Euler(rotations[1]);
                allCarsCreated.Add(redCar);
                break;
            }
        }

        //Other cars
        List<string> usedIdentifiers = new List<string>();
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                //Ignore emptys and red
                if (playfield[x,y] == "0" || playfield[x, y] == "R")
                {
                    continue;
                }

                string foundLetter = playfield[x,y];
                if (usedIdentifiers.Contains(foundLetter)) 
                {
                    continue;
                }

                int lengthOfCar = 0;
                int direction = -1;

                //check direction and size
                for (int i = x - 2; i <= x + 2; i++)
                {
                    for (int j = y - 2; j <= y + 2; j++)
                    {
                        //Out of bound
                        if(i < 0 || i > 5 || j < 0 || j > 5)
                        { 
                            continue;
                        }
                        //Is start point - ignore
                        //if(i == 0 && j == 0)
                        //{
                        //    continue;
                        //}
                        //Direction
                        if (playfield[i,j] == foundLetter)
                        {
                            lengthOfCar++;
                            if(i < x)
                            {
                                direction = 1;//left
                            }
                            else if(i > x)
                            {
                                direction = 3;//right
                            }
                            else if (j < y)
                            {
                                direction = 0;//down
                            }
                            else if (j > y)
                            {
                                direction = 2;//up
                            }
                        }
                    }
                }
                usedIdentifiers.Add(foundLetter);
                print(foundLetter + " has a size of: " + lengthOfCar);
                //instantiatte in the correct way
                switch (lengthOfCar)
                {
                    case 2:
                        Vector3 pos = new Vector3(
                            playfieldReferencePoint.position.x + x
                            ,playfieldReferencePoint.position.y
                            ,playfieldReferencePoint.position.z + y);
                        int randomIndex = Random.Range(0,carPrefabs.Length);
                        GameObject newCar = UnityEditor.PrefabUtility.InstantiatePrefab(carPrefabs[randomIndex]) as GameObject;
                        newCar.transform.position = pos;
                        newCar.transform.rotation = Quaternion.Euler(rotations[direction]);
                        allCarsCreated.Add(newCar);
                        break;
                    case 3:
                        Vector3 pos1 = new Vector3(
                            playfieldReferencePoint.position.x + x
                            , playfieldReferencePoint.position.y
                            , playfieldReferencePoint.position.z + y);
                        int randomTruckIndex = Random.Range(0, truckPrefabs.Length);
                        GameObject newTruck = UnityEditor.PrefabUtility.InstantiatePrefab(truckPrefabs[randomTruckIndex]) as GameObject;
                        newTruck.transform.position = pos1;
                        newTruck.transform.rotation = Quaternion.Euler(rotations[direction]);
                        allCarsCreated.Add(newTruck);
                        break;
                }
            }
        }
    }
}
