//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NeuroNet : MonoBehaviour
//{
//    public GameObject Processor, BacteriaPrefab;
//    private Enviroment env;
//    private NeuroBactSpawn nbs;
//    private float distance, startEnergy;
//    private Vector2 vec;
//    public Vector3 rott;
//    private Transform tr;
//    public float speed, area, pochEnergy, View;
//    public float remant, consump, energy;
//    public Vector2 normal = new Vector2(0, 1);
//    public float ang;
//    private int counter;
//    public int points;
//    public Vector2 giants, similar, farmFood, Food, outVec;

//    public float[][] input = new float[3][];
//    public float[][] weights = new float [6][];

//    void Start()
//    {
//        weights[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[1] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[2] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[3] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        weights[4] = new float[4] { 0f, 0f, 0f, 0f };
//        weights[5] = new float[4] { 0f, 0f, 0f, 0f };

//        input[0] = new float[9] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
//        input[1] = new float[4] { 0f, 0f, 0f, 0f };
//        input[2] = new float[2] { 0f, 0f };

//        points = 0;
//        env = Enviroment.Instance;
//        nbs = NeuroBactSpawn.Instance;
//        tr = GetComponent<Transform>();
//        counter = 0;
//        name = "BacteriaN " + env.numb;
//        env.numb++;
//        area = tr.localScale.x * tr.localScale.y;
//        consump = speed * speed / 100 + 10 + View / 5 + area * 50;
//        area = tr.localScale.x * tr.localScale.y;

//        //input = nbs.input;
//        input[0][0] = 0;// energy / consump * speed;

//        for (byte k = 0; k < 4; k++)
//        {
//            for (byte j = 0; j < 9; j++)
//            {
//                weights[k][j] = nbs.weights[k][j] + Random.Range(-0.01f, 0.01f);
//            }
//        }
//        for (byte k = 4; k < 6; k++)
//        {
//            for (byte j = 0; j < 4; j++)
//            {
//                weights[k][j] = nbs.weights[k][j] + Random.Range(-0.01f, 0.01f);
//            }
//        }

//        //for (byte i = 0; i < 4; i++)
//        //{
//        //    for (byte k = 0; k < 9; k++)
//        //    {
//        //        Debug.Log(weights[i][k]);
//        //    }
//        //}
        
//        pochEnergy = nbs.pochEnergy;
//        energy = nbs.energy;
//        speed = nbs.speed;
//        View = nbs.View;
//        transform.localScale = nbs.scale;
//    }



//    void FixedUpdate()
//    {
//        //input[0][0] = energy / consump * speed;
//        if (counter >= 50)
//        {
//            energy -= consump;
//            remant = Mathf.Round(energy / consump);
//            if (remant < 0)
//            {
//                Destroy(gameObject);
//            }
//            //ang += Random.Range(-5, 5);
//            counter = 0;
//        }
//        else counter++;

//        if (energy > pochEnergy)
//        {
//            NewBacN();
//        }

//        giants = new Vector2(0f, 0f);
//        similar = new Vector2(0f, 0f);
//        farmFood = new Vector2(0f, 0f);
        
//        for (int i = 0; i < env.bacterias.Count; i++)
//        {
//            if (env.bacterias[i] != null && env.bacterias[i].name != name)
//            {
//                if (Vector3.Distance(env.bacterias[i].GetComponent<Transform>().position, tr.position) < View)
//                {
//                    vec = env.bacterias[i].GetComponent<Transform>().position - tr.position;
//                    if (vec == new Vector2(0f, 0f))
//                    {
//                        Debug.Log("idinan");
//                        giants.x = 0;
//                        giants.y = 0;
//                        farmFood.x = 0;
//                        farmFood.y = 0;
//                        similar.x = 0;
//                        similar.y = 0;
//                        continue;
//                    }
//                    else if (vec.x == 0) vec.x = 0.0001f;
//                    else if (vec.y == 0) vec.y = 0.0001f;

//                    if (env.bacterias[i].GetComponent<BNeuroNet>() == null)
//                    {
//                        if (env.bacterias[i].GetComponent<NeuroNet>().area > area * 1.3f)
//                        {
//                            giants += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                        else if (env.bacterias[i].GetComponent<NeuroNet>().area * 1.3f < area)
//                        {
//                            farmFood += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                        else
//                        {
//                            similar += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                    }
//                    else
//                    {
//                        if (env.bacterias[i].GetComponent<BNeuroNet>().area > area * 1.3f)
//                        {
//                            giants += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                        else if (env.bacterias[i].GetComponent<BNeuroNet>().area * 1.3f < area)
//                        {
//                            farmFood += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                        else
//                        {
//                            similar += new Vector2(1 / vec.x, 1 / vec.y);
//                        }
//                    }
//                }
//            }
//        }
        
//        //input[0][1] = giants.x;
//        //input[0][2] = giants.y;
//        input[0][3] = similar.x;
//        input[0][4] = similar.y;
//        //input[0][5] = farmFood.x;
//        //input[0][6] = farmFood.y;
//        // Off
//        input[0][1] = 0;
//        input[0][2] = 0;
//        //input[0][3] = 0;
//        //input[0][4] = 0;
//        input[0][5] = 0;
//        input[0][6] = 0;

