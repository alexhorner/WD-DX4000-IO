using System;
using System.IO;

namespace WD_DX4000_IO
{
    public class IoController
    {
        public static readonly ushort PchOffset = 0x480;
        public static readonly ushort SuperIoPointerOffset = 0x4E;
        public static readonly ushort SuperIoDataOffset = 0x4F;
        
        private readonly ushort _offset;
        private readonly string _ioDevice;
        private FileStream _device = null;

        public IoController(ushort offset, string ioDevice = "/dev/port")
        {
            _offset =  offset;
            _ioDevice = ioDevice;
        }
        
        public IoController(byte offset, string ioDevice = "/dev/port") : this(Convert.ToUInt16(offset), ioDevice) { }

        public byte Peek(ushort address)
        {
            OpenDevice();
            
            _device.Seek(_offset + address, SeekOrigin.Begin);
            int byteAsInt = _device.ReadByte();

            CloseDevice();
            
            return Convert.ToByte(byteAsInt);
        }
        
        public byte Peek(byte address)
        {
            return Peek(Convert.ToUInt16(address));
        }

        public void Poke(ushort address, byte value)
        {
            OpenDevice();
            
            _device.Seek(_offset + address, SeekOrigin.Begin);
            _device.WriteByte(value);
            _device.Flush();
            
            CloseDevice();
        }
        
        public void Poke(byte address, byte value)
        {
            Poke(Convert.ToUInt16(address), value);
        }

        private void OpenDevice()
        {
            if (_device != null) throw new InvalidOperationException("Device in use");
            _device = File.Open(_ioDevice, FileMode.Open);
        }

        private void CloseDevice()
        {
            if (_device == null) throw new InvalidOperationException("Device not available");
            
            _device.Close();
            _device.Dispose();
            _device = null;
        }
    }
}