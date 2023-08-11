//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NeuroBactSpawn : MonoBehaviour
//{
//    public int countBests = 3, countBacts = 20, xb = 6, yb = 4, rec, best = 12;
//    public GameObject BacteriaPrefab, fooooood, BestBacteriaPrefab, Foodplate;
//    public bool train = true;
//    public float[][] input = new float[3][];
//    public float[][] weights = new float[6][], sweights = new float[6][], bestweights = new float[6][];
//    public float[][] edu = new float[50][];
//    public float time, refresh = 15;
//    public float speed = 50, pochEnergy = 2000, energy = 500, View = 20;
//    public Vector3 scale = new Vector3(0.4f, 0.4f, 0f);

//    public static NeuroBactSpawn Instance { get; private set; }

//    void Awake()
//    {
//        Instance = this;
//    }
//    void Start()
//    {
//        time = 0;
//        best = 0;
//        input[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        input[1] = new float[4] { 0f, 0f, 0f, 0f };
//        input[2] = new float[2] { 0f, 0f };

//        sweights[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        sweights[1] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        sweights[2] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        sweights[3] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        sweights[4] = new float[4] { 0f, 0f, 0f, 0f };
//        sweights[5] = new float[4] { 0f, 0f, 0f, 0f };

//        bestweights[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        bestweights[1] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        bestweights[2] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        bestweights[3] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        bestweights[4] = new float[4] { 0f, 0f, 0f, 0f };
//        bestweights[5] = new float[4] { 0f, 0f, 0f, 0f };

//        weights[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[1] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[2] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[3] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[4] = new float[4] { 0f, 0f, 0f, 0f };
//        weights[5] = new float[4] { 0f, 0f, 0f, 0f };

//        time = -refresh;
//    }

//    void FixedUpdate()
//    {
//        if (time + refresh < Time.time)
//        {
//            if (train)
//            {
//                time += refresh;

//                // Best weights
//                rec = 0;
//                for (int i = 0; i < GetComponent<Enviroment>().bacteriasN.Count; i++)
//                {
//                    if (GetComponent<Enviroment>().bacteriasN[i] != null)
//                    {
//                        if (GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>() != null)
//                        {
//                            if (rec <= GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>().points)
//                            {
//                                rec = GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>().points;
//                                //weights = GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>().weights;
//                                //Debug.Log("Neuro");
//                                for (int k = 0; k < 4; k++)
//                                {
//                                    for (int j = 0; j < 9; j++)
//                                    {
//                                        weights[k][j] = GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>().weights[k][j];
//                                    }
//                                }
//                                for (int k = 4; k < 6; k++)
//                                {
//                                    for (int j = 0; j < 4; j++)
//                                    {
//                                        weights[k][j] = GetComponent<Enviroment>().bacteriasN[i].GetComponent<NeuroNet>().weights[k][j];
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            if (rec <= GetComponent<Enviroment>().bacteriasN[i].GetComponent<BNeuroNet>().points)
//                            {
//                                rec = GetComponent<Enviroment>().bacteriasN[i].GetComponent<BNeuroNet>().points;
//                                //weights = GetComponent<Enviroment>().bacteriasN[i].GetComponent<BNeuroNet>().weights;
//                                //Debug.Log("BestNeuro");
//                                for (int k = 0; k < 4; k++)
//                                {
//                                    for (int j = 0; j < 9; j++)
//                                    {
//                                        weights[k][j] = GetComponent<Enviroment>().bacteriasN[i].GetComponent<BNeuroNet>().weights[k][j];
//                                    }
//                                }
//                                for (int k = 4; k < 6; k++)
//                                {
//                                    for (int j = 0; j < 4; j++)
//                                    {
//                                        weights[k][j] = GetComponent<Enviroment>().bacteriasN[i].GetComponent<BNeuroNet>().weights[k][j];
//                                    }
//                                }
//                            }
//                        }
//                    }
//                    Destroy(GetComponent<Enviroment>().bacteriasN[i]);
//                }
//                for (int i = 0; i < GetComponent<Enviroment>().food.Count; i++)
//                {
//                    Destroy(GetComponent<Enviroment>().food[i]);
//                }

//                Destroy(GameObject.FindGameObjectWithTag("Fooooood"));

//                PereferBest();

//                if (rec >= best)
//                {
//                    //Debug.Log("New Best!    Points: " + rec.ToString());
//                    best = rec;
//                    //GetComponent<Enviroment>().WeiWriteBest(best);
//                    for (byte k = 0; k < 4; k++)
//                    {
//                        for (byte j = 0; j < 9; j++)
//                        {
//                            bestweights[k][j] = weights[k][j];
//                        }
//                    }
//                    for (byte k = 4; k < 6; k++)
//                    {
//                        for (byte j = 0; j < 4; j++)
//                        {
//                            bestweights[k][j] = weights[k][j];
//                        }
//                    }
//                }

