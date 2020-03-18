using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class thematrix : MonoBehaviour
{
    private salle[,] matrix;
    public GameObject None,room;
    private Random r = new Random();

    [SerializeField] int size;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
        thematrix dungeon = new thematrix(size,0);
        
        dungeon.generatedungeon(size);
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 vec =new Vector3(i*10,j*10,0);
               Instantiate(dungeon.matrix[i,j].room, vec, Quaternion.identity);
                if (matrix[i,j].bot)
                    //Destroy(dungeon.matrix[i,j].Botdoor);
                    Debug.Log("bot");
                if (matrix[i,j].top)
                    //Destroy(dungeon.matrix[i,j].Topdoor);
                    Debug.Log("top");
                if (matrix[i,j].right)
                   // Destroy(dungeon.matrix[i,j].Rightdoor);
                    Debug.Log("ri");
                if (matrix[i, j].left)
                    Debug.Log("le"); 
                //Destroy(dungeon.matrix[i,j].Leftdoor);
                //Instantiate(room, vec, Quaternion.identity);
            }
        }
        GameObject.Find("Main Camera").transform.position=new Vector3(size*5,size*5,-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // constructeur de la matrice
    public thematrix(int size,int biome)
    { 
        if (size % 2 == 0) size += 1;
        matrix = new salle[size,size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                
                matrix[i, j].room = room;
            }
        } 
    }


  // fonction chapeau globale qui crée le donjon
    public void generatedungeon(int size)
    {
        int maxroom = (size * size) /3;
        int compteur = 5;
        bool boule = true;
        
        matrix[size / 2, size / 2].bot = false;
        matrix[size / 2, size / 2].top = false;
        matrix[size / 2, size / 2].left = false;
        matrix[size / 2, size / 2].right = false;
        matrix[size / 2, size / 2].Spawn = true;

        while (compteur < maxroom && boule)
        {
            (int x, int y)= recdungeon(size);
            if (x >= 0)
            {
                int a = generateroom(x, y, maxroom - compteur);
                compteur += a;
            }
            else boule = false;

            // Console.WriteLine(MatrixToString());
            //Thread.Sleep(2000);
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                checkdoors(size,i,j);
            }
        }
    }
    
    
    
    //fonction qui va choisir une salle ou continuer le donjon
    public (int,int) recdungeon(int size)
    {
        List<(int,int)> a = new List<(int, int)>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(IsAccesible(size,i,j))
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

    
/* partie des tests sur les cases*/    
    
    // regarde si la case est dans la matrice
    public bool isvalid(int size,int i ,int j)
    {
        return (i >= 0 && j >= 0 && i < size && j < size);
    }

    
    //regarde si la case est reliée au donjon
    public bool ishere(int size, int i, int j)
    {
        if (isvalid(size, i, j))
        {
            salle s = matrix[i, j];
            return !(s.bot && s.top && s.left && s.right);
        }
        return false;
    }

    
    //regarde si une porte mene sur cette case mais que la case n'est pas reliée au donjon
    public bool IsAccesible(int size, int i, int j)
    {
        bool b = false;
        if (!ishere(size,i,j))
        {
            if (isvalid(size,i,j-1) && !matrix[i,j-1].bot)
                b = true;
            else if (isvalid(size,i,j+1) && !matrix[i,j+1].top)
                b = true;
            else if (isvalid(size,i+1,j) && !matrix[i+1,j].left)
                b = true;
            else if (isvalid(size,i-1,j) && !matrix[i-1,j].right)
                b = true;
        }

        return b;
    }
    
    
    
    
    
    
//partie génération de portes sur une salle    
    
    //fonction chapeau qui va choisir aléatoirement le nombre de portes à creuser entre 0 et 3 
    public int generateroom(int i, int j,int diff)
    {
        int size = matrix.GetLength(0);
        int compteur = 0;
        
        
        checkdoors(size,i,j);
        int d = possibledirections(size, i, j);


        if (d >= 3 && diff >= 3) 
        { 
            int a = r.Next(4);
            if (a == 3)
                compteur += randomdoor3(i, j);
            if (a == 2) 
                compteur += randomdoor2(i, j);
            if (a == 1 )  
                compteur += randomdoor1(i, j);
        }
        else if (d >= 2 && diff >= 2) 
        { 
            int a = r.Next(3); 
            if (a == 2) 
                compteur += randomdoor2(i, j);
            if (a == 1|| a == 0) 
                compteur += randomdoor1(i, j);
        }
        else if (d >= 1 && diff >= 1) 
        { 
            int a = r.Next(2);
            if (a == 1)
                compteur += randomdoor1(i, j);
        }
        return compteur;
    }

    
    
    // fonction qui regarde dans combien de direction on peut creuser
    public int possibledirections(int size, int i, int j)
    {
        int d = 0;
        if (!ishere(size, i + 1, j) && isvalid(size, i + 1, j))
            d++;
        if (!ishere(size, i - 1, j) && isvalid(size, i - 1, j))
            d++;
        if (!ishere(size, i, j + 1) && isvalid(size, i, j + 1))
            d++;
        if (!ishere(size, i, j - 1) && isvalid(size, i, j - 1))
            d++;
        return d;
    }
    
    
    
    //fonction qui casse les murs qui touchent une porte
    public void checkdoors(int size, int i, int j)
    {
        if (isvalid(size,i+1,j) && !matrix[i+1,j].left)
            matrix[i, j].right = false;
        if (isvalid(size,i-1,j) && !matrix[i-1,j].right)
            matrix[i, j].left = false;
        if (isvalid(size,i,j+1) && !matrix[i,j+1].top)
            matrix[i, j].bot = false;
        if (isvalid(size,i,j-1) && !matrix[i,j-1].bot)
            matrix[i, j].top = false;
    }
    
    //fonction qui creuse 3 portes
    public int randomdoor3(int i, int j)
    {
        int a = 0;
        a += randomdoor1(i, j);
        a += randomdoor2(i, j);
        return a;
    }
    
    
    //fonction qui creuse 2 portes 
    public int randomdoor2(int i, int j)
    {
        int a = 0;
        a += randomdoor1(i, j);
        a += randomdoor1(i, j);
        return a;
    }
    
    
    //fonction qui creuse 1 porte aléatoirement
    public int randomdoor1(int i, int j)
    {
        int size = matrix.GetLength(0);
        bool added = false;
        
        while (!added && possibledirections(size,i,j)!=0)
        {
            int a = r.Next(4);
                    
            if (a == 0 && isvalid(size, i, j + 1) && matrix[i, j].right)
            {
                added = true;
                matrix[i, j].right = false;
            }
            if (a == 1 && isvalid(size, i, j - 1) && matrix[i, j].left)
            {
                added = true;
                matrix[i, j].left = false;
            }
            if (a == 2 && isvalid(size, i+1, j ) && matrix[i, j].bot)
            {
                added = true;
                matrix[i, j].bot = false;
            }
            if (a == 3 && isvalid(size, i-1, j) && matrix[i, j].top)
            { 
                added = true;
                matrix[i, j].top = false;
            }
        }

        if (added)
        {
            return 1;
        }
        return 0;
    }
}
