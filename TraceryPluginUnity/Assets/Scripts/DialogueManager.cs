using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraceryPlugin;
using UnityTracery;

public class DialogueManager : MonoBehaviour
{
    public Character char1;
    public Character char2;

    private GrammarManager gm;
    private TraceryGrammar tracery_grammar;

    readonly string[] adjectives =
    {
        "ugly ass",
        "beautiful",
        "awesome",
        "disgusting"
    };

    readonly string[] nouns =
    {
        "animal",
        "fish",
        "chicken",
        "person",
        "elephant",
        "Coca cola",
        "dog"
    };

    readonly string[] greetings =
    {
        "howdy",
        "what's up",
        "how's it going",
        "Whasssup"
    };

    



    // Start is called before the first frame update
    private void Start()
    {
        gm = new GrammarManager();
        gm.AddRule("adjective");
        gm.AddRule("noun");
        gm.AddRule("greeting");
        gm.AddRule("origin");

        gm.AddRuleItem("origin", "[#greeting##adjective##noun#]", 1);
        
        foreach (var str in adjectives)
        {
            if (gm.AddRuleItem("adjective", str, (int)Random.Range(1, 10)))
            {
                Debug.Log("adjective rule added");
            }
        }

        foreach (var str in nouns)
        {
            if (gm.AddRuleItem("noun", str, (int)Random.Range(1, 10)))
            {
                Debug.Log("noun rule added");
            }
        }

        foreach (var str in greetings)
        {
            if (gm.AddRuleItem("greeting", str, (int)Random.Range(1, 10)))
            {
                Debug.Log("greeting rule added");
            }
        }


        string relationship_grammar = gm.GetRelationshipGrammar(1, 3, 3);

        Debug.Log(relationship_grammar);


        tracery_grammar = new TraceryGrammar(relationship_grammar);
        print(tracery_grammar.Grammar);

        string tracery_output = tracery_grammar.Generate();
        
        
        Debug.Log(tracery_output);
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