//                PereferBact();

//                //for (sbyte i = -1; i < xb + 1; i++)
//                //{
//                //    for (int k = -1; k < yb + 1; k += yb + 1)
//                //    {
//                //        Instantiate(fooooood, new Vector3(-64.5f + i * 25, -37f + k * 25, 0f), Quaternion.identity);
//                //    }
//                //}

//                //for (int k = 0; k < yb; k++)
//                //{
//                //    for (int i = -1; i < xb + 1; i += xb + 1)
//                //    {
//                //        Instantiate(fooooood, new Vector3(-64.5f + i * 25, -37f + k * 25, 0f), Quaternion.identity);
//                //    }
//                //}

//                Instantiate(Foodplate, new Vector3(5f, 10f, 0f), Quaternion.identity);

//                //GetComponent<Enviroment>().WeiWrite(rec);
//                //GetComponent<Spawner>().SpawnFood();
//            }
//        }
//    }

//    void CreateBactsN1()
//    {
//        for (sbyte i = 1; i < xb; i++)
//        {
//            for (sbyte k = 0; k < yb; k++)
//            {
//                CreateBact(i * 25 - 67, k * 25 - 39.5f);
//                //Instantiate(fooooood, new Vector3(-55.5f + i * 25, -30f + k * 25, 0f), Quaternion.identity);
//            }
//        }
//    }
//    void CreateBactsN()
//    {
//        for (int i = 0; i < countBacts; i++)
//        {
//            GameObject Bact = Instantiate(BacteriaPrefab, new Vector3(Random.Range(-50f, 50f), Random.Range(-30f, 30f), 0f), Quaternion.identity);
//            Bact.name = "Bacteria" + i;

//            Bact.GetComponent<NeuroNet>().weights = weights;
//            Bact.GetComponent<NeuroNet>().input = input;
//            Bact.GetComponent<NeuroNet>().pochEnergy = pochEnergy;
//            Bact.GetComponent<NeuroNet>().energy = energy;
//            Bact.GetComponent<NeuroNet>().speed = speed;
//            Bact.GetComponent<NeuroNet>().View = View;
//            Bact.GetComponent<NeuroNet>().transform.localScale = scale;
//        }
//    }

//    void CreateBact(float x, float y)
//    {
//        Instantiate(BacteriaPrefab, new Vector3(x, y, 0f), Quaternion.identity);
//    }
//    void CreateBactBest(float x, float y)
//    {
//        Instantiate(BestBacteriaPrefab, new Vector3(x, y, 0f), Quaternion.identity);
//    }
//    void CreateBactsBest()
//    {
//        for (sbyte i = 0; i < 1; i++)
//        {
//            for (sbyte k = 0; k < yb; k++)
//            {
//                if (k == 0)
//                    CreateBactBest(i * 25 - 67, k * 25 - 39.5f);
//                else
//                    CreateBact(i * 25 - 67, k * 25 - 39.5f);
//                //Instantiate(fooooood, new Vector3(-55.5f + i * 25, -30f + k * 25, 0f), Quaternion.identity);
//            }
//        }
//    }
//    void PereferBact()
//    {
//        for (byte i = 0; i < Mathf.Round(countBacts / 4); i++)
//            CreateBact(Random.Range(-60f, -50f), Random.Range(-40f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBacts / 4); i++)
//            CreateBact(Random.Range(50f, 60f), Random.Range(-40f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBacts / 4); i++)
//            CreateBact(Random.Range(-60f, 60f), Random.Range(35f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBacts / 4); i++)
//            CreateBact(Random.Range(-60f, 60f), Random.Range(-40f, -35f));
//    }
    
//    void PereferBest()
//    {
//        for (byte i = 0; i < Mathf.Round(countBests / 4); i++)
//            CreateBactBest(Random.Range(-60f, -50f), Random.Range(-40f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBests / 4); i++)
//            CreateBactBest(Random.Range(50f, 60f), Random.Range(-40f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBests / 4); i++)
//            CreateBactBest(Random.Range(-60f, 60f), Random.Range(35f, 40f));
//        for (byte i = 0; i < Mathf.Round(countBests / 4); i++)
//            CreateBactBest(Random.Range(-60f, 60f), Random.Range(-40f, -35f));
//    }
//}
