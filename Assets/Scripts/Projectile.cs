using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void Shoot(Vector3 target)
    {
        rb.velocity = speed * (target - transform.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.Die();
            Destroy(gameObject);
        }
    }
}
