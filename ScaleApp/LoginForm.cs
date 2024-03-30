using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Net.NetworkInformation;


namespace ScaleApp
{
   
    public partial class LoginForm : Form
    {
        public string usr = "";
        public static string port_id;
        public static string username;
        public static string user_id;
        public static string scaleName;
        public static int scale_id;
       // public static string portId;
        public static string portName;
        public static string portAdress;
        public static string connection_str;
        public string constr = "";
        ConnectionDetails connDtls = new ConnectionDetails();
        EncryptDecrypt  enDeCls=new EncryptDecrypt();
        public LoginForm()
        {
            InitializeComponent();
            constr = connDtls.constring();
        }

       
        
        private void button1_Click(object sender, EventArgs e)
        {
           WeighbridgeEntry frm = new WeighbridgeEntry();
                        frm.userName = "DEV TEAM";
                        frm.userID =1;
                        frm.portName = "Test";
                        frm.portId = "1";
                        frm.portAdress ="BLPA";
                        frm.constr=constr;
                        frm.Show();// this.Dispose();
        }
        
        private void button12_Click(object sender, EventArgs e)
        {
                       
            //Get ip address and Mac Adddress
            string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            string addr = "";
            foreach (NetworkInterface adapter in nics)
            {
                //if (sMacAddress == String.Empty)// only return MAC Address from first card  
               // {
                    //IPInterfaceProperties properties = adapter.GetIPProperties(); Line is not required
                    //sMacAddress = adapter.GetPhysicalAddress().ToString();
                //}
                if (adapter.OperationalStatus == OperationalStatus.Up) {
                       addr += adapter.GetPhysicalAddress().ToString();
                   break;
                  }
                
            }
            //MessageBox.Show(addr);
            // MessageBox.Show(sMacAddress);   
            //return;
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Plesae enter Login ID;");
                textBox1.Focus();
                return;

            }
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Plesae enter Password;");
                textBox2.Focus();
                return;
            }
            int pass = 0, actStat=0;
            if (constr.Equals("")) return;
            
            Console.WriteLine("db connection details login  ");
            Console.WriteLine(constr);

            MySqlConnection con = new MySqlConnection(constr);
            string query = @"SELECT COUNT(*) AS cnt,u.id, u.active,IFNULL(wu.ip_address,'') AS ip_address,IFNULL(wu.mac_address,'') AS mac_address, pu.port_id,p.port_name,p.port_alias,
                                p.Operator_description,p.port_add1,
                                CASE
                                WHEN u.active = '0' THEN '0' 
                                WHEN u.active = '1' THEN '1' 
                                WHEN u.active > '1' THEN '2' 
                                END  AS act
                                FROM users AS u
                                JOIN port_user AS pu ON pu.user_id=u.id
                                LEFT JOIN ports AS p ON p.id = pu.port_id 
                                LEFT JOIN weighbridge_users AS wu on wu.user_id=u.id
                                WHERE u.username='" + textBox1.Text.Trim()+ "' and u.password1=SHA1('" + textBox2.Text.Trim() + "')";
            Console.WriteLine(query);
            MySqlCommand cmd = new MySqlCommand(query, con);


            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();

            if (mred.Read())
            {
                //pass = mred.GetString("cnt");
                Console.WriteLine("TEAST 1");
                pass = mred.GetInt16("cnt");
                Console.WriteLine(pass);
                if (pass != 0)
                {
                    actStat = mred.GetInt16("act");
                    string port_name = mred.GetString("port_name");
                    string ipAddress=mred.GetString("ip_address");
                    string macAddress=mred.GetString("mac_address");
                   // MessageBox.Show(actStat.ToString()+ port_name)
                   //MessageBox.Show(enDeCls.Decrypt(macAddress));
                   //check ip address 
                    if(myIP!=enDeCls.Decrypt(ipAddress))
                    {
                       MessageBox.Show("You are not Permited for this Host.");
                       con.Close();
                       return;
                    }
                    //check mac address 
                     if(addr!=enDeCls.Decrypt(macAddress))
                    {
                       MessageBox.Show("You are not Permited for this Host.");
                       con.Close();
                       return;
                    }


                    if (pass >= 1 & actStat == 1)
                    {
                        ReportViewForm rvForm = new ReportViewForm();

                        rvForm.username = textBox1.Text;
                        usr = textBox1.Text;
                        int userId = mred.GetUInt16("id");
                        WeighbridgeEntry frm = new WeighbridgeEntry();
                        //userCreationForm usrfrm = new userCreationForm();
                        this.Hide();
                        frm.userName = textBox1.Text;
                        frm.userID = userId;
                        frm.portName = mred.GetString("port_name");
                        frm.portId = mred.GetString("port_id");
                        frm.portAdress = mred.GetString("port_add1");

                        port_id = mred.GetString("port_id");
                        username = textBox1.Text;
                        user_id = userId.ToString(); ;
                        portName = mred.GetString("port_name")+", " +mred.GetString("port_alias");
                        portAdress = mred.GetString("port_add1");
                        connection_str = constr;

                        globalClass gc = new globalClass();

                        gc.userName = textBox1.Text;
                        gc.userID = userId;
                        gc.portName = mred.GetString("port_name");
                        gc.portId = mred.GetString("port_id");
                        gc.portAdress = mred.GetString("port_add1");
                        gc.constr = constr;

                       // MessageBox.Show(gc.portId);
                        frm.constr = constr;
                        frm.Show();// this.Dispose();
                        con.Close();   

                    }

                    else if (pass >= 1 & actStat == 2)
                    {

                        MessageBox.Show("Your login status is Pending.");
                        con.Close();
                    }
                    else if (pass >= 0 & actStat == 0)
                    {

                        MessageBox.Show("Your login status is not activate.");
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username/Password!");
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Username/Password!");
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Plesae enter Correct Login ID;");
                textBox1.Focus();
                con.Close();
                return;
            }
            con.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(""))
                {
                    MessageBox.Show("Plesae enter Login ID;");
                    textBox1.Focus();
                    return;

                }
                
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (textBox2.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Password ");
                    textBox2.Focus();
                    return;

                }

                button1.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void WeighScaleLoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
