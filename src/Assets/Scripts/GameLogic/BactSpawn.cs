using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BactSpawn : MonoBehaviour
{
    public float len, wid, xb, yb, Ro, pre, attack, defence;
    public float Mutation;
    public int countBacts, startEnergy, pochEnergy, speed, View;
    private GameObject BacteriaPrefab;

    public void Spawn()
    {
        BacteriaPrefab = GetComponent<InsertSet>().Bacteria;
        for (int i = 0; i < countBacts; i++)
        {
            GameObject Bact = Instantiate(BacteriaPrefab, new Vector3(Random.Range(-xb / 2, xb / 2), Random.Range(-yb / 2, yb / 2), 0f), Quaternion.identity);
            Bact.name = "Bacteria" + i;
            UnitMove u = Bact.GetComponent<UnitMove>();
            u.Ro = Ro;
            u.pochEnergy = pochEnergy;
            u.energy = startEnergy;
            u.speed = speed;
            u.View = View;
            u.transform.localScale = new Vector3(wid, len, 0f);
            u.Mutation = Mutation;
            u.pre = pre;
            u.attack = attack;
            u.defence = defence;
        }
    }
}
