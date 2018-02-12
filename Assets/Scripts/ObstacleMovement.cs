using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    public Rigidbody obstacle;
    public float force;
	
	void FixedUpdate () {
        if(!FindObjectOfType<GameManager>().stop && gameObject.activeInHierarchy)
        { 
            if (obstacle.transform.position.z < -10f)
            {
                gameObject.SetActive(false);
            }

            obstacle.AddForce(0, 0, -force * Time.deltaTime);
        }
    }
}
