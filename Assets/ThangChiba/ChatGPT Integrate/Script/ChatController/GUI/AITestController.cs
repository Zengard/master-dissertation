using UnityEngine;



namespace ThangChibaGPT
{
    public class AITestController : AIChatController
    {
        [TextArea(8,8)]
        public string finalGeneratedPhrase;

        public override void OnReceiveChunkResponse(string content)
        {
            Debug.Log(content);
        }

        public override void OnReceiveResponse(string content)
        {
            base.OnReceiveResponse(content);
            finalGeneratedPhrase = content;
        }
    }
}


