#include <Hash.h>
String GetHash(String clearText)
{
  uint8_t hash[20];
  sha1(clearText, &hash[0]);
  String myString;
  for (int n = 0; n < 20; n++)
  {
    myString.concat(String(hash[n], HEX));
  }
  return myString;
}

