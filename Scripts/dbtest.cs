using UnityEngine;
using MongoDB.Driver;
using System;

public class tester : MonoBehaviour
{
    private Analytix _analyticx;
    public void TestFeature()
    {
        Debug.Log("Testing this feature");

        var connectionUri = Environment.GetEnvironmentVariable("MONGO_URI");
        if (connectionUri == null)
        {
            Debug.Log("MONGO_URI env var could not be loaded");
            Environment.Exit(0);
        }

        var client = new MongoClient(connectionUri);
        var collection = client.GetDatabase("GAMOT").GetCollection<Analytix>("analytics");

        _analyticx = new Analytix();
        _analyticx.min_flex_range = 3;
        _analyticx.max_flex_range = 3;
        _analyticx.avg_flex_range = 2;
        _analyticx.haptic_intensity = 80;
        _analyticx.temperature = 30;

        try
        {
            collection.InsertOne(_analyticx);
            Debug.Log("Adding analytics");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
}
