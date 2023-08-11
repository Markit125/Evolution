using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public float speed, View, startEnergy, pochEnergy, Mutation, Ro;
    public GameObject BacteriaPrefab;
    private Enviroment env;
    private float x, y, xf, yf, distance, scaler;
    public Vector2 vecFood, vecAttack, vecRun, vec;
    private Transform tr;
    public float remant, consump, energy;
    private Vector2 normal = new Vector2(0, 1);
    public int nclos, nclos1;
    public float ang, closest, closest1, area, pre, attack, defence;
    private int counter;
    private bool run;

    void Start()
    {
        env = Enviroment.Instance;
        BacteriaPrefab = env.GetComponentInParent<InsertSet>().Bacteria;
        tr = GetComponent<Transform>();
        scaler = env.GetComponentInParent<InsertSet>().x;
        counter = 0;
        name = "Bacteria " + env.numb;
        env.numb++;
        consump = Mathf.Round(speed * speed / 100 + View / 3 + 10 + tr.localScale.x * tr.localScale.y * 100 / scaler / scaler);
        area = tr.localScale.x * tr.localScale.y / scaler / scaler;
        Ro *= tr.localScale.x / scaler;
        speed -= Ro;
    }
    void FixedUpdate()
    {
        GetComponent<SpriteRenderer>().color = new Color((1 - energy / pochEnergy) + pre, (1 - energy / pochEnergy) + (1 - pre), 1 - energy / pochEnergy, 1f);
        if (counter >= 50)
        {
            energy -= consump;
            remant = Mathf.Round(energy / consump);
            if (remant < 0) Destroy(gameObject);
            ang += Random.Range(-5, 5);
            if (nclos == -1)
            {
                ang += Random.Range(-30, 30);
                tr.rotation = Quaternion.Euler(0, 0, ang);
            }
            counter = 0;
        }
        else counter++;

        if (energy > pochEnergy) NewBac();


        nclos = -1;
        closest = View + 10;
        if (pre < 0.5f)
        {
            for (int i = 0; i < env.food.Count; i++)
            {
                if (env.food[i] != null)
                {
                    vecFood = env.food[i].GetComponent<Transform>().position - tr.position;
                    distance = Mathf.Round(vecFood.magnitude * 100) / 100f;

                    if (distance < View)
                    {
                        if (closest > distance)
                        {
                            closest = distance;
                            nclos = i;
                        }
                    }
                }
            }
        }

        if (nclos == -1) vecFood = new Vector2(View + 10, View + 10);
        else vecFood = (env.food[nclos].GetComponent<Transform>().position - tr.position) * (1 - pre);

        nclos = -1; nclos1 = -1;
        closest = View + 10; closest1 = closest;

        for (int i = 0; i < env.bacterias.Count; i++)
        {
            if (env.bacterias[i] != null && env.bacterias[i].name != name)
            {
                if (Vector3.Distance(env.bacterias[i].GetComponent<Transform>().position, tr.position) < View)
                {
                    UnitMove u = env.bacterias[i].GetComponent<UnitMove>();
                    
                    distance = Mathf.Round((env.bacterias[i].GetComponent<Transform>().position - tr.position).magnitude * 1000) / 1000f;

                    bool sca = pre - u.pre > 0.2f || area / u.area > 1.5f && pre > 0.3f;
                    bool scr = u.pre - pre > 0.2f || area / u.area < 0.6f;
                    if (env.bacterias[i].GetComponent<UnitMove>().defence + 0.05f < attack && sca)
                    {
                        if (closest > distance)
                        {
                            closest = distance;
                            nclos = i;
                        }
                    }
                    else if (env.bacterias[i].GetComponent<UnitMove>().attack > defence && scr)
                    {
                        if (closest1 > distance * (attack + 0.01f - env.bacterias[i].GetComponent<UnitMove>().defence) * 100)
                        {
                            closest1 = distance * (attack + 0.01f - env.bacterias[i].GetComponent<UnitMove>().defence) * 100;
                            nclos1 = i;
                        }
                    }
                }
            }
        }

        if (nclos == -1) vecAttack = new Vector2(View + 10, View + 10);
        else vecAttack = (env.bacterias[nclos].GetComponent<Transform>().position - tr.position) * pre * (attack + 0.05f - env.bacterias[nclos].GetComponent<UnitMove>().defence) * 10;
        if (nclos1 == -1) vecRun = new Vector2(View + 10, View + 10);
        else vecRun = (-env.bacterias[nclos1].GetComponent<Transform>().position + tr.position) * (1 - pre) * (defence + 0.001f) / (env.bacterias[nclos1].GetComponent<UnitMove>().attack * 5 + 0.001f);

        if (nclos != -1 || nclos1 != -1 || vecFood != new Vector2(View + 10, View + 10))
        {
            if (vecAttack.magnitude < vecRun.magnitude)
            {
                vec = vecAttack;
                if (vecFood.magnitude < vec.magnitude) vec = vecFood;
            }
            else
            {
                vec = vecRun;
                if (vecFood.magnitude < vec.magnitude) vec = vecFood;
            }
            if (vec != vecRun && vec != new Vector2(0, 0))
            {
                run = false;
                tr.position = Vector3.Lerp(tr.position, new Vector2(vec.normalized.x * speed / 10 + tr.position.x, vec.normalized.y * speed / 10 + tr.position.y), Time.deltaTime);
                ang = Vector2.Angle(normal, vec);
                if (Vector3.Cross(vec, normal).z > 0) ang = -ang;
                tr.rotation = Quaternion.Euler(0, 0, ang + Random.Range(-0.5f, 0.5f));
            }
            else
            {
                if (!run)
                {
                    ang = Vector2.Angle(normal, vec);
                    run = !run;
                }
                else ang += Random.Range(-10, 10);
                tr.rotation = Quaternion.Euler(0, 0, ang);
                tr.position = Vector3.Lerp(tr.position, new Vector2(tr.up.x * speed / 10 + tr.position.x, tr.up.y * speed / 10 + tr.position.y), Time.deltaTime);
            }
        }
        else
        {
            ang += Random.Range(-5, 5);
            tr.rotation = Quaternion.Euler(0, 0, ang);
            tr.position = Vector3.Lerp(tr.position, new Vector2(tr.up.x * speed / 10 + tr.position.x, tr.up.y * speed / 10 + tr.position.y), Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) // Eating Food
    {
        if (col.tag == "Food" && pre < 0.5f)
        {
            energy += col.GetComponent<FoodPower>().energyf * (1 - pre * 1.5f);
            Destroy(col.gameObject);
            if (env != null) env.ChangeFood = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col) // Attacking
    {
        if (col.collider.tag == "Bacteria")
        {
            if (vec == vecAttack)
            {
                UnitMove u = col.collider.GetComponent<UnitMove>();
                if (pre - u.pre > 0.2f || area / u.area > 1.5f)
                {
                    u.energy -= pochEnergy / 10 * pre * (attack - u.defence);
                    energy += pochEnergy / 10 * pre * (attack - u.defence);
                }
            }
        }
    }

    private void NewBac() // Parametres of New Bacteria
    {
        GameObject child = Instantiate(BacteriaPrefab, new Vector3(tr.position.x + Random.Range(-tr.localScale.x / scaler, -tr.localScale.x / scaler), tr.position.y + Random.Range(-tr.localScale.y / scaler, -tr.localScale.y / scaler), 0f), Quaternion.identity);

        startEnergy = Mathf.Round(pochEnergy / 3);
        energy -= startEnergy;
        UnitMove u = child.GetComponent<UnitMove>();
        u.energy = startEnergy;
        u.transform.localScale = new Vector3(tr.localScale.x * Random.Range(1 - Mutation / 100, 1 + Mutation / 100), tr.localScale.y * Random.Range(1 - Mutation / 100, 1 + Mutation / 100));
        u.pochEnergy = Mathf.Round(pochEnergy * Random.Range(1 - Mutation / 100, 1 + Mutation / 100));
        u.speed = Mathf.Round((speed * Random.Range(1 - Mutation / 100, 1 + Mutation / 100) - Ro) * 100) / 100f;
        u.View = Mathf.Round(View * Random.Range(1 - Mutation / 100, 1 + Mutation / 100) * 100) / 100f;
        u.pre = Mathf.Round(((pre + 1) * Random.Range(1 - Mutation / 100, 1 + Mutation / 100) - 1) * 1000) / 1000f;
        if (u.pre < 0) u.pre = 0f;
        u.defence = Mathf.Round(((defence + 1) * Random.Range(1 - Mutation / 100, 1 + Mutation / 100) - 1) * 100) / 100f;
        if (u.defence < 0) u.defence = 0;
        u.attack = Mathf.Round(((attack + 1) * Random.Range(1 - Mutation / 100, 1 + Mutation / 100) - 1) * 100) / 100f;
        if (u.attack < 0) u.attack = 0;
        env.bacterias.Add(child);
    }
}
