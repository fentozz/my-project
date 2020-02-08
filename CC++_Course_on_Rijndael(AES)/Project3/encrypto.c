#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <malloc.h>
#include <stdlib.h>
#include <memory.h>

#define _CRT_SECURE_NO_WARNINGS


int getSBoxValue(int num)
{
	int sbox[256] = {
		//0     1    2      3     4    5     6     7      8    9     A      B    C     D     E     F
		0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, //0
		0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, //1
		0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, //2
		0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, //3
		0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, //4
		0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, //5
		0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, //6
		0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, //7
		0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, //8
		0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, //9
		0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, //A
		0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, //B
		0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, //C
		0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, //D
		0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, //E
		0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 }; //F
	return sbox[num];
}

// Раундовый массив констант, содержит значения заданные Х к степени (i-1), являющейся степенью Х
//  конечное поле GF (2 в 8) 
int Rcon[255] = {
	0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a,
	0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39,
	0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a,
	0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8,
	0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef,
	0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc,
	0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b,
	0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3,
	0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94,
	0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
	0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35,
	0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd, 0x61, 0xc2, 0x9f,
	0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb, 0x8d, 0x01, 0x02, 0x04,
	0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc, 0x63,
	0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39, 0x72, 0xe4, 0xd3, 0xbd,
	0x61, 0xc2, 0x9f, 0x25, 0x4a, 0x94, 0x33, 0x66, 0xcc, 0x83, 0x1d, 0x3a, 0x74, 0xe8, 0xcb };

//количество столбцов содержащих состояние AES. Содержится в константе 
#define Nb 4
// Количество раундов в Шифровании. 
int Nr = 0;
// Количество 32 битных слов в ключе. 
int Nk = 0;
//вход, выход и массив который содержит промежуточные результаты шифрования
unsigned char in[16], out[16], state[4][4];
// Массив ххранения раундовых ключей
unsigned char RoundKey[240];
// Ключ шифрования 
unsigned char Key[32];

// Это функция создает Nb(Nr+1) раундовых ключей. 
// Раундовые ключи используются в каждом раунде для шифрования состояний.
void KeyExpansion()
{
	int i, j;
	unsigned char temp[4], k;

	// Ключ первого раунда это сам ключ
	for (i = 0; i < Nk; i++)
	{
		RoundKey[i * 4] = Key[i * 4];
		RoundKey[i * 4 + 1] = Key[i * 4 + 1];
		RoundKey[i * 4 + 2] = Key[i * 4 + 2];
		RoundKey[i * 4 + 3] = Key[i * 4 + 3];
	}
	
	// Все остальные ключи создаются из предидуших ключей
	while (i < (Nb * (Nr + 1)))
	{
		for (j = 0; j < 4; j++)
		{
			temp[j] = RoundKey[(i - 1) * 4 + j];
		}
		if (i % Nk == 0)
		{
			// Эта функция поворачивает 4 байта в слове влево 
			// [a0,a1,a2,a3] становится [a1,a2,a3,a0]
			{
				k = temp[0];
				temp[0] = temp[1];
				temp[1] = temp[2];
				temp[2] = temp[3];
				temp[3] = k;
			}

			// Функция которая принимает четырехбайтовое входное слово и применяет S блок к каждому
			// из четырех байтов, чтобы получить выходное слово 
			{
				temp[0] = getSBoxValue(temp[0]);
				temp[1] = getSBoxValue(temp[1]);
				temp[2] = getSBoxValue(temp[2]);
				temp[3] = getSBoxValue(temp[3]);
			}

			temp[0] = temp[0] ^ Rcon[i / Nk];
		}
		else if (Nk > 6 && i % Nk == 4)
		{
			{
				temp[0] = getSBoxValue(temp[0]);
				temp[1] = getSBoxValue(temp[1]);
				temp[2] = getSBoxValue(temp[2]);
				temp[3] = getSBoxValue(temp[3]);
			}
		}
		RoundKey[i * 4 + 0] = RoundKey[(i - Nk) * 4 + 0] ^ temp[0];
		RoundKey[i * 4 + 1] = RoundKey[(i - Nk) * 4 + 1] ^ temp[1];
		RoundKey[i * 4 + 2] = RoundKey[(i - Nk) * 4 + 2] ^ temp[2];
		RoundKey[i * 4 + 3] = RoundKey[(i - Nk) * 4 + 3] ^ temp[3];
		i++;
	}
}

