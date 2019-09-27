#include <Windows.h>
#include <CommCtrl.h>
#include <Shellapi.h>

#include "decrypto.c"
#include "encrypto.c"

//using namespace rijndael;

#define width 475;
#define height 120;

HWND Edit_key;
POINT point;
HDC parentDC;
RECT rect;

int button_1 = 10; //индекс события нажатия на кнопку или кнопки
int button_2 = 20;
int IDC_EDIT0 = 30;
int IDC_EDIT1 = 31;
int IDC_EDIT2 = 32;
int E_key = 40;

char rad_b = 0;

LRESULT CALLBACK WindowProc(_In_ HWND hwnd,_In_ UINT uMsg,_In_ WPARAM wParam,_In_ LPARAM lParam);

int CALLBACK WinMain(_In_ HINSTANCE hInstance,_In_ HINSTANCE hPrevInstance,_In_ LPSTR lpCmdLine,_In_ int nCmdShow) 
{
	INT nArgs = 0;
	LPWSTR lpCommandLine = GetCommandLineW();
	LPWSTR* lpArgs = CommandLineToArgvW(lpCommandLine, &nArgs);

	if (nArgs > 1) 
	{ 
		wchar_t* ars = lpArgs[1];
		TCHAR szFileName[MAX_PATH];
		int i = 0;
		for (i = 0; ars[i] != '\0'; i++)
			szFileName[i] = ars[i];
		szFileName[i] = '\0';

		if (szFileName[strlen(szFileName) - 1] == 't' &&
			szFileName[strlen(szFileName) - 2] == 'p' &&
			szFileName[strlen(szFileName) - 3] == 'y' &&
			szFileName[strlen(szFileName) - 4] == 'r' &&
			szFileName[strlen(szFileName) - 5] == 'c' &&
			szFileName[strlen(szFileName) - 6] == '.')
		{
			int b_c = 128;
			char str_str[17];
			GetWindowText(Edit_key, str_str, 17);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_decrypt(f_l, b_c, str_str);
		}
		else
		{
			int b_c = 128;
			char str_str[17];
			GetWindowText(Edit_key, str_str, 17);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_encrypt(f_l, b_c, str_str);
		}

		LocalFree(lpArgs);
		return 0; 
	}
	LocalFree(lpArgs);	

	WNDCLASSEX windowClass;
	HWND hWnd;
	MSG uMsg;

	memset(&windowClass, 0, sizeof(WNDCLASSEX));
	windowClass.cbSize = sizeof(WNDCLASSEX);
	windowClass.hbrBackground = (HBRUSH)GetStockObject(BLACK_BRUSH);
	windowClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	windowClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	windowClass.hInstance = hInstance;
	windowClass.lpfnWndProc = WindowProc;
	windowClass.lpszClassName = "Simple window";

	int wid = width;
	int heig = height;

	RegisterClassEx(&windowClass);

	hWnd = CreateWindow(windowClass.lpszClassName, "crypto AES Rijndael",/* WS_OVERLAPPED |*/ /*WS_CAPTION |*/ WS_SYSMENU ,
		(GetSystemMetrics(SM_CXSCREEN) - wid) / 2, (GetSystemMetrics(SM_CYSCREEN) - heig) / 2, wid, heig, NULL, NULL, NULL, NULL);

	DragAcceptFiles(hWnd, TRUE);

	ShowWindow(hWnd, nCmdShow);

	while (GetMessage(&uMsg,hWnd,NULL,NULL))
	{
		TranslateMessage(&uMsg);
		DispatchMessage(&uMsg);
	}

	DragAcceptFiles(hWnd, FALSE);

	return uMsg.wParam;
}

