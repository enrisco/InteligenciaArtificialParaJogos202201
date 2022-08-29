using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inteligencia : MonoBehaviour
{
    [Header("Sensores")]
    [SerializeField] SensorVisao visao;
    [Header("Atuadores")]
    [SerializeField] AtuadorPerseguir seguir;
    // Start is called before the first frame update
    [Header("Geral")]
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (visao.vendo)
        {
            anim.SetBool("podeAndar", true);
            seguir.Perseguir();
        }
        else
        {
            anim.SetBool("podeAndar", false);
        }
    }
}
