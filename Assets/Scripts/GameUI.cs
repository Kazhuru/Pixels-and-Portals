using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;

    private GameSession session;
    // Start is called before the first frame update
    void Start()
    {
        session = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (session != null)
        {
            livesText.text = "x" + session.CurrentPlayerLives.ToString();
            coinsText.text = session.CurrentScore.ToString();
        }
        
    }
}
