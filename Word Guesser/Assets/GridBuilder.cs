using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Link to the tutorial I used to make this by Lost Relic Games: https://www.youtube.com/watch?v=u2_O-jQDD6s
public class GridBuilder : MonoBehaviour
{
    public int tileGap;
    public void GenerateGuessesGrid(TMP_Text[][] guesses, int guessLength, GameObject referenceTextTile)
    {
        GameObject referenceTile = Instantiate(referenceTextTile);
        for (int row = 0; row < 6; row++)
        {
            for (int letter = 0; letter < guessLength; letter++)
            {
                GameObject letterTile = Instantiate(referenceTile, transform);
                letterTile.name = "Guess: " + (row + 1) + "x" + (letter + 1);

                guesses[row][letter] = letterTile.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

                float xPostition = letter * tileGap;
                float yPostition = row * -tileGap;

                letterTile.transform.position = new Vector2(xPostition, yPostition);
            }
        }
        Destroy(referenceTile);
        transform.position = new Vector2(transform.position.x - ((tileGap / 2) * (guessLength - 1)), transform.position.y);
    }
}
