using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SARIMA : MonoBehaviour
{
    public GameObject myGraph;
    public WMG_Axis_Graph graph;
    public WMG_Series enConSerie1;
    public WMG_Series enConSerie2;
    public List<Vector2> consumptionData;
    public List<Vector2> SARPred;
    bool showGraph = false;
    // Start is called before the first frame update

    void Start()
    {
        //Define Variables using in start() function
        TextAsset questdata = Resources.Load<TextAsset>("prediction");
        string[] data = questdata.text.Split(new char[] { '\n' });


        for (int i = 1; i < data.Length - 1; i++)
        {
            //Define variables
            // RaycastHit hit;
            QuestPred q = new QuestPred();
            // Vector2 consumptionData = new Vector2();


            //seperate data from CSV and put the in the q list
            string[] row = data[i].Split(new char[] { ',' });

            //Point No.
            int.TryParse(row[0], out q.PointNo);

            //year of construction
            int.TryParse(row[1], out q.year);

            //Overall energy consumption
            float.TryParse(row[2], out q.overallEnCon);
            // Debug.Log(row[3]);

            float.TryParse(row[3], out q.prediction);
            // Debug.Log(q.prediction);


            //Prediction

            myGraph.SetActive(false);
            consumptionData.Add(new Vector2(q.PointNo, q.overallEnCon));
            if (q.prediction != 0)
            {
                SARPred.Add(new Vector2(q.PointNo, q.prediction));
                Debug.Log(q.prediction);
            }

            // Debug.Log(q.PointNo);

        }

        // showGraph = !showGraph;
        // myGraph.SetActive(showGraph);
        // consumptionData = new Vec


        graph = myGraph.transform.GetComponent<WMG_Axis_Graph>();
        enConSerie1 = graph.addSeries();
        enConSerie2 = graph.addSeries();
        enConSerie1.pointValues.SetList(consumptionData);
        enConSerie2.pointValues.SetList(SARPred);



    }

    // Update is called once per frame
    public void SARIMAPred()
    {
        showGraph = !showGraph;
        myGraph.SetActive(showGraph);
        // consumptionData = new Vec


        // graph = myGraph.transform.GetComponent<WMG_Axis_Graph>();
        // enConSerie1 = graph.addSeries();
        // enConSerie1.pointValues.SetList(consumptionData);


    }
}
