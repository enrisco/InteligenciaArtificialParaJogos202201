using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtuadorPerseguir : MonoBehaviour
{
    public GameObject alvo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Perseguir(float velocidade)
    {
        Vector3 direcao = (alvo.transform.position - transform.position).normalized;
        Quaternion giro = Quaternion.LookRotation(direcao);
        giro.x = 0;
        giro.z = 0;
        transform.rotation = giro;
        transform.Translate(0, 0, velocidade * Time.deltaTime);
    }
}
