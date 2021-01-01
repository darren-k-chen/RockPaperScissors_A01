using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
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
        transform.position = new Vector3(0f, -2.5f, 0f);

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
        //usr_fist_history = new List<int>();
        if (usr_fist_history.Count > 2)
        {
            float[] fistOdds = {
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 1),
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 0),
                getFistOdds(usr_fist_history[usr_fist_history.Count-1], usr_fist_history[usr_fist_history.Count-2], 2)
            }; return (fistOdds.ToList().IndexOf(fistOdds.Max()) + 1);
        } else return UnityEngine.Random.Range(1, 4);
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

        if (tmp == 1 || tmp == -2) { usr_score++; updateScoreBoard(); Mibo.startTTS("恭喜你贏了，耶"); Mibo.motionPlay("666_SP_HorizontalBar"); }
        else if (tmp == 0) Mibo.startTTS("平手，看來我們不相上下呢"); // DRAW
        else { robot_score++; updateScoreBoard(); Mibo.startTTS("唉呀，你輸了，加把勁"); Mibo.motionPlay("666_PE_Phubbing"); }
    }

    private void OnMouseDown() { onClick(); }
    void Start() { INIT(); }
    void Update() { }
}
