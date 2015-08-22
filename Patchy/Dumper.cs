using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Patchy
{
    public static class Dumper
    {
        public static List<string> BinaryStrings(string file, int minLength = 6)
        {      
            List<string> data = new List<string>();
            if (File.Exists(file))
            {
                byte[] bytes = File.ReadAllBytes(file);
                int add = 0;
                byte[] text;

                for(long i = 0; i < bytes.LongLength; i++)
                {
                    if(bytes[i] >= 32 && bytes[i] <= 127)
                    {
                        add++;
                    }
                    else
                    {
                        if(add >= minLength)
                        {
                            text = new byte[add];
                            Array.Copy(bytes, i - add, text, 0, add);
                            data.Add(Encoding.ASCII.GetString(text));
                        }
                        add = 0;
                    }
                }
                
            }
            return data;
        }
 
        public static List<string> GetEventStrings(List<string> raw)
        {
            List<string> o = new List<string>();
            foreach (string str in raw)
            {
                if (IsValidEvent(str))
                {
                    o.Add(str);
                }
            }
            return o;
        }
        public static List<string> GetUsageStrings(List<string> raw)
        {
            List<string> o = new List<string>();
            foreach (string str in raw)
            {
                if (IsValidUsage(str))
                {
                    o.Add(str);
                }
            }
            return o;
        }

        public static bool IsValidEvent(string s)
        {
            if (s.Contains("_") && !s.StartsWith("_") && !s.EndsWith("_"))
            {
                foreach (char c in s)
                {
                    if (Char.IsLower(c) || (c != '_' && !Char.IsLetter(c)))
                        return false;
                }
                return true;
            }
            return false;
        }
        public static bool IsValidUsage(string s)
        {
            if ((s.Contains("Usage:") || s.Contains("USAGE:") || s.Contains("usage:")))
            {
                int count = s.Length - s.Replace(":", "").Length;
                if(count > 1)
                {
                    return false;
                }
                else if(count == 1 && s.Contains("."))
                {
                    return true;
                }
                
            }
            return false;
        }

    }
}
