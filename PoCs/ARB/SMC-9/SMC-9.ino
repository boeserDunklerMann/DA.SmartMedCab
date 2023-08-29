/*
   SMC-9 https://boeserdunklermann.atlassian.net/jira/software/projects/SMC/boards/2?selectedIssue=SMC-9
   2023-08-29 --DA
*/
int Gpio17InputPin = 8;

void setup()
{
  pinMode(Gpio17InputPin, INPUT);
  pinMode(LED_BUILTIN, OUTPUT);
}

void loop()
{
  digitalWrite(LED_BUILTIN, digitalRead(Gpio17InputPin));
}

