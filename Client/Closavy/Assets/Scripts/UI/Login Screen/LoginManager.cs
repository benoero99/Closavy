using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField displayNameTIF;
    [SerializeField] private TMP_Text errorMessageText;

    public void LoginPressed()
    {
        StartCoroutine(Login(displayNameTIF.text));
    }

    public void EmptyErrorMessage(string value)
    {
        errorMessageText.text = string.Empty;
    }

    private IEnumerator Login(string displayName)
    {
        Debug.Log("Display name: " + displayNameTIF.text);

        LoginRequest loginRequest = new()
        {
            DisplayName = displayName,
        };

        yield return ApiClient.Post<LoginRequest, AccountRespone>("Auth/Login", loginRequest, result =>
        {
            if (!result.Success)
            {
                errorMessageText.text = result.ProblemDetails.Title;
                return;
            }

            Debug.Log("Login successful");

            SceneManager.LoadScene(1);
        });
    }
}
