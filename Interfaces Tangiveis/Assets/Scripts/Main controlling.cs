using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static GameStarter;
using System.Collections;

public class Maincontrolling : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private List<GameObject> imagesObject;
    [SerializeField] private List<TextMeshProUGUI> uiText;
    [SerializeField] private List<GameObject> uiTextGameObject;
    [SerializeField] private TextMeshProUGUI thatOneText;
    [SerializeField] private List<Sprite> imagesList;
    [SerializeField] private List<Sprite> altImagesList;
    private int chosenKey1 = 0;
    private int chosenKey2 = 0;
    private int chosenKey3 = 0;
    private int correctChoice;
    private List<int> chosenImagesList = new List<int>();
    private List<int> allImages = new List<int>();
    private int howManyRight;
    private int round;
    private int roundFinish;


    void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundFinish = GameData.rounds;
        for (int i = 0; i < imagesList.Count; i++)
        {
            allImages.Add(i);
        }
        for (int i = 0; i < 3; i++)
        {
            int chosen = Random.Range(0, allImages.Count);
            chosenImagesList.Add(allImages[chosen]);


            allImages.Remove(chosenImagesList[i]);
            images[i].sprite = imagesList[chosenImagesList[i]];
            uiText[i].text = imagesList[chosenImagesList[i]].name[..^2];
        }
        correctChoice = Random.Range(0, chosenImagesList.Count);
        thatOneText.text = imagesList[chosenImagesList[correctChoice]].name[..^2];
    }

    // Update is called once per frame
    void Update()
    {
        //print($"round - {round} / Finish - {roundFinish}");

        if (Input.GetButtonDown("First Choice"))
        {
            if(round == roundFinish)
                SceneManager.LoadScene("Begin");
            chosenKey1++;
            chosenKey2 = 0;
            chosenKey3 = 0;
        }
        if (Input.GetButtonDown("Second Choice"))
        {
            if(round == roundFinish)
                SceneManager.LoadScene("Begin");
            chosenKey1 = 0;
            chosenKey2++;
            chosenKey3 = 0;
        }
        if (Input.GetButtonDown("Third Choice"))
        {
            if(round == roundFinish)
                SceneManager.LoadScene("Begin");
            chosenKey1 = 0;
            chosenKey2 = 0;
            chosenKey3++;
        }


        if (chosenKey1 == 1)
        {
            images[0].sprite = altImagesList[chosenImagesList[0]];
        }
        else if (chosenKey1 == 0)
        {
            images[0].sprite = imagesList[chosenImagesList[0]];
        }

        if (chosenKey2 == 1)
        {
            images[1].sprite = altImagesList[chosenImagesList[1]];
        }
        else if (chosenKey2 == 0)
        {
            images[1].sprite = imagesList[chosenImagesList[1]];
        }

        if (chosenKey3 == 1)
        {
            images[2].sprite = altImagesList[chosenImagesList[2]];
        }
        else if (chosenKey3 == 0)
        {
            images[2].sprite = imagesList[chosenImagesList[2]];
        }

        //children[i - 1].GetChild(0).gameObject.SetActive(true);
        //children[i - 1].gameObject.SetActive(false);

        if (chosenKey1 == 2 || chosenKey2 == 2 || chosenKey3 == 2)
        {
            if (correctChoice == 0 && chosenKey1 == 2)
            {
                howManyRight++;
            }
            if (correctChoice == 1 && chosenKey2 == 2)
            {
                howManyRight++;
            }
            if (correctChoice == 2 && chosenKey3 == 2)
            {
                howManyRight++;
            }

            chosenKey1 = 0;
            chosenKey2 = 0;
            chosenKey3 = 0;

            if (round < roundFinish - 1)
            {

                chosenImagesList.Clear();
                allImages.Clear();
                for (int i = 0; i < imagesList.Count; i++)
                {
                    allImages.Add(i);
                }
                for (int i = 0; i < 3; i++)
                {

                    chosenImagesList.Add(allImages[Random.Range(0, allImages.Count)]);
                    allImages.Remove(chosenImagesList[i]);
                    images[i].sprite = imagesList[chosenImagesList[i]];
                    images[i].GetComponentInChildren<TextMeshProUGUI>().text = imagesList[chosenImagesList[i]].name[..^2];
                }
                correctChoice = Random.Range(0, chosenImagesList.Count);
                thatOneText.text = imagesList[chosenImagesList[correctChoice]].name[..^2];
            }
            else
            {
                foreach (GameObject child in uiTextGameObject)
                {
                    child.SetActive(false);
                    //children[3].GetComponentInChildren<Text>().transform.position.y = 0;
                }
                foreach (GameObject child in imagesObject)
                    child.SetActive(false);

                thatOneText.text = $"You finished with {howManyRight} rights";
                StartCoroutine(WaitADamnMinute());
            }


            round++;
        }
    }

    IEnumerator WaitADamnMinute()
    {
        yield return new WaitForSeconds(2f);
    }


    
}
