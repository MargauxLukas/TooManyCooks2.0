using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text servName;

    public void ServerSetup()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeServerName(InputField field)
    {
        ChangeName(field, servName);
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Options()
    {
        SceneManager.LoadScene(3);
    }

    public void Shop()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeName(InputField _field, Text _servName)
    {
        if(_field.text.Length > 0)
        {
            _servName.text = _field.text.ToUpper();
        }
        else
        {
            _servName.text = "[SERVER NAME]";
        }
    }

    public void RevertInputFieldPosition(InputField field)
    {
        field.GetComponent<RectTransform>().localPosition = new Vector3(0, 40, 0);
    }
}
