using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public CameraFade camFade;
    public MusicFade music;
    public AudioSource buttonSound;

    private enum ButtonPress
    {
        start, exit, none
    }
    private ButtonPress buttonPressed; 

	void Start ()
    {
        music.fadeIn();
        camFade.Fade();
        buttonPressed = ButtonPress.none;
        startButton.onClick.AddListener(StartButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
	}
	
	void Update ()
    {
		if (buttonPressed != ButtonPress.none && camFade.Saturated())
        {
            switch (buttonPressed)
            {
                case ButtonPress.start:
                    Data.numDrinks = 0;
                    SceneManager.LoadScene("Bar");
                    break;
                case ButtonPress.exit:
                    Application.Quit();
                    break;

            }
        }
	}

    public void ExitButtonClicked()
    {
        buttonProc();
        buttonPressed = ButtonPress.exit;
    }

    public void StartButtonClicked()
    {
        buttonProc();
        buttonPressed = ButtonPress.start;
    }

    void buttonProc()
    {
        buttonSound.Play();
        startButton.enabled = false;
        exitButton.enabled = false;
        camFade.ReverseFade();
        music.fadeRate = camFade.fadeRate;
        music.fadeOut();
    }
}
