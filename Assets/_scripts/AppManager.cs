using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AppManager : MonoBehaviour
{

    static AppManager _instance;

    public static AppManager Instance {
        get {
            return _instance;
        }
    }

    public InputField TextInput;
    public Text TextResult;
    public Text TextFullScreen;
    public AudioSource TypeSfx;
    public PlayMorseCode MorseCodePlayer;

    public CryptoSystems.CryptoLanguage currentCryptoSystem;
    public CryptoSystems cryptoTranslator;

    public GameObject HelpMorse;

    private bool _audioOn = true;

    bool direction = true;

    void Awake()
    {
        _instance = this;
        ClearText();

        cryptoTranslator = new CryptoSystems();
        ChangeCryptoMorse(true);
    }

    void Start()
    {

    }

    public void LoadSceneHome()
    {
        SceneManager.LoadScene("_home");
    }




    public void ClearText()
    {
        TextResult.text = "";
        TextInput.text = "";
        TextFullScreen.text = "";
    }

    public void Swap()
    {
        direction = !direction;
        TextInput.text = TextResult.text;

        Translate();
    }

    public void EnableAudio(bool value)
    {
        _audioOn = !value;

        if (_audioOn) {
            AudioListener.volume = 1f;
        } else {
            AudioListener.volume = 0f;
        }

    }


    public void ChangeCryptoMorse(bool status)
    {
        if (status) {
            Debug.Log("ChangeCryptoMorse()");
            currentCryptoSystem = CryptoSystems.CryptoLanguage.morse;
            Translate();

            HelpMorse.SetActive(true);
        } else {
            HelpMorse.SetActive(false);
        }
    }


    public void ChangeCryptoCaesar(bool status)
    {
        if (status) {
            Debug.Log("ChangeCryptoCaesar()");
            currentCryptoSystem = CryptoSystems.CryptoLanguage.caesar;
            Translate();
        }
        HelpMorse.SetActive(false);
    }

    public void ChangeCryptoInverse(bool status)
    {
        if (status) {
            Debug.Log("ChangeCryptoInverse()");
            currentCryptoSystem = CryptoSystems.CryptoLanguage.inverse;
            Translate();
        }
        HelpMorse.SetActive(false);
    }

    public void ChangeCryptoReverse(bool status)
    {
        if (status) {
            Debug.Log("ChangeCryptoReverse()");
            currentCryptoSystem = CryptoSystems.CryptoLanguage.reverse;
            Translate();
        }
        HelpMorse.SetActive(false);
    }

    public void ChangeCryptoNumeric(bool status)
    {
        if (status) {
            Debug.Log("ChangeCryptoNumeric()");
            currentCryptoSystem = CryptoSystems.CryptoLanguage.numeric;
            Translate();
        }
        HelpMorse.SetActive(false);
    }

    public void PlayMorseCode()
    {
        MorseCodePlayer.PlayMorseCodeMessage(TextInput.text);
    }

    public void Translate()
    {
        switch (currentCryptoSystem) {
            case CryptoSystems.CryptoLanguage.caesar:
                TextResult.text = cryptoTranslator.Caesar(TextInput.text, (direction ? -3 : 3));
                break;
            case CryptoSystems.CryptoLanguage.inverse:
                TextResult.text = cryptoTranslator.Inverse(TextInput.text);
                break;
            case CryptoSystems.CryptoLanguage.reverse:
                TextResult.text = cryptoTranslator.Reverse(TextInput.text);
                break;
            case CryptoSystems.CryptoLanguage.numeric:
                TextResult.text = cryptoTranslator.Numeric(TextInput.text);
                break;
            case CryptoSystems.CryptoLanguage.morse:
                TextResult.text = cryptoTranslator.Morse(TextInput.text);
                break;
        }
        TextFullScreen.text = TextResult.text;
        if (_audioOn) {
            TypeSfx.Play();
        }
    }


}
