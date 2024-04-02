using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class spherescript : MonoBehaviour
{
    private HandGrabInteractable _interactable;
    private bool grabbed = false;
    public bool isSnow = false;

    void Start()
    {
        _interactable = gameObject.GetComponent<HandGrabInteractable>();
    }

    void Update()
    {
        var hand = _interactable.Interactors.FirstOrDefault<HandGrabInteractor>();
        if (hand != null && !grabbed && !isSnow)
        {
            Debug.Log("FIRE");
            updateQueue("pelth");
            grabbed = true;
        } else if( hand != null && !grabbed && isSnow)
        {
            Debug.Log("SNOW");
            updateQueue("peltc");
            grabbed = true;
        }
        else if(hand == null)
        {
            grabbed = false;
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
