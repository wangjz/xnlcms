using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;

namespace COM.SingNo.Common
{
  public class HttpClient
    {
      //public  ManualResetEvent allDone = new ManualResetEvent(false);
      //public HttpClient()
      //{

      //}

      public static string request(string requestPath)
      {
          //webRequest.Connection
          // Create a new HttpWebRequest object.
          HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestPath);
          // Set the ContentType property. 
          request.ContentType = "application/x-www-form-urlencoded";
          // Set the Method property to 'POST' to post data to the URI.
         // request.Method = "POST";
          // Start the asynchronous operation.    
         // request.BeginGetRequestStream(new AsyncCallback(ReadCallback), request);

          // Keep the main thread from continuing while the asynchronous
          // operation completes. A real world application
          // could do something useful such as updating its user interface. 
         //allDone.WaitOne();
          // Get the response.
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          Stream streamResponse = response.GetResponseStream();
          StreamReader streamRead = new StreamReader(streamResponse);
          string responseString = streamRead.ReadToEnd();
          // Close the stream object.
          streamResponse.Close();
          streamRead.Close();
          // Release the HttpWebResponse.
          response.Close();
          return responseString;
      }

      //private void ReadCallback(IAsyncResult asynchronousResult)
      //{
      //    allDone.Set();
      //}
    }
}
