#pragma once
#include <iostream>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
using namespace std;

const int N = 55;

void wait(int seconds);

class Gioco70
{
    char p[N][N];
    
public:
    int PunteggioX, PunteggioO;
    int newposSX, newposDX;
    int PnewposY, PnewposX;
    char palla = '@';
    int X, Y;
    int regoloSX, regoloDX; //necessari per movimento SX e DX
    bool b = false;

    bool lastmodXinc = false;
    bool lastmodYinc = false;
    bool lastmodXdec = false;
    bool lastmodYdec = false;

    Gioco70();

    void modSXDX();
    void changePosition(Gioco70& gioco);
    void BotPosition();
    Gioco70& isNear(Gioco70& gioco);
    Gioco70& muovi( char direct,Gioco70&gioco1);
    void inizializz(Gioco70& gioco);
    friend ostream& operator<<(ostream& os,  Gioco70& gioco);
};
