using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inteligencia : MonoBehaviour
{
    [Header("Sensores")]
    [SerializeField] SensorVisao visao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (visao.vendo)
        {
            Debug.Log("estou vendo!!!");
        }
    }
}
