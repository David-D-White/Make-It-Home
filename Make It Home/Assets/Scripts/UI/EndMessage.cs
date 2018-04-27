using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMessage : MonoBehaviour
{
    public Score score;
    public Text endMessage;
    public Text scoreMessage;
    public Button menuButton;
    public Button exitButton;
    public AudioSource buttonSound;

    private TextFade messageFade;
    private TextFade scoreFade;
    private enum ButtonPress
    {
        menu, exit, none
    }
    private ButtonPress buttonPressed;

    public bool displayed
    {
        get { return messageFade.Faded();}
    }

	void Start ()
    {
        messageFade = endMessage.GetComponent<TextFade>();
        scoreFade = scoreMessage.GetComponent<TextFade>();
        menuButton.onClick.AddListener(MenuButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
        menuButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        buttonPressed = ButtonPress.none;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(messageFade.Faded())
        {
            messageFade.StopFade();
            scoreFade.StopFade();
            menuButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        if (buttonPressed != ButtonPress.none && !buttonSound.isPlaying)
        {
            switch (buttonPressed)
            {
                case ButtonPress.menu:
                    SceneManager.LoadScene("Menu");
                    break;
                case ButtonPress.exit:
                    Application.Quit();
                    break;
            }
        }
	}

    public void display(bool won)
    {
        scoreMessage.text = "score: "+ score.scoreVal;
        if (won)
            endMessage.text = "You Made It Home";
        else
            endMessage.text = "You Crashed";
        messageFade.Fade();
        scoreFade.Fade();
    }

    void MenuButtonClicked()
    {
        buttonPressed = ButtonPress.menu;
        buttonSound.Play();
    }

    void ExitButtonClicked()
    {
        buttonPressed = ButtonPress.exit;
        buttonSound.Play();
    }
}
