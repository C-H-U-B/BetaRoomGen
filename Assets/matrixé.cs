using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class matrixé : MonoBehaviour
{
    [SerializeField] private GameObject[,] morpheus;
    [SerializeField] private int size;
    [SerializeField] private GameObject neo;
    private Random r = new Random();
    private void Awake()
    {
        if (size % 2 == 0) size += 1;
        morpheus = new GameObject[size,size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                morpheus[i, j] = neo;
                GameObject trinity = Instantiate(neo, new Vector2(i*10,j*10), Quaternion.identity);
                trinity.transform.parent = gameObject.transform;
            }
        }
        generatedungeon(size,morpheus);
        /*for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (morpheus[i, j].GetComponent<cleanscript>().top) 
                    transform.GetChild(1).gameObject.SetActive(false);
                if (morpheus[i, j].GetComponent<cleanscript>().bot) 
                    transform.GetChild(2).gameObject.SetActive(false);
                if (morpheus[i, j].GetComponent<cleanscript>().left) 
                    transform.GetChild(3).gameObject.SetActive(false);
                if (morpheus[i, j].GetComponent<cleanscript>().right) 
                    transform.GetChild(4).gameObject.SetActive(false);
            }
        }
        */
        for (int i = 0; i <size*size; i++)
        {
            if (transform.GetChild(i).GetComponent<cleanscript>().top) 
                transform.GetChild(1).gameObject.SetActive(false);
            if (transform.GetChild(i).GetComponent<cleanscript>().bot) 
                transform.GetChild(2).gameObject.SetActive(false);
            if (transform.GetChild(i).GetComponent<cleanscript>().left) 
                transform.GetChild(3).gameObject.SetActive(false);
            if (transform.GetChild(i).GetComponent<cleanscript>().right) 
                transform.GetChild(4).gameObject.SetActive(false);
        }
    }
    
    public void generatedungeon(int size,GameObject[,] matrix)
    {
        int maxroom = (size * size) /3;
        int compteur = 5;
        bool boule = true;
        
        matrix[size / 2, size / 2].GetComponent<cleanscript>().top = false;
        matrix[size / 2, size / 2].GetComponent<cleanscript>().bot = false;
        matrix[size / 2, size / 2].GetComponent<cleanscript>().left = false;
        matrix[size / 2, size / 2].GetComponent<cleanscript>().right = false;
        matrix[size / 2, size / 2].GetComponent<cleanscript>().spawn = true;

        while (compteur < maxroom && boule)
        {
            (int x, int y)= recdungeon(size,matrix);
            if (x >= 0)
            {
                int a = generateroom(x, y, maxroom - compteur,matrix);
                compteur += a;
            }
            else boule = false;
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                checkdoors(size,i,j,matrix);
            }
        }
    }
    public (int,int) recdungeon(int size,GameObject[,] matrix)
    {
        List<(int,int)> a = new List<(int, int)>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(IsAccesible(size,i,j,matrix))
                    a.Add((i,j));
            }
        }
        int b = 0;
        int c = a.Count-1;
        if (c >= 0)
            b += r.Next(c);
        if (c == -1)
            return (-1, -1);
        (int x, int y) = a[b];
        return (x,y);
    }
    
    
    public bool isvalid(int size,int i ,int j)
    {
        return (i >= 0 && j >= 0 && i < size && j < size);
    }
    
    public bool ishere(int size, int i, int j, GameObject[,] matrix)
    {
        if (isvalid(size, i, j))
        {
            GameObject s = matrix[i, j];
            return !(s.GetComponent<cleanscript>().bot && s.GetComponent<cleanscript>().top && s.GetComponent<cleanscript>().left && s.GetComponent<cleanscript>().right);
        }
        return false;
    }
    
    public bool IsAccesible(int size, int i, int j, GameObject[,] matrix)
    {
        bool b = false;
        if (!ishere(size,i,j,matrix))
        {
            if (isvalid(size,i,j-1) && !matrix[i,j-1].GetComponent<cleanscript>().bot)
                b = true;
            else if (isvalid(size,i,j+1) && !matrix[i,j+1].GetComponent<cleanscript>().top)
                b = true;
            else if (isvalid(size,i+1,j) && !matrix[i+1,j].GetComponent<cleanscript>().left)
                b = true;
            else if (isvalid(size,i-1,j) && !matrix[i-1,j].GetComponent<cleanscript>().right)
                b = true;
        }

        return b;
    }
    
    
    public int generateroom(int i, int j,int diff,GameObject[,] matrix)
    {
        int size = matrix.GetLength(0);
        int compteur = 0;
        
        
        checkdoors(size,i,j,matrix);
        int d = possibledirections(size, i, j,matrix);


        if (d >= 3 && diff >= 3) 
        { 
            int a = r.Next(4);
            if (a == 3)
                compteur += randomdoor3(i, j,matrix);
            if (a == 2) 
                compteur += randomdoor2(i, j,matrix);
            if (a == 1 )  
                compteur += randomdoor1(i, j,matrix);
        }
        else if (d >= 2 && diff >= 2) 
        { 
            int a = r.Next(3); 
            if (a == 2) 
                compteur += randomdoor2(i, j,matrix);
            if (a == 1|| a == 0) 
                compteur += randomdoor1(i, j,matrix);
        }
        else if (d >= 1 && diff >= 1) 
        { 
            int a = r.Next(2);
            if (a == 1)
                compteur += randomdoor1(i, j,matrix);
        }
        return compteur;
    }
    
    public int possibledirections(int size, int i, int j,GameObject[,] matrix)
    {
        int d = 0;
        if (!ishere(size, i + 1, j,matrix) && isvalid(size, i + 1, j))
            d++;
        if (!ishere(size, i - 1, j,matrix) && isvalid(size, i - 1, j))
            d++;
        if (!ishere(size, i, j + 1,matrix) && isvalid(size, i, j + 1))
            d++;
        if (!ishere(size, i, j - 1,matrix) && isvalid(size, i, j - 1))
            d++;
        return d;
    }
    
    public void checkdoors(int size, int i, int j,GameObject[,] matrix)
    {
        if (isvalid(size,i+1,j) && !matrix[i+1,j].GetComponent<cleanscript>().left)
            matrix[i, j].GetComponent<cleanscript>().right = false;
        
        if (isvalid(size,i-1,j) && !matrix[i-1,j].GetComponent<cleanscript>().right)
            matrix[i, j].GetComponent<cleanscript>().left = false;
        
        if (isvalid(size,i,j+1) && !matrix[i,j+1].GetComponent<cleanscript>().top)
            matrix[i, j].GetComponent<cleanscript>().bot = false;
        
        if (isvalid(size,i,j-1) && !matrix[i,j-1].GetComponent<cleanscript>().bot)
            matrix[i, j].GetComponent<cleanscript>().top = false;
    }


    public int randomdoor3(int i, int j, GameObject[,] matrix)
    {
        int a = 0;
        a += randomdoor1(i, j,matrix);
        a += randomdoor2(i, j,matrix);
        return a;
    }
    
    
    //fonction qui creuse 2 portes 
    public int randomdoor2(int i, int j,GameObject[,] matrix)
    {
        int a = 0;
        a += randomdoor1(i, j,matrix);
        a += randomdoor1(i, j,matrix);
        return a;
    }
    
    
    //fonction qui creuse 1 porte aléatoirement
    public int randomdoor1(int i, int j,GameObject[,] matrix)
    {
        int size = matrix.GetLength(0);
        bool added = false;
        
        while (!added && possibledirections(size,i,j,matrix)!=0)
        {
            int a = r.Next(4);
                    
            if (a == 0 && isvalid(size, i, j + 1) && matrix[i, j].GetComponent<cleanscript>().right)
            {
                added = true;
                matrix[i, j].GetComponent<cleanscript>().right = false;
            }
            if (a == 1 && isvalid(size, i, j - 1) && matrix[i, j].GetComponent<cleanscript>().left)
            {
                added = true;
                matrix[i, j].GetComponent<cleanscript>().left = false;
            }
            if (a == 2 && isvalid(size, i+1, j ) && matrix[i, j].GetComponent<cleanscript>().bot)
            {
                added = true;
                matrix[i, j].GetComponent<cleanscript>().bot = false;
            }
            if (a == 3 && isvalid(size, i-1, j) && matrix[i, j].GetComponent<cleanscript>().top)
            { 
                added = true;
                matrix[i, j].GetComponent<cleanscript>().top = false;
            }
        }

        if (added)
        {
            return 1;
        }
        return 0;
    }
}


