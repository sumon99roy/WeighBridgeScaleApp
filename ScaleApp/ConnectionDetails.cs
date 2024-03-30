using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScaleApp
{
    class ConnectionDetails
    {
         EncryptDecrypt  enDeCls=new EncryptDecrypt();
       
        public string constring()
        {
            string server = "", dbname = "", uname = "", upass = "";
            string constr = "Server=ServerName;Database=DataBaseName;UID=username;Password=password";
            string filePath = @"configfile.dat";
            //  string line;
            string firstline = "";
            if (File.Exists(filePath))
            {
                StreamReader file = null;
                try
                {
                    file = new StreamReader(filePath);
                    firstline = file.ReadLine();
                    String[] Sline = firstline.Split('|');
                   // Console.WriteLine("db connection details ");
                   // Console.WriteLine(Sline);
                   // Console.WriteLine(Sline[0]);
                   // Console.WriteLine(Sline[1]);
                   // Console.WriteLine(Sline[2]);
                   // Console.WriteLine(Sline[3]);
                   // byte[] b = Convert.FromBase64String(Sline[0]);
                   // server = System.Text.ASCIIEncoding.ASCII.GetString(b);
                    server = Sline[0];
                   
                 //For older encryption decription start
                   // byte[] b1 = Convert.FromBase64String(Sline[1]);
                   // byte[] b2 = Convert.FromBase64String(Sline[2]);
                   // byte[] b3 = Convert.FromBase64String(Sline[3]);
                   // dbname = System.Text.ASCIIEncoding.ASCII.GetString(b1);
                   // uname = System.Text.ASCIIEncoding.ASCII.GetString(b2);
                   // upass = System.Text.ASCIIEncoding.ASCII.GetString(b3);
                 //For older encryption decription start     
                //For new Encryption Decription With Key start here
                    dbname = enDeCls.Decrypt(Sline[1]);
                    uname = enDeCls.Decrypt(Sline[2]);
                    upass = enDeCls.Decrypt(Sline[3]);
                    
                //For new Encryption Decription With Key end here


                    // Console.WriteLine("db connection after decoding details ");
                   // Console.WriteLine(server);
                   // Console.WriteLine(dbname);
                   // Console.WriteLine(uname);
                   // Console.WriteLine(upass);          
                    //server = Sline[0];
                    //dbname = Sline[1];
                    //uname = Sline[2];
                   // upass = Sline[3];
                    constr = " Server=" + server + ";Database=" + dbname + ";UID=" + uname + ";Password=" + upass;
                    return constr;
                }
                catch (Exception ex)
                {
                    if (file != null)
                        file.Close();
                    return constr = ex.Message;

                }
            }
            else
            {
                return constr = "Please configure Database or Contact System Administrator";

            }
        }
    }
}
