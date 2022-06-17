#include "Gioco70.h"
#include <cmath>

Gioco70::Gioco70() {
	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++) {
			p[i][j] = ' ';
			if(i>0)p[i][N - 1] = '|';
			if(j>0)p[N - 1][j] = '|';
			if (j > 0) {
				p[1][j] = '|';
				p[j][1] = '|';
			}
		}
	}
	PunteggioO = 0;
	PunteggioX = 0;

	newposSX = 0;
	newposDX = 0;

	regoloSX = -25;
	regoloDX = -25;

	PnewposY = 0;
	PnewposX = 0;


	X = N / 2;
	Y = N / 2;

	//creating 2 tables 
	for (int i = (N / 2); i < (N / 5 * 3); i++) {
		p[i][2] = 'X';
		p[i][N - 2] = 'O';

	}

	p[Y][X] = palla;


}


/*l'inclinazione sarà data tramite il rapporto di due numeri randomici*/
// tan = y/x
// sin = x/ipo
// cos = y/ipo

void Gioco70::inizializz(Gioco70& gioco) { 

	srand(time(NULL)); // per generare costantemente valori casuali modificando il seme

	while (PnewposX == 0 && PnewposY == 0 || PnewposX == 0 && PnewposY == 1 || PnewposX == 0 && PnewposY == -1) {
		PnewposX = 1;
		PnewposY = 1;

		float Y1 = rand() % 100 -50;
		float X1 = rand() % 100 -50;

		float ipo = sqrt((Y1 * Y1) + (X1 * X1));

		float sin = (X1 / ipo);
		float cos = (Y1 / ipo);

		PnewposX = sin * PnewposX;
		PnewposY = cos * PnewposY;

		if (PnewposY != 0) {
			if (PnewposY < 0) {
				--PnewposY;
			}
			if (PnewposY > 0) {
				++PnewposY;
			}
		}
		if (PnewposX != 0) {
			if (PnewposX < 0)
				--PnewposX;
			if (PnewposX > 0)
				++PnewposX;
		}
	}
	/*PunteggioO = 0;
	PunteggioX = 0;*/

	

}

Gioco70& Gioco70::muovi( char direct,Gioco70& gioco1) {

	if (!b) {
		if (direct == 'a') { --PnewposY; --PnewposX; }
		if (direct == 'd') { ++PnewposY; ++PnewposX; }
		if (direct == 'w') { --PnewposX; ++PnewposY; }
		if (direct == 's') { ++PnewposX; --PnewposY; }
	}

	Gioco70::isNear(gioco1);

	return *this;
}

