using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector2 _normal;
    public Vector2 Project(Vector2 forward)
    {
        return forward - Vector2.Dot(forward, _normal) * _normal;
    }

    public Vector2 GetNormal()
    {
        return _normal;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _normal = collision.contacts[0].normal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)_normal * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)Project(transform.right));
    }
}

