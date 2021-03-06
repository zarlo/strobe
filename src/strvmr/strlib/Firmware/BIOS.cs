﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
namespace StrobeVM.Firmware
{
	/// <summary>
	/// Basic Input/Output System.
	/// </summary>
	public class BIOS
	{
        /// <summary>
        ///  Turn the string into bytes.
        /// </summary>
        /// <param name="s">Input String</param>
        /// <returns>The byte array</returns>
        public byte[] GetBytes(string s)
        {
            byte[] ret = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
                ret[i] = (byte)s[i];
            return ret;
        }

        /// <summary>
        /// Return the code for BIOS Setup.
        /// </summary>
        public static byte[] Setup()
		{
			return Firmware.Setup.GetSetup(); // Return the setup
		}

		/// <summary>
		/// The buffer.
		/// </summary>
		List<char> buffer;

		/// <summary>
		/// The location.
		/// </summary>
		int loc;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:StrobeVM.Firmware.BIOS"/> class.
		/// </summary>
		public BIOS()
		{
			buffer = new List<char>();
			loc = 0;
		}

		/// <summary>
		/// Reads the file.
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="file">File.</param>
		public byte[] ReadFile(byte[] file)
		{
			return File.ReadAllBytes(Encoding.ASCII.GetString(file));
		}

		/// <summary>
		/// Writes the file.
		/// </summary>
		/// <param name="file">File.</param>
		/// <param name="content">Content.</param>
		public void WriteFile(byte[] file, byte[] content)
		{
			File.WriteAllBytes(Encoding.ASCII.GetString(file),
							   content);
		}

		/// <summary>
		/// Deletes the file.
		/// </summary>
		/// <param name="file">File.</param>
		public void DeleteFile(byte[] file)
		{
			File.Delete(Encoding.ASCII.GetString(file));
		}

		public void DeleteFolder(byte[] folder)
		{
			Directory.Delete(Encoding.ASCII.GetString(folder));
		}

		/// <summary>
		/// Does the folder exist?
		/// </summary>
		/// <returns>True or False.</returns>
		/// <param name="folder">Folder.</param>
		public byte[] FolderExists(byte[] folder)
		{
			if (Directory.Exists(Encoding.ASCII.GetString(folder)))
			{
				return new byte[] { 0x1 };
			}
			return new byte[] { 0x0 };
		}

		/// <summary>
		/// Does the file exist?
		/// </summary>
		/// <returns>True or False.</returns>
		/// <param name="file">Folder.</param>
		public byte[] FileExists(byte[] file)
		{
			if (File.Exists(Encoding.ASCII.GetString(file)))
			{
				return new byte[] { 0x1 };
			}
			return new byte[] { 0x0 };
		}

		/// <summary>
		/// Creates the folder.
		/// </summary>
		/// <param name="folder">Folder.</param>
		public void CreateFolder(byte[] folder)
		{
			Directory.CreateDirectory(Encoding.ASCII.GetString(folder));
		}

		/// <summary>
		/// Appends the file.
		/// </summary>
		/// <param name="file">File.</param>
		/// <param name="content">Content.</param>
		public void AppendFile(byte[] file, byte[] content)
		{
            List<byte> c = new List<byte>();
            byte[] one = ReadFile(file);
            foreach (byte b in one)
                c.Add(b);
            foreach (byte b in content)
                c.Add(b);
            WriteFile(file,c.ToArray());
		}

		/// <summary>
		/// Exits the VM with the specified error code.
		/// </summary>
		/// <param name="i">The error code.</param>
		public void Exit(int i)
		{
			//Environment.Exit(i);
		}

        /// <summary>
        /// Write one character to buffer.
        /// </summary>
        /// <param name="c">Character</param>
		public void Write(char c)
		{
			buffer.Add(c);
			loc++;
		}

		/// <summary>
		/// Reads the line.
		/// </summary>
		/// <returns>The line.</returns>
		public byte[] ReadLine()
		{
			return GetBytes(Console.ReadLine());
		}

		/// <summary>
		/// Read the integer.
		/// </summary>
		/// <returns>The integer.</returns>
		public byte[] Read()
		{
			return BitConverter.GetBytes(Console.Read());
		}

		/// <summary>
		/// Reads the key.
		/// </summary>
		/// <returns>The key.</returns>
		public byte[] ReadKey()
		{
			ConsoleKeyInfo x = Console.ReadKey();
			return BitConverter.GetBytes(x.KeyChar);
		}

		/// <summary>
		/// Write the specified byte array.
		/// </summary>
		/// <param name="ba">Byte Array.</param>
		public void Write(byte[] ba, byte[] c)
		{
            if (c[0] == 1)
                Write(((int)ba[0]).ToString());
            else
			    Write(Encoding.ASCII.GetString(ba));
		}

		/// <summary>
		/// Write the specified string.
		/// </summary>
		/// <param name="s">Sring.</param>
		public void Write(string s)
		{
			foreach (char c in s)
				Write(c);
		}

		/// <summary>
		/// Clear the buffer.
		/// </summary>
		public void Clear()
		{
			buffer.Clear();
		}

		/// <summary>
		/// Display this instance.
		/// </summary>
		public void Display()
		{
			Console.Write(buffer.ToArray());
			Clear();
		}
	}
}
