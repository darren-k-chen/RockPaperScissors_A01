using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMain : MonoBehaviour {

    public ESceneConfig[] SceneConfigArr;

    private GameObject Button;

    public Transform Container;

    private Button[] ButtonArr;

    // Use this for initialization
    void Start()
    {
        if (Container.childCount > 0)
        {
            Button = Container.GetChild(0).gameObject;
        }

        if (Button != null && SceneConfigArr != null && SceneConfigArr.Length > 0)
        {
            ButtonArr = new Button[SceneConfigArr.Length];
            for (int i = 0; i < SceneConfigArr.Length; i++)
            {
                //Debug.Log("A");
                GameObject go = Instantiate(Button, Vector2.zero, Quaternion.identity, Container);
                Button btn = go.GetComponent<Button>();
                if (btn != null)
                {
                    //Debug.Log("B");
                    ButtonArr[i] = btn;
                    Text txt = btn.GetComponentInChildren<Text>();
                    if (txt != null)
                    {
                        //Debug.Log("C, s");
                        //txt.name = SceneArr[i].name;
                        txt.text = SceneConfigArr[i].ToString();
                    }
                }
                else
                    Debug.LogError("+===Button err");
            }

            Destroy(Button);
        }


        for (int i = 0; i < ButtonArr.Length; i++)
        {
            Button btn = ButtonArr[i];
            btn.onClick.AddListener( delegate { OnButtonClickEvent(btn);  } );
        }
	}


    private void OnButtonClickEvent(Button button)
    {
        int idx = button.transform.GetSiblingIndex();
        Debug.Log(idx +",Scene:"+ SceneConfigArr[idx].ToString());
        SceneManager.LoadScene(SceneConfigArr[idx].ToString());
    }

	
}

public enum ESceneConfig
{
    Demo_Title,
    Demo_LED,
    Demo_LocalCommand,
    Demo_Speed2Text,
    Demo_Motor,
    Demo_touch,
    Demo_motion_play,
    Demo_tts,
    Demo_Recognize,
    Demo_Facetrack,
    Demo_movement,
    Demo_FaceDetect,
}
