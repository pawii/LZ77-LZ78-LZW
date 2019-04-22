using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WPF
{
    public static class Converters
    {
        public static string ToSingleString(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var enc = GetEncoding(value);
            switch (enc)
            {
                case ASCIIEncoding AsciiEncoding:
                    return Encoding.ASCII.GetString(value);
                case UnicodeEncoding UnicodeEncoding:
                    return Encoding.Unicode.GetString(value);
                case UTF32Encoding Utf32Encoding:
                    return Encoding.UTF32.GetString(value);
                case UTF7Encoding Utf7Encoding:
                    return Encoding.UTF7.GetString(value);
                case UTF8Encoding Utf8Encoding:
                    return Encoding.UTF8.GetString(value);
                default:
                    return Encoding.ASCII.GetString(value);
            }
        }
        private static Encoding GetEncoding(byte[] Data)
        {
            if (Data == null)
                throw new Exception("Array cannot be null.");
            if (Data.Length < 2)
                return Encoding.Default;
            if (Data[0] == 0xff && Data[1] == 0xfe)
                return Encoding.Unicode;
            if (Data[0] == 0xfe && Data[1] == 0xff)
                return Encoding.BigEndianUnicode;
            if (Data.Length < 3)
                return Encoding.Default;
            if (Data[0] == 0xef && Data[1] == 0xbb && Data[2] == 0xbf)
                return Encoding.UTF8;
            if (Data[0] == 0x2b && Data[1] == 0x2f && Data[2] == 0x76)
                return Encoding.UTF7;
            if (Data.Length < 4)
                return Encoding.Default;
            if (Data[0] == 0xff && Data[1] == 0xfe && Data[2] == 0 && Data[3] == 0)
                return Encoding.UTF32;
            return Encoding.Default;
        }

        public static bool IsValidPrimitive(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Single:
                case TypeCode.Object:
                case TypeCode.DateTime:
                case TypeCode.String:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        ///     Get Bytes from a single boolean object
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this bool value)
        {
            return new[] { value ? (byte)1 : (byte)0 };
        }
        /// <summary>
        ///     Get Bytes from a single byte object
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this byte value)
        {
            return new[] { value };
        }
        /// <summary>
        ///     Get Bytes from an array of sbyte objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this sbyte[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (sbyte[]) object cannot be null.");
            var numArray = new byte[value.Length];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of short objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this short[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (short[]) object cannot be null.");
            var numArray = new byte[value.Length * 2];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of unsigned short objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this ushort[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (ushort[]) object cannot be null.");
            var numArray = new byte[value.Length * 2];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of character objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this char[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (char[]) object cannot be null.");
            var numArray = new byte[value.Length * 2];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of integer objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this int[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (int[]) object cannot be null.");
            var numArray = new byte[value.Length * 4];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        public static byte[] GetBytes(this uint value, int sIndex = 0, int count = 4)
        {
            if (count > 4)
                throw new Exception("Size cannot exceed 4 bytes.");
            return value.GetBytes().SubArray(sIndex, count);
        }
        /// <summary>
        ///     Get Bytes from an array of unsigned integer objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this uint[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (uint[]) object cannot be null.");
            var numArray = new byte[value.Length * 4];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from a single long object
        ///     (Converters.cs)
        /// </summary>
        public static unsafe byte[] GetBytes(this long value)
        {
            var numArray = new byte[8];
            fixed (byte* ptr = numArray)
            {
                *(long*)ptr = value;
            }
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of long objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this long[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (long[]) object cannot be null.");
            var numArray = new byte[value.Length * 8];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of long objects with index and count
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this long value, int sIndex = 0, int count = 8)
        {
            if (count > 8)
                throw new Exception("Size cannot exceed 8 bytes.");
            return value.GetBytes().SubArray(sIndex, count);
        }
        /// <summary>
        ///     Get Bytes from a single unsigned long object
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this ulong value)
        {
            return ((long)value).GetBytes();
        }
        public static byte[] GetBytes(this ulong value, int sIndex = 0, int count = 8)
        {
            if (count > 8)
                throw new Exception("Size cannot exceed 8 bytes.");
            return ((long)value).GetBytes().SubArray(sIndex, count);
        }
        /// <summary>
        ///     Get Bytes from an array of unsigned long objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this ulong[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (ulong[]) object cannot be null.");
            var numArray = new byte[value.Length * 8];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of float objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this float[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (float[]) object cannot be null.");
            var numArray = new byte[value.Length * 4];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from a single double object
        ///     (Converters.cs)
        /// </summary>
        public static unsafe byte[] GetBytes(this double value)
        {
            return (*(long*)&value).GetBytes();
        }
        /// <summary>
        ///     Get Bytes from an array of double objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this double[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (double[]) object cannot be null.");
            var numArray = new byte[value.Length * 8];
            Buffer.BlockCopy(value, 0, numArray, 0, numArray.Length);
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from a single decimal object
        ///     (Converters.cs)
        /// </summary>
        public static unsafe byte[] GetBytes(this decimal value)
        {
            var array = new byte[16];
            fixed (byte* bp = array)
            {
                *(decimal*)bp = value;
            }
            return array;
        }
        /// <summary>
        ///     Get Bytes from a single DateTime object
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this DateTime value)
        {
            return value.Ticks.GetBytes();
        }
        public static byte[] GetBytes(this DateTime[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (DateTime[]) object cannot be null.");
            var sodt = 0;
            unsafe
            {
                sodt = sizeof(DateTime);
            }
            var numArray = new byte[value.Length * sodt];
            for (var i = 0; i < value.Length; i++)
            {
                var dba = value[i].GetBytes();
                Buffer.BlockCopy(dba, 0, numArray, i * sodt, sodt);
            }
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from an array of decimal objects
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this decimal[] value)
        {
            if (value == null)
                throw new Exception("GetBytes (decimal[]) object cannot be null.");
            var numArray = new byte[value.Length * 16];
            for (var i = 0; i < value.Length; i++)
            {
                var dba = value[i].GetBytes();
                Buffer.BlockCopy(dba, 0, numArray, i * 16, 16);
            }
            return numArray;
        }
        /// <summary>
        ///     Get Bytes from a single string object using a specified Encoding.
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this string value, Encoding enc = null)
        {
            if (value == null)
                throw new Exception("GetBytes (string) object cannot be null.");
            if (enc == null)
                return Encoding.ASCII.GetBytes(value);
            switch (enc)
            {
                case ASCIIEncoding AsciiEncoding:
                    {
                        return Encoding.ASCII.GetBytes(value);
                    }
                case UnicodeEncoding UnicodeEncoding:
                    {
                        var ba = Encoding.Unicode.GetBytes(value);
                        var pre = new byte[] { 0xff, 0xfe };
                        var ra = new byte[ba.Length + 2];
                        Array.Copy(pre, 0, ra, 0, 2);
                        Array.Copy(ba, 0, ra, 2, ba.Length);
                        return ra;
                    }
                case UTF32Encoding Utf32Encoding:
                    {
                        var ba = Encoding.UTF32.GetBytes(value);
                        var pre = new byte[] { 0xff, 0xfe, 0, 0 };
                        var ra = new byte[ba.Length + 4];
                        Array.Copy(pre, 0, ra, 0, 4);
                        Array.Copy(ba, 0, ra, 4, ba.Length);
                        return ra;
                    }
                case UTF7Encoding Utf7Encoding:
                    {
                        var ba = Encoding.UTF7.GetBytes(value);
                        var pre = new byte[] { 0x2b, 0x2f, 0x76 };
                        var ra = new byte[ba.Length + 3];
                        Array.Copy(pre, 0, ra, 0, 3);
                        Array.Copy(ba, 0, ra, 3, ba.Length);
                        return ra;
                    }
                case UTF8Encoding Utf8Encoding:
                    {
                        var ba = Encoding.UTF8.GetBytes(value);
                        var pre = new byte[] { 0xef, 0xbb, 0xbf };
                        var ra = new byte[ba.Length + 3];
                        Array.Copy(pre, 0, ra, 0, 3);
                        Array.Copy(ba, 0, ra, 3, ba.Length);
                        return ra;
                    }
                default:
                    return Encoding.ASCII.GetBytes(value);
            }
        }
        /// <summary>
        ///     Get Bytes from a array of string objects.
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytes(this string[] value, Encoding enc = null)
        {
            if (value == null)
                throw new Exception("GetBytes (string[]) object cannot be null.");
            var numArray = new byte[value.Where(ss => ss != null).Sum(ss => ss.Length)];
            var dstOffset = 0;
            foreach (var str in value)
                if (str != null)
                {
                    Buffer.BlockCopy(str.GetBytes(enc), 0, numArray, dstOffset, str.Length);
                    dstOffset += str.Length;
                }
            return numArray;
        }
        /// <summary>
        ///     Gets list of byte arrays from a list of objects of type T.
        ///     (Converters.cs)
        /// </summary>
        public static List<byte[]> GetBytesObject<T>(this List<T> value)
        {
            return value.Select(o => o.GetBytesObjectSerial()).ToList();
        }
        /// <summary>
        ///     Get Bytes from a single object with an optional object type specified.
        ///     (Converters.cs)
        /// </summary>
        public static byte[] GetBytesObjectSerial(this object value)
        {
            var type = value.GetType();
            if (!type.IsSerializable)
                throw new Exception("Error: Object is not Serializable.");
            using (var _fs = new MemoryStream())
            {
                var _formatter = new BinaryFormatter();
                _formatter.Serialize(_fs, value);
                return _fs.ToArray().SubArray(0, (int)_fs.Length);
            }
        }
        /// <summary>
        ///     Gets a single object of type T from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static T ToObject<T>(this byte[] value)
        {
            if (value == null)
                throw new Exception("value cannot be null.");
            using (var stream = new MemoryStream(value))
            {
                var formatter = new BinaryFormatter();
                var result = (T)formatter.Deserialize(stream);
                return result;
            }
        }
        /// <summary>
        ///     Gets an array of objects of type T from a list of byte arrays.
        ///     (Converters.cs)
        /// </summary>
        public static T[] ToObject<T>(this List<byte[]> value)
        {
            if (value == null)
                throw new Exception("value cannot be null.");
            if (value.Count == 0)
                throw new Exception("value is empty.");
            var lst = new List<T>();
            foreach (var o in value)
                lst.Add(o.ToObject<T>());
            return lst.ToArray();
        }
        /// <summary>
        ///     Converts a byte array to a hex string.
        ///     (Converters.cs)
        /// </summary>
        public static string ToHexString(this byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        /// <summary>
        ///     Converts a byte array to a hex string.
        ///     (Converters.cs)
        /// </summary>
        public static string ToBinaryString(this byte[] bytes)
        {
            var len = bytes.Length;
            var sb = new StringBuilder();
            for (var i = 0; i < len; i++)
                sb.Append(Convert.ToString(bytes[i], 2).PadLeft(8, '0'));
            return sb.ToString();
        }
        /// <summary>
        ///     Converts a binary string to an unsigned long.
        ///     (Converters.cs)
        /// </summary>
        public static ulong FromBinaryStringTo(this string value)
        {
            var reversed = value.Reverse().ToArray();
            var num = 0ul;
            for (var p = 0; p < reversed.Count(); p++)
            {
                if (reversed[p] != '1')
                    continue;
                num += (ulong)Math.Pow(2, p);
            }
            return num;
        }
        /// <summary>
        ///     Returns a Boolean value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static bool ToBool(this byte[] value, int pos = 0)
        {
            return BitConverter.ToBoolean(value, pos);
        }
        /// <summary>
        ///     Returns a Character value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static char ToChar(this byte[] value, int pos = 0)
        {
            return BitConverter.ToChar(value, pos);
        }
        public static unsafe byte ToByte(this byte[] value, int pos = 0)
        {
            byte bv;
            fixed (byte* bp = value)
            {
                var bpp = bp + pos;
                bv = *bpp;
                return bv;
            }
        }
        public static unsafe sbyte ToSByte(this byte[] value, int pos = 0)
        {
            fixed (byte* bp = value)
            {
                var ptr = bp + pos;
                if (pos % 2 == 0)
                    return *(sbyte*)ptr;
                return (sbyte)*ptr;
            }
        }
        /// <summary>
        ///     Returns a Short value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static short ToShort(this byte[] value, int pos = 0)
        {
            return BitConverter.ToInt16(value, pos);
        }
        /// <summary>
        ///     Returns a Unsigned Short value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static ushort ToUShort(this byte[] value, int pos = 0)
        {
            return BitConverter.ToUInt16(value, pos);
        }
        /// <summary>
        ///     Returns a Integer value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static int ToInt(this byte[] value, int pos = 0)
        {
            return BitConverter.ToInt32(value, pos);
        }
        /// <summary>
        ///     Returns a Unsigned Integer value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static uint ToUInt(this byte[] value, int pos = 0)
        {
            return BitConverter.ToUInt32(value, pos);
        }
        /// <summary>
        ///     Returns a Long value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static long ToLong(this byte[] value, int pos = 0)
        {
            return BitConverter.ToInt64(value, pos);
        }
        /// <summary>
        ///     Returns a Unsigned Long value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static ulong ToULong(this byte[] value, int pos = 0)
        {
            return BitConverter.ToUInt64(value, pos);
        }
        /// <summary>
        ///     Returns a Float value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static float ToFloat(this byte[] value, int pos = 0)
        {
            return BitConverter.ToSingle(value, pos);
        }
        /// <summary>
        ///     Returns a Double value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static double ToDouble(this byte[] value, int pos = 0)
        {
            return BitConverter.ToDouble(value, pos);
        }
        /// <summary>
        ///     Returns a Decimal value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static unsafe decimal ToDecimal(this byte[] value, int pos = 0)
        {
            decimal dv;
            fixed (byte* bp = value)
            {
                var bpp = bp + pos;
                dv = *(decimal*)bpp;
            }
            return dv;
        }
        /// <summary>
        ///     Returns a String value converted from the byte at a specified position.
        ///     (Converters.cs)
        /// </summary>
        public static string ToString(this byte[] value, int pos = 0)
        {
            return BitConverter.ToString(value, pos);
        }
        /// <summary>
        ///     Returns a Boolean array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static bool[] ToBooleanArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new bool[value.Length];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Character array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static char[] ToCharArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new char[value.Length];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        public static byte[] ToByteArray(this byte[] value, int index = 0, int length = -1)
        {
            if (length == -1)
                length = value.Length - index;
            var ba = new byte[length];
            Buffer.BlockCopy(value, index, ba, 0, length);
            return ba;
        }
        /// <summary>
        ///     Returns a SByte array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static sbyte[] ToSByteArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new sbyte[value.Length];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Short array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static short[] ToShortArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new short[value.Length / 2];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Unsigned Short array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static ushort[] ToUShortArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new ushort[value.Length / 2];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Integer array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static int[] ToIntArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new int[value.Length / 4];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Unsigned Integer array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static uint[] ToUIntArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new uint[value.Length / 4];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Long array converted from the byte array.
        ///     (Converters.cs)
        /// </summary>
        public static long[] ToLongArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new long[value.Length / 8];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Unsigned Long array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static ulong[] ToULongArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            var arr = new ulong[value.Length / 8];
            Buffer.BlockCopy(value, 0, arr, 0, value.Length);
            return arr;
        }
        /// <summary>
        ///     Returns a Float array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static float[] ToFloatArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            if (value.Length % 4 != 0)
                throw new Exception("Byte Object length must be a multiple of 4");
            var arr = new List<float>();
            for (var i = 0; i < value.Length; i += 4)
            {
                var t = new[] { value[i], value[i + 1], value[i + 2], value[i + 3] };
                arr.Add(t.ToFloat());
            }
            return arr.ToArray();
        }
        /// <summary>
        ///     Returns a Double array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static double[] ToDoubleArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            if (value.Length % 8 != 0)
                throw new Exception("Byte Object length must be a multiple of 8");
            var arr = new List<double>();
            for (var i = 0; i < value.Length; i += 8)
            {
                var t = new[] { value[i], value[i + 1], value[i + 2], value[i + 3], value[i + 4], value[i + 5], value[i + 6], value[i + 7] };
                arr.Add(t.ToDouble());
            }
            return arr.ToArray();
        }
        /// <summary>
        ///     Returns a decimal array converted from a byte array.
        ///     (Converters.cs)
        /// </summary>
        public static decimal[] ToDecimalArray(this byte[] value)
        {
            if (value == null)
                throw new Exception("Value cannot be null.");
            if (value.Length % 16 != 0)
                throw new Exception("Byte Object length must be a multiple of 16");
            var arr = new List<decimal>();
            for (var i = 0; i < value.Length; i += 16)
            {
                var t = new[] { value[i], value[i + 1], value[i + 2], value[i + 3], value[i + 4], value[i + 5], value[i + 6], value[i + 7], value[i + 8], value[i + 9], value[i + 10], value[i + 11], value[i + 12], value[i + 13], value[i + 14], value[i + 15] };
                arr.Add(t.ToDecimal());
            }
            return arr.ToArray();
        }
        public static short ToInt16(this string value)
        {
            short result = 0;
            if (!string.IsNullOrEmpty(value))
                short.TryParse(value, out result);
            return result;
        }
        public static ushort ToUInt16(this string value)
        {
            ushort result = 0;
            if (!string.IsNullOrEmpty(value))
                ushort.TryParse(value, out result);
            return result;
        }
        public static int ToInt32(this string value)
        {
            var result = 0;
            if (!string.IsNullOrEmpty(value))
                int.TryParse(value, out result);
            return result;
        }
        public static uint ToUInt32(this string value)
        {
            uint result = 0;
            if (!string.IsNullOrEmpty(value))
                uint.TryParse(value, out result);
            return result;
        }
        public static long ToInt64(this string value)
        {
            long result = 0;
            if (!string.IsNullOrEmpty(value))
                long.TryParse(value, out result);
            return result;
        }
        public static ulong ToUInt64(this string value)
        {
            ulong result = 0;
            if (!string.IsNullOrEmpty(value))
                ulong.TryParse(value, out result);
            return result;
        }
        public static float ToFloat(this string value)
        {
            float result = 0;
            if (!string.IsNullOrEmpty(value))
                float.TryParse(value, out result);
            return result;
        }
        public static double ToDouble(this string value)
        {
            double result = 0;
            if (!string.IsNullOrEmpty(value))
                double.TryParse(value, out result);
            return result;
        }
        public static decimal ToDecimal(this string value)
        {
            decimal result = 0;
            if (!string.IsNullOrEmpty(value))
                decimal.TryParse(value, out result);
            return result;
        }
        public static short ToInt16(this char value)
        {
            short result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                short.TryParse(value.ToString(), out result);
            return result;
        }
        public static ushort ToUInt16(this char value)
        {
            ushort result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                ushort.TryParse(value.ToString(), out result);
            return result;
        }
        public static int ToInt32(this char value)
        {
            var result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                int.TryParse(value.ToString(), out result);
            return result;
        }
        public static uint ToUInt32(this char value)
        {
            uint result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                uint.TryParse(value.ToString(), out result);
            return result;
        }
        public static long ToInt64(this char value)
        {
            long result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                long.TryParse(value.ToString(), out result);
            return result;
        }
        public static ulong ToUInt64(this char value)
        {
            ulong result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                ulong.TryParse(value.ToString(), out result);
            return result;
        }
        public static float ToFloat(this char value)
        {
            float result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                float.TryParse(value.ToString(), out result);
            return result;
        }
        public static double ToDouble(this char value)
        {
            double result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                double.TryParse(value.ToString(), out result);
            return result;
        }
        public static decimal ToDecimal(this char value)
        {
            decimal result = 0;
            if (!string.IsNullOrEmpty(value.ToString()))
                decimal.TryParse(value.ToString(), out result);
            return result;
        }
    }
}
