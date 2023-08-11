using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class Set : MonoBehaviour
{
    private const byte integers = 22, toggles = 2;
    public Toggle FGrow, Res;
    public InputField FCount, FTime, FCountPerCycle, MaxE, MinE, FEGrow, FERot, FMaxGrow;
    public InputField BCount, BLength, BWidth, BSpeed, BView, BEnergy, BPochEnergy, BMutation, BPredation, BAttack, BDefence;
    public InputField MapX, MapY, MapRo;
    private string[] alltxt;
    private List<string> b, c;
    private string[] s;
    private InputField[] ld = new InputField[integers];
    private Toggle[] tg = new Toggle[toggles];
    private string dflt;
    public bool t;
    private byte[] zero = new byte[] { 0, 2, 5, 6, 9, 12, 13, 15, 16, 17, 18, 19, 20, 21 }, positives = new byte[] { 1, 3, 4, 7, 8, 10, 11, 14 };

    private void Awake()
    {
        t = true;
        dflt = "50 1 5 80 40 50 10 1000 1500 2 120 80 500 500 2 5 5 0 0 50 0 0 0 1 50 1 5 50 20 60 15 1200 1500 2 120 80 500 500 2 5 5 0 100 50 10 0 0 1";
        //      0  1 2 3  4  5  6  7    8    9 10  11 12  13 14 1516171819 20210 1|0  1 2 3  4  5  6   7    8   9 10  11 12  13 14 151617 18 19 20 210 1
        if (!File.Exists(Application.persistentDataPath + @"/Set.txt"))
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + @"/Set.txt");
            sw.Write(dflt);
            sw.Close();
        }
        s = File.ReadAllLines(Application.persistentDataPath + "/Set.txt");
        b = s[s.Length - 1].Split().ToList();
        c = b.GetRange(b.Count / 2, b.Count / 2);
        b = b.GetRange(0, b.Count / 2);

        ld[0] = FCount;//
        ld[1] = FTime;
        ld[2] = FCountPerCycle;//
        ld[3] = BLength;
        ld[4] = BWidth;
        ld[5] = BSpeed;//
        ld[6] = BView;//
        ld[7] = BEnergy;
        ld[8] = BPochEnergy;
        ld[9] = BCount;//
        ld[10] = MapX;
        ld[11] = MapY;
        ld[12] = MaxE;//
        ld[13] = MinE;//
        ld[14] = FEGrow;
        ld[15] = FERot;//
        ld[16] = BMutation;//
        ld[17] = MapRo;//
        ld[18] = BPredation;//
        ld[19] = FMaxGrow;//
        ld[20] = BAttack;//
        ld[21] = BDefence;//

        tg[0] = FGrow;
        tg[1] = Res;

        StartData();
        Check();
    }
    //
    public void StartData()
    {
        if (t) SD(b);
        else SD(c);
    }
    private void SD(List<string> b)
    {
        for (byte i = 0; i < ld.Length; i++) ld[i].GetComponent<InputField>().text = b[i];
        FGrow.isOn = b[integers] == "1" ? true : false;
        Res.isOn = b[integers + 1] == "1" ? true : false;
    }
    //
    //
    public void Copy()
    {
        if (t) cp(b, c);
        else cp(c, b);
    }
    private void cp(List<string> a, List<string> b)
    {
        for (byte i = 0; i < 3; i++) a[i] = b[i];
        for (byte i = 10; i < 16; i++) a[i] = b[i];
        a[17] = b[17];
        for (byte i = 19; i < 20; i++) a[i] = b[i];
        for (byte i = integers; i < integers + toggles; i++) a[i] = b[i];
    }
    //
    //
    public void Check()
    {
        if (t) Ch(b);
        else Ch(c);
    }
    private void Ch(List<string> b)
    {
        foreach (byte i in zero) PositiveOrZero(-1, i, b);
        foreach (byte i in positives) PositiveOrZero(0, i, b);

        if (int.Parse(ld[12].GetComponent<InputField>().text) < int.Parse(ld[13].GetComponent<InputField>().text))
        {
            ld[12].GetComponent<InputField>().text = ld[13].GetComponent<InputField>().text;
        }
    }
    //
    private void PositiveOrZero(sbyte j, byte i, List<string> b)
    {
        if (ld[i].GetComponent<InputField>().text != "")
            if (int.TryParse(ld[i].GetComponent<InputField>().text, out _))
                if (int.Parse(ld[i].GetComponent<InputField>().text) > j)
                {
                    b[i] = ld[i].GetComponent<InputField>().text;
                }
        ld[i].GetComponent<InputField>().text = b[i];
    }
    public void WriteSets()
    {
        //for (byte i = 0; i < tg.Length; i++)
        //{
        //    if (tg[i].isOn) b[i + integers + floats] = "1";
        //    else b[i + integers + floats] = "0";
        //}
        string str = File.ReadAllText(Application.persistentDataPath + "/Set.txt") + '\n';
        t = !t;
        Copy();
        for (byte i = 0; i < integers + toggles; i++) str += b[i] + ' ';
        for (byte i = 0; i < integers + toggles; i++) str += c[i] + ' ';
        File.WriteAllText(Application.persistentDataPath + "/Set.txt", str);

        SceneManager.LoadScene("Simulation");
    }

    public void ChToggle()
    {
        for (byte i = 0; i < toggles; i++) b[integers + i] = tg[i].isOn ? "1" : "0";
        for (byte i = 0; i < toggles; i++) c[integers + i] = tg[i].isOn ? "1" : "0";
    }
    public void Grow()
    {
        if (FGrow.isOn)
        {
            FERot.GetComponent<InputField>().enabled = true;
            FEGrow.GetComponent<InputField>().enabled = true;
            FMaxGrow.GetComponent<InputField>().enabled = true;
        }
        else
        {
            FERot.GetComponent<InputField>().enabled = false;
            FEGrow.GetComponent<InputField>().enabled = false;
            FMaxGrow.GetComponent<InputField>().enabled = false;
        }
    }

    public void Default() //Insert Default Settings in St.txt and Restart Game
    {
        string str = File.ReadAllText(Application.persistentDataPath + "/Set.txt") + '\n' + dflt;
        File.WriteAllText(Application.persistentDataPath + "/Set.txt", str);
        SceneManager.LoadScene("Settings");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }
}
