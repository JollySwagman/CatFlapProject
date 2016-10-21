
void HttpPost(String url, String payload)
{
  // wait for WiFi connection
  if ((WiFiMulti.run() == WL_CONNECTED)) {

    HTTPClient http;

    USE_SERIAL.println("[HTTP] POST " + url);

    http.begin(url);

    // start connection and send HTTP header
    //int httpCode = http.GET();
    int httpCode = http.POST("from=user%40mail.com&to=user%40mail.com&text=Test+message+post&subject=Alarm%21%21%21");

    // httpCode will be negative on error
    if (httpCode > 0) {
      // HTTP header has been send and Server response header has been handled
      USE_SERIAL.printf("[HTTP] POST... code: %d\n", httpCode);

      // file found at server
      if (httpCode == HTTP_CODE_OK) {
        String payload = http.getString();
        USE_SERIAL.println(payload);
      }
    } else {
      USE_SERIAL.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }

    http.end();
  }
}






//// include https:// etc. in url
//void CallService(String url, String payload)
//{
//  //url.concat(message);
//
//  // wait for WiFi connection
//  if ((WiFiMulti.run() == WL_CONNECTED)) {
//
//    HTTPClient http;
//
//    USE_SERIAL.print("[HTTP] begin...\n");
//
//    //http.begin("https://192.168.1.12/test.html", "7a 9c f4 db 40 d3 62 5a 6e 21 bc 5c cc 66 c8 3e a1 45 59 38"); //HTTPS
//    http.begin(url); //HTTP
//    http.addHeader("Content-Type", "text/plain");
//
//    USE_SERIAL.print("URL: ");
//    USE_SERIAL.println(url);
//
//    // start connection and send HTTP header
//    //int httpCode = http.GET();
//    int httpCode = 0; // = http.PUT(message);
//
//    //http.PUT(message.c_str());
//    http.sendRequest("PUT", "");
//
//
//    // httpCode will be negative on error
//    if (httpCode > 0) {
//      // HTTP header has been send and Server response header has been handled
//      USE_SERIAL.printf("[HTTP] GET... code: %d\n", httpCode);
//
//      // file found at server
//      if (httpCode == HTTP_CODE_OK) {
//        String payload = http.getString();
//        USE_SERIAL.println(payload);
//      }
//    } else {
//      USE_SERIAL.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
//      USE_SERIAL.println("Error: ");
//      USE_SERIAL.println(httpCode);
//    }
//
//    http.end();
//  }
//}

//void CallServiceAsynch(String url, String payload)
//{
//  if ((WiFiMulti.run() == WL_CONNECTED))
//  {
//    //url.concat("X!X");
//    USE_SERIAL.print("URL: ");
//    USE_SERIAL.println(url);
//    USE_SERIAL.print("Service response: [");
//
//    HTTPClient http;
//    http.begin(url);
//    http.addHeader("Content-Type", "application/x-www-form-urlencoded");
//
//    //String form = "hash=" + ;
//    //"?payload=p&hash=h"
//
//    http.POST("from=user%40mail.com&to=user%40mail.com&text=Test+message+post&subject=Alarm%21%21%21");
//    http.writeToStream(&Serial);
//    http.end();
//    USE_SERIAL.print("]");
//  }
//}


