#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>

#define USE_SERIAL Serial

//#define URL_OUT "http://localhost:54206/api/catflap/IN"
//#define URL_IN "http://localhost:54206/api/catflap/OUT"
//#define URL "http://gwynn-jones.com.au/irriduino/api/catflap/"

//#define URL_OUT "http://DESKTOP-RCITD5B:54206/api/catflap/OUT"
//#define URL_IN  "http://DESKTOP-RCITD5B:54206/api/catflap/IN"

#define URL_OUT "http://gwynn-jones.com.au/irriduino/api/catflap/OUT"
#define URL_IN  "http://gwynn-jones.com.au/irriduino/api/catflap/IN"

const uint8_t PIN_ENTRY = D7;
const uint8_t PIN_EXIT = D6;

ESP8266WiFiMulti WiFiMulti;

void setup() {

  pinMode(LED_BUILTIN, OUTPUT);     // Initialize the LED_BUILTIN pin as an output

//  pinMode(PIN_ENTRY, INPUT_PULLUP);
//  pinMode(PIN_EXIT, INPUT_PULLUP);
  pinMode(PIN_ENTRY, INPUT);
  pinMode(PIN_EXIT, INPUT);

  USE_SERIAL.begin(115200);
  // USE_SERIAL.setDebugOutput(true);

  USE_SERIAL.println();
  USE_SERIAL.println();
  USE_SERIAL.println();

  for (uint8_t t = 4; t > 0; t--) {
    USE_SERIAL.printf("[SETUP] WAIT %d...\n", t);
    USE_SERIAL.flush();
    delay(500);
  }

  WiFiMulti.addAP("Brian", "cymru am byth");

  USE_SERIAL.println("Connected to WiFi.");
  USE_SERIAL.println("");
  USE_SERIAL.println("Setup complete.");
  ReadyFlash();

  digitalWrite(LED_BUILTIN, LOW);
}


void loop() {

  int buttonEntry = digitalRead(PIN_ENTRY);
  int buttonExit = digitalRead(PIN_EXIT);

  if (buttonEntry == 0)
  {
    USE_SERIAL.println("Entry detected");
    digitalWrite(LED_BUILTIN, HIGH);
    CallService(URL_IN);
    digitalWrite(LED_BUILTIN, LOW);
  }

  if (buttonExit == 0)
  {
    USE_SERIAL.println("Exit detected");
    digitalWrite(LED_BUILTIN, HIGH);
    CallService(URL_OUT);
    digitalWrite(LED_BUILTIN, LOW);
  }

}


void CallService(String url)
{
  if ((WiFiMulti.run() == WL_CONNECTED))
  {
    //URL.concat(message);
    USE_SERIAL.print("URL: ");
    USE_SERIAL.println(url);
    USE_SERIAL.print("Service response: [");
    HTTPClient http;
    http.begin(url);
    http.addHeader("Content-Type", "application/x-www-form-urlencoded");
    http.POST("from=user%40mail.com&to=user%40mail.com&text=Test+message+post&subject=Alarm%21%21%21");
    http.writeToStream(&Serial);
    http.end();
    USE_SERIAL.print("]");
  }
}



//
// include https:// etc. in url
void CallServiceOLD(String url, String message)
{
  url.concat(message);

  // wait for WiFi connection
  if ((WiFiMulti.run() == WL_CONNECTED)) {

    HTTPClient http;

    USE_SERIAL.print("[HTTP] begin...\n");

    //http.begin("https://192.168.1.12/test.html", "7a 9c f4 db 40 d3 62 5a 6e 21 bc 5c cc 66 c8 3e a1 45 59 38"); //HTTPS
    http.begin(url); //HTTP

    USE_SERIAL.print("URL: ");
    USE_SERIAL.println(url);

    // start connection and send HTTP header
    //int httpCode = http.GET();
    int httpCode = 0; // = http.PUT(message);

    //http.PUT(message.c_str());
    http.sendRequest("PUT", message);

    // httpCode will be negative on error
    if (httpCode > 0) {
      // HTTP header has been send and Server response header has been handled
      USE_SERIAL.printf("[HTTP] GET... code: %d\n", httpCode);

      // file found at server
      if (httpCode == HTTP_CODE_OK) {
        String payload = http.getString();
        USE_SERIAL.println(payload);
      }
    } else {
      USE_SERIAL.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
      USE_SERIAL.println("Error: ");
      USE_SERIAL.println(httpCode);
    }

    http.end();
  }



}

void ReadyFlash()
{
  // flash ready
  for (int i = 0; i < 3; i++)
  {
    for (int j = 10; j <= 30; j += 5)
    {
      digitalWrite(LED_BUILTIN, HIGH);
      delay(j);
      digitalWrite(LED_BUILTIN, LOW);
      delay(j);
      //USE_SERIAL.print("*");
    }
    delay(50);
  }
}

























void DisplayDigitalPins()
{
  //    for (int pin = 0; pin <= 15; pin++)
  //    {
  //      Serial.print("pin: ");
  //      Serial.print(pin);
  //      Serial.print(" = ");
  //      Serial.println(digitalRead(pin));
  //    }
  //    Serial.println("");
}





