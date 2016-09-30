using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HelpPanel : MonoBehaviour
{
    public Text Title;
    public Text Description;
    public GameObject Container;

    public GameObject HelpCellPrefab;

    char[] AlphabetDictionary;

    // Use this for initialization
    void Start()
    {
        AlphabetDictionary = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'x', 'y', 'w', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }

    public void Show()
    {
        Debug.Log("Show Help Panel");
        AlphabetDictionary = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'x', 'y', 'w', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        foreach (Transform t in Container.transform) {
            Destroy(t.gameObject);
        }

        switch (AppManager.Instance.currentCryptoSystem) {
            case CryptoSystems.CryptoLanguage.caesar:
                Title.text = "Julius Caesar";
                Description.text = "every char gets shifted by -3";

                foreach (char c in AlphabetDictionary) {
                    var newItem = (GameObject)Instantiate(HelpCellPrefab);
                    newItem.transform.SetParent(Container.transform, false);
                    newItem.GetComponent<Text>().text = c + " = " + AppManager.Instance.cryptoTranslator.CaesarChar(c, -3);
                }

                break;
            case CryptoSystems.CryptoLanguage.inverse:
                Title.text = "Inverse";
                Description.text = "every char is inversed like in the table:";

                foreach (char c in AlphabetDictionary) {
                    var newItem = (GameObject)Instantiate(HelpCellPrefab);
                    newItem.transform.SetParent(Container.transform, false);
                    newItem.GetComponent<Text>().text = c + " = " + AppManager.Instance.cryptoTranslator.InverseChar(c);
                }
                break;

            case CryptoSystems.CryptoLanguage.reverse:
                Title.text = "Reverse";
                Description.text = "the string gets reversed!";
                break;
            case CryptoSystems.CryptoLanguage.numeric:
                Title.text = "Numeric";
                Description.text = "every char is converted into its numeric position in the alphabet";

                foreach (char c in AlphabetDictionary) {
                    var newItem = (GameObject)Instantiate(HelpCellPrefab);
                    newItem.transform.SetParent(Container.transform, false);
                    newItem.GetComponent<Text>().text = c + " = " + AppManager.Instance.cryptoTranslator.NumericChar(c);
                }


                break;
            case CryptoSystems.CryptoLanguage.morse:
                Title.text = "Morse Code";
                Description.text = "every char is converted to a group of dots and dashes";

                foreach (char c in AlphabetDictionary) {
                    var newItem = (GameObject)Instantiate(HelpCellPrefab);
                    newItem.transform.SetParent(Container.transform, false);
                    newItem.GetComponent<Text>().text = c + " = " + AppManager.Instance.cryptoTranslator.MorseChar(c);
                }

                break;
        }


        gameObject.SetActive(true);
    }
}
