using UnityEngine;
using System.IO.Ports;

// Test script for debugging with serial

public class ports : MonoBehaviour
{
    public void TestFeature()
    {
        SerialPort port = new SerialPort("COM7", 9600); // connect to the arduino port
        if (!port.IsOpen)
        {
            port.Open();
        }
        port.Write("1");
        Debug.Log("Testing Serial Comms");
        port.Close();
    }
}
