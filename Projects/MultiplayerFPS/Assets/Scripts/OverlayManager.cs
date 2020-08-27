using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    public static OverlayManager instance;

    public InputField chatToSend;
    public GameObject textPrefab;
    public GameObject chatBox;
    public Transform chatLog;

    public bool isEnabled;
    public bool inputIsEnabled;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ToggleChatBox();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    public void AddChat(int _id, int _index, string _msg)
    {
        string username = GameManager.players[_id].username;
        GameObject chat = Instantiate(textPrefab) as GameObject;
        chat.GetComponent<Text>().text = username + " : " + _msg;
        chat.GetComponent<ChatText>().index = _index;
        chat.transform.SetParent(chatLog);

        //TODO : rearrange by index
    }

    public void ToggleChatBox()
    {
        if (isEnabled)
        {
            if (chatToSend.text.Length != 0)
            {
                string msg = chatToSend.text;
                chatToSend.text = "";
                //chatToSend.Select();
                chatToSend.gameObject.SetActive(false);
                inputIsEnabled = false;
                ClientSend.SendChatMessage(chatToSend.text);
            }
            else
            {
                isEnabled = false;
                chatBox.SetActive(false);
            }
        }
        else
        {
            isEnabled = true;
            chatBox.SetActive(true);
        }
    }
}
