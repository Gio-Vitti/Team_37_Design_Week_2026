using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerHandScript : MonoBehaviour
{
    private Rigidbody rb;
    public float camDistance;
    public float sensitivity;
    public LayerMask hairLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //fixes the damn object to the damn mouse cursor
        rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), Camera.main.nearClipPlane + camDistance));

        //Change distance from camera

        if (Input.GetMouseButton(0))
        {
            Collider[] hairStrands = Physics.OverlapSphere(rb.position, 100, hairLayer);
            hairStrands[0].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        otherRb.useGravity = true;
    }
}
