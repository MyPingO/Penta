using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Link to the tutorial I used to make this by Lost Relic Games: https://www.youtube.com/watch?v=u2_O-jQDD6s
public class GridBuilder : MonoBehaviour
{
    private int tileGap; 
    public void GenerateGuessesGrid(TMP_Text[][] guesses, int guessLength, GameObject referenceTextTile)
    {
        tileGap = Screen.width / 21;
        transform.position = new Vector2((Screen.width / 2), transform.position.y);
        GameObject referenceTile = Instantiate(referenceTextTile);
        for (int row = 0; row < 6; row++)
        {
            for (int letter = 0; letter < guessLength; letter++)
            {
                GameObject letterTile = Instantiate(referenceTile, transform);
                letterTile.name = "Guess: " + (row + 1) + "x" + (letter + 1);

                guesses[row][letter] = letterTile.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

                letterTile.transform.position = new Vector2(transform.position.x + (letter * tileGap), transform.position.y - (row * tileGap));
            }
        }
        Destroy(referenceTile);
        if (guessLength % 2 != 0)
            transform.position = new Vector2(transform.position.x - (tileGap * (guessLength / 2)), transform.position.y);
        else transform.position = new Vector2(transform.position.x - (tileGap * ((guessLength / 2) - 0.5f)), transform.position.y);
    }
}
