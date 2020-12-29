// Author: Darren K.J. Chen

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HandleUsrRock : MonoBehaviour
{
    public void init()
    {
        //this.gameObject.SetActive(true);
        move_to(-13.3f, 2f);
    }
    public void hide()
    {
        move_to(-13.3f, 2f);

        //HandleLoseStamp loseStamp = GameObject.Find("lose_stamp").GetComponent<HandleLoseStamp>();
        //loseStamp.display();
        //GameObject.Find("lose_stamp").SetActive(true);
    }
    public void move_to(float x = -0.55f, float y = -1.9f, float z = 0f)
    {
        this.transform.position = new Vector3(x, y, z);
    }
    public void OnCollisionEnter(Collision collision) { if (collision.gameObject.tag == "Paper") hide(); }
    public void onClick()
    {
        StartScript start = GameObject.Find("start").GetComponent<StartScript>();
        start.play_game("Rock");
    }
    private void OnMouseDown() { onClick(); }
    void Start() { }
    void Update() { }
}
