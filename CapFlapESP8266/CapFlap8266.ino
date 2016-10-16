/**
   BasicHTTPClient.ino

    Created on: 24.05.2015

*/

#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>

#define USE_SERIAL Serial

//#define URL "http://gwynn-jones.com.au/irriduino/api/products/2"
//#define URL "http://localhost:54206/api/catflap/IN"
#define URL "http://gwynn-jones.com.au/irriduino/api/catflap/"

const uint8_t PIN_ENTRY = D4;
const uint8_t  PIN_EXIT = D5;

ESP8266WiFiMulti WiFiMulti;

//int pinEntry = 2; // "D4" on wemos...
//int pinExit = 3; // "D5" on wemos...?



void setup() {

  pinMode(LED_BUILTIN, OUTPUT);     // Initialize the LED_BUILTIN pin as an output
  digitalWrite(LED_BUILTIN, HIGH);

  pinMode(PIN_ENTRY, INPUT_PULLUP);
  pinMode(PIN_EXIT, INPUT_PULLUP);

  USE_SERIAL.begin(115200);
  // USE_SERIAL.setDebugOutput(true);

  USE_SERIAL.println();
  USE_SERIAL.println();
  USE_SERIAL.println();

  for (uint8_t t = 4; t > 0; t--) {
    USE_SERIAL.printf("[SETUP] WAIT %d...\n", t);
    USE_SERIAL.flush();
    delay(1000);
  }

  WiFiMulti.addAP("Brian", "cymru am byth");


  USE_SERIAL.println("Connected to Brian.");

  digitalWrite(LED_BUILTIN, LOW);
}

void loop() {

  int buttonEntry = digitalRead(PIN_ENTRY);
  int buttonExit = digitalRead(PIN_EXIT);

  if (buttonEntry == 0)
  {
    USE_SERIAL.println("Entry detected");
    CallService("IN");
  }

  if (buttonExit == 0)
  {
    USE_SERIAL.println("Exit detected");
    CallService("OUT");
  }

  delay(500);
}


void CallService(String message)
{
  if ((WiFiMulti.run() == WL_CONNECTED))
  {
    digitalWrite(LED_BUILTIN, HIGH);

    USE_SERIAL.print("Service response: ");

    HTTPClient http;
    http.begin(URL + message);
    http.addHeader("Content-Type", "application/x-www-form-urlencoded");
    http.POST("from=user%40mail.com&to=user%40mail.com&text=Test+message+post&subject=Alarm%21%21%21");
    http.writeToStream(&Serial);
    http.end();

    USE_SERIAL.println("");
    digitalWrite(LED_BUILTIN, LOW);
  }
}


void CallServiceOLD()
{

  // wait for WiFi connection
  if ((WiFiMulti.run() == WL_CONNECTED)) {

    HTTPClient http;

    USE_SERIAL.print("[HTTP] begin...\n");

    //http.begin("https://192.168.1.12/test.html", "7a 9c f4 db 40 d3 62 5a 6e 21 bc 5c cc 66 c8 3e a1 45 59 38"); //HTTPS
    http.begin(URL); //HTTP

    USE_SERIAL.print("[HTTP] GET...\n");
    // start connection and send HTTP header
    int httpCode = http.GET();

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
    }

    http.end();
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

