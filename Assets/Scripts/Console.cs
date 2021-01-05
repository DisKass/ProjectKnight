using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    // Start is called before the first frame update
    InputField history;
    GameObject console;
    RectTransform consoleRectTransform;
    InputField commandLine;
    CharacterController characterController;
    float closedCoord = 300, commandLineCoord = 250, openedCoord = -250;
    bool consoleIsOpened = false;
    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterController>();
        console = GameObject.Find("Console");
        commandLine = GetComponent<InputField>();
        consoleRectTransform = console.GetComponent<RectTransform>();
        history = console.GetComponent<InputField>();
        history.text = "Connected";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            SetConsolePosition(commandLineCoord);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetConsolePosition(openedCoord);
        }
    }
    void SetConsolePosition(float YCoord)
    {
        if (characterController == null)
            characterController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterController>();
        if (consoleRectTransform.anchoredPosition.y == YCoord)
        {
            consoleRectTransform.anchoredPosition = new Vector2(0, closedCoord);
            characterController.isPlaying = true;
        }
        else
        {
            consoleRectTransform.anchoredPosition = new Vector2(0, YCoord);
            commandLine.Select();
            characterController.isPlaying = false;
        }
    }
    public void OnCommandInput()
    {
        string message = commandLine.text;
        history.text += "\n" + commandLine.text;
        commandLine.text = "";
        string firstWord = "";
        if (message.IndexOf(" ") > 0)
        {
            firstWord = message.Substring(0, message.IndexOf(" "));
        }
        switch (firstWord)
        {
            case "initServer": initializeServer(); break;
        }
    }

    void initializeServer()
    {
        
    }
}