//        // Food
//        Food = new Vector2(0f, 0f);
//        for (int i = 0; i < env.food.Count; i++)
//        {
//            if (env.food[i] != null)
//            {
//                if (env.food[i].GetComponent<FoodPower>().energyf < pochEnergy)
//                {
//                    vec = env.food[i].GetComponent<Transform>().position - tr.position;
                    
//                    distance = vec.magnitude;

//                    if (vec == new Vector2(0f, 0f))
//                    {
//                        Food.x = 0;
//                        Food.y = 0;
//                    }
//                    else if (vec.x == 0) vec.x = 0.0001f;
//                    else if (vec.y == 0) vec.y = 0.0001f;

//                    if (distance < View)
//                    {
//                        Food += new Vector2(1 / vec.x, 1 / vec.y);
//                    }
//                }
//            }
//        }

//        input[0][7] = Food.x;
//        input[0][8] = Food.y;

//        input[1][0] = 0;
//        input[1][1] = 0;
//        input[1][2] = 0;
//        input[2][0] = 0;
//        input[2][1] = 0;

//        for (byte i = 0; i < 4; i++)
//        {
//            for(byte k = 0; k < 9; k++)
//            {
//                input[1][i] += input[0][k] * weights[i][k];
//            }
//        }
//        for(byte i = 0; i < 2; i++)
//        {
//            for(byte k = 0; k < 4; k++)
//            {
//                input[2][i] += input[1][k] * weights[i + 4][k];
//            }
//        }

//        input[2][0] = Mathf.Sin(input[2][0]);
//        input[2][1] = Mathf.Sin(input[2][1]);
//        //Debug.Log(input[2][0]);
//        //Debug.Log(input[2][1]);

//        outVec = new Vector2(input[2][0], input[2][1]);
//        tr.position = Vector3.Lerp(tr.position, new Vector2(outVec.normalized.x * speed / 10 + tr.position.x, outVec.normalized.y * speed / 10 + tr.position.y), Time.deltaTime);
//        ang = Vector2.Angle(normal, outVec);

//        if (Vector3.Cross(outVec, normal).z > 0)
//        {
//            ang = -ang;
//        }
        
//        //rott = new Vector3(0, 0, ang);
//        tr.rotation = Quaternion.Euler(0f, 0f, ang /* + Random.Range(-0.5f, 0.5f)*/);

//        //Debug.Log("     0       1       2");
//        //Debug.Log("___________________________");
//        //Debug.Log("0| " + input[0][0] + " " + input[1][0] + " " + input[2][0]);
//        //Debug.Log("1| " + input[0][1] + " " + input[1][1] + " " + input[2][1]);
//        //Debug.Log("2| " + input[0][2] + " " + input[1][2]);
//        //Debug.Log("3| " + input[0][3]);
//        //Debug.Log("4| " + input[0][4]);
//        //Debug.Log("5| " + input[0][5]);
//        //Debug.Log("6| " + input[0][6]);
//        //Debug.Log("7| " + input[0][7]);
//        //Debug.Log("8| " + input[0][8]);
//    }

//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if(collision.tag == "Bacteria" || collision.tag == "BacteriaN")
//        {
//            if(collision.GetComponent<NeuroNet>().area < area * 1.3f)
//            {
//                points += 5;
//                energy += collision.GetComponent<NeuroNet>().energy * 0.7f;
//                Destroy(collision);
//            }
//        }
//        else if(collision.tag == "Food")
//        {
//            if (collision.GetComponent<FoodPower>().energyf < pochEnergy)
//            {
//                energy += collision.GetComponent<FoodPower>().energyf;
//                points += 1;
//                Destroy(collision.gameObject);

//                if (env != null)
//                {
//                    env.ChangeFood = true;
//                }
//            }
//        }
//    }
    
//    void NewBacN()
//    {
//        points += 100;
//        GameObject child = Instantiate(BacteriaPrefab, new Vector3(tr.position.x + Random.Range(-1f, 1f), tr.position.y + Random.Range(-1f, 1f), 0f), Quaternion.identity);
//        startEnergy = Mathf.Round(pochEnergy / 3);
//        energy -= startEnergy;
//        child.GetComponent<NeuroNet>().energy = startEnergy;
//        child.GetComponent<NeuroNet>().transform.localScale = new Vector3(tr.localScale.x * Random.Range(0.95f, 1.05f), tr.localScale.y * Random.Range(0.95f, 1.05f));

//        child.GetComponent<NeuroNet>().pochEnergy = pochEnergy * Random.Range(0.99f, 1.01f);

//        child.GetComponent<NeuroNet>().speed = speed * Random.Range(0.95f, 1.05f) * Mathf.Sqrt(child.GetComponent<NeuroNet>().transform.localScale.y / child.GetComponent<NeuroNet>().transform.localScale.x);
//        child.GetComponent<NeuroNet>().View = View * Random.Range(0.98f, 1.02f) * (1 + child.GetComponent<NeuroNet>().transform.localScale.x * child.GetComponent<NeuroNet>().transform.localScale.y / 16);

//        child.GetComponent<NeuroNet>().weights = weights;
//        child.GetComponent<NeuroNet>().input = input;
//        child.GetComponent<NeuroNet>().transform.localScale = new Vector3(0.4f, 0.4f, 0);

//        env.bacterias.Add(child);
//    }
//}
