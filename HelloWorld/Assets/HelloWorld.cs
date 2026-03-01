using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] string userName = "Dave";

    void Start()
    {
        Debug.Log($"Hello, {userName}!");
    }

}
