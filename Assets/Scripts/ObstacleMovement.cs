using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    public Rigidbody obstacle;
    public float force;
	
	void FixedUpdate () {
        if(!FindObjectOfType<GameManager>().stop) { 

            if (obstacle.transform.position.z < -10f)
            {
                Destroy(this.gameObject);
            }

            obstacle.AddForce(0, 0, -force * Time.deltaTime);
        }
    }
}
