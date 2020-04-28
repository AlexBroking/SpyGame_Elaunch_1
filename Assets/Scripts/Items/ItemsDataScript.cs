using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDataScript : MonoBehaviour
{
    private string ItemsString;

    private GameObject itemTab;
    private GameObject closeTab;
    private GameObject controlCircle;

    private PlayerInventoryButton invScript;
    private PlayerControllerMovement playerMove;

    private Vector3 mousePos;

    private bool openTab = false;
    private bool canClickonItem = true;

    public TextAsset jsonFile;
    private ItemText QuestionsInJson;
    

    void Start()
    {
        itemTab = GameObject.Find("ItemTab");
        closeTab = itemTab.transform.GetChild(1).gameObject;

        controlCircle = GameObject.Find("Control_Circle");

        itemTab.transform.GetChild(0).GetComponent<Renderer>().sortingLayerName = "Question";

        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();

        itemTab.SetActive(false);
        QuestionsInJson = JsonUtility.FromJson<ItemText>(jsonFile.text);

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                mousePos = Camera.main.ScreenToWorldPoint(touch.position);
                

                if (touch.phase == TouchPhase.Began)
                {
                    if (openTab == true)
                    {
                        mousePos = new Vector3(mousePos.x, mousePos.y, closeTab.transform.position.z);
                        if (closeTab.GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                        {
                            CloseTab();
                        }
                    }

                    if (invScript.invIsOpen == true)
                    {
                        if (canClickonItem == true)
                        {
                            for (int i = 0; i < invScript.inventoryHolder.transform.childCount; i++)
                            {
                                mousePos = new Vector3(mousePos.x, mousePos.y, invScript.inventoryHolder.transform.GetChild(i).GetChild(0).transform.position.z);
                                
                                if (invScript.inventoryHolder.transform.GetChild(i).GetChild(0).GetComponent<BoxCollider2D>().bounds.Contains(mousePos))
                                {
                                    GameObject childOBJ = invScript.inventoryHolder.transform.GetChild(i).gameObject;
                                    
                                    for (int j = 0; j < QuestionsInJson.itemtext.Length; j++)
                                    {
                                        if (QuestionsInJson.itemtext[i].id.ToString() == childOBJ.name)
                                        {
                                            OpenTab(j);
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }

            }
        }
    }

    public void CloseTab()
    {
        itemTab.transform.GetChild(0).GetComponent<Text>().text = "";
        itemTab.SetActive(false);
        playerMove.canMove = true;
        canClickonItem = true;
        invScript.inventoryManager.SetActive(true);
        invScript.ButtonIcon.SetActive(true);
        controlCircle.SetActive(true);
    }

    public void OpenTab(int numberText)
    {
        itemTab.SetActive(true);
        itemTab.transform.GetChild(0).GetComponent<Text>().text = QuestionsInJson.itemtext[numberText].Tekst.ToString();
        openTab = true;
        canClickonItem = false;
        playerMove.canMove = false;
        invScript.inventoryManager.SetActive(false);
        invScript.ButtonIcon.SetActive(false);
        controlCircle.SetActive(false);
    }
}
