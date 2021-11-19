using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Message : MonoBehaviour
{

    private void Start()
    {
        GameManager.instance.Message += UpdateMessage;
    }

    public void UpdateMessage(Player player, string messageText)
    {
        Text myText = GetComponent<Text>();
        switch (messageText)
        {
            case Constants.CLICK:
                myText.text = "Click a Piece";
                myText.color = player == Player.RED ? Color.red : Color.blue;
                break;
            case Constants.MOVE:
                myText.text = "Move a Piece";
                myText.color = player == Player.RED ? Color.red : Color.blue;
                break;
            case Constants.FINISHED:
                myText.text = player == Player.RED ? "Blue Wins" : "Red Wins";
                myText.color = player == Player.RED ? Color.blue: Color.red;
                break;
            default:
                break;
        }
    }
}
