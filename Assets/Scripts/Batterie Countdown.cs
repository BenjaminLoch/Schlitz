using UnityEngine;

public class BatterieCountdown : MonoBehaviour
{
    public int dash = 7; //Anfangswert
    private PlayerDeath player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pruefung ob linke Maustaste gedrueckt wurde 
        if (Input.GetKeyDown(KeyCode.Space))
        {
        dash --;
        Debug.Log("Dash uebrig: " + dash); //Aktuellen dash Wert in Konsole
            if (dash == 0)
            {
                player.Die(); //Spieler stirbt
            }
        
        }
        

    }
}
