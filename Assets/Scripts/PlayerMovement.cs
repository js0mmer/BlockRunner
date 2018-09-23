using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float force = 50;
    private float mobileInput = 0f;

	void FixedUpdate ()
    {
        if (Input.GetAxis("Horizontal") > 0f || mobileInput > 0f)
        {
            rb.AddForce(force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetAxis("Horizontal") < 0f || mobileInput < 0f)
        {
            rb.AddForce(-force * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }

    void Update()
    {
        float input = 0f;

        foreach(Touch touch in Input.touches)
        {
            if(touch.position.x > Screen.currentResolution.width / 2)
            {
                input += 1f;
            } else
            {
                input -= 1f;
            }
        }

        mobileInput = input;
    }


}