// Функция добавляет раундовый ключ в состояние 
// Функция добавляется в состояние функцией XOR 
void AddRoundKey(int round)
{
	int i, j;
	for (i = 0; i < 4; i++)
	{
		for (j = 0; j < 4; j++)
		{
			state[j][i] ^= RoundKey[round * Nb * 4 + i * Nb + j];
		}
	}
}

// Функция подставляет значения в матрицу значения из S блока
void SubBytes()
{
	int i, j;
	for (i = 0; i < 4; i++)
	{
		for (j = 0; j < 4; j++)
		{
			state[i][j] = getSBoxValue(state[i][j]);

		}
	}
}

// Функция сдвигает ряды состояний влево
// Каждый ряд сдвигается с разным смещением 
// смещение = номеру строки. первая строка не смещается
void ShiftRows()
{
	unsigned char temp;

	// Поворачивает первый ряд на 1 столбец влево
	temp = state[1][0];
	state[1][0] = state[1][1];
	state[1][1] = state[1][2];
	state[1][2] = state[1][3];
	state[1][3] = temp;

	// Поворачивает второй ряд на 2 столбца влево
	temp = state[2][0];
	state[2][0] = state[2][2];
	state[2][2] = temp;

	temp = state[2][1];
	state[2][1] = state[2][3];
	state[2][3] = temp;

	//поворачивает третий ряд на 3 столбца влево
	temp = state[3][0];
	state[3][0] = state[3][3];
	state[3][3] = state[3][2];
	state[3][2] = state[3][1];
	state[3][1] = temp;
}

// макрос нахождения произведения 
#define xtime(x)   ((x<<1) ^ (((x>>7) & 1) * 0x1b))

// Смешивает столбцы матрицы состояний
void MixColumns()
{
	int i;
	unsigned char Tmp, Tm, t;
	for (i = 0; i < 4; i++)
	{
		t = state[0][i];
		Tmp = state[0][i] ^ state[1][i] ^ state[2][i] ^ state[3][i];
		Tm = state[0][i] ^ state[1][i]; Tm = xtime(Tm); state[0][i] ^= Tm ^ Tmp;
		Tm = state[1][i] ^ state[2][i]; Tm = xtime(Tm); state[1][i] ^= Tm ^ Tmp;
		Tm = state[2][i] ^ state[3][i]; Tm = xtime(Tm); state[2][i] ^= Tm ^ Tmp;
		Tm = state[3][i] ^ t; Tm = xtime(Tm); state[3][i] ^= Tm ^ Tmp;
	}
}

// Основная функция которая шифрует текст (байты)
void Cipher()
{
	int i, j, round = 0;

	//копирует входной текст в массив состояний
	for (i = 0; i < 4; i++)
	{
		for (j = 0; j < 4; j++)
		{
			state[j][i] = in[i * 4 + j];
		}
	}

	// Добавляет первый ключ в состояния перед началов раундов
	AddRoundKey(0);

	// Nr - количество раундов
	// Первые раунды - идентичны (до Nr -1 )
	for (round = 1; round < Nr; round++)
	{
		SubBytes();
		ShiftRows();
		MixColumns();
		AddRoundKey(round);
	}

	// Последний раунд Nr
	// В последнем раунде не перемешиваются столбцы
	SubBytes();
	ShiftRows();
	AddRoundKey(Nr);

	// Шифрование окончено 
	// Копируем массив на выход 
	for (i = 0; i < 4; i++)
	{
		for (j = 0; j < 4; j++)
		{
			out[i * 4 + j] = state[j][i];
		}
	}
}

