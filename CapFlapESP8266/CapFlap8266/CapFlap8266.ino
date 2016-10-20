#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>

#define USE_SERIAL Serial

//#define URL "http://gwynn-jones.com.au/irriduino/api/catflap/"
//#define URL "http://DESKTOP-RCITD5B:54206/api/catflap/"

const uint8_t PIN_ENTRY = D7;
const uint8_t PIN_EXIT = D6;

const String URL = "http://DESKTOP-RCITD5B/catflap/api/catflap/";

int DetectionIgnoreMillis = 5000;

ESP8266WiFiMulti WiFiMulti;

void setup() {
  pinMode(LED_BUILTIN, OUTPUT);     // Initialize the LED_BUILTIN pin as an output
  pinMode(PIN_ENTRY, INPUT);
  pinMode(PIN_EXIT, INPUT);

  USE_SERIAL.begin(115200);
  // USE_SERIAL.setDebugOutput(true);

  //  for (uint8_t t = 4; t > 0; t--) {
  //    USE_SERIAL.printf("[SETUP] WAIT %d...\n", t);
  //    USE_SERIAL.flush();
  //    delay(500);
  //  }

  WiFiMulti.addAP("Brian", "cymru am byth");

  USE_SERIAL.println("Connected to Brian, setup complete.");
}


void loop() {
  int buttonEntry = digitalRead(PIN_ENTRY);
  int buttonExit = digitalRead(PIN_EXIT);
  if (buttonEntry == 0)
  {
    Action(URL, "Entry detected", "IN");
  }
  if (buttonExit == 0)
  {
    Action(URL, "Exit detected", "OUT");
  }
}


void Action(String url, String message, String payload)
{

  String hash = GetHash(payload);

  url.concat("?payload=");
  url.concat(payload);

  url.concat("&hash=");
  url.concat(hash);

  USE_SERIAL.print("Payload: ");
  USE_SERIAL.println(payload);
  USE_SERIAL.print("Hash: ");
  USE_SERIAL.println(hash);
  USE_SERIAL.print("URL: ");
  USE_SERIAL.println(url);

  digitalWrite(LED_BUILTIN, LOW);

  HttpPost(url, payload);

  ReadyFlash();
  delay(DetectionIgnoreMillis);
  digitalWrite(LED_BUILTIN, HIGH);
}

