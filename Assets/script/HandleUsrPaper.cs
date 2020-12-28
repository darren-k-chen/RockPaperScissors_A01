using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HandleUsrPaper : MonoBehaviour
{
    public void init()
    {
        //this.gameObject.SetActive(true);
        move_to(-14.65f, -2.2f);
    }
    public void hide()
    {
        move_to(-14.65f, -2.2f);

        //HandleLoseStamp loseStamp = GameObject.Find("lose_stamp").GetComponent<HandleLoseStamp>();
        //loseStamp.display();
        GameObject.Find("lose_stamp").SetActive(true);
    }
    //public void show_or_hide(bool b)
    //{
    //    this.gameObject.SetActive(b);
    //}
    public void move_to(float x = 4.3f, float y = 0.45f, float z = 0f)
    {
        transform.position = new Vector3(x, y, z);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Scissors") hide();
    }
    public void onClick()
    {
        StartScript start = GameObject.Find("start").GetComponent<StartScript>();
        start.play_game("Paper");
    }
    private void OnMouseDown()
    {
        onClick();
    }

    //public void display()
    //{
    //    this.enabled = true;
    //}

    void Start()
    {
        
    }
    void Update()
    {
        // Update is called once per frame
    }
}
