using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    internal class Program
    {
        public class Junction
        {
            public string Left;
            public string Right;
        }

        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            var txt = File.ReadAllText("input.txt");

            txt = txt.Replace("\r", "");
            var lines = txt.Split('\n');
            string instr = lines[0];

            var junctions = new Dictionary<string, Junction>();
            var curKey = "";
            for (int ia = 2; ia < lines.Length; ia++)
            {
                var line = lines[ia];
                if (String.IsNullOrEmpty(line))
                    continue;
                var ps = line.Split('=');
                var key = ps[0].Trim();
                if (curKey == "")
                    curKey = key;
                //var values = ps[1].Split(',')[0].Replace("(", "");

                var j = new Junction();
                j.Left = ps[1].Split(',')[0].Replace("(", "").Trim();
                j.Right = ps[1].Split(',')[1].Replace(")", "").Trim();

                junctions.Add(key, j);
            }

            bool found = false;
            int cnt = 0;
            int i = 0;
            curKey = "AAA";
            while (!found)
            {
                cnt++;
                var j = junctions[curKey];
                if (instr[i] == 'L')
                {
                    curKey = j.Left;
                }
                else
                {
                    curKey = j.Right;
                }
                if (curKey == "ZZZ")
                {
                    // Found it
                    found = true;
                    Console.WriteLine("Steps : " + cnt);
                }
                i++;
                if (i >= instr.Length)
                    i = 0;
            }

            Console.ReadLine();
        }
       
        static void Part2()
        {
            var txt = File.ReadAllText("input.txt");

            txt = txt.Replace("\r", "");
            var lines = txt.Split('\n');
            string instr = lines[0];

            var junctions = new Dictionary<string, Junction>();
            var curKey = "";
            for (int ia = 2; ia < lines.Length; ia++)
            {
                var line = lines[ia];
                if (String.IsNullOrEmpty(line))
                    continue;
                var ps = line.Split('=');
                var key = ps[0].Trim();
                if (curKey == "")
                    curKey = key;
                //var values = ps[1].Split(',')[0].Replace("(", "");

                var j = new Junction();
                j.Left = ps[1].Split(',')[0].Replace("(", "").Trim();
                j.Right = ps[1].Split(',')[1].Replace(")", "").Trim();

                junctions.Add(key, j);
            }

            foreach (var j in junctions)
            {
                Console.WriteLine("Key " + j.Key + " " + j.Value.Left + " " + j.Value.Right);
            }

            bool found = false;
            
            int i = 0;
            var sKeys = junctions.Keys.Where(k => k.EndsWith("A")).ToList();
            
            var kFreqNr = new Dictionary<int, long>();
            
            long nrLoops = 1;
            while (!found)
            {   
                for (int ki = 0; ki < sKeys.Count(); ki++)                
                {
                    if (kFreqNr.ContainsKey(ki))
                        continue;
                    var k = sKeys[ki];
                                        
                    var j = junctions[sKeys[ki]];
                    if (instr[i] == 'L')
                    {
                        sKeys[ki] = j.Left;
                    }
                    else
                    {
                        sKeys[ki] = j.Right;
                    }
                    
                    if (sKeys[ki].EndsWith("Z"))
                        kFreqNr.Add(ki, nrLoops);                                        
                }

                if (kFreqNr.Count() == sKeys.Count())
                    found = true;               
                
                i++;
                if (i >= instr.Length)
                {
                    i = 0;
                    nrLoops++;
                }
            }

            // Calculate nr steps : length of instructions * all the individual frequencies
            long nrSteps = instr.Length;
            foreach(var nr in kFreqNr.Values)
            {
                nrSteps *= nr;
            }
            Console.WriteLine("NR Steps " + nrSteps);
            Console.ReadLine();
        }


    }
}
