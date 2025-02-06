using UnityEngine;

public class TestingScript : MonoBehaviour {

    // Execute Order of Functions in unity Monobehaviour

    // Executes before game starts and runs once
    private void Awake() {
        Debug.Log("Awake");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
         Debug.Log("Start");
    }

    // Update is called once per frame
    void Update() {
         Debug.Log("Update");
    }

    private void FixedUpdate() {
        
    }

    private void LateUpdate() {
        
    }
    
}
