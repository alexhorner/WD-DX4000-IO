using System;
using System.Threading;

namespace WD_DX4000_IO.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IoController superIoData = new IoController(IoController.SuperIoDataOffset);
            IoController superIoPointer = new IoController(IoController.SuperIoPointerOffset);
            
            superIoPointer.Poke(0x00, 0x87);
            superIoPointer.Poke(0x00, 0x87);
            
            superIoPointer.Poke(0x00, 0x07);
            superIoData.Poke(0x00, 0x07);
            
            superIoPointer.Poke(0x00, 0xE9);
            
            while (true)
            {
                try
                {
                    Console.WriteLine($"Button State: {superIoData.Peek(0x00)}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Button State Read Failed: {e.Message}");
                }
            }
        }
    }
}