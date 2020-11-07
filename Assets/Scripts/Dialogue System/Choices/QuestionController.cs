using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Question question;
    public Text questionText;
    public Button choiceButton;

    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question _question)
    {
        RemoveChoices();
        question = _question;
        gameObject.SetActive(true);
        Initialize();
    }

    public void Hide(Conversation conversation)
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices()
    {
        foreach (ChoiceController existingChoices in choiceControllers)
            Destroy(existingChoices.gameObject);

        choiceControllers.Clear();
    }

    private void Start() {}

    private void Initialize()
    {
        questionText.text = question.text;

        for (int index = 0; index < question.choices.Length; index++)
        {
            ChoiceController choiceSet = ChoiceController.AddChoiceButton(choiceButton, question.choices[index], index);
            choiceControllers.Add(choiceSet);
        }

        choiceButton.gameObject.SetActive(false);
    }
}
