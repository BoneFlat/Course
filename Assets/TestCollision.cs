using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    public List<ObjectCollision> collisionsList = new List<ObjectCollision>(2);

    private void Awake()
    {
        Debug.Log("Ameo");
        for (int i = 0; i < collisionsList.Count; i++)
        {
            collisionsList[i].SetUpCollision();
        }
    }

}

[Serializable]
public class ObjectCollision
{
    public GameObject gameObject;
    public TypeCollision typeCollision;

    public void SetUpCollision()
    {
        if (gameObject.GetComponent<BoxCollider2D>() == null) // Note is null not usefull in this case.
        {
            gameObject.AddComponent<BoxCollider2D>();
            Debug.Log(gameObject.GetComponent<BoxCollider2D>());
        }
        switch (typeCollision)
        {
            case TypeCollision.Static:
                if(gameObject.TryGetComponent(out Rigidbody2D m_rb))
                {
                    m_rb.hideFlags = HideFlags.HideAndDontSave;
                }
                break;
            case TypeCollision.Kinematic:
                if(gameObject.TryGetComponent(out Rigidbody2D m_rigidBodyKinematic))
                {
                    m_rigidBodyKinematic.isKinematic = true;
                } else
                {
                    var m_newRigidBody = gameObject.AddComponent<Rigidbody2D>();
                    m_newRigidBody.isKinematic = true;
                }
                break;
            case TypeCollision.RigidBody:
                if (gameObject.TryGetComponent(out Rigidbody2D m_rigidBody))
                {
                    m_rigidBody.isKinematic = false;
                }
                else
                {
                    var m_newRigidBody = gameObject.AddComponent<Rigidbody2D>();
                    m_newRigidBody.isKinematic = false;
                }
                break;
        }
    }
}

public enum TypeCollision
{
    Static, RigidBody, Kinematic
}