//Точка входа при шифровании 
int core_encrypt(wchar_t CHarr[],int byte_col, char key_c[]) 
{
	//количество двойных слэшей
	int sp = 0;
	//длинна 
	int len = 0;
	
	for (int i = 0; CHarr[i] != '\0'; i++) 
	{
		len++;
		if (CHarr[i] == '\\') sp++;
	}

	//строка пути хранения исходного файла
	char* str;
	str = (char*)malloc(len-sp);

	for (int i = 0; ; i++)
	{
		if (CHarr[i] == '\\') str[i] = '/';
		else if (CHarr[i] == '\0') 
		{
			str[i] = '\0';
			break;
		}
		else str[i] = CHarr[i];
	}
	//получили правильную строку

	errno_t err;
	FILE *mf;//исходный файл
	err = fopen_s(&mf, str, "rb");
	if (err != 0) return 1;

	//Зашифрованый файл , конечный
	char* str_save;
	str_save = (char*)malloc(sizeof(char)*(strlen(str) + 6));
	for (int i = 0; ; i++)
	{
		if (str[i] == '\0')
		{
			str_save[i] = '.';
			str_save[i + 1] = 'c';
			str_save[i + 2] = 'r';
			str_save[i + 3] = 'y';
			str_save[i + 4] = 'p';
			str_save[i + 5] = 't';
			str_save[i + 6] = '\0';
			break;
		}
		else str_save[i] = str[i];
	}
	FILE *crypt;//конечный файл
	errno_t err_cr;
	err_cr = fopen_s(&crypt, str_save, "wb+");	
	if (err_cr != 0) return 1;

	//значение байта
	 int i_f_c;
	 // массив с байтами файла 
	 int *ar_f_s;
	 //номер байта в блоке 
	 unsigned int num = 0;

	 Nr = byte_col;//128;  /*192;*/  //, 256 Длинна ключа в битах 
	 Nk = Nr / 32;
	 Nr = Nk + 6;

	 //массив для ключа
	 unsigned char temp[32] = { 0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  
							   ,0x00  ,0x00  ,0x0a  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00 
							   ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  
		                       ,0x00  ,0x00  ,0x0a  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00 };

	 for (int i = 0; i < strlen(key_c); i++) //считываем ключ переданный в программу
	 {
		 temp[i] = key_c[i];
	 }

	 for (int i = 0; i < Nk * 4; i++) //записываем ключ в зависимости от количества раундов
	 {
		 Key[i] = temp[i];
	 }

	 //текст на шифр
	 unsigned char temp2[16] = { 0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00 
		                        ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00  ,0x00 };

	 rewind(mf);//переходим в начало файла
	 while ((i_f_c = getc(mf)) != EOF) //читаем по не дойдём до конца
	 {
		 temp2[num] = i_f_c; 
		 num++;
		 if (num == 16) //если считали 16 байт
		 {
			 for (int i = 0; i < 16; i++) //записываем их в массив для шифрования
			 {
				 in[i] = temp2[i];
			 }

			 //шифруем считанный блок
			 KeyExpansion();
			 Cipher();

			 for (int i = 0; i < 16; i++) 
			 {
				 putc(out[i], crypt);//записываем зашифрованный блок в файл
			 }
			 num = 0;//заполняем этот блок заного
		 }
	 }

	 int n_s = 0;
	 if (num != 0) //если последний блок не был заполнен до конца
	 {
		 for (int i = num; i < 16; i++) //заполняем его доконца (чем угодно)
		 {
			 n_s++;
			 temp2[i] = 0x99;
		 }

		 for (int i = 0; i < 16; i++)//передаем в массив для зашифровки
		 {
			 in[i] = temp2[i];
		 }
		 //шифруем законченный блок
		 KeyExpansion();
		 Cipher();
		 
		 for (int i = 0; i < 16; i++)//записываем последний блок
		 {
			 putc(out[i]/*in[i]*/, crypt);
		 }
	 }
	 char end_f = (n_s); //+ '0';
	 putc( end_f, crypt);//вконец файла записываем сколько лишних символов мы добавили 

	 fclose(mf);//исходный файл
	 fclose(crypt);//конечный файл

	 if (err != 0)
		 return 1;
	 else
		 return 0;
}
