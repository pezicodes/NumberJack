  using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codility : MonoBehaviour
{
   // int[] x = {1,2,3};

    void Start()
    {
        work();
    }

    void Update()
    {
        
    }

    // public int Solution(int[] A){

    //     if(!(x[0].Equals(0))){

    //     }

    //     return 0;
    // }

    LinkedList<int> tim = new LinkedList<int>();
    void work(){
        tim.AddFirst(10);
        tim.AddFirst(20);

        print(tim.First.Value);

        

    }



    //tests
}
