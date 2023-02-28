using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Map;

using Mapbox.Examples;
using Mapbox.Unity.MeshGeneration.Modifiers;


public class importCSV : MonoBehaviour
{
    public Dropdown methodDropdownMenu;
    public Dropdown typeDropdownMenu;
    public Dropdown year;
    public Dropdown monthFrom;
    public Dropdown monthTo;

    //Define of Variables in the whole importCSV class
    // Dictionary<string, Quest> myData = new Dictionary<string, Quest>();
    public static List<Quest> quests = new List<Quest>();

    // Quest q = new Quest();
    Vector2d _Locations = new Vector2d();

    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    [Geocode]
    // string[] _locationStrings;





    string RaycastRet;
    Vector3 org;

    public static float max_enCon = 0f;

    public GameObject FloatingTextPrefab;
    public static List<GameObject> myTextList = new List<GameObject>();




    void Start()
    {
        //Define Variables using in start() function
        TextAsset questdata = Resources.Load<TextAsset>("Smart_city_bottomToUp");
        string[] data = questdata.text.Split(new char[] { '\n' });

        float max_enCon_temp = 0f;
        Vector3 UnityCo;



        for (int i = 1; i < data.Length - 1; i++)
        {
            //Define variables
            RaycastHit hit;
            Quest q = new Quest();



            //seperate data from CSV and put the in the q list
            string[] row = data[i].Split(new char[] { ',' });

            //Convert Global location to Unity Location
            _Locations = Conversions.StringToLatLon(row[0] + ',' + row[1]);
            UnityCo = _map.GeoToWorldPosition(_Locations, true);
            q.locationString = UnityCo;

            //year
            int.TryParse(row[2], out q.year);

            //year of Construction
            int.TryParse(row[3], out q.yearOfConstruction);

            //Area/Volume
            float.TryParse(row[4], out q.SV);

            //Area
            float.TryParse(row[5], out q.Area);

            //Building Name
            // q.buildingName = row[6];

            for (int j = 0; j < 12; j++)
            {
                float.TryParse(row[j + 7], out q.enCon[j]);
                q.enCon[j] = q.enCon[j] * q.Area;
                if (q.enCon[j] > q.maxEnCon)
                {
                    q.maxEnCon = q.enCon[j];
                    // Debug.Log(j);
                    // Debug.Log(q.maxEnCon);

                }
                if (q.enCon[j] < q.minEnCon)
                {
                    q.minEnCon = q.enCon[j];
                    // Debug.Log(q.minEnCon);
                }


            }


            //Find object base the Unity Location

            org = new Vector3(UnityCo[0], 20, UnityCo[2]); //The origine of Raycasting
            Debug.DrawRay(org, Vector3.down * 50, Color.blue);
            if (Physics.Raycast(org, Vector3.down, out hit, 1000f))
            {

                string RaycastRet = hit.collider.gameObject.name;
                q.buildingName = RaycastRet;
                quests.Add(q);

            }
            else
            {
                // Debug.Log(RaycastRet);
                q.buildingName = "No object is found";
                quests.Add(q);
            }


        }
        foreach (Quest item in quests)
        {
            // Debug.Log(item.buildingName);
            // Debug.Log(item.enCon[2]);
            max_enCon_temp = Mathf.Max(item.enCon);
            if (max_enCon_temp > max_enCon)
            {
                max_enCon = max_enCon_temp;
            }
        }
        // Debug.Log(max_enCon);


    }

    // Update is called once per frame
    void Update()
    {


        GameObject FoundObject;
        // RaycastHit hit;
        Vector3 org1;

        //year
        int jj = 0;
        int currentYear = 2017 + year.value;
        // Debug.Log(year.value);

        if (typeDropdownMenu.value == 1)
        {
            foreach (Quest item in quests)
            {

                if (item.year == currentYear)
                {
                    // Debug.Log("aa");
                    FoundObject = GameObject.Find(item.buildingName);
                    org1 = new Vector3(item.locationString[0], 20, item.locationString[2]); //The origine of Raycasting
                    Debug.DrawRay(org1, Vector3.down * 50, Color.blue);



                    if (monthFrom.value <= monthTo.value)
                    {
                        item.enConFromTo = 0f;
                        int yearDiff = monthTo.value - monthFrom.value;

                        for (int i = monthFrom.value; i <= monthTo.value; i++)
                        {
                            item.enConFromTo = item.enConFromTo + item.enCon[i];
                        }

                        // Color of building based on normalize accumulative consumption energy
                        //Normalize accumulative consumption energy
                        //item.enConFromTo / ((monthTo.value - monthFrom.value + 1) * max_enCon);

                        item.objectColor = new Color(item.enConFromTo / ((monthTo.value - monthFrom.value + 1) * max_enCon), 1 - item.enConFromTo / ((monthTo.value - monthFrom.value + 1) * max_enCon), 0);

                        if (FoundObject != null)
                        {
                            jj += 1;
                            FoundObject.GetComponent<Renderer>().materials[0].color = item.objectColor;
                            FoundObject.GetComponent<Renderer>().materials[1].color = item.objectColor;

                        }

                    }

                }


            }


        }

        if (Input.GetMouseButtonDown(0) && typeDropdownMenu.value == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 100))
            {
                FoundObject = hit.transform.gameObject;
                List<Quest> result = importCSV.quests.FindAll(x => (x.buildingName == FoundObject.name));
                foreach (Quest item in result)
                {
                    // if (item.year == year.value)
                    // {
                    // Debug.Log(item.buildingName);
                    GameObject FloatingText = Instantiate(FloatingTextPrefab, new Vector3(item.locationString[0], 10f, item.locationString[2]), Quaternion.identity) as GameObject;
                    // FloatingText.GetComponent<TMPro>.text = 
                    myTextList.Add(FloatingText);

                }
            }
        }




    }







}

