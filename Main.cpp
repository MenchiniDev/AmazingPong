#include "Gioco70.h"
#include<conio.h>
#include <Windows.h>


/*funzionamento*/
/*inserisci il carattere di partenza della pallina, poi viene la magia*/


int main()  {

	Gioco70 g1;

	cout << g1 << endl;

	char direct;
	cin >> direct;

	while (true) {
		Sleep(10);
		g1.muovi(direct,g1);
		cout << g1;
	}
}