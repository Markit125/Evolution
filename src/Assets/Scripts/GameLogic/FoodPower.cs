using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPower : MonoBehaviour
{
    public int minE, maxE, rot, gro;
    public float energyf, MaxGrow;
    public GameObject Processor;
    private Enviroment env;
    private Transform tr;
    private bool old;
    public bool grow;
    private float small;

    void Start()
    {
        env = Enviroment.Instance;
        old = false;
        name = "Food" + env.numf;
        env.numf++;
        env.ChangeFood = true;
        energyf = Mathf.Round(Random.Range(minE, maxE));
        MaxGrow = Random.Range(MaxGrow * 0.9f * energyf, MaxGrow * 1.1f * energyf);
        tr = GetComponent<Transform>();
        tr.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }
    void FixedUpdate()
    {
        if (grow)
        {
            if (!old)
            {
                if (energyf < MaxGrow)
                {
                    tr.localScale += new Vector3(0.0007f, 0.0007f);
                    energyf += gro;
                }
                else
                {
                    small = tr.localScale.x / (energyf / rot);
                    old = true;
                }
            }
            else
            {
                tr.localScale -= new Vector3(small, small);
                energyf -= rot;
                if (tr.localScale.x < 0 || energyf < 10) Destroy(gameObject);
            }
        }
    }
}
