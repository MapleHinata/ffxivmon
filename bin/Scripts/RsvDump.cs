using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;


dynamic parsed = PacketObj;

if(!Directory.Exists("CapturedRsv"))
{
	Directory.CreateDirectory("CapturedRsv");
}

if(Packet.Name == "RSVData")
{
	//This fucking sucks but SE had my hands tied, null terminated UTF-8 strings, fucking hell
	byte[] keyArray = ((IEnumerable)parsed.key_str).Cast<char>().Select(s => Convert.ToByte(s)).TakeWhile(b => !b.Equals(0)).ToArray();
	byte[] valueArray = ((IEnumerable)parsed.value_str).Cast<char>().Select(s => Convert.ToByte(s)).TakeWhile(b => !b.Equals(0)).ToArray();

    string key = Encoding.UTF8.GetString(keyArray);
	string value = Encoding.UTF8.GetString(valueArray);

	string keyValue = $"{key}:{value}{Environment.NewLine}";
	Debug.WriteLine(keyValue);

	File.AppendAllText(Path.Combine(Environment.CurrentDirectory, "CapturedRsv","RSV.txt"), keyValue);
}