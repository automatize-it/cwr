using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace cwr
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length != 3)
            {
                Console.WriteLine("CWR error in arguments");
                Console.WriteLine("CWR usage: cwr inputfilename filewithregex outputfilename");
                Environment.Exit(2);
                return;
            }

            Encoding enc = new UTF8Encoding(true, true);

            string parsefile = args[0];
            string regexfile = args[1];
            string outfile = args[2];
            string regexstr = ".*";
            string buf = "0";

            /*
            using (var reader = new StreamReader(regexfile, true))
            {
                reader.Peek(); // you need this!
                var encoding = reader.CurrentEncoding;
                if (encoding.CodePage != enc.CodePage)
                {
                    Console.WriteLine("CWR regex encoding error – not UTF-8");
                    reader.Close();
                    Environment.Exit(1);
                }
                reader.Close();
            }

            using (var reader = new StreamReader(parsefile, true))
            {
                reader.Peek(); // you need this!
                var encoding = reader.CurrentEncoding;
                if (encoding.CodePage != enc.CodePage)
                {
                    Console.WriteLine("CWR input encoding error – not UTF-8");
                    reader.Close();
                    Environment.Exit(1);
                }    
                reader.Close();
                
            }
            */

            StreamReader strr = new StreamReader(regexfile, enc, false);

            while (!strr.EndOfStream)
            {
                try
                {
                    regexstr = strr.ReadLine();
                }
                catch (System.ObjectDisposedException)
                {
                    return;
                }
                catch (System.IO.IOException)
                {
                    return;
                }
            }

            strr.Close();

            StreamReader strr2 = new StreamReader(parsefile, enc, false);

            while (!strr2.EndOfStream)
            {
                try
                {
                    buf = strr2.ReadToEnd();
                }
                catch (System.ObjectDisposedException)
                {
                    //MessageBox.Show("Ошибка чтения");
                    return;
                }
                catch (System.IO.IOException)
                {
                    //MessageBox.Show("Ошибка чтения");
                    return;
                }
            }

            strr2.Close();

            StreamWriter sw = new StreamWriter(outfile, false, enc);
            sw.AutoFlush = true;
            
            var match = Regex.Match(buf, @regexstr);

            while (match.Success)    
            {
                try
                {
                    sw.WriteLine(match.Value);
                }
                catch (System.ObjectDisposedException)
                {
                    return;
                }
                catch (System.IO.IOException)
                {
                    return;
                }

                match = match.NextMatch();
            }

            sw.Close();
            Environment.Exit(0);

        }
    }
}
