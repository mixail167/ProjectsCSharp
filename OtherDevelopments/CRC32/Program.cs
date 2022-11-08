using System;
using System.IO;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class Crc32 : HashAlgorithm
    {
        public const uint DefaultPolynomial = 0x04C11DB7;
        public const uint DefaultSeed = 0xffffffff;

        private uint hash;
        private readonly uint seed;
        private readonly uint[] table;
        private static uint[] defaultTable;

        public Crc32()
        {
            table = InitializeTable(DefaultPolynomial);
            seed = DefaultSeed;
            Initialize();
        }

        public Crc32(uint polynomial, uint seed)
        {
            table = InitializeTable(polynomial);
            this.seed = seed;
            Initialize();
        }

        public override void Initialize()
        {
            hash = seed;
        }

        protected override void HashCore(byte[] buffer, int start, int length)
        {
            hash = CalculateHash(table, hash, buffer, start, length);
        }

        protected override byte[] HashFinal()
        {
            byte[] hashBuffer = UInt32ToBigEndianBytes(~hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize
        {
            get { return 32; }
        }

        public static uint Compute(byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer, 0, buffer.Length);
        }

        public static uint Compute(uint seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(DefaultPolynomial), seed, buffer, 0, buffer.Length);
        }

        public static uint Compute(uint polynomial, uint seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        private static uint[] InitializeTable(uint polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            uint[] createTable = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint entry = i;
                for (int j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int start, int size)
        {
            uint crc = seed;
            for (int i = start; i < size; i++)
                unchecked
                {
                    crc = (crc >> 8) ^ table[buffer[i] ^ crc & 0xff];
                }
            return crc;
        }

        private byte[] UInt32ToBigEndianBytes(uint x)
        {
            return new byte[] {
            (byte)((x >> 24) & 0xff),
            (byte)((x >> 16) & 0xff),
            (byte)((x >> 8) & 0xff),
            (byte)(x & 0xff)
        };
        }

        public string Get(string FilePath)
        {
            Crc32 crc32 = new Crc32();
            string hash = string.Empty;

            using (FileStream fs = File.Open(FilePath, FileMode.Open))
                foreach (byte b in crc32.ComputeHash(fs)) hash += b.ToString("x2").ToLower();
            return hash;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //int count = Convert.ToInt32(Console.ReadLine());
            //Random random = new Random();
            //using (StreamWriter writer = new StreamWriter(count.ToString() + ".txt"))
            //{
            //    for (int k = 0; k < count; k++)
            //    {
            //        if (k > 0)
            //        {
            //            writer.Write("\n");
            //        }
            //        string[] fio = new string[3];
            //        for (int i = 0; i < 3; i++)
            //        {
            //            for (int j = 0; j < 5; j++)
            //            {
            //                if (j % 2 != 0)
            //                {
            //                    fio[i] += Convert.ToChar(random.Next(97, 123)).ToString();
            //                }
            //                else
            //                {
            //                    fio[i] += Convert.ToChar(random.Next(65, 91)).ToString();
            //                }
            //            }
            //        }
            //        int number = random.Next(1, 100);
            //        int year = random.Next(2000, 2020);
            //        writer.Write(string.Join(";", string.Join(" ", fio), number, year));
            //    }
            //}

            Crc32 crc32 = new Crc32();
            string hash = string.Empty;
            using (FileStream fs = File.OpenRead("100.txt"))
            {
                foreach (byte b in crc32.ComputeHash(fs))
                {
                    hash += b.ToString("x2").ToLower();
                }
                Console.WriteLine("this Application CRC-32 is {0}", hash);
            }
            Console.ReadKey();
        }
    }
}
