// Author: Darren K.J. Chen

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HandleUsrScissors : MonoBehaviour
{
    public void init()
    {
        //this.gameObject.SetActive(true);
        move_to(-16f, 5f);
    }
    public void hide()
    {
        move_to(-16f, 5f);

        //GameObject.Find("lose_stamp").SetActive(true);
        //HandleLoseStamp loseStamp = GameObject.Find("lose_stamp").GetComponent<HandleLoseStamp>();
        //loseStamp.display();
    }
    public void move_to(float x = -4.6f, float y = -0.2f, float z = 0f)
    {
        transform.position = new Vector3(x, y, z);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rock") hide();

        //StartScript start = GameObject.Find("start").GetComponent<StartScript>();
        //Thread.Sleep(5000);
        //start.INIT();
    }
    public void onClick()
    {
        StartScript start = GameObject.Find("start").GetComponent<StartScript>();
        start.play_game();
    }
    private void OnMouseDown()
    {
        onClick();
    }

    void Update()
    {
        // Update is called once per frame
    }
    void Start()
    {
        // Start is called before the first frame update
        //onClick();
    }
}
