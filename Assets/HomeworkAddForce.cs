using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkAddForce : MonoBehaviour
{

    private Rigidbody2D m_rigidbody2D;

    public Vector2 forceAdd;

    #region Collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision at: " + m_rigidbody2D);
    }

    #endregion

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // F(Force) = m(mass) * a(acceration)
    // StartVelocity = 0 _ StartForce = m * (-g)

    private void AddedForceModeForce()
    {
        m_rigidbody2D.AddForce(forceAdd, ForceMode2D.Force); // F_new = StartForce + forceAdd; => a => Velocity
    }

    private void AddedForceModeImpulse()
    {
        // This instant add to Velocity 
        // Veclocity = StartVelocity + forceAdd;
        m_rigidbody2D.AddForce(forceAdd, ForceMode2D.Impulse); 
    }

    private void AddVelocity()
    {
        m_rigidbody2D.velocity = forceAdd; // Set the velocity
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            AddedForceModeForce();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) 
        { 
            AddedForceModeImpulse();
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            AddVelocity();
        }
    }
}
