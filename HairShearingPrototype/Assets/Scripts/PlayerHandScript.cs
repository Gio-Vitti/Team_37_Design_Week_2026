using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerHandScript : MonoBehaviour
{
   
    public float camDistance;
    public float sensitivity;
    public LayerMask hairLayer;
    public float strength;
    private bool shaving = false;
    private Collider col;
    private AudioSource shaveSound;
    private MeshRenderer mesh;
    private float timeDelay = 0;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
       
        col = GetComponent<Collider>();
        shaveSound = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shave when pressed
        if (Mouse.current.leftButton.isPressed)
        {
           
            col.enabled = true;
            
        } else
        {
            shaveSound.Play();
            col.enabled = false;
        }

        //Control size
        Vector2 scroll = Mouse.current.scroll.ReadValue();

        if (scroll.y > 0 && transform.localScale.y < 1)
        {
            SizeUp();
        }

        if (scroll.y < 0 && transform.localScale.y > 0)
        {
            SizeDown();
        }

        //Show signifier for size
        if (timeDelay > 0)
        {
            mesh.enabled = true;
            timeDelay -= Time.deltaTime;
        } else
        {
            mesh.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {     
        //make hair fall
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            otherRb.constraints = RigidbodyConstraints.None;
            otherRb.useGravity = true;

        //send hair flying in random directions
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
        otherRb.AddForce(randomDirection.normalized * strength, ForceMode.Impulse);

            StartCoroutine(DestroyObj(other.gameObject));
    }

    private IEnumerator DestroyObj(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    } 

    private void SizeUp()
    {
        transform.localScale += Vector3.one * 0.1f;
        timeDelay = 1;
    }

    private void SizeDown()
    {
        transform.localScale -= Vector3.one * 0.1f;
        timeDelay = 1;
    }
}
