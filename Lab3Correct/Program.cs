using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace Lab3Correct
{
    class Program
    {


        public static byte[] bmp = { 66, 77 };
        public static byte[] png = { 137, 80, 78, 71, 13, 10, 26, 10 };
        
        static void Main(string[] args)
        {

            
            var fileWay = args[0];



            Console.WriteLine(fileWay);
           
            if (!File.Exists(fileWay))
            {
                return;
            }

            var fileReader = File.ReadAllBytes(fileWay);
            

            var reader = new BinaryReader(new MemoryStream(fileReader));
            var bmpRead = reader.ReadBytes(2);
            var isBmp = Enumerable.SequenceEqual(bmp, bmpRead);

            if (isBmp)
            {
                var fileSize = reader.ReadInt32();
                var reversefile = reader.ReadInt32();
                var datar = reader.ReadInt32();

                var infoheaderSize = reader.ReadInt32();
                var width = reader.ReadInt32();
                var height = reader.ReadInt32();

                Console.WriteLine("The width of the bmp is {0} and the height of the bmp is {1}", width, height);
            }

            else
            {

                reader.BaseStream.Position = 0;
                var pngSequence = reader.ReadBytes(8);
                var isPng = Enumerable.SequenceEqual(png, pngSequence);
                if (isPng)
                {
                    var DataLength = IPAddress.HostToNetworkOrder(reader.ReadInt32());
                    var ChunkType = Encoding.ASCII.GetString(reader.ReadBytes(4));

                    var data = reader.ReadBytes(DataLength);
                    var crc = reader.ReadBytes(4);
                    reader = new BinaryReader(new MemoryStream(data));
                    var width = IPAddress.HostToNetworkOrder(BitConverter.ToInt32(reader.ReadBytes(4)));
                    var height = IPAddress.HostToNetworkOrder(BitConverter.ToInt32(reader.ReadBytes(4)));



                    Console.WriteLine("The width of the png is {0} and the height of the png is {1}", width, height);
                }
                else
                {
                    Console.WriteLine("File format not existing");

                    
                }

            }

          

            }








            

        }
    }

