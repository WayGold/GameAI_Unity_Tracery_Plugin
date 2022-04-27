using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TraceryPlugin;
using UnityTracery;

public class DialogueManager : MonoBehaviour
{
    public Character char1;
    public Character char2;

    private GrammarManager gm;
    private TraceryGrammar tracery_grammar;

    private RelationshipTracker relationship_tracker;

    readonly string[] positive_adjectives =
    {
        "beautiful",
        "awesome",
        "smart",
        "brilliant",
        "excellent",
        "cool",
        "outstanding",
        "nice",
        "lovely",
        "cute"
    };

    readonly string[] negative_adjectives =
    {
        "ugly ass",
        "disgusting",
        "retarded",
        "dumb",
        "rude",
        "smelly",
        "selfish",
        "disgraceful"
    };

    readonly string[] nouns =
    {
        "fish",
        "chicken",
        "person",
        "elephant",
        "Coca cola",
        "dog",
        "human-being",
        "kitten"
    };

    readonly string[] greetings =
    {
        "howdy",
        "what's up",
        "how's it going",
        "Whasssup",
        "Yoooo",
        "How are you",
        "It's been a while"
    };

    readonly string[] transition_sentences =
    {
        "Hey you know what? ",
        "Yo listen up,",
        "I'm sorry but ",
        "Umm...Should I say this? ",
        "Hey check this out, ",
        "I really have to tell you this,"
    };

    readonly string[] positive_comments =
    {
        "I love you!",
        "I can't take my eyes off you.",
        "You're just a wonderful person.",
        "Let's hang out sometime!",
        "I look forward to see you in the future.",
        "What do I do without you?"
    };

    readonly string[] negative_comments =
    {
        "Beat it and never show up again!",
        "I hate you!",
        "Screw you!",
        "You're a terrible existence.",
        "I can't be friends with you.",
        "Can you just leave me alone?"
    };

    



    // Start is called before the first frame update
    private void Start()
    {
        gm = new GrammarManager();
        gm.AddRule("adjective");
        gm.AddRule("positive_adj");
        gm.AddRule("negative_adj");
        gm.AddRule("noun");
        gm.AddRule("greeting");
        gm.AddRule("comment_about_other_character");
        gm.AddRule("transition_sentence");
        gm.AddRule("comment");
        gm.AddRule("positive_comment");
        gm.AddRule("negative_comment");
        gm.AddRule("origin");

        
        gm.AddRuleItem("comment_about_other_character", "#transition_sentence# #comment#", 0);

        //gm.AddRuleItem("adjective", "#positive_adj#", 1);
        gm.AddRuleItem("adjective", "#negative_adj#", -1);

        gm.AddRuleItem("comment", "#negative_comment#", -1);
        //gm.AddRuleItem("comment", "#positive_comment#", 1);

        gm.AddRuleItem("origin", "#greeting#, #adjective# #noun#! #comment_about_other_character#", 0);
        
        foreach (var str in positive_adjectives)
        {
            if (gm.AddRuleItem("positive_adj", str, Random.Range(1, 10)))
            {
                Debug.Log("pos adjective rule added");
            }
        }

        foreach (var str in negative_adjectives)
        {
            if (gm.AddRuleItem("negative_adj", str, Random.Range(-10, -1)))
            {
                Debug.Log("neg adjective rule added");
            }
        }

        foreach (var str in nouns)
        {
            if (gm.AddRuleItem("noun", str, 0))
            {
                Debug.Log("noun rule added");
            }
        }

        foreach (var str in greetings)
        {
            if (gm.AddRuleItem("greeting", str, 0))
            {
                Debug.Log("greeting rule added");
            }
        }

        foreach (var str in negative_comments)
        {
            if (gm.AddRuleItem("negative_comment", str, Random.Range(-10, -1)))
            {
                Debug.Log("negative comment rule added");
            }
        }

        foreach (var str in positive_comments)
        {
            if (gm.AddRuleItem("positive_comment", str, Random.Range(1, 10)))
            {
                Debug.Log("positive comment rule added");
            }
        }

        foreach (var str in transition_sentences)
        {
            if (gm.AddRuleItem("transition_sentence", str, 0))
            {
                Debug.Log("trans sentence rule added");
            }
        }
        


        // set up relationship agents
        //char1.relationship_agent = new RelationshipAgent();
        //char2.relationship_agent = new RelationshipAgent();

        //relationship_tracker = new RelationshipTracker();
 
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

        // find out who was previously speaking
        if (char1.speaking)
        {
            string relationship_grammar = gm.GetRelationshipGrammar(1, 0, 3);
            tracery_grammar = new TraceryGrammar(relationship_grammar);
            char1.sentenceGUI.text = tracery_grammar.Generate();

        }
        else if (char2.speaking)
        {
            string relationship_grammar = gm.GetRelationshipGrammar(1, 0, 3);
            tracery_grammar = new TraceryGrammar(relationship_grammar);
            char2.sentenceGUI.text = tracery_grammar.Generate();
        }




        ToggleSpeakingCharacter();

    }

    public async void WriteSentencesToFile()
    {
        List<string> sentences = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            string relationship_grammar = gm.GetRelationshipGrammar(1, 0, 3);
            tracery_grammar = new TraceryGrammar(relationship_grammar);

            using (StreamWriter w = File.AppendText("negative_sentences.txt"))
            {
                w.WriteLine(tracery_grammar.Generate());
            }

            tracery_grammar = null;
        }

       

       
    }

    void ToggleSpeakingCharacter()
    {
        char1.speaking = !char1.speaking;
        char2.speaking = !char2.speaking;
    }

    

}
