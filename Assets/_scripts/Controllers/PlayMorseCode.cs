using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class PlayMorseCode : MonoBehaviour
{

    public AudioSource beeper;

    public bool iSOn = false;

    public TMP_InputField inputFieldMCode;
    public TextMeshProUGUI morseCodeDisplayText;

    public AudioClip dotSound;
    public AudioClip dashSound;
    public float spaceDelay;
    public float letterDelay;
    public float bipDelay;

    // International Morse Code Alphabet
    private string[] alphabet =
    //A     B       C       D      E    F       G
    {".-", "-...", "-.-.", "-..", ".", "..-.", "--.",
    //H       I     J       K      L       M     N
     "....", "..", ".---", "-.-", ".-..", "--", "-.",
    //O      P       Q       R      S      T    U
     "---", ".--.", "--.-", ".-.", "...", "-", "..-",
    //V       W      X       Y       Z
     "...-", ".--", "-..-", "-.--", "--..",
    //0        1        2        3        4      
     "-----", ".----", "..---", "...--", "....-",
    //5        6        7        8        9    
     ".....", "-....", "--...", "---..", "----."};

    // Use this for initialization
    void Start()
    {
        //morseCodeDisplayText = GameObject.Find ("MorseCodeDisplayText").GetComponent<Text> ();
        //inputFieldMCode = GameObject.Find("InputField").GetComponent<InputField>();

        // H   E  L    L   O       W  O    R   L   D
        //.... . .-.. .-.. ---    .-- --- .-. .-.. -..
        //PlayMorseCodeMessage("Hello World.");
        //PlayMorseCodeMessage("");
    }

    public void MorseCodeField(string inputFieldString)
    {
        morseCodeDisplayText.text = inputFieldString;
    }

    public void RepeatMessage()
    {
        iSOn = !iSOn;
        Debug.Log("Checking if Bool is On");
        if (iSOn) {
            InvokeRepeating("repeat", 15f, 60f);
            Debug.Log("I will Invoke Repeat");
        } else {
            Debug.Log("Confirming Bool is Off!");
            CancelInvoke("repeat");
            Debug.Log("I Am Cancelling Invoke Repeating");
            beeper = GameObject.Find("MorseCodeGenerator").GetComponent<AudioSource>();
            beeper.mute = true;
        }


    }

    //Repeat Message Being Sent after "X" time has passed.
    void repeat()
    {
        Debug.Log("Activating Input Field");
        inputFieldMCode.Select();
        inputFieldMCode.ActivateInputField();
        Debug.Log("I Have Activated Text Field");
        //submitText ();
        //Debug.Log ("I Am Calling messageToSend Function");
        message = inputFieldMCode.text;
        StartCoroutine("_PlayMorseCodeMessage", message);
    }


    public void readButton(string c)
    {

        inputFieldMCode.text += c;

    }

    string message;
    public void submitText()
    {
        message = inputFieldMCode.text;
        //inputFieldMCode.Select();
        //inputFieldMCode.ActivateInputField();
        Debug.Log("I Have Submitted Text");
        beeper = GameObject.Find("MorseCodeGenerator").GetComponent<AudioSource>();
        beeper.mute = false;
        StartCoroutine("_PlayMorseCodeMessage", message);
    }



    public void PlayMorseCodeMessage(string message)
    {
        StartCoroutine("_PlayMorseCodeMessage", message);
    }

    private IEnumerator _PlayMorseCodeMessage(string message)
    {
        // Remove all characters that are not supported by Morse code...
        Regex regex = new Regex("[^A-z0-9 ]");
        message = regex.Replace(message.ToUpper(), "");

        // Convert the message into Morse code audio...
        foreach (char letter in message) {
            if (letter == ' ')
                yield return new WaitForSeconds(spaceDelay);
            else {
                int index = letter - 'A';
                if (index < 0)
                    index = letter - '0' + 26;
                string letterCode = alphabet[index];
                foreach (char bit in letterCode) {
                    // Dot or Dash?
                    AudioClip sound = dotSound;
                    if (bit == '-') sound = dashSound;

                    // Play the audio clip and wait for it to end before playing the next one.
                    GetComponent<AudioSource>().PlayOneShot(sound);
                    yield return new WaitForSeconds(sound.length + bipDelay);
                }
                yield return new WaitForSeconds(letterDelay);
            }
        }
    }

    //Transmit Button
    public void Transmit()
    {
        //Transmit Code here - most likely Arduino Pulse fro external short wave transmitter

    }

    //Receive Transmitted Morse Code
    public void Receive()
    {

    }
}