void OnDropFiles(HWND hWnd, HDROP hDrop)
{
	TCHAR szFileName[MAX_PATH];
	DWORD dwCount = DragQueryFile(hDrop, 0xFFFFFFFF, szFileName, MAX_PATH);
	for (int i = 0; i < dwCount; i++)
	{
		DragQueryFile(hDrop, i, szFileName, MAX_PATH);
		break;
	}
	if (szFileName[strlen(szFileName) - 1] == 't' &&
		szFileName[strlen(szFileName) - 2] == 'p' &&
		szFileName[strlen(szFileName) - 3] == 'y' &&
		szFileName[strlen(szFileName) - 4] == 'r' &&
		szFileName[strlen(szFileName) - 5] == 'c' &&
		szFileName[strlen(szFileName) - 6] == '.')
	{
		if (rad_b == 0)
		{
			int b_c = 128;
			char str_str[17];
			GetWindowText(Edit_key, str_str, 17);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_decrypt(f_l, b_c, str_str);
		}
		else if (rad_b == 1)
		{
			int b_c = 192;
			char str_str[25];
			GetWindowText(Edit_key, str_str, 25);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_decrypt(f_l, b_c, str_str);
		}
		else if (rad_b == 2)
		{
			int b_c = 256;
			char str_str[33];
			GetWindowText(Edit_key, str_str, 33);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_decrypt(f_l, b_c, str_str);
		}
	}
	else
	{
		if (rad_b == 0)
		{
			int b_c = 128;
			char str_str[17];
			GetWindowText(Edit_key, str_str, 17);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_encrypt(f_l, b_c, str_str);
		}
		else if (rad_b == 1)
		{
			int b_c = 192;
			char str_str[25];
			GetWindowText(Edit_key, str_str, 25);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_encrypt(f_l, b_c, str_str);
		}
		else if (rad_b == 2)
		{
			int b_c = 256;
			char str_str[33];
			GetWindowText(Edit_key, str_str, 33);
			SetWindowText(Edit_key, str_str);
			wchar_t f_l[MAX_PATH];
			for (int i = 0; i < strlen(szFileName); i++)
				f_l[i] = szFileName[i];
			f_l[strlen(szFileName)] = '\0';
			core_encrypt(f_l, b_c, str_str);
		}
	}


	DragFinish(hDrop);
}


