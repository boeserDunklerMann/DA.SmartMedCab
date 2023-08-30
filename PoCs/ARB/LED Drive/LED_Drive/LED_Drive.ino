/*
 * 2023-08-30 --DA
 */
int YELLOWLED=2;
int WHITELED=3;
int REDLED=4;
int BLUELED=5;
int LEDS[] = {YELLOWLED, WHITELED, REDLED, BLUELED};
const int ledCount = 4;

void setup()
{
  for (int n=0; n<ledCount; n++)
  {
    pinMode(LEDS[n], OUTPUT);
  }
}

void SwitchOffAll()
{
  for (int n=0; n<ledCount; n++)
  {
    digitalWrite(LEDS[n], LOW);
  }
}

void loop()
{
  SwitchOffAll();
  while(1)
  {
    for (int n=0; n<ledCount; n++)
    {
      SwitchOffAll();
      digitalWrite(LEDS[n], HIGH);
      delay(50);
    }
  }
}
