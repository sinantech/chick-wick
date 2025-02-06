using UnityEngine;

public class TestingScript : MonoBehaviour {

    // Execute Order of Functions in unity Monobehaviour

    // Executes before game starts and runs once
    private void Awake() {
        Debug.Log("Awake");
        TestFunction();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
         Debug.Log("Start");
    }

    // Update is called once per frame
    void Update() {
         Debug.Log("Update");
    }

    //Managing Physical Objects and Moves like jumping,hitting etc...
    private void FixedUpdate() {
        
    }

    //No necessary we will use fixed update and update 
    private void LateUpdate() {
        
    }

    // Private functions not execute itself on the engine should be called in the unitys own functions
    void TestFunction() {
        Debug.Log("Test Function");
    }
    
}
