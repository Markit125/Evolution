using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class Enviroment : MonoBehaviour
{
    public TMP_Text ST;
    public List<GameObject> food;
    public List<GameObject> bacterias;
    public bool ChangeFood, UpdateDate, HR;
    public int numf = 0, numb = 0;
    private float s, y, tme;
    private bool Stats;
    private string[] stsname = new string[] { "Времени прошло: ", "Бактерий: ", "Еды: ", "Длина: ", "Ширина: ", "Плошадь: ", "Обзор: ", "Скорость: ", "Hачальная Е: ", "Е почкования: ", "Энергия: ", "Потребление E: ", "Xишность, %: ", "Е еды: " };
    
    public static Enviroment Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        tme = Mathf.Round(Time.time);
    }
    void Start()
    {
        Stats = false;
        ChangeFood = false;
        food = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
        bacterias = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bacteria"));
        numb = bacterias.Count;
    }

    void Update()
    {
        if (ChangeFood)
        {
            bacterias = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bacteria"));
            food = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
            ChangeFood = false;
        }
    }

    private void FixedUpdate()
    {
        if (Stats) CalculateStats();
    }
    private void CalculateStats()
    {
        ST.text = "";
        float[] av = new float[stsname.Length];
        //  0       1          2        3     4     5    6    7     8      9     10     11     12
        // time countBacts countFood length width area View speed startE pochE Energy consump pre
        for (byte i = 0; i < stsname.Length; i++) av[i] = 0;

        av[0] = Mathf.Round(Time.time) - tme;
        av[1] = bacterias.Count;
        av[2] = food.Count;

        for (int i = 0; i < bacterias.Count; i++)
        {
            if (bacterias[i] != null)
            {
                av[3] += bacterias[i].transform.localScale.y;
                av[4] += bacterias[i].transform.localScale.x;
                av[5] += bacterias[i].GetComponent<UnitMove>().area;
                av[6] += bacterias[i].GetComponent<UnitMove>().View;
                av[7] += bacterias[i].GetComponent<UnitMove>().speed;
                av[8] += bacterias[i].GetComponent<UnitMove>().startEnergy;
                av[9] += bacterias[i].GetComponent<UnitMove>().pochEnergy;
                av[10] += bacterias[i].GetComponent<UnitMove>().energy;
                av[11] += bacterias[i].GetComponent<UnitMove>().consump;
                av[12] += bacterias[i].GetComponent<UnitMove>().pre;
            }
        }
        for (int i = 0; i < food.Count; i++) if (food[i] != null) av[13] += food[i].GetComponent<FoodPower>().energyf;
        av[3] *= 200;
        av[4] *= 100;
        av[5] *= 2000;
        av[12] *= 100;
        if (bacterias.Count > 0) for (byte i = 3; i < stsname.Length - 1; i++) av[i] /= bacterias.Count;
        if (food.Count > 0) av[13] /= food.Count;

        for (byte i = 0; i < stsname.Length; i++)
        {
            ST.text += stsname[i] + Math.Round(av[i], 2).ToString() + '\n';
            if (i == 2) ST.text += "    Средние значения:\n";
        }
    }
    public void StatsView()
    {
        Stats = !Stats;
        ST.GetComponent<TMP_Text>().enabled = Stats;
    }

    //public void WeiWrite(int a)
    //{
    //    if (!File.Exists("Stats/Weights.txt"))
    //    {
    //        using (StreamWriter sw = File.CreateText("Stats/Weights.txt"))
    //        {
    //            WriteTXT(sw, a);
    //        }
    //    }
    //    else
    //    {
    //        using (StreamWriter sw = File.AppendText("Stats/Weights.txt"))
    //        {
    //            WriteTXT(sw, a);
    //        }
    //    }
    //}
    ////void FixedUpdate()
    ////{
    ////    if (Time.time > tm + 10)
    ////    {
    ////        bacterias = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bacteria"));
    ////        //
    ////        if (!File.Exists("Stats/BactCount.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactCount.txt"))
    ////            {
    ////                if (bacterias.Count != null)
    ////                    sw.WriteLine(bacterias.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactCount.txt"))
    ////            {
    ////                if (bacterias.Count != null)
    ////                    sw.WriteLine(bacterias.Count);
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/BactSpeed.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactSpeed.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().speed;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactSpeed.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().speed;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/BactView.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactView.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().View;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactView.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().View;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/BactPochEnergy.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactPochEnergy.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().pochEnergy;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactPochEnergy.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().pochEnergy;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/BactScale.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactScale.txt"))
    ////            {
    ////                s = 0;
    ////                y = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<Transform>().localScale.x;
    ////                    if (bacterias[i] != null)
    ////                        y += bacterias[i].GetComponent<Transform>().localScale.y;
    ////                }
    ////                sw.WriteLine((s / bacterias.Count) + " " + (y / bacterias.Count));
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactScale.txt"))
    ////            {
    ////                s = 0;
    ////                y = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<Transform>().localScale.x;
    ////                    if (bacterias[i] != null)
    ////                        y += bacterias[i].GetComponent<Transform>().localScale.y;
    ////                }
    ////                sw.WriteLine((s / bacterias.Count) + " " + (y / bacterias.Count));
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/BactConsump.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/BactConsump.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().consump;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/BactConsump.txt"))
    ////            {
    ////                s = 0;
    ////                for (int i = 0; i < bacterias.Count; i++)
    ////                {
    ////                    if (bacterias[i] != null)
    ////                        s += bacterias[i].GetComponent<UnitMove>().consump;
    ////                }
    ////                sw.WriteLine(s / bacterias.Count);
    ////            }
    ////        }
    ////        //

    ////        //
    ////        if (!File.Exists("Stats/FoodCount.txt"))
    ////        {
    ////            using (StreamWriter sw = File.CreateText("Stats/FoodCount.txt"))
    ////            {
    ////                if (food.Count != null)
    ////                    sw.WriteLine(food.Count);
    ////            }
    ////        }
    ////        else
    ////        {
    ////            using (StreamWriter sw = File.AppendText("Stats/FoodCount.txt"))
    ////            {
    ////                if (food.Count != null)
    ////                    sw.WriteLine(food.Count);
    ////            }
    ////        }
    ////        //

    ////        tm += 10;
    ////    }
    ////}

    //void WriteTXT(StreamWriter txt, int pts)
    //{
    //    txt.WriteLine("==============" + pts + "==============");
    //    for (int i = 0; i < 4; i++)
    //    {
    //        txt.WriteLine(ns.weights[i][0] + "f, " + ns.weights[i][1] + "f, " + ns.weights[i][2] + "f, " + ns.weights[i][3] + "f, " + ns.weights[i][4] + "f, " + ns.weights[i][5] + "f, " + ns.weights[i][6] + "f, " + ns.weights[i][7] + "f, " + ns.weights[i][8] + "f ");
    //    }
    //    for (int i = 4; i < 6; i++)
    //    {
    //        txt.WriteLine(ns.weights[i][0] + "f, " + ns.weights[i][1] + "f, " + ns.weights[i][2] + "f, " + ns.weights[i][3] + "f ");
    //    }
    //}

    //public void WeiWriteBest(int a)
    //{
    //    if (!File.Exists("Stats/WeightsBest.txt"))
    //    {
    //        using (StreamWriter sw = File.CreateText("Stats/WeightsBest.txt"))
    //        {
    //            WriteTXT(sw, a);
    //        }
    //    }
    //    else
    //    {
    //        using (StreamWriter sw = File.AppendText("Stats/WeightsBest.txt"))
    //        {
    //            WriteTXT(sw, a);
    //        }
    //    }
    //}
}
