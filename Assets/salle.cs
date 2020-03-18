using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;

public class salle : MonoBehaviour
{
    public int biome;
    public bool top = true;
    public bool bot = true;
    public bool left = true;
    public bool right = true;
    private Random r = new Random();


    private bool boss;
    private bool spawn;

    public bool Boss
    {
        get => boss;
        set => boss = value;
    }

    public bool Spawn
    {
        get => spawn;
        set => spawn = value;
    }

    public GameObject room;
    private GameObject topdoor, botdoor, leftdoor, rightdoor;
    public GameObject None;

    public int[,] layout;


    public GameObject Topdoor
    {
        get => topdoor;
        set => topdoor = value;
    }

    public GameObject Botdoor
    {
        get => botdoor;
        set => botdoor = value;
    }

    public GameObject Rightdoor
    {
        get => rightdoor;
        set => rightdoor = value;
    }

    public GameObject Leftdoor
    {
        get => leftdoor;
        set => leftdoor = value;
    }

    public salle(int biome)
    {
        Spawn = false;
        Boss = false;
        layout = new int[10, 15];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                layout[i, j] = 0;
            }
        }

        this.biome = biome;
        topdoor = room.transform.GetChild(1).gameObject;
        botdoor = room.transform.GetChild(2).gameObject;
        leftdoor = room.transform.GetChild(3).gameObject;
        rightdoor = room.transform.GetChild(4).gameObject;
    }
}