using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerHandScript : MonoBehaviour
{
    private Rigidbody rb;
    public float camDistance;
    public float sensitivity;
    public LayerMask hairLayer;
    private bool shaving = false;
    private Collider col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            col.enabled = true;
        } else
        {
            col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            otherRb.useGravity = true;
            StartCoroutine(DestroyObj(other.gameObject));
        
    }

    private IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    } 
}
