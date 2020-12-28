﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    public void onClick()
    {
        HandleUsrScissors usrScissors = GameObject.Find("usr_scissors").GetComponent<HandleUsrScissors>();
        HandleUsrRock usrRock = GameObject.Find("usr_rock").GetComponent<HandleUsrRock>();
        HandleUsrPaper usrPaper = GameObject.Find("usr_paper").GetComponent<HandleUsrPaper>();

        usrScissors.move_to();
        usrRock.move_to();
        usrPaper.move_to();

        transform.position = new Vector3(25f, 25f, 0f);
    }
    public void INIT()
    {
        //this.gameObject.SetActive(true);
        transform.position = new Vector3(0f, -2.5f, 0f);
        //float posx = -578f, posy = -255f;
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, posy);

        GameObject.Find("scoreboard").GetComponent<TextMesh>().text
            = "You " + usr_score + " : " + robot_score + " Kebbi";

        HandleUsrScissors usrScissors = GameObject.Find("usr_scissors").GetComponent<HandleUsrScissors>();
        HandleUsrRock usrRock = GameObject.Find("usr_rock").GetComponent<HandleUsrRock>();
        HandleUsrPaper usrPaper = GameObject.Find("usr_paper").GetComponent<HandleUsrPaper>();
        HandleRobotScissors robotScissors = GameObject.Find("robot_scissors").GetComponent<HandleRobotScissors>();
        HandleRobotRock robotRock = GameObject.Find("robot_rock").GetComponent<HandleRobotRock>();
        HandleRobotPaper robotPaper = GameObject.Find("robot_paper").GetComponent<HandleRobotPaper>();

        usrScissors.init(); usrRock.init(); usrPaper.init();
        robotScissors.init(); robotRock.init(); robotPaper.init();

        HandleWinStamp winStamp = GameObject.Find("win_stamp").GetComponent<HandleWinStamp>();
        HandleDrawStamp drawStamp = GameObject.Find("draw_stamp").GetComponent<HandleDrawStamp>();
        HandleLoseStamp loseStamp = GameObject.Find("lose_stamp").GetComponent<HandleLoseStamp>();

        winStamp.init(); drawStamp.init(); loseStamp.init();
    }

    public static int usr_score = 0, robot_score = 0;
    public void updateScoreBoard()
    {
        GameObject.Find("scoreboard").GetComponent<TextMesh>().text
            = "You " + usr_score + " : " + robot_score + " Kebbi";
    }
    public void play_game(string usr_fist = "Scissors")
    {
        HandleUsrScissors usrScissors = GameObject.Find("usr_scissors").GetComponent<HandleUsrScissors>();
        HandleUsrRock usrRock = GameObject.Find("usr_rock").GetComponent<HandleUsrRock>();
        HandleUsrPaper usrPaper = GameObject.Find("usr_paper").GetComponent<HandleUsrPaper>();
        HandleRobotScissors robotScissors = GameObject.Find("robot_scissors").GetComponent<HandleRobotScissors>();
        HandleRobotRock robotRock = GameObject.Find("robot_rock").GetComponent<HandleRobotRock>();
        HandleRobotPaper robotPaper = GameObject.Find("robot_paper").GetComponent<HandleRobotPaper>();

        int robot_fist_code, usr_fist_code = 0;
        switch (usr_fist)
        {
            case "Scissors":
                usr_fist_code = 1;
                usrRock.init();
                usrPaper.init();
                //Thread.Sleep(1);
                usrScissors.move_to(0f, -1.5f);
                break;
            case "Rock":
                usr_fist_code = 2;
                usrPaper.init();
                usrScissors.init();
                //Thread.Sleep(1);
                usrRock.move_to(0f, -2f);
                break;
            case "Paper":
                usr_fist_code = 3;
                usrRock.init();
                usrScissors.init();
                //Thread.Sleep(1);
                usrPaper.move_to(0f, -2.2f);
                break;
        }

        robot_fist_code = UnityEngine.Random.Range(1, 4);
        switch (robot_fist_code)
        {
            case 1:
                robotScissors.move_to();
                robotScissors.setRigidbodyWakeUp(true);
                break;
            case 2:
                robotRock.move_to();
                robotRock.setRigidbodyWakeUp(true);
                break;
            case 3:
                robotPaper.move_to();
                robotPaper.setRigidbodyWakeUp(true);
                break;
        }

        // ====Fist Code====
        // code 1: Scissors
        // code 2: Rock
        // code 3: Paper
        // =================

        int tmp = usr_fist_code - robot_fist_code;

        //HandleDrawStamp drawStamp = GameObject.Find("draw_stamp").GetComponent<HandleDrawStamp>();
        if (tmp == 1 || tmp == -2) { usr_score++; updateScoreBoard(); }
        else if (tmp == 0) GameObject.Find("draw_stamp").SetActive(true) /*drawStamp.display()*/;
        else { robot_score++; updateScoreBoard(); }
    }

    private void OnMouseDown() { onClick(); }
    void Start() { INIT(); }
    void Update() { }
}