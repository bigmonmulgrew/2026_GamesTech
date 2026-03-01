using System;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    string userName = "Dave";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Hello, {userName}!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
