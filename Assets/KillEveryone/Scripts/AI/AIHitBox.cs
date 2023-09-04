using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class AIHitBox : MonoBehaviour
{

    [SerializeField] private GameObject rigidMesh;
    [SerializeField] private GameObject bodyMesh;
    [SerializeField] private float _health;
    [SerializeField] private bool _isCritical = false;

    public void TakeDamage(float damage,Vector3 direction)
    {
        _health -= damage;
        if (_health < 0)
        {
            bodyMesh.SetActive(false);
            rigidMesh.SetActive(true);
            rigidMesh.GetComponent<Rigidbody>().AddForce(direction * 2f,ForceMode.Force);
            if(_isCritical)
            {
                GetComponentInParent<AIController>().SetRagdoll(false);
            }
        }
    }
}
