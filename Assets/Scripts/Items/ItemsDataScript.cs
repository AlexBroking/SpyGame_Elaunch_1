using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDataScript : MonoBehaviour
{

    private GameObject itemTab;
    private GameObject closeTab;
    private GameObject controlCircle;
    public Sprite noteGlow;
    public Sprite uspGlow;

    private PlayerInventoryButton invScript;
    private PlayerControllerMovement playerMove;

    private Vector3 mousePos;

    private bool openTab = false;
    private bool canClickonItem = true;

    public TextAsset jsonFile;
    private ItemText QuestionsInJson;
    public bool justFoundItem = false;
    private PlayerItem playerItem;
    private ProfTekstWay profT;

    public Sprite NotitieImg;
    public Sprite UspImg;
    public List<Sprite> Images = new List<Sprite>();

    private int numberText = 100;
    private bool foundone = false;



    void Start()
    {
        itemTab = GameObject.Find("ItemTab");
        closeTab = itemTab.transform.GetChild(1).gameObject;


        controlCircle = GameObject.Find("Control_Circle");

        invScript = this.gameObject.GetComponent<PlayerInventoryButton>();
        playerMove = this.gameObject.GetComponent<PlayerControllerMovement>();
        playerItem = GameObject.Find("Player").GetComponent<PlayerItem>();
        profT = this.gameObject.GetComponent<ProfTekstWay>();
        


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

                                    for (int j = 0; j < QuestionsInJson.Items.Length; j++)
                                    {
                                        if (QuestionsInJson.Items[j].id.ToString() == childOBJ.name)
                                        {
                                            OpenTab(QuestionsInJson.Items[j].id.ToString());
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
        itemTab.transform.GetChild(2).GetComponent<Text>().text = "";
        itemTab.SetActive(false);
        canClickonItem = true;
        foundone = false;

        if (justFoundItem == false)
        {
            playerMove.canMove = true;
            invScript.inventoryManager.SetActive(true);
            invScript.ButtonIcon.SetActive(true);
            controlCircle.SetActive(true);
            invScript.outPull.SetActive(true);
        }

        if (justFoundItem == true)
        {
            invScript.inventoryManager.SetActive(true);
            invScript.outPull.SetActive(true);
            profT.PutText(playerItem.foundObjectScript.pTekst);
            justFoundItem = false;
        }
    }

    public void OpenTab(string name)
    {
        for (int i = 0; i < QuestionsInJson.Items.Length; i++)
        {
            if (QuestionsInJson.Items[i].id == name)
            {
                numberText = i;
                foundone = true;
            }
        }

        if (foundone == true)
        {
            if (QuestionsInJson.Items[numberText].Kind.ToString() == "Notitie")
            {
                itemTab.GetComponent<SpriteRenderer>().sprite = NotitieImg;
                itemTab.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                itemTab.transform.GetChild(2).GetComponent<Text>().color = Color.black;
                itemTab.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = noteGlow;

            }

            if (QuestionsInJson.Items[numberText].Kind.ToString() == "Usp")
            {
                itemTab.GetComponent<SpriteRenderer>().sprite = UspImg;
                itemTab.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                itemTab.transform.GetChild(2).GetComponent<Text>().color = Color.white;
                itemTab.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = uspGlow;
            }

            for (int i = 0; i < Images.Count; i++)
            {
                if (QuestionsInJson.Items[numberText].Image == Images[i].name)
                {
                    itemTab.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = Images[i];
                }
            }

            itemTab.SetActive(true);
            itemTab.transform.GetChild(0).GetComponent<Text>().text = QuestionsInJson.Items[numberText].Tekst.ToString();
            itemTab.transform.GetChild(2).GetComponent<Text>().text = QuestionsInJson.Items[numberText].HeadTekst.ToString();
            openTab = true;
            canClickonItem = false;
            playerMove.canMove = false;
            invScript.inventoryManager.SetActive(false);
            invScript.ButtonIcon.SetActive(false);
            invScript.outPull.SetActive(false);
            controlCircle.SetActive(false);
        }

        if (foundone == false)
        {
            justFoundItem = true;
            CloseTab();
        }
    }


    
}
