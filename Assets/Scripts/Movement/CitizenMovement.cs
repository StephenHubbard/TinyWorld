using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMovement : MonoBehaviour
{
    [SerializeField] private GameObject planet;

    public float speed = 4;

    [SerializeField] private float gravity = 100;
    [SerializeField] private bool onGround = false;

    private Vector3 groundNormal;
    [SerializeField] private float distanceToGround;

    [SerializeField] private LayerMask planetLayerMask = new LayerMask();

    private Vector3 currentDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        planet = GameObject.Find("Planet");

        StartCoroutine(randomMovement());
    }

    private void Update()
    {
        if (distanceToGround <= .2f)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        GravityOrientation();
    }

    private void GravityOrientation()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, planetLayerMask))
        {

            groundNormal = hit.normal;
            distanceToGround = hit.distance;
            
        }

        Vector3 gravityDirection = (transform.position - planet.transform.position).normalized;

        if (onGround == false)
        {
            rb.AddForce(gravityDirection * -gravity);
            gravity += 10;

        }
        else if (onGround == true)
        {
            gravity = 100;
        }



        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;
    }

    private void Move()
    {
        rb.velocity = new Vector3(currentDirection.x, currentDirection.y, currentDirection.z ) * speed * Time.deltaTime;
    }

    private IEnumerator randomMovement()
    {
        float RandomX = Random.Range(-1f, 1f);
        float RandomZ = Random.Range(-1f, 1f);
        currentDirection = new Vector3(RandomX, 0, RandomZ).normalized;
        yield return new WaitForSeconds(3f);
        StartCoroutine(randomMovement());
    }
}
