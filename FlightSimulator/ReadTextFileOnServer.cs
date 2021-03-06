// --------------------------------------------------------------------------------------------------
// This file was automatically generated by J2CS Translator (http://j2cstranslator.sourceforge.net/). 
// Version 1.3.6.20110331_01     
// 11/05/19 19:45    
// ${CustomMessageForDisclaimer}                                                                             
// --------------------------------------------------------------------------------------------------
namespace Jp.Maker1.Io.Textfile
{


    using Jp.Maker1.Vsys3.Tools;
    using System;
    using System.Drawing;

    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class ReadTextFileOnServer
    {
        public FileInfo file;

        public string fileURL;
        private StreamReader isr;
        private FileStream fis;

        public TextReader br;
       
        public String lastKugiri;

        public ReadTextFileOnServer()
        {
            file = null;
            fileURL = null;
            isr = null;
            fis = null;
            br = null;
            lastKugiri = " ";
        }

        public ReadTextFileOnServer(FileInfo inFile)
        {
            file = null;
            fileURL = null;
            isr = null;
            fis = null;
            br = null;
            lastKugiri = " ";
            file = inFile;
            Open(inFile);
        }

        public ReadTextFileOnServer(string inFileURL)
        {
            file = null;
            fileURL = null;
            isr = null;
            fis = null;
            br = null;
            lastKugiri = " ";
            fileURL = inFileURL;
            Open(inFileURL);
        }

        //public virtual void Finalize()
        //{
        //    Close();
        //}

        public void Open(FileInfo inFile)
        {
            try
            {
                fis = inFile.OpenRead();
                isr = new StreamReader(fis);
                br = isr;
            }
            catch (IOException e)
            {
                System.Console.Out.WriteLine("[" + inFile.ToString() + "] file not found");
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        public void Open(string inFileURL)
        {
            fileURL = inFileURL;
            try
            {
                isr = new StreamReader(fileURL);
                br = isr;
            }
            catch (IOException e)
            {
                System.Console.Out.WriteLine("[" + fileURL.ToString() + "] file not found");
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        public void Close()
        {
            try
            {
                if (fis != null)
                {
                    fis.Close();
                }
                if (br != null)
                {
                    br.Close();
                }
                if (isr != null)
                {
                    isr.Close();
                }
                br = null;
                isr = null;
            }
            catch (IOException e)
            {
                if (fileURL != null)
                    System.Console.Out.WriteLine("[" + fileURL.ToString() + "] file close error");
                if (file != null)
                    System.Console.Out.WriteLine("[" + file.ToString() + "] file close error");
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        public String ReadLine()
        {
            String ret = null;
            try
            {
                if (br != null)
                    ret = br.ReadLine();
            }
            catch (IOException e)
            {
                if (fileURL != null)
                    System.Console.Out.WriteLine("[" + fileURL.ToString() + "] readLine() Error");
                if (file != null)
                    System.Console.Out.WriteLine("[" + file.ToString() + "] readLine() Error");
                Console.Error.WriteLine(e.StackTrace);
            }

            return ret;
        }


        public int Read()
        {
            int ret = -1;
            try
            {
                if (br != null)
                {
                    ret = br.Read();
                    
                    if ((char)ret < ' ')
                        ret = 32;
                }
            }
            catch (IOException e)
            {
                if (fileURL != null)
                    System.Console.Out.WriteLine("[" + fileURL.ToString() + "] read() Error");
                if (file != null)
                    System.Console.Out.WriteLine("[" + file.ToString() + "] read() Error");
                Console.Error.WriteLine(e.StackTrace);
            }

            return ret;
        }

        public int Peek()
        {
            return isr.Peek();
        }       

        public int ReadSkipSpace()
        {
            while (true)
            {
                int c = Read();
                if (((char)c != ' ') || (c == -1))
                    return c;
            }
        }

        public String ReadToKugiri(String kugiri)
        {
            String ret = "";

            bool flag = false;
            lastKugiri = "";
            do
            {
                int c = Read();
                if (c == -1)
                {
                    break;
                }
                for (int i = 0; i < kugiri.Length; i++)
                {
                    if (kugiri[i] == (char)c)
                    {
                        lastKugiri += (char)c;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    ret = ret + (char)c;
            } while (!flag);
            return ret;
        }

        public String ReadAWord()
        {
            String ret = "";

            int c = ReadSkipSpace();
            if (c != -1)
            {
                ret = ret + (char)c;
                while (true)
                {
                    c = Read();
                    if ((char)c == ' ')
                        break;
                    ret = ret + (char)c;
                }
            }
            return ret;
        }

        public bool Pass(String str)
        {
            bool ret = true;

            int i = 0;

            String str2 = DispFormat.Replace(str, 32, "");

            for (i = 0; i < str2.Length; )
            {
                char c = (char)Peek();


                if (c == -1)
                {
                    ret = false;
                    break;
                }

                if ((char)c == ' ' || (char)c == '\t')
                {
                    Read();
                    continue;
                }

                if ((char)c != str2[i])
                {

                    ret = true;
                    break;
                }

                Read();
                i++;

            }

            return ret;
        }


        public int ReadInteger()
        {
            int ret = 0;
            String str = "";
            int sign = 1;
            int c = 1;
            try
            {
                //br.Mark(32);
                long mark = isr.BaseStream.Seek(0, SeekOrigin.Current);

                c = ReadSkipSpace();
                if (c == -1)
                    return ret;

                if (c == 43)
                {
                    c = Read();
                    if (c == -1)
                        return ret;
                }
                else if ((char)c == '-')
                {
                    sign = -1;
                    c = Read();
                    if (c == -1)
                        return ret;
                }
                if (((char)c < '0') || ((char)c > '9'))
                {
                    //br.Reset();
                    isr.BaseStream.Seek(mark, SeekOrigin.Begin);
                    return ret;
                }
                str = str + (char)c;
                while (true)
                {
                    //br.Mark(32);
                    mark = isr.BaseStream.Seek(0, SeekOrigin.Current);
                    c = Read();
                    if (c == -1)
                    {
                        break;
                    }
                    if (((char)c < '0') || ((char)c > '9'))
                    {
                        //br.Reset();
                        isr.BaseStream.Seek(mark, SeekOrigin.Begin);
                        break;
                    }
                    str = str + (char)c;
                }
                ret = ((Int32)Int32.Parse(str));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }

            return sign * ret;
        }

        public double ReadDouble()
        {
            double ret = 0.0D;
            String str = "";
            int sign = 1;
            int c = 1;
            bool flagPeriodExist = false;
            try
            {

                //  br.Mark(32);
                long mark = isr.BaseStream.Seek(0, SeekOrigin.Current);

                c = ReadSkipSpace();
                if (c == -1)
                    return ret;

                if (c == 43)
                {
                    c = Read();
                    if (c == -1)
                        return ret;
                }
                else if ((char)c == '-')
                {
                    sign = -1;
                    c = Read();
                    if (c == -1)
                        return ret;
                }

                if ((((char)c < '0') || ((char)c > '9')) && ((char)c != '.'))
                {
                    //br.Reset();
                    isr.BaseStream.Seek(mark, SeekOrigin.Begin);
                    return ret;
                }
                if ((char)c == '.')
                {
                    str = str + "0.";
                    flagPeriodExist = true;
                }
                else
                {
                    str = str + (char)c;
                }
                while (true)
                {
                    // br.Mark(32);
                    mark = isr.BaseStream.Seek(0, SeekOrigin.Current);
                    c = Read();
                    if (c == -1)
                    {
                        break;
                    }
                    if ((((char)c < '0') || ((char)c > '9')) && ((char)c != '.'))
                    {
                        //  br.Reset();
                        isr.BaseStream.Seek(mark, SeekOrigin.Begin);
                        break;
                    }
                    if ((char)c == '.')
                    {
                        if (flagPeriodExist)
                        {
                            // br.Reset();
                            isr.BaseStream.Seek(mark, SeekOrigin.Begin);
                            break;
                        }
                        flagPeriodExist = true;
                        str = str + (char)c;
                    }
                    else
                    {
                        str = str + (char)c;
                    }
                }
                if (str.Substring(str.Length - 1).Equals("."))
                {
                    str = str + "0";
                }
                ret = Double.Parse(str);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }

            return sign * ret;
        }

        public int Read_int_element(int[] element)
        {
            int n = 0;
            int mode = 0;
            int kugiri = 0;

            int c = ReadSkipSpace();
            if ((char)c != '{')
            {
                return -1;
            }

            while (true)
            {
                if (kugiri == 1)
                {
                    c = ReadSkipSpace();
                    if (c == -1)
                        return -2;

                    kugiri = 0;
                    if ((char)c == '}')
                        break;
                    if (((char)c == '-') || ((char)c == ':') || ((char)c == '~'))
                    {
                        mode = 1;
                    }
                    else if ((char)c != ',')
                        return -3;
                }
                else
                {
                    kugiri = 1;
                    int x = ReadInteger();
                    if (mode == 0)
                    {
                        element[(n++)] = x;
                    }
                    else
                    {
                        mode = 0;
                        int x0 = element[(n - 1)];
                        if (x0 < x)
                        {
                            for (int i = x0 + 1; i <= x; i++)
                            {
                                element[(n++)] = i;
                            }
                        }
                        if (x0 > x)
                        {
                            for (int i_0 = x0 - 1; i_0 >= x; i_0--)
                            {
                                element[(n++)] = i_0;
                            }
                        }
                        if (x0 == x)
                        {
                            element[(n++)] = x;
                        }
                    }
                }
            }

            return n;
        }

        public String ReadString(String item, bool flagMessage)
        {
            String temp = DispFormat.Replace(ReadToKugiri("/"), 32, "");
            if (!temp.Equals(DispFormat.Replace(item, 32, "")))
            {
                System.Console.Out.WriteLine("*** Error in readString() item=(" + item + "!=" + temp + ")");
                return "";
            }
            temp = ReadToKugiri("/");
            ReadLine();
            if (flagMessage)
                System.Console.Out.WriteLine(item + "=" + temp);
            return temp;
        }

        public double ReadDblValue(String item, bool flagMessage)
        {
            String temp = DispFormat.Replace(ReadToKugiri("="), 32, "");
            if (!temp.Equals(DispFormat.Replace(item, 32, "")))
            {
                System.Console.Out.WriteLine("*** Error in readDblValue() item=(" + item + "!=" + temp + ")");
                return 0.0D;
            }
            double ret = ReadDouble();
            ReadLine();
            if (flagMessage)
                System.Console.Out.WriteLine(item + "=" + DispFormat.DoubleFormat(ret, 3));
            return ret;
        }

        public int ReadIntValue(String item, bool flagMessage)
        {
            String temp = DispFormat.Replace(ReadToKugiri("="), 32, "");
            if (!temp.Equals(DispFormat.Replace(item, 32, "")))
            {
                System.Console.Out.WriteLine("*** Error in readIntValue() item=(" + item + "!=" + temp + ")");
                return 0;
            }
            int ret = ReadInteger();
            ReadLine();
            if (flagMessage)
                System.Console.Out.WriteLine(item + "=" + ret);
            return ret;
        }

        public Color ReadColor(String item, bool flagMessage)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            String temp = DispFormat.Replace(ReadToKugiri("="), 32, "");
            if (!temp.Equals(DispFormat.Replace(item, 32, "")))
            {
                System.Console.Out.WriteLine("*** Error in readColor() item=(" + item + "!=" + temp + ")");
                return Color.White;
            }
            if (!Pass("["))
            {
                System.Console.Out.WriteLine("*** Error in readColor() item=(" + item + ") no '['");
                return Color.White;
            }
            r = ReadInteger();
            if (!Pass(","))
            {
                System.Console.Out.WriteLine("*** Error in readColor() item=(" + item + ") no first','");
                return Color.White;
            }
            g = ReadInteger();
            if (!Pass(","))
            {
                System.Console.Out.WriteLine("*** Error in readColor() item=(" + item + ") no second','");
                return Color.White;
            }
            b = ReadInteger();
            if (!Pass("]"))
            {
                System.Console.Out.WriteLine("*** Error in readColor() item=(" + item + ") no ']'");
                return Color.White;
            }
            Color ret = Color.FromArgb(r, g, b);
            if (flagMessage)
                System.Console.Out.WriteLine(item + "=" + ret);
            ReadLine();
            return ret;
        }

        public Vector3D ReadVector(String item, bool flagMessage)
        {
            double x = 0.0D;
            double y = 0.0D;
            double z = 0.0D;

            String temp = DispFormat.Replace(ReadToKugiri("="), 32, "");
            if (!temp.Equals(DispFormat.Replace(item, 32, "")))
            {
                System.Console.Out.WriteLine("*** Error in readVector() item=(" + item + "!=" + temp + ")");
                return null;
            }
            if (!Pass("("))
            {
                System.Console.Out.WriteLine("*** Error in readVector() item=(" + item + ") no '('");
                return null;
            }
            x = ReadDouble();
            if (!Pass(","))
            {
                System.Console.Out.WriteLine("*** Error in readVector() item=(" + item + ") no first','");
                return null;
            }
            y = ReadDouble();
            if (!Pass(","))
            {
                System.Console.Out.WriteLine("*** Error in readVector() item=(" + item + ") no second','");
                return null;
            }
            z = ReadDouble();
            if (!Pass(")"))
            {
                System.Console.Out.WriteLine("*** Error in readVector() item=(" + item + ") no ')'");
                return null;
            }
            Vector3D ret = new Vector3D(x, y, z);
            if (flagMessage)
            {
                System.Console.Out.Write(item + "=");
                ret.PrintPos();
                System.Console.Out.WriteLine("");
            }
            ReadLine();
            return ret;
        }

        public Vector3D ReadVector()
        {
            if (!Pass("("))
                return null;
            double xtmp = ReadDouble();
            if (!Pass(","))
                return null;
            double ytmp = ReadDouble();
            if (!Pass(","))
                return null;
            double ztmp = ReadDouble();
            if (!Pass(")"))
                return null;

            return new Vector3D(xtmp, ytmp, ztmp);
        }
    }
}