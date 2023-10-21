using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public bool ispointermove;
    private EventSystem eventSystem;
    private GraphicRaycaster raycaster;
    GameManager gameManager;
    [SerializeField]
    TMP_InputField InputFieldPlayerName;
    [SerializeField]
    Button StartButon;
    [SerializeField]
    private PointerEventData pointerEventCurrent;
    private PointerEventData pointerEventLast;


    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();        
        gameManager = FindObjectOfType<GameManager>();
        pointerEventLast = new PointerEventData(eventSystem);
    }
    private void Update()
    {
        StartOnFirstSelection();
        SelectUIOnMousePos();
    }
    private void SelectUIOnMousePos()
    {
        pointerEventCurrent = new PointerEventData(eventSystem);
        pointerEventCurrent.position = eventSystem.currentInputModule.input.mousePosition;
        pointerEventCurrent.delta = pointerEventCurrent.position - pointerEventLast.position;
        List<RaycastResult> results = new List<RaycastResult>();
        if (pointerEventCurrent.IsPointerMoving())
        {
            Cursor.visible = true;
            Debug.Log("eureca");
            ispointermove = true;
            raycaster.Raycast(pointerEventCurrent, results);
            foreach (RaycastResult swatch in results)
            {
                if (swatch.gameObject.GetComponent<Button>() != null)
                {
                    var currentButton = swatch.gameObject.GetComponent<Button>();
                    if (currentButton.gameObject != eventSystem.currentSelectedGameObject)
                    {
                        Debug.Log(swatch.gameObject.transform.name);
                        swatch.gameObject.GetComponent<Button>().Select();
                    }

                }

            }
        }        
        pointerEventLast.position = pointerEventCurrent.position;

    }
    private void StartOnFirstSelection()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            Cursor.visible = false;
            if (eventSystem.currentSelectedGameObject == null)
            {                
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);                
            }
        }
    }

    public void SavePlayerName()
    {
        if (InputFieldPlayerName.text != "")
        {
            gameManager.playerName = InputFieldPlayerName.text;
            StartButon.interactable = true;
        }
        else
        {
            gameManager.playerName = null;
            StartButon.interactable = false;

        }
    }

    
}
