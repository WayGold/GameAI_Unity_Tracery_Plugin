using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraceryPlugin;

public class DialogueManager : MonoBehaviour
{
    public Character char1;
    public Character char2;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // This is the function that somehow processes a sentence thru Tracery
    // (takes in consider the character's relationship)
    string ProcessInputSentence(string sentenceFromOtherCharater)
    {
        // example of processing sentence
        return sentenceFromOtherCharater + " " + Random.Range(0, 20);
    }

    // On continue button click event
    // we should decide who speaks now
    // and process the input sentence thru tracery
    public void OnConversationContinueButtonClick()
    {
        string input;

        // find out who was previously speaking, get that sentence as input to tracery
        if (char1.speaking)
        {
            input = char1.sentenceGUI.text;
            string output = ProcessInputSentence(input);
            char2.sentenceGUI.text = output;
            
        }
        else if (char2.speaking)
        {
            input = char2.sentenceGUI.text;
            string output = ProcessInputSentence(input);
            char1.sentenceGUI.text = output;
        }

        ToggleSpeakingCharacter();

        

    }

    void ToggleSpeakingCharacter()
    {
        char1.speaking = !char1.speaking;
        char2.speaking = !char2.speaking;
    }

    

}