LRESULT CALLBACK WindowProc(_In_ HWND hwnd,_In_ UINT uMsg,_In_ WPARAM wParam,_In_ LPARAM lParam) 
{
	switch (uMsg)
	{
	case WM_DROPFILES:
		OnDropFiles(hwnd, (HDROP) wParam);
		break;
	case WM_CREATE:
		Edit_key = CreateWindowEx(
			0,
			"EDIT",
			TEXT("key"),
			WS_CHILD | WS_VISIBLE | WS_TABSTOP | ES_MULTILINE,
			20, //window position by X
			10, //window position by y
			420, //WIDTH
			20, //HEIGHT
			hwnd,
			(HMENU)E_key,
			NULL,
			NULL
		);

		CreateWindowEx(
			0,
			"BUTTON",
			"Шифрование",
			BS_PUSHBUTTON | WS_VISIBLE | WS_CHILD,
			20, //window position by X
			40, //window position by y
			125, //WIDTH
			25, //HEIGHT
			hwnd,
			(HMENU)button_1,
			NULL,
			NULL
		);

		CreateWindowEx(
			0,
			"BUTTON",
			"Дешифрование",
			BS_PUSHBUTTON | WS_VISIBLE | WS_CHILD,
			155, //window position by X
			40, //window position by y
			125, //WIDTH
			25, //HEIGHT
			hwnd,
			(HMENU)button_2,
			NULL,
			NULL
		);

		CreateWindowEx(
			0,
			"BUTTON",
			"128",
			WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
			290, //window position by X
			40, //window position by y
			50, //WIDTH
			20, //HEIGHT
			hwnd,
			(HMENU)IDC_EDIT0,
			NULL,
			NULL
		);

		CreateWindowEx(
			0,
			"BUTTON",
			"192",
			WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
			340, //window position by X
			40, //window position by y
			50, //WIDTH
			20, //HEIGHT
			hwnd,
			(HMENU)IDC_EDIT1,
			NULL,
			NULL
		);

		CreateWindowEx(
			0,
			"BUTTON",
			"256",
			WS_CHILD | WS_VISIBLE | BS_AUTORADIOBUTTON,
			390, //window position by X
			40, //window position by y
			50, //WIDTH
			20, //HEIGHT
			hwnd,
			(HMENU)IDC_EDIT2,
			NULL,
			NULL
		);

		CheckRadioButton(hwnd, IDC_EDIT0, IDC_EDIT2, IDC_EDIT0);

		break;
	case WM_CTLCOLORSTATIC:
		SetTextColor((HDC)wParam, RGB(0, 255, 0)); // Set red color for text of the button
		SetBkMode((HDC)wParam, TRANSPARENT);

	case WM_CTLCOLORBTN:
		GetWindowRect((HWND)lParam, &rect);
		point.x = rect.left;
		point.y = rect.top;
		ScreenToClient(hwnd, &point);
		parentDC = GetDC(hwnd);

		BitBlt((HDC)wParam, 0, 0, rect.right - rect.left, rect.bottom - rect.top, parentDC, point.x, point.y, SRCCOPY);

		ReleaseDC(hwnd, parentDC);

		return (LRESULT)GetStockObject(NULL_BRUSH);
		break;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case 10://encrypto
			wchar_t fn0[1024];
			OPENFILENAMEW ofn0;

			fn0[0] = L'\0';
			ofn0.lStructSize = sizeof(OPENFILENAMEW);
			ofn0.hwndOwner = hwnd;
			ofn0.lpstrFilter = L"Все файлы\0*.*\0\0";
			ofn0.lpstrCustomFilter = NULL;
			ofn0.nFilterIndex = 1;
			ofn0.lpstrFile = fn0;
			ofn0.nMaxFile = 1024;
			ofn0.lpstrFileTitle = NULL;
			ofn0.lpstrInitialDir = NULL;
			ofn0.lpstrTitle = NULL;
			ofn0.Flags = OFN_EXPLORER;
			ofn0.lpstrDefExt = NULL;
			ofn0.FlagsEx = 0;
			if (GetOpenFileNameW(&ofn0))
			{

			}
			if (rad_b == 0)
			{
				int b_c = 128;
				char str_str[17];
				GetWindowText(Edit_key, str_str, 17);
				SetWindowText(Edit_key, str_str);
				core_encrypt(fn0, b_c, str_str);
			}
			else if (rad_b == 1)
			{
				int b_c = 192;
				char str_str[25];
				GetWindowText(Edit_key, str_str, 25);
				SetWindowText(Edit_key, str_str);
				core_encrypt(fn0, b_c, str_str);
			}
			else if (rad_b == 2)
			{
				int b_c = 256;
				char str_str[33];
				GetWindowText(Edit_key, str_str, 33);
				SetWindowText(Edit_key, str_str);
				core_encrypt(fn0, b_c, str_str);
			}
			break;
		case 20://decrypto
			wchar_t fn1[1024];
			OPENFILENAMEW ofn1;

			fn1[0] = L'\0';
			ofn1.lStructSize = sizeof(OPENFILENAMEW);
			ofn1.hwndOwner = hwnd;
			ofn1.lpstrFilter = L"crypt\0*.crypt*\0\0";
			ofn1.lpstrCustomFilter = NULL;
			ofn1.nFilterIndex = 1;
			ofn1.lpstrFile = fn1;
			ofn1.nMaxFile = 1024;
			ofn1.lpstrFileTitle = NULL;
			ofn1.lpstrInitialDir = NULL;
			ofn1.lpstrTitle = NULL;
			ofn1.Flags = OFN_EXPLORER;
			ofn1.lpstrDefExt = NULL;
			ofn1.FlagsEx = 0;
			if (GetOpenFileNameW(&ofn1))
			{

			}
			if (rad_b == 0)
			{
				int b_c = 128;
				char str_str[17];
				GetWindowText(Edit_key, str_str, 17);
				SetWindowText(Edit_key, str_str);
				core_decrypt(fn1, b_c, str_str);
			}
			else if (rad_b == 1)
			{
				int b_c = 192;
				char str_str[25];
				GetWindowText(Edit_key, str_str, 25);
				SetWindowText(Edit_key, str_str);
				core_decrypt(fn1, b_c, str_str);
			}
			else if (rad_b == 2)
			{
				int b_c = 256;
				char str_str[33];
				GetWindowText(Edit_key, str_str, 33);
				SetWindowText(Edit_key, str_str);
				core_decrypt(fn1, b_c, str_str);
			}
			break;
		case 30:
			rad_b = 0;
			char str_str0[17];
			GetWindowText(Edit_key, str_str0, 17);
			SetWindowText(Edit_key, str_str0);
			break;
		case 31:
			rad_b = 1;
			char str_str1[25];
			GetWindowText(Edit_key, str_str1, 25);
			SetWindowText(Edit_key, str_str1);
			break;
		case 32:
			rad_b = 2;
			char str_str2[33];
			GetWindowText(Edit_key, str_str2, 33);
			SetWindowText(Edit_key, str_str2);
			break;
		case 40:
			if (rad_b == 0) 
			{
				char str_str[17];
				GetWindowText(Edit_key, str_str, 17);
				SetWindowText(Edit_key, str_str);
			}
			else if (rad_b == 1)
			{
				char str_str[25];
				GetWindowText(Edit_key, str_str, 25);
				SetWindowText(Edit_key, str_str);
			}
			else if (rad_b == 2) 
			{
				char str_str[33];
				GetWindowText(Edit_key, str_str, 33);
				SetWindowText(Edit_key, str_str);
			}
			break;
		default:
			break;
		}
		break;
	case WM_ACTIVATE:
		break;
	case WM_CLOSE:
		ExitProcess(0);
		break;
	case WM_NCCREATE:
		break;
	default:
		break;
	}
	return DefWindowProc(hwnd, uMsg, wParam, lParam);

}

