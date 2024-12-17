using System;
using UnityEngine;

public class Interactalbe : MonoBehaviour
{
    private Material mat;
    private bool focused;
    public float health;
    public GameObject thisObj;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        health = 100;
    }

    private void Die()
    {
        Destroy(thisObj);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Focus()
    {
        if (!focused)
        {
            mat.color = Color.red;
            focused = true;
        }
    }

    public void OnExitFocus()
    {
        if (focused)
        {
            mat.color = Color.white;
            focused = false;
        }
    }
}
