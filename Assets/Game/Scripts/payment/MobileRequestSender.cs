using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using System.Text;
public class MobileRequestSender : MonoBehaviour {

    private string APIChellenge = "iosw3sn-rest.orange.com:8443/challenge/v1/challenges";

    public void MoneyRequest() {
        WebRequest httpWebRequest = WebRequest.Create("https://iosw3sn-rest.orange.com:8443/challenge/v1/challenges");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
            string json = "{"+
                              "\"challenge\":{"+
                                  "\"method\": \"OTP-SMS- AUTH\","+
		                          "\"country\": \"TUN\","+
		                          "\"service\": \"ODCTOUNSI\","+
		                          "\"partnerId\": \"PADOCK\","+
		                          "\"inputs\":["+
			                          "{"+
				                          "\"type\":\"MSISDN\","+
				                          "\"value\":\"+216xxxxxxxx\""+
                                      "},"+
			                          "{"+
				                          "\"type\":\"confirmationCode\","+
				                          "\"value\":\"\""+
			                          "},"+
			                          "{"+
				                          "\"type\": \"message\","+
				                          "\"value\": \"To confirm your purchase please enter the code %OTP%\""+
			                          "},"+
			                          "{"+
                                          "\"type\": \"otpLength\","+
				                          "\"value\": \"4\""+
			                          "},"+
			                          "{"+
				                          "\"type\": \"senderName\","+
			                              "\"value\": \"ORANGE\""+	
			                          "}"+
		                          "]"+
	                          "}"+
                          "}"; 
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
            string result = streamReader.ReadToEnd();
            Debug.Log(result);

        }
    }

    public void SendRequest() {
        string json = "{" +
                              "\"challenge\":{" +
                                  "\"method\": \"OTP-SMS- AUTH\"," +
                                  "\"country\": \"TUN\"," +
                                  "\"service\": \"ODCTOUNSI\"," +
                                  "\"partnerId\": \"PADOCK\"," +
                                  "\"inputs\":[" +
                                      "{" +
                                          "\"type\":\"MSISDN\"," +
                                          "\"value\":\"+216xxxxxxxx\"" +
                                      "}," +
                                      "{" +
                                          "\"type\":\"confirmationCode\"," +
                                          "\"value\":\"\"" +
                                      "}," +
                                      "{" +
                                          "\"type\": \"message\"," +
                                          "\"value\": \"To confirm your purchase please enter the code %OTP%\"" +
                                      "}," +
                                      "{" +
                                          "\"type\": \"otpLength\"," +
                                          "\"value\": \"4\"" +
                                      "}," +
                                      "{" +
                                          "\"type\": \"senderName\"," +
                                          "\"value\": \"ORANGE\"" +
                                      "}" +
                                  "]" +
                              "}" +
                          "}";


        StartCoroutine(Post("https://iosw3sn-rest.orange.com:8443/challenge/v1/challenges",json));
    }

    IEnumerator Post(string url, string bodyJsonString) {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log("error: " + request.error);
        Debug.Log("is error: " + request.isError);
        Debug.Log("is done: " + request.downloadHandler.isDone);
        Debug.Log("text: " + request.downloadHandler.text);
    }
}
