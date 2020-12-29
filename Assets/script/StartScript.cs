using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

    public float getFistOdds(int cd_2, int cd_1, int cd_0)
    {
        string url = "https://bot01.darren-cv.site/rockPaperScissors/getOdds";
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"fist\":[" + cd_2 + "," + cd_1 + "," + cd_0 + "]}";
            print("[POST] " + json + "\n[TO] " + url);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        string result; using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
            print("[RESULT] " + result);
            return float.Parse(result);
        }
    }
    public List<int> usr_fist_history = new List<int>();
    public int getRobotFistCode()
    {
        usr_fist_history = new List<int>();
        if (usr_fist_history.Count > 2)
        {
            float[] fistOdds = { 
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 1),
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 0),
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 2)
            }; return (fistOdds.ToList().IndexOf(fistOdds.Max()) + 1);
        }
        else return UnityEngine.Random.Range(1, 4);
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
                usr_fist_history.Add(1);
                usrRock.init();
                usrPaper.init();
                usrScissors.move_to(0f, -1.5f);
                break;
            case "Rock":
                usr_fist_code = 2;
                usr_fist_history.Add(0);
                usrPaper.init();
                usrScissors.init();
                usrRock.move_to(0f, -2f);
                break;
            case "Paper":
                usr_fist_code = 3;
                usr_fist_history.Add(2);
                usrRock.init();
                usrScissors.init();
                usrPaper.move_to(0f, -2.2f);
                break;
        }

        robot_fist_code = getRobotFistCode();
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
