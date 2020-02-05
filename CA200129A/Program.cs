using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA200129A
{
    struct Jatek
    {
        public int ev;
        public string cim;
        public string mufaj;
        public string kiado;
        public string[] platformok;
    }
    class Program
    {
        static List<Jatek> jatekok;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            F02();
            F03();
            F04();
            F05();
            F06();
            F07();
            F08();
            F09();
            F10();
            Console.ReadKey();
        }

        private static void F10()
        {
            Console.Write("10. feladat:\n\t");
            int eao = 0;
            int eapc = 0;
            foreach (var j in jatekok)
            {
                if(j.kiado == "Electronic Arts")
                {
                    eao++;
                    if(j.platformok.Contains("PC"))
                    {
                        eapc++;
                    }
                }
            }
            Console.WriteLine(
                "Az EA díjazott játékainak {0:0.00}%-a jelent meg PC-re",
                eapc * 100 / (float)eao);
        }
        private static void F09()
        {
            Console.Write(" 9. feladat:\n\t");
            Console.Write("Írja be egy platform nevét: ");
            var plat = Console.ReadLine();

            var pj = new List<string>();
            foreach (var j in jatekok)
            {
                if (j.platformok.Contains(plat)) pj.Add(j.cim);
            }

            if(pj.Count != 0)
            {
                for (int j = 0; j < pj.Count; j++)
                {
                    int x = rnd.Next(pj.Count);
                    int y = rnd.Next(pj.Count);
                    var s = pj[x];
                    pj[x] = pj[y];
                    pj[y] = s;
                }

                Console.WriteLine($"\t{plat} platformra pldául ezek a játékok jelentek meg:");
                int i = 0;
                while (i < 10 && i < pj.Count)
                {
                    Console.WriteLine("\t\t{0, 2}.: {1}", i + 1, pj[i]);
                    i++;
                }
            }
            else Console.WriteLine("\tIlyen platform nincs a file-ban");
        }
        private static void F08()
        {
            var mufajok = new List<string>();
            foreach (var j in jatekok)
            {
                if (!mufajok.Contains(j.mufaj)) mufajok.Add(j.mufaj);
            }
            mufajok.Sort();
            var sw = new StreamWriter(@"..\..\res\genres.txt", false, Encoding.UTF8);
            foreach (var m in mufajok) sw.WriteLine(m);
            sw.Close();
        }
        private static void F07()
        {
            Console.Write(" 7. feladat:\n\t");
            var dic = new Dictionary<string, int>();
            foreach (var j in jatekok)
            {
                if (!dic.ContainsKey(j.kiado))
                    dic.Add(j.kiado, 1);
                else dic[j.kiado]++;
            }
            var max = new KeyValuePair<string, int>("", int.MinValue);
            foreach (var kvp in dic)
            {
                if (kvp.Value > max.Value) max = kvp;
            }
            Console.WriteLine(
                $"A legtöbb játékkal szereplő kiadó: {max.Key} ({max.Value} játék).");
        }
        private static void F06()
        {
            Console.Write(" 6. feladat:\n\t");
            var lng = new Jatek() { ev = int.MinValue, };
            foreach (var j in jatekok)
            {
                if(j.platformok.Contains("NES") && j.ev > lng.ev)
                    lng = j;
            }
            Console.WriteLine(
                $"A legkésőbbi NES megjelenés a listába: {lng.cim} ({lng.ev})");
        }
        private static void F05()
        {
            Console.Write(" 5. feladat:\n\t");
            Console.Write("Igazán sikeres évek:\n\t");
            var dic = new Dictionary<int, int>();
            foreach (var j in jatekok)
            {
                if(!dic.ContainsKey(j.ev))
                    dic.Add(j.ev, 1);
                else dic[j.ev]++;
            }
            var rend = dic.OrderBy(k => k.Key);
            foreach (var kvp in rend)
            {
                if (kvp.Value >= 7) Console.Write($"{kvp.Key}, ");
            }
            Console.Write("\n");
        }
        private static void F04()
        {
            Console.Write(" 4. feladat:\n\t");

            int db = 0;
            foreach (var j in jatekok)
            {
                if (j.platformok.Contains("PC")) db++;
            }
            Console.WriteLine($"{db} db játk jelent meg PC platformra is!");

        }
        private static void F03()
        {
            Console.Write(" 3. feladat:\n\t");
            Console.WriteLine($"Összesen {jatekok.Count} játék szerepel a listában");
        }
        private static void F02()
        {
            jatekok = new List<Jatek>();
            var sr = new StreamReader(@"..\..\res\games.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                var s = sr.ReadLine().Split(';');
                var j = new Jatek()
                {
                    ev = int.Parse(s[0]),
                    cim = s[1],
                    mufaj = s[2],
                    kiado = s[3],
                    platformok = s[4].Split(','),
                };
                jatekok.Add(j);
            }
            sr.Close();
        }
    }
}