Gioco70& Gioco70::isNear(Gioco70& gioco) {


	if (gioco.p[PnewposY + Y - 1][PnewposX + X] == 'X' || gioco.p[PnewposY + Y - 1][PnewposX + X] == 'O') { // sbarra verticale sotto
		gioco.p[PnewposY + Y][PnewposX + X] = ' ';
		gioco.inizializz(gioco);
		lastmodYinc = true;
	}
	if (gioco.p[PnewposY + Y + 1][PnewposX + X] == 'X' || gioco.p[PnewposY + Y + 1][PnewposX + X] == 'O') { //verticale sopra
		gioco.p[PnewposY + Y][PnewposX + X] = ' ';
		//gioco.inizializz(gioco);
		lastmodYdec = true;
	}
	if (gioco.p[PnewposY + Y][PnewposX + X - 1] == 'X') { //sbarra orizzontale sx solo X
		gioco.p[PnewposY + Y][PnewposX + X] = ' ';
		gioco.inizializz(gioco);
		lastmodXinc = true;
	}

	if (gioco.p[PnewposY + Y][PnewposX + X + 1] == 'O') { //sbarra orizzontale dx solo O
		gioco.p[PnewposY + Y][PnewposX + X] = ' ';
		//gioco.inizializz(gioco);
		lastmodXdec = true;
	}

	if (PnewposX + X > N-4) { //qui modifico la palla
		lastmodXdec = true;
		lastmodXinc = false;
		if (gioco.p[PnewposY + Y][PnewposX + X - 1] != 'O')
			PunteggioX++;
		b = true;
	}
	if (PnewposY + Y > N-4) {
		lastmodYdec = true;
		lastmodYinc = false;
		gioco.inizializz(gioco);
		b = true;
	}
	if (PnewposX + X < 4) { //qui modifico la palla
		lastmodXdec = false;
		lastmodXinc = true;
		if(gioco.p[PnewposY + Y][PnewposX + X - 1] != 'X')
			PunteggioO++;
		b = true;
	}
	if (PnewposY + Y < 3) {
		lastmodYdec = false;
		lastmodYinc = true;
		gioco.inizializz(gioco);
		b = true;
	}

		if (lastmodYinc) {
			++PnewposY;
		}
		if (lastmodYdec) {
			--PnewposY;
		}
		if (lastmodXinc) {
			++PnewposX;
		}
		if (lastmodXdec) {
			--PnewposX;
		}
	BotPosition();

	gioco.changePosition(gioco);

	return *this;
}
void Gioco70::BotPosition() {

	modSXDX();

	//implementazione "intelligenza" : deve predire dove finirà la palla e posizionarvisi in anticipo
	if (newposSX < regoloSX)
		newposSX++;

	if (newposSX > 25)
		newposSX = 23;


	if (newposDX < regoloSX)
		newposDX++;

	if (newposDX > N - 33)
		newposDX--;

}
void Gioco70::changePosition(Gioco70& gioco) {


	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++) {
			gioco.p[i][j] = ' ';
			if (i > 0)p[i][N - 1] = '|';
			if (j > 0)p[N - 1][j] = '|';
			if (j > 0) {
				p[1][j] = '|';
				p[j][1] = '|';
			}	
		}
		if (i == N / 2)
			gioco.p[i][0] = 'A';
	}

	for (int i = (N / 2); i < (N / 5 * 3); i++) {
		p[i + newposSX][2] = 'X';
		p[i + PnewposY][N - 2] = 'O';
	}

	gioco.p[Y + PnewposY][X + PnewposX] = '@';
}

void wait(int seconds)
{
	clock_t endwait;
	endwait = clock() + 0.01*(seconds) * CLOCKS_PER_SEC;
	while (clock() < endwait) {}
}

void Gioco70::modSXDX() {
	/*funzionamento:*/

	/*la funzione agisce calcolando il triangolo formato da PnewposX+X e PnewposY+Y, 
	trovandone l'angolo e cosi risalendo alla posizione Y dove deve sistemarsi SX */

	/*lo schema predittivo può esser allargato calcolando il triangolo che genera nel caso in cui urti un lato orizzontale*/
	

		int ipo=sqrt((PnewposX + X) * (PnewposX + X) + (PnewposY + Y) * (PnewposY + Y)); //trovo l'ipotenusa
		int cos = (PnewposX + X) / ipo;
		int sin = (sqrt(1 - (cos * cos)));

			

		if (((sin)*ipo) - Y != newposSX + 1 || ((sin)*ipo) - Y != newposSX - 1) {
			(((sin)*ipo) - Y > newposSX) ? ++newposSX : --newposSX;
			(((sin)*ipo) - Y > newposSX) ? newposSX++ : newposSX--;

		}


		if (newposSX > N / 2 && sin <0) { //non va
			cos = sin; //proprietà triangoli

			newposSX = ((sin)*ipo) - Y; //collisione lato orizzontale, è necessario che se ne accorga
		}
		if (newposSX > N / 2 && sin > 0) {
			newposSX = ((sin)*ipo) - Y; //collisione lato orizzontale, è necessario che se ne accorga
		}

}

ostream& operator<<(ostream& os, Gioco70& gioco) {

	char stringa[N * N + 1];

	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++)
			stringa[i * N + j] = gioco.p[i][j];
		stringa[i * N] = '\n';
	}
	stringa[N * N] = '\0';

	system("cls");
	printf("%s", stringa);
	cout << endl;
	cout << gioco.newposSX << endl;
	return os;
}