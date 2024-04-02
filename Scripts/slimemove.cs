using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Net;

//using MongoDB.Driver;
//using System.Threading.Tasks;

public class SlimeMove : MonoBehaviour
{
    const float TOL = 0.25f;
    const float Y = 0.5f;
    public float minX = -2.0f;
    public float maxX = 2.0f;
    public float minZ = -1.25f;
    public float maxZ = 1.25f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;

    private float speed;
    private Vector3 target;
    private Quaternion spin = Quaternion.Euler(0, 360, 0);

    private Analytix _analyticx;

    void setTarget()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        target = new Vector3(x, Y, z);
    }

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        setTarget();
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, spin, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < TOL)
        {
            setTarget();
        }
        checkInRange();
    }

    void checkInRange()
    {
        if(gameObject.transform.position.x > maxX || gameObject.transform.position.x < minX || gameObject.transform.position.z > maxZ || gameObject.transform.position.z < minZ)
        {
            Destroy(gameObject);
            Debug.Log("Game Object out of range");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sphere"))
        {
            Destroy(gameObject);
            updateQueue("slime");
            /*
            Task.Run(async () =>
            {
                await SendDBAsync(data);
            });
            */
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

