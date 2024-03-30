using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace ScaleApp
{
    public partial class EncryptDecrypt : Form
    {
        public EncryptDecrypt()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = " ";
            //byte[] b;
           // string decrypted;
           // byte[] b1 = System.Text.ASCIIEncoding.ASCII.GetBytes(textBox1.Text);
            //string encrypted = Convert.ToBase64String(b1);
           // Console.WriteLine(encrypted);
            //textBox2.Text = encrypted;
           string encrypted = Encrypt(textBox1.Text);
             Console.WriteLine("Encrypt Text here === ");
            Console.WriteLine(textBox1.Text);
            Console.WriteLine(encrypted);
           textBox2.Text = encrypted;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            try
            {
               // byte[] b = Convert.FromBase64String(textBox3.Text);
               //String decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
               String decrypted=Decrypt(textBox3.Text);
               textBox2.Text = decrypted;
                Console.WriteLine("Decrypt Text here === ");
                Console.WriteLine(textBox3.Text);
                Console.WriteLine(decrypted);
            }catch(Exception ex) { }

        }

        private void EncryptDecrypt_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public  string Encrypt(string clearText) 
        {
            string EncryptionKey = "D@T@#Soft%2@22&Blpa"; // SecurityKey
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using(Aes encryptor = Aes.Create()) {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                 0x49,0x76,0x61,0x6e,0x20,0x4d,0x65,0x64,0x76,0x65,0x64,0x65,0x76
             });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using(MemoryStream ms = new MemoryStream()) {
              using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
              }
                clearText = Convert.ToBase64String(ms.ToArray());
              }
            }
             return clearText;
        }
        public  string Decrypt(string cipherText) {
            try 
            {
            string EncryptionKey = "D@T@#Soft%2@22&Blpa"; // SecurityKey
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using(Aes encryptor = Aes.Create()) {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49,0x76,0x61,0x6e,0x20,0x4d,0x65,0x64,0x76,0x65,0x64,0x65,0x76
            });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using(MemoryStream ms = new MemoryStream()) {
                 using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                     cs.Close();
                  }
            cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
            }
            return cipherText;
             } catch (Exception ex) {
            return "";
        }
        }

    }

}
