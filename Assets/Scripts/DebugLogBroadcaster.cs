using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

/*
 * Broadcast all Debug Log messages on the current WiFi network
 * By Peter Koch <peterept@gmail.com>
 * 
 * Useful for debugging GearVR builds over WiFi
 * 
 * Use this with any UDP Listener on your PC 
 * eg: SocketTest
 *     http://sourceforge.net/projects/sockettest/
 *     Launch the app, go to UDP tab, set port to 9999 and press Start Listening
 * 
 * Important Note:
 *  - Callstacks are only sent in non-editor builds when "Development Build" is checkmarked in Build Settings
 */  
public class DebugLogBroadcaster : MonoBehaviour 
{
	public int broadcastPort = 9999;

	IPEndPoint remoteEndPoint;
	UdpClient client;

	void OnEnable() 
	{
		remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, broadcastPort);
		client = new UdpClient();
		Application.logMessageReceived += HandlelogMessageReceived;
	}

	void OnDisable() 
	{
		Application.logMessageReceived -= HandlelogMessageReceived;
		client.Close();
		remoteEndPoint = null;
	}

	void HandlelogMessageReceived (string condition, string stackTrace, LogType type)
	{
		string s = stackTrace.Replace ("\n", "\n  ");

		string msg = string.Format ("[{0}] {1}{2}", 
		                           type.ToString ().ToUpper (), 
		                           condition,
		                           "\n    " + stackTrace.Replace ("\n", "\n    "));
		byte[] data = Encoding.UTF8.GetBytes(msg);
		client.Send(data, data.Length, remoteEndPoint);
	}


}
