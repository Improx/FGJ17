using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFootballPlayer : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    private Vector3 movementDirection;
    private bool isRunning;

    [SerializeField]
    private float minTimeToChangeDirection;
    [SerializeField]
    private float maxTimeToChangeDirection;
	private Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void Start () {
        float newCoolDown = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);
        float newDirection = Random.Range(0, 360);
        StartCoroutine(changeDirection(newCoolDown, newDirection));
    }
	
	// Update is called once per frame
	void Update () {

		rb.velocity = transform.InverseTransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime));
    }

    private IEnumerator changeDirection(float coolDown, float direction) {

        transform.Rotate(0, direction, 0);
        yield return new WaitForSeconds(coolDown);
        float newCoolDown = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);
        float newDirection = Random.Range(0, 360);
        StartCoroutine(changeDirection(newCoolDown, newDirection));

    }



}
