using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeForce : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float m_Thrust = 20f;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        
        m_Rigidbody.AddForce(transform.right * m_Thrust, ForceMode2D.Impulse );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
