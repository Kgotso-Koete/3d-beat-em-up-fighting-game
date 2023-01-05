using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    //private PlayerAnimation player_Animation;
    private Rigidbody myBody;
    public float walk_Speed = 2f;
    public float z_Speed = 1.5f;
    private float rotation_Y = -90;
    private float rotation_Speed = 15;
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        //player_Animation = GetComponentInChildren<PlayerAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }
    void FixedUpdate()
    {
        // rather use FixedUpdate for physics calculations
        DetectMovement();
    }
    void DetectMovement()
    {
        float x_Axis = Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walk_Speed);
        float y_Axis = myBody.velocity.y;
        float z_Axis = Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-z_Speed);
        myBody.velocity = new Vector3(x_Axis,y_Axis,z_Axis);
    }
    void RotatePlayer()
    {
        if(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);
        }
        else if(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
    }
}
