using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generation : MonoBehaviour
{
    public thematrix dungeon;
    [SerializeField] int size;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity);
         
        
        dungeon.generatedungeon(size);
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 vec =new Vector3(i*10,j*10,0);
                Instantiate(dungeon.matrix[i,j].room, vec, Quaternion.identity);
                if (dungeon.matrix[i,j].bot)
                    //Destroy(dungeon.matrix[i,j].Botdoor);
                    Debug.Log("bot");
                if (dungeon.matrix[i,j].top)
                    //Destroy(dungeon.matrix[i,j].Topdoor);
                    Debug.Log("top");
                if (dungeon.matrix[i,j].right)
                    // Destroy(dungeon.matrix[i,j].Rightdoor);
                    Debug.Log("ri");
                if (dungeon.matrix[i, j].left)
                    Debug.Log("le"); 
                //Destroy(dungeon.matrix[i,j].Leftdoor);
                //Instantiate(room, vec, Quaternion.identity);
            }
        }

        GameObject.Find("Main Camera").transform.position = new Vector3(size * 5, size * 5, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
