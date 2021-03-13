#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include "DHT.h"
#include <ESP8266WiFiMulti.h>
ESP8266WiFiMulti WiFiMulti;

const char* ssid = "DIR-620-9d98";
const char* password = "25687507";

//Your IP address or domain name with URL path
String serverNameTemp = "http://sergeyutkin.site/CreateData";
String serverNameData = "http://sergeyutkin.site/GetDataArduino";

#include <Wire.h>

// Declaration for an SSD1306 display connected to I2C (SDA, SCL pins)
// датчик DHT:
const int DHTPin = D8;
const int FanPin = D2;
// инициализируем датчик DHT:
DHT dht(DHTPin, DHT11);
String temperature;
String humidity;
String pressure;

unsigned long previousMillis = 0;
const long interval = 5000; 

void setup() {
  Serial.begin(115200);
  Serial.println();
  dht.begin();
  // Address 0x3C for 128x64, you might need to change this value (use an I2C scanner)
  pinMode(FanPin, OUTPUT);


 
  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP(ssid, password);
  while((WiFiMulti.run() == WL_CONNECTED)) { 
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("Connected to WiFi");
}

void loop() {
  unsigned long currentMillis = millis();
  
  if(currentMillis - previousMillis >= interval) {
     // Check WiFi connection status
    if ((WiFiMulti.run() == WL_CONNECTED)) {
          
      
            float h = dht.readHumidity();
            // считываем температуру в Цельсиях (по умолчанию):
            float t = dht.readTemperature();
            // считываем температуру в Фаренгейтах
            // (isFahrenheit = true):
            float f = dht.readTemperature(true);
            // проверяем, прочитались ли данные от датчика, 
            // и если нет, то выходим и начинаем заново:
            if (isnan(h) || isnan(t) || isnan(f)) {
              Serial.println("Failed to read from DHT sensor!");
                          // "Не удалось прочитать 
                          // данные от датчика DHT"
              //String request = httpGETRequest(serverNameTemp, t, h);
              String responseGetData = httpGETData(serverNameData);
            }
            else{
              // рассчитываем градусы в Цельсиях и Фаренгейтах,
              // а также влажность:
              String request = httpGETRequest(serverNameTemp, t, h);
              String responseGetData = httpGETData(serverNameData);
            }
      // save the last HTTP GET Request
      previousMillis = currentMillis;
    }
    else {
      Serial.println("WiFi Disconnected");
    }
  }
}

String httpGETRequest(String serverName, float t, float h) {
  WiFiClient client;
  HTTPClient http;
    serverName = serverName + "/" + t + "/" + h;
    Serial.println(serverName);
  // Your IP address with path or Domain name with URL path 
  http.begin(client, serverName );
  
  // Send HTTP POST request
  int httpResponseCode = http.GET();
  
  String payload = "--"; 
  
  if (httpResponseCode>0) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    payload = http.getString();
  }
  else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  // Free resources
  http.end();
  delay(2000);
  return payload;
}

String httpGETData(String serverName) {
  WiFiClient client;
  HTTPClient http;
    serverName = serverNameData;
    Serial.println(serverName);
  // Your IP address with path or Domain name with URL path 
  http.begin(client, serverName );
  
  // Send HTTP POST request
  int httpResponseCode = http.GET();
  
  String payload = "--"; 
  
  if (httpResponseCode>0) {
    Serial.print("HTTP Response code: ");
    Serial.println(httpResponseCode);
    payload = http.getString();
    Serial.println(payload);
    if (payload == "true")
    {
  // put your main code here, to run repeatedly:
  digitalWrite(FanPin, LOW);
    delay(5000);
    }
    else
    {
  digitalWrite(FanPin, HIGH);
  delay(5000);
    }
  }
  else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  // Free resources
  http.end();
  return payload;
}