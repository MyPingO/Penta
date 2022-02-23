using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintBuilder : MonoBehaviour
{
    public int hintLetterGap;
    public void GenerateHintPositions(TMP_Text[] knowLetterPositions, int guessLength, GameObject referenceHintObject)
    {
        GameObject referenceHintLetter = Instantiate(referenceHintObject);
        for (int i = 0; i < guessLength; i++)
        {
            GameObject hintLetter = Instantiate(referenceHintObject, transform);
            hintLetter.name = "HintLetter: " + (i + 1);

            knowLetterPositions[i] = hintLetter.GetComponent<TMP_Text>();
            GameObject underscore = Instantiate(referenceHintLetter, hintLetter.transform);
            underscore.name = "Underscore " + (i + 1);
            underscore.GetComponent<TMP_Text>().text = "_";
            underscore.GetComponent<TMP_Text>().color = Color.white;
            underscore.GetComponent<TMP_Text>().fontSize = hintLetter.GetComponent<TMP_Text>().fontSize + 10f;

            //move the hint letters to the position of the gameobject this script is attached to AKA the parent gameobject
            hintLetter.transform.position = new Vector2(transform.position.x + (i * hintLetterGap), transform.position.y);
            //move the underscores to the position of the letter hints and move them up a bit for a little adjustment to look nicer / more compact
            underscore.transform.position = new Vector2(hintLetter.transform.position.x, hintLetter.transform.position.y + 7);
        }
        Destroy(referenceHintLetter);
        transform.position = new Vector2(transform.position.x - ((hintLetterGap / 2) * (guessLength - 1)), transform.position.y);
    }
}
