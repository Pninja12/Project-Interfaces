using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static GameStarter;

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
    private bool waitingNextRound = false;

    void Start()
    {
        roundFinish = GameData.rounds;
        SetupRound();

       
        for (int i = 0; i < images.Count; i++)
        {
            var outlines = images[i].GetComponents<Outline>();
            if (outlines.Length > 1)
                Debug.LogWarning($"Image {i} tem {outlines.Length} Outline components. Remove os duplicados no Inspector.");
        }
    }

    void Update()
    {
        if (waitingNextRound) return; 

        if (Input.GetButtonDown("First Choice"))
        {
            if (round == roundFinish) { SceneManager.LoadScene("Begin"); return; }
            chosenKey1++;
            chosenKey2 = 0;
            chosenKey3 = 0;
            Debug.Log("First Choice pressed -> chosenKey1 = " + chosenKey1);
        }
        if (Input.GetButtonDown("Second Choice"))
        {
            if (round == roundFinish) { SceneManager.LoadScene("Begin"); return; }
            chosenKey1 = 0;
            chosenKey2++;
            chosenKey3 = 0;
            Debug.Log("Second Choice pressed -> chosenKey2 = " + chosenKey2);
        }
        if (Input.GetButtonDown("Third Choice"))
        {
            if (round == roundFinish) { SceneManager.LoadScene("Begin"); return; }
            chosenKey1 = 0;
            chosenKey2 = 0;
            chosenKey3++;
            Debug.Log("Third Choice pressed -> chosenKey3 = " + chosenKey3);
        }

        
        for (int i = 0; i < 3; i++)
        {
            bool selected = (i == 0 && chosenKey1 == 1) ||
                            (i == 1 && chosenKey2 == 1) ||
                            (i == 2 && chosenKey3 == 1);

            if (selected)
            {
                images[i].sprite = altImagesList[chosenImagesList[i]];
                SetOutline(images[i], true, Color.yellow);
            }
            else
            {
                images[i].sprite = imagesList[chosenImagesList[i]];
                SetOutline(images[i], false, Color.clear);
            }
        }

       
        if (chosenKey1 == 2 || chosenKey2 == 2 || chosenKey3 == 2)
        {
            int chosenIndex = chosenKey1 == 2 ? 0 : chosenKey2 == 2 ? 1 : 2;
            bool isCorrect = (chosenIndex == correctChoice);

            Debug.Log($"Confirm choice index={chosenIndex} correct={correctChoice} isCorrect={isCorrect}");

            if (isCorrect) howManyRight++;

            StartCoroutine(FlashAndProceed(chosenIndex, isCorrect));
        }
    }

    private void SetupRound()
    {
        chosenImagesList.Clear();
        allImages.Clear();

        for (int i = 0; i < imagesList.Count; i++)
            allImages.Add(i);

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, allImages.Count);
            chosenImagesList.Add(allImages[index]);
            allImages.RemoveAt(index);

            images[i].sprite = imagesList[chosenImagesList[i]];
            uiText[i].text = imagesList[chosenImagesList[i]].name[..^2];
            SetOutline(images[i], false, Color.clear);
        }

        correctChoice = Random.Range(0, chosenImagesList.Count);
        thatOneText.text = imagesList[chosenImagesList[correctChoice]].name[..^2];
    }

    private IEnumerator FlashAndProceed(int chosenIndex, bool wasCorrect)
    {
        waitingNextRound = true;

        
        Color original = images[chosenIndex].color;
        images[chosenIndex].color = wasCorrect ? Color.green : Color.red;
        yield return new WaitForSeconds(0.9f); 
        images[chosenIndex].color = original;

        
        chosenKey1 = chosenKey2 = chosenKey3 = 0;

        
        if (round < roundFinish - 1)
        {
            SetupRound();
        }
        else
        {
            foreach (GameObject child in uiTextGameObject)
                child.SetActive(false);

            foreach (GameObject child in imagesObject)
                child.SetActive(false);

            thatOneText.text = $"You finished with {howManyRight} rights";
            StartCoroutine(WaitADamnMinute());
        }

        round++;
        waitingNextRound = false;
    }

    IEnumerator WaitADamnMinute()
    {
        yield return new WaitForSeconds(2f);
    }

    private void SetOutline(Image img, bool enabled, Color color)
    {
        
        Outline outline = img.GetComponent<Outline>();

        if (outline == null && enabled)
            outline = img.gameObject.AddComponent<Outline>();

        if (outline != null)
        {
            outline.effectColor = Color.black;
            outline.effectDistance = new Vector2(8, 8);
            outline.enabled = enabled;
        }
    }
}
