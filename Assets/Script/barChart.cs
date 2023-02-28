using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChartAndGraph;


public class barChart : MonoBehaviour
{
    public Dropdown methodDropdownMenu;
    public Dropdown typeDropdownMenu;
    public GameObject prefab;
    public Transform myRotation;
    public Dropdown yearDropDown;
    public Material[] myMaterials;

    public List<Color> myBarChartColor = new List<Color>();
    public static List<GameObject> myBarCharts = new List<GameObject>();

    void Update()
    {

        GameObject FoundObject;


        if (Input.GetMouseButtonDown(0) && typeDropdownMenu.value == 2)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;



            if (Physics.Raycast(ray, out hit, 100))
            {
                FoundObject = hit.transform.gameObject;

                // Debug.Log(FoundObject.name);
                FoundObject.GetComponent<Renderer>().materials[0].color = Color.red;
                FoundObject.GetComponent<Renderer>().materials[1].color = Color.red;
                int.TryParse(yearDropDown.options[yearDropDown.value].text, out int curYear);
                List<Quest> result = importCSV.quests.FindAll(x => (x.buildingName == FoundObject.name) && (x.year == curYear));
                // List<Quest> result1 = importCSV.quests.FindAll(x => x.year == year.value);
                // Debug.Log();
                // Debug.Log(year.year.GetType());
                foreach (Quest item in result)
                {
                    // Debug.Log(item.year.GetType());
                }


                foreach (Quest item in result)
                {
                    // if (item.year == year.value)
                    // {
                    // Debug.Log(item.buildingName);
                    GameObject instantiatedObject = Instantiate(prefab, new Vector3(item.locationString[0] - 20f, 5f, item.locationString[2]), Quaternion.identity) as GameObject;
                    myBarCharts.Add(instantiatedObject);
                    for (int i = 0; i < 12; i++)
                    {
                        //Instantiate Barchart on the selected building
                        myBarCharts[myBarCharts.Count - 1].GetComponent<WorldSpaceBarChart>().DataSource.SetValue(item.months[i], "Metal", item.enCon[i]);
                        Color tempColor = new Color((item.enCon[i] - item.minEnCon) / (item.maxEnCon - item.minEnCon), (item.maxEnCon - item.enCon[i]) / (item.maxEnCon - item.minEnCon), 0);

                        // Color tempColor = new Color(item.enCon[i] / importCSV.max_enCon, 1 - item.enCon[i] / importCSV.max_enCon, 0);
                        myBarChartColor.Add(tempColor);

                        //Change the color of bars based on value
                        // myMaterials[i].color =
                        // item.myChartColor[i] = new Color(1, 0, 0);
                        instantiatedObject.GetComponent<WorldSpaceBarChart>().DataSource.GetMaterial(item.months[i]).Normal.color = myBarChartColor[myBarChartColor.Count - 1];
                    }
                    // }


                }

            }

        }


    }
}
