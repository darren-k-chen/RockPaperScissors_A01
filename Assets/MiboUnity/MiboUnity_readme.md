# MiboUnity 
###### `ver 1.1`

###Feature list:
 * `動作播放(Motion)`
 * `觸碰(Touch)`
 * `馬達轉動(Motor)`
 * `燈光控制(LED)`
 * `語音播放(TTS)`
 * `聲音辨識(in app localcommand)`
 * `語音轉文字(Speed2Text)`
 * `物件辨識(Recognize)`
 * `人臉追蹤(Face_track)`
 * `人臉辨識(Face_Recognize)`
 * `移動/旋轉(Movement)`

# `DemoScene使用`
匯入MiboPlugins的UnityPackage後，把MiboUnity\Scene 資料夾下的所有Scene加入Build setting中，並把Demo_title放在首位即可。 

# `How to use`
在Editor中使用addComponent加入MiboEventTrigger.cs後即可使用。目前無法在執行期間使用addComponent掛載。



# `動作播放(Motion)`
在Demo_motion_play Scene中可以看到動作播放功能。

1.播放動作 - Mibo要有對應的動作檔名才能正確撥放動作
```csharp
Mibo.motionPlay(string);
```

2.停止播放動作
```csharp
Mibo.motionStop();
```

3.暫停動作
```csharp
Mibo.motionPause();
```

4.從暫停處重新撥放
```csharp
Mibo.motionResume();
```
<br />

# `觸碰(Touch)`

在Demo_touch Scene 可以看到對觸控的反應。

以下是TouchBegin,TouchEnd,Tap,LongPress的接收Event方式
```csharp
MiboEventTrigger trigger = this.GetComponent<MiboEventTrigger>();
if (trigger != null)
{
    trigger.onTouchBegan.AddListener(OnTouchBegin);
    trigger.onTouchEnd.AddListener(OnTouchEnd);
    trigger.onTap.AddListener(OnTap);
    trigger.onLongPress.AddListener(OnLongPress);
}
```

注意!在接收從Android端傳過來的Event時更新UI會出現Error(error狀況 : 無法在非Main Thread的狀況下更新Monobehavior的內容)，要更新UI或是Transform的東西的話需要放在Update中處理。

<br />

# `馬達轉動(Motor)`
在Demon_motor Scene 中可以看到控制指定馬達的轉動位置, 程式碼在MotorMain.cs中

1.取得指定目標的馬達角度
```csharp
Mibo.getMotorPresentPossitionInDegree(Mibo.MiboMotorType.neck_y)
```

2.設定指定目標的馬達角度
```csharp
Mibo.setMotorPositionInDegree((int)type, (int)motorRotateDegree, (int)motorSpeed);
```
<br />

# `燈光控制(LED)`
在Demo_LED Scene中可以讓UnityApp控制機器人的LED。呼叫此功能，機器人上的LED都會被應用程式控制。

1.設定APP控制燈
```csharp
Mibo.disableSystemLED();//關閉系統LED
Mibo.enableLed(Mibo.LEDPosition, bool);//開啟App的LED控制權
Mibo.setLedColor(Mibo.LEDPosition , Color);//設定部位的LED顏色
```


2.關閉設定自由控制燈
```csharp
Mibo.enableLed(Mibo.LEDPosition, false); // 關閉app的LED控制權
Mibo.enableSystemLED(); //讓系統管理LED
```

3.讓燈有呼吸效果
```csharp
Mibo.enableLedBreath(Mibo.LEDPosition, int, int);
```
<br />

# `語音播放(TTS)`
在Demo_tts Scene中可以使用字串來撥放TTS語音, 支援中、英文。

1.播放tts
```csharp
Mibo.startTTS(string);
```

2.停止播放tts
```csharp
Mibo.stopTTS(string);
```

3.暫停播放tts
```csharp
Mibo.pauseTTS(string);
```

4.恢復播放tts
```csharp
Mibo.resumeTTS(string);
```
<br />

