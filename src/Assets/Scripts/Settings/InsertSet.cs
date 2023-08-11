using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class InsertSet : MonoBehaviour
{
    public Camera cam;
    public GameObject Bacteria0, BacteriaLR, Bacteria, Food, Food0, FoodLR;
    const byte integers = 22, toggles = 2;
    private Spawner food;
    private BactSpawn bact;
    private string[] s;
    public string[] s1;
    public float x;

    private void Start()
    {
        s = File.ReadAllLines(Application.persistentDataPath + "/Set.txt");
        s1 = s[s.Length - 1].Split();
        food = GetComponent<Spawner>();
        bact = GetComponent<BactSpawn>();

        if (s1[integers + 1] == "1")
        {
            x = 1;
            Bacteria = Bacteria0;
            Food = Food0;
        }
        else
        {
            x = 6.425f;
            Bacteria = BacteriaLR;
            Food = FoodLR;
        }

        food.countFood = int.Parse(s1[0]);
        food.FoodTime = float.Parse(s1[1]);
        food.countPerCycle = int.Parse(s1[2]);
        food.MapX = int.Parse(s1[10]);
        food.MapY = int.Parse(s1[11]);
        food.maxe = int.Parse(s1[12]);
        food.mine = int.Parse(s1[13]);
        food.gro = int.Parse(s1[14]);
        food.rot = int.Parse(s1[15]);
        food.MaxGrow = float.Parse(s1[19]) / 100 + 1;
        food.grow = s1[integers] == "1" ? true : false;
        food.enabled = true;

        SpawnType(0);
        SpawnType(integers + toggles);
        GetComponent<Controls>().enabled = true;
        cam.GetComponent<Camera>().orthographicSize = float.Parse(GetComponent<InsertSet>().s1[10]) / 2.5f;
    }

    public void SpawnType(byte j)
    {
        bact.len = float.Parse(s1[j + 3]) / 200 * x;
        bact.wid = float.Parse(s1[j + 4]) / 100 * x;
        bact.speed = int.Parse(s1[j + 5]);
        bact.View = int.Parse(s1[j + 6]);
        bact.startEnergy = int.Parse(s1[j + 7]);
        bact.pochEnergy = int.Parse(s1[j + 8]);
        bact.countBacts = int.Parse(s1[j + 9]);
        bact.xb = int.Parse(s1[j + 10]);
        bact.yb = int.Parse(s1[j + 11]);
        bact.Mutation = float.Parse(s1[j + 16]);
        bact.Ro = int.Parse(s1[j + 17]);
        bact.pre = float.Parse(s1[j + 18]) / 100;
        bact.attack = float.Parse(s1[j + 20]) / 100;
        bact.defence = float.Parse(s1[j + 21]) / 100;
        bact.Spawn();
    }
}
