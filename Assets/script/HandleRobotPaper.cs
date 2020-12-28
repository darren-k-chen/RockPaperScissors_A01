// Author: Darren K.J. Chen

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HandleRobotPaper : MonoBehaviour
{
    public void init()
    {
        //this.gameObject.SetActive(true);
        move_to(14.65f, -2.2f);
    }
    public void hide()
    {
        move_to(14.65f, -2.2f);

        GameObject.Find("win_stamp").SetActive(true);
        //HandleWinStamp winStamp = GameObject.Find("win_stamp").GetComponent<HandleWinStamp>();
        //winStamp.display();
    }
    public void move_to(float x = 0f, float y = 8f, float z = 0f)
    {
        transform.position = new Vector3(x, y, z);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Scissors") hide();
        setRigidbodyWakeUp(false);
    }
    public void setRigidbodyWakeUp(bool b)
    {
        Rigidbody rigid = GetComponent<Rigidbody>();

        if (b) rigid.WakeUp();
        else rigid.Sleep();
    }
    void Start()
    {
        // Start is called before the first frame update
    }
    void Update()
    {
        // Update is called once per frame
    }
}
