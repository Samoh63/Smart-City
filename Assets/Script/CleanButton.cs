using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanButton : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject FoundObject;
    // RaycastHit hit;
    Vector3 org1;
    public void onclick()

    {
        // 
        foreach (Quest item in importCSV.quests)
        {

            org1 = new Vector3(item.locationString[0], 20, item.locationString[2]); //The origine of Raycasting
            FoundObject = GameObject.Find(item.buildingName);
            if (FoundObject != null)
            {
                FoundObject.GetComponent<Renderer>().materials[0].color = Color.gray;
                FoundObject.GetComponent<Renderer>().materials[1].color = Color.gray;

            }
        }
        // var clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject clone in barChart.myBarCharts)
        {
            Destroy(clone);
        }
        foreach (GameObject clone in importCSV.myTextList)
        {
            Destroy(clone);
        }

    }


}
