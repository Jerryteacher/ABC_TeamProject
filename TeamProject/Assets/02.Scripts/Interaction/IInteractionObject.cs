using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    INTERACTION,    //"상호작용"
    LOOT,           //"줍 기"
    CHAT,           //"대화하기"
    MOVE,           //"이동하기"
}
public interface IInteractionObject
{
    InteractionType InterType { get; }
    string InterString { get; }
    bool IsShowInter { get; set; }
    bool IsIntering { get; set; }

    void ShowInter(bool isShow);
    void ActionInter();
    void EndInter();
}
