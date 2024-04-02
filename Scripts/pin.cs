using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class pin : MonoBehaviour
{
    bool toggled = false;

    // Update is called once per frame
    void Update()
    {
        float xRotation = transform.eulerAngles.x;
        if (Mathf.Abs(xRotation) - 5.0f < 80.0f && !toggled)
        {
            Debug.Log("hit pin");
            updateQueue("slime");
            toggled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sphere"))
        {
            Destroy(gameObject);
            updateQueue("pin");
        }
    }


    public void updateQueue(string msg)
    {
        StartCoroutine(PostRequest("<url>", msg)); //data
    }

    IEnumerator PostRequest(string url, string msg)
    {
        WWWForm form = new WWWForm();
        form.AddField("Message Info", msg);
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        uwr.certificateHandler = new BypassCertificate();
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
        }
    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
