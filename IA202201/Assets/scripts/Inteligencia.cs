using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inteligencia : MonoBehaviour
{
    [Header("Sensores")]
    [SerializeField] SensorVisao visao;
    [Header("Atuadores")]
    [SerializeField] AtuadorPerseguir seguir;
    [SerializeField] AtuadorPatrulha patrulhando;
    // Start is called before the first frame update
    [Header("Configuração")]
    Animator anim;
    [SerializeField] float velocidade;
    public AtuadorPatrulha.tipoDePatrulha tipoDePatrulha;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("podeAndar", true);
        if (visao.vendo)
        {
            
            seguir.Perseguir(velocidade);
        }
        else
        {
            patrulhando.Patrulhar(tipoDePatrulha, velocidade);
        }
    }
}
