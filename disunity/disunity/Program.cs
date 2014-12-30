using System;
using System.IO;
using Ionic.Zlib;

namespace disunity
{
	class MainClass
	{
		public static float ssdt(){
			byte key = 188;
			byte[] a = BitConverter.GetBytes( 140f );
			for( int i = 0; i < a.Length; i++ )
			{
				a[ i ] ^= key;
			}
			float b = BitConverter.ToSingle( a, 0 );
			byte[] c = BitConverter.GetBytes( b );
			for( int i = 0; i < c.Length; i++ )
			{
				c[ i ] ^= key;
			}
			float d = BitConverter.ToSingle( c, 0 );
			float r = d;
			return r;
		}
		public static void Main (string[] args)
		{
			ssdt ();
			/*
			string name = "dump";
			System.Random random = new System.Random (name.GetHashCode ());
			for (int i = 0; i < 16; i++) {
				byte key = (byte)random.Next (256);
				Console.WriteLine ("key:" + key.ToString());
			}
			Console.WriteLine ();
			*/
			//test ();
			Console.WriteLine ("Hello World!");
		}

		static void test(){
			FileStream fs = File.OpenRead("dump.txt");
			byte[] data = new byte[fs.Length];
			Console.WriteLine("File Size:{0,0:d}", fs.Length);
			fs.Read(data, 0, data.Length);
			fs.Close();

			for (int i = 0; i < 16; i++)
			{
				Console.Write("{0,2:x} ", data[i]);
			}
			Console.WriteLine();

			string name = "dump";
			Console.WriteLine("HashCode:{0,8:x}", name.GetHashCode());

			System.Random random = new System.Random(name.GetHashCode());
			for (int i = 0; i < data.Length; i++)
			{
				byte key = (byte)random.Next(256);
				if (i < 16) {
					Console.WriteLine("key:" + key.ToString());
				}
				data[i] ^= key;
			}


			for (int i = 0; i < 16; i++)
			{
				Console.Write("{0,2:x} ", data[i]);
			}
			Console.WriteLine();

			byte[] data_decompressed = ZlibStream.UncompressBuffer(data);

			fs = new FileStream("test.dll", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			for (int i = 0; i < data_decompressed.Length; i++)
			{
			    fs.WriteByte(data_decompressed[i]);
			}
			fs.Close();
		}
	}
}
