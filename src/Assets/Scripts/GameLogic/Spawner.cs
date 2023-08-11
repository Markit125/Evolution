using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int countFood, mine, maxe, gro, rot, countPerCycle;
    public float FoodTime, MaxGrow;
    public float MapX, MapY;
    private GameObject FoodPrefab;
    public float timer;
    public bool grow;

    void Start()
    {
        FoodPrefab = GetComponent<InsertSet>().Food;
        SpawnFood();
        GetComponent<Enviroment>().numf = 0;
        GetComponent<Enviroment>().enabled = true;
        timer = Mathf.Round(Time.time);
    }
    void FixedUpdate()
    {
        if (timer + FoodTime < Time.time)
        {
            for (int i = 0; i < countPerCycle; i++)
            {
                timer = Time.time;
                GameObject foodti = Instantiate(FoodPrefab, new Vector3(Random.Range(-MapX / 2, MapX / 2), Random.Range(-MapY / 2, MapY / 2), 0f), Quaternion.identity);
                FoodSettings(foodti);
            }
        }
    }
    public void SpawnFood()
    {
        for (int i = 0; i < countFood; i++)
        {
            GameObject Food = Instantiate(FoodPrefab, new Vector3(Random.Range(-MapX / 2, MapX / 2), Random.Range(-MapY / 2, MapY / 2), 0f), Quaternion.identity);
            FoodSettings(Food);
            Food.name = "Food" + i;
        }
    }
    private void FoodSettings(GameObject go)
    {
        if (grow) go.GetComponent<FoodPower>().grow = true;
        go.GetComponent<FoodPower>().maxE = maxe;
        go.GetComponent<FoodPower>().minE = mine;
        go.GetComponent<FoodPower>().gro = gro;
        go.GetComponent<FoodPower>().rot = rot;
        go.GetComponent<FoodPower>().MaxGrow = MaxGrow;
    }
}
