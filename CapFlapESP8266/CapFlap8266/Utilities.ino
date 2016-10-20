
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
    }
    delay(50);
  }
}




void DisplayDigitalPins()
{
  for (int pin = 0; pin <= 15; pin++)
  {
    Serial.print("pin: ");
    Serial.print(pin);
    Serial.print(" = ");
    Serial.println(digitalRead(pin));
  }
  Serial.println("");
}

