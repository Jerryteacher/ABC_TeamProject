using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChat : MonoBehaviour, IInteractionObject
{
    public bool IsIntering { get; set; }
    public InteractionType InterType { get { return InteractionType.CHAT; } }

    public string InterString { get { return "대화하기"; } }

    public bool IsShowInter { get; set; }

    public void ActionInter()
    {
        if (!IsIntering)
        {
            ObjData objData = GetComponent<ObjData>();
            UIManager.getInstance.SetChatDialog(this, gameObject.name, objData.id, objData.isNpc);
            IsIntering = true;
        }
        else
        {
            UIManager.getInstance.ReqestNextChat();
        }
        //throw new System.NotImplementedException();
        //UIManager.getInstance.SetChatID(10);
        //UIManager.getInstance.StartChatAction();

        // UIManaget. Chat Dialog Show
        // player(interaction) - > interaction(콜라이더(레이어) -> scanobj -> objdata(script) -> npcid/obj id
        // -> talksystem -> (talkmanger, questmanager) 
    }

    public void EndInter()
    {
        IsIntering = false;
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

