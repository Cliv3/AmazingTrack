using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

namespace AmazingTrack
{
    public class EmailSend : MonoBehaviour
    {
        public InputField inputField;
        public GameObject discountsuccess;
        public GameObject discountfailure;
        public GameObject email;
        public GameObject playagain;

        public void GetInputText()
        {
            string text = inputField.text;
            Debug.Log("Text from Input Field: " + text);
        }
        public void SendEmail()
        {
            CallEndpoint(inputField.text, (success, response) =>
            {
                if (success)
                {
                    Debug.Log("Response: " + response);
                    discountsuccess.SetActive(true);
                    discountfailure.SetActive(false);
                    email.SetActive(false);
                    playagain.SetActive(true);

                }
                else
                {
                    Debug.LogError("Error: " + response);
                    discountfailure.SetActive(true);

                }
            });
        }
     

        // Method to send email with callback
        async void CallEndpoint(string input, Action<bool, string> callback)
        {
            Debug.Log(input);
            string url = "https://2axf1bp8oj.execute-api.eu-west-2.amazonaws.com/dev/createNFT";
            string jsonData = $"{{\"identifier\": \"{input}\"}}"; // Corrected jsonData format
            byte[] postData = Encoding.UTF8.GetBytes(jsonData);
            Debug.Log(jsonData);


            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(postData);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                // Send the request
                var operation = request.SendWebRequest();

                // Wait until the operation is done
                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                // Handle the response
                if (request.result != UnityWebRequest.Result.Success)
                {
                    callback?.Invoke(false, request.error);
                }
                else
                {
                    callback?.Invoke(true, request.downloadHandler.text);
                    
                }
            }
            
        }
    }
}





