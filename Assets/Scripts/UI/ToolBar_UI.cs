using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolBar_UI : MonoBehaviour
{
    public UnityEvent onSelectedSlotChange;
    [SerializeField] List<Slot_UI> toolBarSlots = new List<Slot_UI>();

    void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            CheckAlphaNumericKeys(Event.current.keyCode);
        }
    }

    public void SelectSlot(int index)
    {
        if(toolBarSlots.Count == 9)
        {
            if(UI_Manager.selectedSlot != null)
            {
                UI_Manager.selectedSlot.SetHighlight(false);
            }
            UI_Manager.selectedSlot = toolBarSlots[index];
            UI_Manager.selectedSlot.SetHighlight(true);
            onSelectedSlotChange.Invoke();
        }
    }

    private void CheckAlphaNumericKeys(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha1:
                SelectSlot(0);
                break;
            case KeyCode.Alpha2:
                SelectSlot(1);
                break;
            case KeyCode.Alpha3:
                SelectSlot(2);
                break;
            case KeyCode.Alpha4:
                SelectSlot(3);
                break;
            case KeyCode.Alpha5:
                SelectSlot(4);
                break;
            case KeyCode.Alpha6:
                SelectSlot(5);
                break;
            case KeyCode.Alpha7:
                SelectSlot(6);
                break;
            case KeyCode.Alpha8:
                SelectSlot(7);
                break;
            case KeyCode.Alpha9:
                SelectSlot(8);
                break;
        }
    }
}