# `聲音辨識(in app localcommand)`
在Demo_LocalCommand Scene中。接收命令，判斷接收到的內容後再做處理。支援中文。

1.設定BnfData 需要先把BNF資料設定好
```csharp
Mibo.BnfData mBnfData = new Mibo.BnfData( mi_Name );
foreach (BnfDataClip clip in clips)
{
    mBnfData.addSlot(clip.mi_Tag, clip.isRequired, clip.cmdKey, clip.values);
}
mBnfData.updateBody();
Mibo.createCrammer( mi_Name , mBnfData.body);
```

2.界接Event，界接成功/失敗的狀況
```csharp
Mibo.onLocalCommandComplete += TrueFunction;
Mibo.onLocalCommandException += FalseFunction;
```

3.呼叫聲音辨識。第一次辨識的時候要先註冊VoiceRecognition__Register()，之後呼叫使用startLocalCommand()
```csharp
if (isFirst)
{
    miboVoice.VoiceRecognition__Register();
    isFirst = false;
}
else
{
    Mibo.startLocalCommand();
}
```

回來的Json檔案會像這樣。格式參照 [12.1 识别结果说明](https://www.xfyun.cn/doccenter/awd "title")
```csharp
json : {
      "sn":1, "ls":true, "bg":0, "ed":0,
      "ws":[{
          "bg":0,
          "slot":"<want>",
          "cw":[{
              "w":"我要", "sc":81, "gm":0, "id":65535
            }]
        },{
          "bg":0,
          "slot":"<good>",
          "cw":[{
              "w":"好吃的", "sc":67, "gm":0, "id":65535
            }]
        },{
          "bg":0,
          "slot":"<food>",
          "cw":[{
              "w":"點心", "sc":53, "gm":0, "id":65535
            }]
        }],
      "sc":57
    }
```
另外，json檔案為空的話代表沒有辨識到要求輸入的內容。
<br />

# `語音轉文字(Speed2Text)`
在Demo_Speed2Text Scene中，把收到的語音轉成文字，支援中文。

1.介接Event
```csharp
Mibo.onSpeech2TextComplete += SpeechCallback;
```

2.呼叫SpeechToText
```csharp
Mibo.setListenParameter(Mibo.ListenType.RECOGNIZE, "language", "en_us");
Mibo.setListenParameter(Mibo.ListenType.RECOGNIZE, "accent", null);
Mibo.startSpeech2Text(false); //不需要wake up, 所以設定false
```
回傳的字串為純文字，直接使用即可。

另外，此功能需要網路連線。假如機器人目前的系統時間沒跟目前所在地區時區相同的話，很有可能會出現回傳回來字串為空的狀態。這點要特別注意。

<br /><br />

# `物件辨識(Recognize)`
在Demo_Recognize Scene中，讓機器人可以辨識物件。

1. 接收連線成功的event
```csharp
Mibo.onConnected += isConnectRecognizeSystem;//連線成功後接收
Mibo.onOutput+=ReconizeCheck;//辨識輸出後的資料
```

2. 發送辨識需求
```csharp
Mibo.startRecognition(Mibo.MiboRecognition.OBJ);
```

3. 接收傳回來的資料後，把陣列中的data[i].dataSets[j].title的資料撈出來處理
```csharp
// ReconizeCheck is called once per frame
private void SetInfoData(Mibo.FaceRecognizeData[] data)
{
     for(int i = 0; i < data.Length; i++)
     {
         InfoText.text += "\nid:" + data[i].idx + ", name:" + data[i].name + ", conf:" + data[i].conf + "\nrect:" + JsonUtility.ToJson(data[i].rect);
     }
}
```

data[i].dataset[j]的資料大致上會如下，抓title中的資料即可。
```csharp
{"confidence":0.21084809303283692,"id":"122","title":"10006_Cup_杯子"}
{"confidence":0.1969204843044281,"id":"265","title":"10337_SodaBottle_汽水瓶"}
{"confidence":0.13715511560440064,"id":"131","title":"10023_Feeding Bottle_奶瓶"}

```
<br /><br />

# `人臉追蹤(Face_track)`
在Demo_Facetrack中，讓機器人追蹤人臉的位置。

1. 註冊人臉追鐘的Event
```csharp
Mibo.onTrack += GetTrackData;
```

2. 發送辨識需求。等待初始辨識完畢約2~3秒後，就會到3.
```csharp
Mibo.startRecognition(Mibo.MiboRecognition.FACE);
```

3. 接收傳回來的data資料並做設定
```csharp
void GetTrackData(Mibo.TrackData[] data)
{
    float _x  = float.Parse(data[0].x); //只攔截第一人
    float _y = float.Parse(data[0].y);
    float _w = float.Parse(data[0].width);
    float _h = float.Parse(data[0].height);
    FaceOriginPos = new Vector2(_x, _y); // set face pos
    FaceOriginSize = new Vector2(_w, _h); // set face size
    FaceCenterPos = FaceOriginPos + (FaceOriginSize / 2f); // set face center
}
```

回來的數據大致如下
```csharp
{"height":175,"width":175,"x":250,"y":93}
```

# `人臉辨識(Face_Recognize)`
讓機器人可以辨識眼前的使用者是誰, 程式在Demo_FaceRecognize中。可以先使用新增家人來先行加入要辨識的使用者。

1. 註冊人臉辨識的Event
```csharp
Mibo.onFaceRecognize += ReconizeCheck;
```

2. 發送辨識需求
```csharp
Mibo.startRecognition(Mibo.MiboRecognition.FACE_RECOGNITION);
```

3. 接收傳回來的data資料並抓取，就可以確認目前在Camera面前的是哪個用戶。-1為沒有被新增家人的使用者。
```csharp
  private void SetInfoData(Mibo.FaceRecognizeData[] data)
    {
        if(data != null)
        {
            for(int i = 0; i < data.Length; i++)
            {
                InfoText.text += "\nid:" + data[i].idx + ", name:" + data[i].name + ", conf:" + data[i].conf + ", rect:" + JsonUtility.ToJson(data[i].rect);
            }
        }
    }
```


# `移動/旋轉(Movement)`
讓機器人可以前後移動以及旋轉。方式有直接設定值做移動/旋轉，或是線性加速的方式做移動/旋轉。
另外，此功能需要解除移動鎖定才能呼叫使用。另外、幫機器人充電時，機器人會自動設定移動鎖定。


<br>下面為解除輪子的移動鎖定/移動鎖定的程式。
```csharp
Mibo.LockWheel(); //鎖定輪子
Mibo.UnLockWheel(); //解除鎖定輪子
```

<br>移動，值介於-0.2~0.2間。需要停止移動的時候設定0即可
```csharp
Mibo.SetMove(float);
```


線性加速移動(前進)
```csharp
Mibo.MoveForwardInAccelerationEx();
```
線性加速移動(後退)
```csharp
Mibo.MoveBackInAccelerationEx();
```
停止線性加速移動。此function只能停止線性加速的移動，無法停止由SetMove()呼叫的移動。
```csharp
Mibo.StopInAcclerationEx();
```


<br>旋轉 , 值介於-20~20, 需要停止旋轉的時候設定0即可
```csharp
Mibo.SetTurn(int);
```

向左線性加速旋轉
```csharp
Mibo.StopTurnEx();
```
向右線性加速旋轉
```csharp
Mibo.TurnRightEx();
```
停止線性加速旋轉。此Function只能停止線性加速旋轉的部分，無法停止由setTurn()呼叫的移動。
```csharp
Mibo.TurnLeftEx();
```

還有，當處於線性加速移動/旋轉時強制鎖定輪子、並且在解除鎖定後再使用相反的線性加入移動，機器人會先維持之前的線釁加速移動方式，才會漸漸變成要做的相反移動方式。