#include <ESP8266WiFi.h>
#include <SoftwareSerial.h>

// Wi-Fi SSID
#define SSID "Buffalo-G-xxxx"
#define PASSWORD "xxxxxxxxxxxxxx"

#define HOST_NAME "192.168.11.23"
#define HOST_PORT   5432
 
SoftwareSerial mySerial(2, 3);
ESP8266 wifi(mySerial);

/**
 * 初期設定
 */
void setup(void)
{
  pinMode(4, INPUT_PULLUP);
  Serial.begin(9600);

  if (wifi.setOprToStationSoftAP()) {
    Serial.println("to station ok");
  } else {
    Serial.println("to station error");
  }

  if (wifi.joinAP(SSID, PASSWORD)) {
    Serial.println("connect success");
  } else {
    Serial.println("connect error");
  }

  if (wifi.disableMUX()) {
    Serial.println("disable mux success");
  } else {
    Serial.println("disable mux error");
  } 

  if (wifi.registerUDP(HOST_NAME, HOST_PORT)) {
      Serial.print("register udp ok\r\n");
  } else {
      Serial.print("register udp err\r\n");
  }   
}

void loop(void)
{
   if(WiFi.softAPgetStationNum() >= 1)
  {
    uint ADC_Value = 0;
    delay(3);ADC_Value = system_adc_read();

    String str = String(ADC_Value) ;
    int a = str.length();
    char msg[a+1];
    str.toCharArray(msg, a+1);

    wifi.send((const uint8_t*)msg, a+1);
  }
}
