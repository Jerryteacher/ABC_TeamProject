using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChat : MonoBehaviour, IInteractionObject
{
    public InteractionType InterType { get { return InteractionType.CHAT; } }

    public string InterString { get { return "대화하기"; } }

    public bool IsShowInter { get; set; }

    public void ActionInter()
    {
        //throw new System.NotImplementedException();
        //UIManager.getInstance.SetChatID(10);
        //UIManager.getInstance.StartChatAction();
    }

    public void EndInter()
    {
        //throw new System.NotImplementedException();
    }

    public void ShowInter(bool isShow)
    {
        //UIManager.getInstance.AddInteraction(this, name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
