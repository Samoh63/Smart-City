using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

public class Quest
{
    public string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

    public string buildingName;
    public Vector3 locationString;

    public int year;
    public int yearOfConstruction;


    public float SV;   //S/V(Area/Volume)
    public float Area;


    public float[] enCon = new float[12]; //energy consumption in each month
    public float maxEnCon = 0;
    public float minEnCon = Mathf.Infinity;
    // public int years;
    public Color objectColor;

    //Energy consumption in a period of time 
    public float enConFromTo;

    public Color[] myChartColor = new Color[12];
}
