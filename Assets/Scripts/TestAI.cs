using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuggingFace.API;
using ThangChibaGPT;

public class TestAI : MonoBehaviour
{
    public string test;

    //public Conversation _conversation;

    public AIChatController aIChatController;

    void Start()
    {
        //_conversation = new Conversation();
        //_conversation.Clear();
        //_conversation.AddUserInput("Imagine you are average man who is pissed off today. You've heard a phrase: I caught a golden fish. Answer it with anger and aggression.");
        //_conversation.AddGeneratedResponse("Oh no! I hate when that happens. I hope you will be a better person in the future");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            //HuggingFaceAPI.Conversation("Imagine you are a pirate. reply to this text 'I caught a golden fish'", onSuccess =>
            //{
            //    Debug.Log(onSuccess);

            //    test = onSuccess;
            //},
            //onError =>
            //{
            //    Debug.Log(onError);
            //}, _conversation

            //);
            //reply to this text 'I caught a golden fish' in a negative

            //chatGPT.Send("reply to this text 'I caught a golden fish' in a negative", aIChatController);


            var content = test.Trim();
            ChatManager.Instance.ChatGPT.Send(test, aIChatController);
        }



    }
}
