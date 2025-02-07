
// Like java libraries
using UnityEngine;
using System.Collections.Generic;

// Code Foldering for big projects and games
namespace deeropsdev
{
    public class TestingScript : MonoBehaviour // Connecting game world and our codes
    {

        // Execute Order of Functions in unity Monobehaviour

        // Executes before game starts and runs once

        //Public,private,protected these are the same with java
        // if we write public keyword to anything this will accessible on unity engine 
        // if we use private keyword on anything we cannot see on the unity engine but while you use [serializefield] now you can see on the editor

        int number = 6;
        private void Awake()
        {
            Debug.Log("Awake");
            TestFunction();
            Debug.LogWarning("Warning Log");
            Debug.LogError("Warning Log");

            switch (number)
            {

                case 5:
                    Debug.Log("sqjkjsd");
                    break;

                case 7:
                    Debug.Log("p1p1ye");
                    break;


            }


            if (number > 9)
            {
                Debug.Log("s1k ye");
            }
            else
            {
                Debug.Log("S1k yeme");
            }
        }

        // All the components same with java loops,lists,array etc there is no different syntax


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Start");

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("Update");
        }

        //Managing Physical Objects and Moves like jumping,hitting etc...
        private void FixedUpdate()
        {

        }

        //No necessary we will use fixed update and update 
        private void LateUpdate()
        {

        }

        // Private functions not execute itself on the engine should be called in the unitys own functions
        void TestFunction()
        {
            Debug.Log("Test Function");
        }


    }
}


