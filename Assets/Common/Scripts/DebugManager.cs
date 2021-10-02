using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RSG;
using UnityEngine.XR;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugManager : Singletone<DebugManager>
{
	public bool slowAnimations;

    public List<XRNode> VRControllers;

    private Map<string, string> debugValues = new Map<string, string>();

    private static int messageID = 0;

    void Awake() {
        Promise.EnablePromiseTracking = true;
		Promise.DoNotHandleExceptions = true;
        Promise.UnhandledException += (object sender, ExceptionEventArgs e) => {
			Debug.LogErrorFormat("Unhandled exception from promises: {0}, {1}", sender, e.Exception);
			if (Promise.DoNotHandleExceptions) {
				throw e.Exception;
			}
        };
    }

    //[SerializeField] private SpeechController.SpeechControllerListener currentListener;

    void TimeDebug()
    {
        //if (VRControllers.Count() > 0 && VRControllers.All(c => VRInput.GetThumbstick(c)))
        //{
        //    if (Input.GetKey(KeyCode.JoystickButton4) && Input.GetKey(KeyCode.JoystickButton5))
        //    {
        //        Time.timeScale = 100;
        //    }
        //    else
        //    {
        //        Time.timeScale = 10;
        //    }
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
        //var rightMenu = KeyCode.Joystick2Button5;

        //DebugManager.LogFormat("43");
        //if (Input.Get
        //if (Input.GetAxis("SeptoThumpAxisYRight") != 0)
        //{
        //    DebugManager.LogFormat("42");
        //}

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            EditorApplication.isPaused = true;
        }
#endif
    }

    bool allButtonDebugOn = false;
    void AllButtonsDebug()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && Input.GetKey(KeyCode.JoystickButton2))
        {
            allButtonDebugOn ^= true;
            DebugManager.DebugValue("allButtonDebugOn", allButtonDebugOn);
            //Endo.SoundManager.instance.SuccessStep();
        }
        if (allButtonDebugOn)
        {
            foreach (KeyCode c in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(c))
                {
                    DebugManager.LogFormat(c.ToString());
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            DebugPanel.instance.gameObject.SetActive(!DebugPanel.instance.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.LogFormat("Pending promises: {0}", Promise.pendingPromises);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
			slowAnimations ^= true;
			DebugManager.LogFormat("Slow animations: {0}", slowAnimations);
        }
        //if (VRControllers.Any(c => VRInput.GetMenuButtonDown

        //SpeechDebug();
        TimeDebug();
        AllButtonsDebug();
    }

    //void InitCurrentListener()
    //{
    //    if (SpeechController.instance.m_speechControllerListeners.Count == 0)
    //    {
    //        return;
    //    }
    //    if (!SpeechController.instance.m_speechControllerListeners.Contains(currentListener))
    //    {
    //        currentListener = SpeechController.instance.m_speechControllerListeners.First();
    //    }
    //}

    //void LogCurrentListener()
    //{
    //    if (currentListener != null)
    //    {
    //        DebugManager.LogFormat("say: <<{0}>>?", currentListener.keyWords.ExtToString());
    //    }
    //    else
    //    {
    //        DebugManager.LogFormat("nothing to say");
    //    }
    //}

    //void NextListener()
    //{
    //    currentListener = SpeechController.instance.m_speechControllerListeners.CyclicNext(currentListener);
    //}

    //private bool speechDebug;
    //void SpeechDebug()
    //{
    //    if (Input.GetKeyDown(KeyCode.JoystickButton2) && Input.GetKey(KeyCode.JoystickButton0))
    //    {
    //        speechDebug ^= true;
    //        DebugManager.DebugValue("speechDebug", speechDebug);
    //        Endo.SoundManager.instance.SuccessStep();
    //    }
    //    if (!speechDebug) return;

    //    var leftMiddle = KeyCode.JoystickButton4;
    //    var rightMiddle = KeyCode.JoystickButton5;

    //    if (Input.GetKeyDown(rightMiddle))
    //    {
    //        InitCurrentListener();
    //        LogCurrentListener();
    //    }

    //    if (Input.GetKeyDown(leftMiddle) && Input.GetKey(rightMiddle))
    //    {
    //        InitCurrentListener();
    //        NextListener();
    //        LogCurrentListener();
    //    }

    //    if (Input.GetKeyUp(rightMiddle))
    //    {
    //        if (currentListener != null)
    //        {
    //            currentListener.Heard();
    //        }
    //        else
    //        {
    //            DebugManager.LogFormat("said nothing");
    //        }
    //    }
    //}

    [SerializeField] private GameObject debugObjects;

    private void Start()
    {
        if (debugObjects != null)
        {
            debugObjects.SetActive(Extensions.InUnityEditor());
        }
    }

    public void Message(string text)
    {
        DebugPanel.instance?.Message(text);
    }

    public static void DebugValue(string key, object value)
    {
        if (instance == null)
        {
            LogFormat("{0}: DEBUG MANAGER DESTROYED", key, value);
            return;
        }
        if (instance.debugValues[key] != value?.ToString())
        {
            instance.debugValues[key] = value?.ToString();
            LogFormat("{0}: {1}", key, value);
        }
    }

    public static void LogFormat(string format, params object[] args)
    {
        ++messageID;

        format = $"{messageID} {format}";

        if (instance != null)
        {
            instance.Message(string.Format(format, args));
        }
        Debug.LogFormat(format, args);
    }
}
