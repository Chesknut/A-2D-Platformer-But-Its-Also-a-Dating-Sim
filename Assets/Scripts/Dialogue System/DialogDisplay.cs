using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> {}

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;
    
    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;
    private bool conversationStarted = false;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }

    // Start is called before the first frame update
    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
            AdvanceConversation();
        else if (Input.GetKeyDown("x"))
            EndConversation();
    }

    private void EndConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    private void AdvanceLine()
    {
        if (conversation == null)
            return;
        if (!conversationStarted)
            Intialize();

        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
        }
        else // End of conversation - look if there's a next conversation
        {
            AdvanceConversation();
        }
    }

    // For only two characters
    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if(speakerUILeft.SpeakerIs(character))
            SetDialog(speakerUILeft, speakerUIRight, line.text);
        else
            SetDialog(speakerUIRight, speakerUILeft, line.text);

        activeLineIndex += 1;
    }

    private void AdvanceConversation()
    {
        if (conversation.question != null)
            questionEvent.Invoke(conversation.question);
        else if (conversation.nextConversation != null)
            ChangeConversation(conversation.nextConversation);
        else
            EndConversation();
    }

    void SetDialog(
        SpeakerUI activeSpeakerUI,
        SpeakerUI inactiveSpeakerUI,
        string text
    )
    {
        activeSpeakerUI.Dialog = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
    }
}
