using System;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data;
using MySql.Data.MySqlClient;
using Rectangle = iTextSharp.text.Rectangle;

namespace ScaleApp
{
    public partial class userCreationForm : Form
    {
        public string username;
        public string scaleName;
        public int scale_id;        
        public string portName;
        public string portAdress;
       // public string ipAddress;
        public string constr = "";
        public string searchVal;
        public string ManifestUpdaeId, TruckUpdateId;
        private List<getScaleName> lstAutoCompleteData = null;
        private List<getPortName> lstAutoCompleteData_port = null;

        public int userID,j = 0, k;
        public string emid="", empName="", empFatherName="", empMotherName="", empDoB="", empMobile="", empEmail="", empDesignation="", empNid="";
        globalClass gc = new globalClass();
        LoginForm loginForm = new LoginForm();
        public string portId = LoginForm.port_id;
        ConnectionDetails connDtls = new ConnectionDetails();
        EncryptDecrypt  enDeCls=new EncryptDecrypt();
        // int usrId;
        
        public userCreationForm()
        {
            InitializeComponent();
            constr = connDtls.constring();
            GetScaleName(portId);
            GetPortName();
            infoGroupBox.Visible = false;
            showList(portId);
            updateBtn.Hide();
            usrid.Hide();
           // MessageBox.Show(portId);
        }

        private void comboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string port_value = comboPort.SelectedValue.ToString();
            showList(port_value);
            GetScaleName(port_value);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string employeeID = employeeIDTextBox.Text.Trim();
            string employeeName = empNameLabel.Text.Trim();
            string usrName = usrNameText.Text.Trim();
            string passwrod = passWordTextBox.Text.Trim();
            string scaleId = comboScale.SelectedValue.ToString();
            string port_id = comboPort.SelectedValue.ToString();
            string empId = empIdLabel.Text.Trim();
            string fatherName = fatherNameLabel.Text.Trim();
            string motherName = motherNameLabel.Text.Trim();
            string nid = nidLabel.Text.Trim();
            string email = emailLabel.Text.Trim();
            string mobileNo = mobileLabel.Text.Trim();
            string designa = designationLabel.Text.Trim();
            string dob = dobLabel.Text.Trim();
            string roleID = "6";
            string user_type = "port";
            string ipAddress= enDeCls.Encrypt(ipAddressText.Text.Trim());
             string macAddress= enDeCls.Encrypt(macAddressText.Text.Trim());
         
            int st = 0;
            string chekStat = "", portUsrChekStat="";// MessageBox.Show("user id : " + UserId);
           
            //MessageBox.Show(numTruck+"");

            //return;

            string usrCheck_query = @"  SELECT count(*) as count from users WHERE username='" + usrName + "' and port_id='" + port_id + "' LIMIT 1";

            string portUsr_query = @"  SELECT count(*) as cnt FROM port_user WHERE user_id='" + employeeID +"' and port_id='"+port_id+"'";
            Console.WriteLine(usrCheck_query);
            MySqlConnection con4 = new MySqlConnection(constr);
            MySqlCommand commd2 = new MySqlCommand(usrCheck_query, con4);
            MySqlCommand commd4 = new MySqlCommand(portUsr_query, con4);

            con4.Open();
            MySqlDataReader chekReader = commd2.ExecuteReader();
            if (chekReader.Read())
            {
                chekStat = chekReader.GetString("count");
            }

            chekReader.Close();

            MySqlDataReader portUsrChekReader = commd4.ExecuteReader();
            if (portUsrChekReader.Read())
            {
                portUsrChekStat = portUsrChekReader.GetString("cnt");
            }
            //MessageBox.Show(portUsrChekStat);
            portUsrChekReader.Close();
            con4.Close();

            if (employeeID == "" )
            {
                MessageBox.Show("Provide Employee Id.");
            }
            else if (empId == "")
            {
                MessageBox.Show("Sorry! this Employee Information not exist in Database.");
            }
            else if (empId == "")
            {
                MessageBox.Show("Sorry! this Employee Information not exist in Database.");
            }
            else if (scaleId == "")
            {
                MessageBox.Show("Please! Select a scale. ");
            }
            else if (chekStat == "1")
            {
                MessageBox.Show("This User Name aleady exist. ");
            }
            else if (!portUsrChekStat.Equals("0"))
            {
                MessageBox.Show("This User aleady exist in Port Users. ");
            }
            else if (chekStat=="1")
            {
                MessageBox.Show("This User Name aleady exist. ");
            }
            else if (!portUsrChekStat.Equals("0"))
            {
                MessageBox.Show("This User aleady exist in Port Users. ");
            }

            else
            {
                string insert_query= @"  INSERT into users(user_type, port_employee_id, name, mobile, email, role_id, username, password1,
                    user_status, father_name, mother_name, phone, designation, date_of_birth, nid_no, active, port_id)
                    values('"+user_type+"','" + empId + "','" + employeeName + "','" +mobileNo+"','" +email+"','" + roleID + "','" + usrName + "', SHA1('" + passwrod + "'), 1, '" + fatherName + "','" + motherName+"'," +
                     "'" +mobileNo+"','" +designa +"','" +dob +"','" +nid+"',1,'" + port_id + "')";
                Console.WriteLine(insert_query);
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmdInsert = new MySqlCommand(insert_query, con);
                st = cmdInsert.ExecuteNonQuery();
                con.Close();
                //int st = 1;
                Console.WriteLine(insert_query);
           
               con4.Open();

                if (st == 1)
                {
                    //con.Close();
                    //con.Open();
                    string strGetUserTableID = "SELECT id AS user_id FROM users where username='" + usrName + "' and port_id='" + port_id + "' LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(strGetUserTableID, con4);
                    MySqlDataReader mred1 = cmd.ExecuteReader();
                    while (mred1.Read())
                    {
                        userID = mred1.GetInt32("user_id");
                    }
                     mred1.Close();

                    string insert_portUser = @"  INSERT into port_user(port_id, user_id)
                            values('" + port_id + "','" + userID + "')";
                    MySqlCommand cmd2 = new MySqlCommand(insert_portUser, con4);
                    int st1 = cmd2.ExecuteNonQuery();

                    string insert_weighBridgeUser = @"  INSERT into weighbridge_users(user_id, scale_id, created_at,ip_address,mac_address)
                            values('" + userID + "','" + scaleId + "',now(),'"+ipAddress+"','"+macAddress+"')";

                    MySqlCommand cmd3= new MySqlCommand(insert_weighBridgeUser, con4);
                    int st2 = cmd3.ExecuteNonQuery();
                    if (st2 == 1 && st1==1)
                    {

                        MessageBox.Show("WeighBridge Scale User created Succesfully.");
                        //this.Close();
                       passWordTextBox.Text = "";
                       employeeIDTextBox.Text="";
                       usrNameText.Text="";
                       ipAddressText.Text="";
                       macAddressText.Text="";
                    }
                    else
                    {
                        MessageBox.Show("User not created.");
                    }

                    }
                    con4.Close();
                    //MessageBox.Show("Manifest No : "+manifest_no+" Goods:"+goods_id);
            }
           showList(portId);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        //WeighbridgeEntry form1 = new WeighbridgeEntry();
       

        private void showList(string port)
        {
            int j = 0;
            // constr = constring();
            if (constr.Equals("")) return;

            MySqlConnection con = new MySqlConnection(constr);
            // MessageBox.Show(gc.portId);
            WeighbridgeEntry wb = new WeighbridgeEntry();
            //MessageBox.Show(wb.portId);

            string sql = @"SELECT name,username,weighbridge_users.scale_id,weighbridge_users.ip_address,weighbridge_users.mac_address,weighbridges.scale_name, users.id, username
                        FROM users
                        INNER JOIN weighbridge_users ON weighbridge_users.user_id = users.id
                        INNER JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id
                        WHERE weighbridges.port_id = '" + port + "' AND NAME IS NOT NULL AND username IS NOT NULL";

            Console.WriteLine(sql);
            // MessageBox.Show(sql);
            MySqlCommand cmd = new MySqlCommand(sql, con);


            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();
            //j = 0; 
            if (mred.Read())
            {
                dataGridView1.Rows.Clear();
                while (mred.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[j].Cells["Column1"].Value = mred["name"].ToString();
                    dataGridView1.Rows[j].Cells["Column2"].Value = mred["username"].ToString();
                    dataGridView1.Rows[j].Cells["Column3"].Value = mred["scale_name"].ToString();
                    dataGridView1.Rows[j].Cells["Column4"].Value = mred["id"].ToString();
                    dataGridView1.Rows[j].Cells["Column5"].Value = enDeCls.Decrypt(mred["ip_address"].ToString());
                    dataGridView1.Rows[j].Cells["Column6"].Value = enDeCls.Decrypt(mred["mac_address"].ToString());
                     dataGridView1.Rows[j].Cells["Column7"].Value = "Update";

                    j++;
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
            }

            con.Close();
            dataGridView1.Refresh();
           // CollectionViewSource.GetDefaultView(dataGridView1.ItemsSource).Refresh();
        }


        private void employeeIDTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //  feildIndex = 3;
                searchVal = employeeIDTextBox.Text.Trim();
                showEmpData(searchVal);
            }
        }

        private void showEmpData(string searchVal)
        {

            string emp_query = @"  SELECT employees.id,emp_id,name, father_name,mother_name,mobile,email,date_of_birth,designation,national_id FROM employees
                                          LEFT JOIN employee_designations ON employee_designations.employee_id=employees.id
                                          LEFT JOIN designations ON employee_designations.desig_id=designations.id
                                          WHERE employees.emp_id='" + searchVal + "' OR employees.name LIKE '%"+ searchVal + "%' LIMIT 1";
            Console.WriteLine(emp_query);
            MySqlConnection conn2 = new MySqlConnection(constr);
            MySqlCommand command2 = new MySqlCommand(emp_query, conn2);
            conn2.Open();
            MySqlDataReader empReader = command2.ExecuteReader();
            employeeIDTextBox.Enabled = true;
            usrNameText.Enabled = true;

            if (empReader.Read())
            {

                empIdLabel.Text = empReader.IsDBNull(0) ? null : empReader.GetString(0);
                empNameLabel.Text = empReader.IsDBNull(2) ? null : empReader.GetString(2);
                fatherNameLabel.Text = empReader.IsDBNull(3) ? null : empReader.GetString(3);
                motherNameLabel.Text = empReader.IsDBNull(4) ? null : empReader.GetString(4);
                mobileLabel.Text = empReader.IsDBNull(5) ? null : empReader.GetString(5);

                emailLabel.Text = empReader.IsDBNull(6) ? null : empReader.GetString(6);
                dobLabel.Text = empReader.IsDBNull(7) ? null : empReader.GetString(7);
                designationLabel.Text = empReader.IsDBNull(8) ? null : empReader.GetString(8);
                nidLabel.Text = empReader.IsDBNull(9) ? null : empReader.GetString(9);

                infoGroupBox.Visible = true;

            }
            else
            {
                infoGroupBox.Visible = false;
            }

        }

        private void GetScaleName(string port)
        {
            // MessageBox.Show(port);
            int portIsNumChek = Convert.ToInt16(port);
            int intParsed;
            if (int.TryParse(port, out intParsed))
            {
                try
                {
                    lstAutoCompleteData = new List<getScaleName>();
                    // Auto
                    DataTable dt = new DataTable();
                    string strScale = "SELECT id,scale_name FROM weighbridges where port_id='" + port + "'";
                    MySqlConnection con = new MySqlConnection(constr);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(strScale, con);
                    MySqlDataReader mred = cmd.ExecuteReader();
                    lstAutoCompleteData.Add(new getScaleName { id = "", scale_name = "--Select Scale--" });
                    while (mred.Read())
                    {
                        lstAutoCompleteData.Add(new getScaleName { id = mred.GetString("id"), scale_name = mred.GetString("scale_name") });
                    }
                    comboScale.DataSource = lstAutoCompleteData;
                    comboScale.DisplayMember = "scale_name";
                    comboScale.ValueMember = "id";               
                    mred.Close();
                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private void GetPortName()
        {
            try
            {
                lstAutoCompleteData_port = new List<getPortName>();
                // Auto
                DataTable dt = new DataTable();
                string strPort = "SELECT id, port_name FROM ports";
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(strPort, con);
                MySqlDataReader mred = cmd.ExecuteReader();
              // lstAutoCompleteData.Add(new getScaleName { port_id = "", port_name = "--Select Port--" });
                while (mred.Read())
                {
                    lstAutoCompleteData_port.Add(new getPortName { id = mred.GetString("id"), port_name = mred.GetString("port_name") });
                }
                comboPort.DataSource = lstAutoCompleteData_port;
                comboPort.DisplayMember = "port_name";
                comboPort.ValueMember = "id";

                mred.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

      


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            // MessageBox.Show(value);
            if (value != null)
            {
                string emp_query = @"SELECT emp_id, users.name,username,weighbridge_users.scale_id, weighbridges.scale_name, users.id,weighbridge_users.ip_address,weighbridge_users.mac_address,weighbridges.port_id
                                FROM users
                                INNER JOIN weighbridge_users ON weighbridge_users.user_id = users.id
                                INNER JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id
                                LEFT JOIN employees ON employees.name=users.name
                                WHERE users.id ='" + value + "' LIMIT 1";
                Console.WriteLine(emp_query);
                MySqlConnection conn2 = new MySqlConnection(constr);
                MySqlCommand command2 = new MySqlCommand(emp_query, conn2);
                conn2.Open();
                MySqlDataReader empReader = command2.ExecuteReader();
                usrNameText.Enabled = false;
                employeeIDTextBox.Enabled = false;
                if (empReader.Read())
                {
                    employeeIDTextBox.Text = empReader.IsDBNull(0) ? null : empReader.GetString(0);
                    usrNameText.Text = empReader.IsDBNull(2) ? null : empReader.GetString(2);
                    comboScale.SelectedIndex = comboScale.FindString(empReader.IsDBNull(4) ? null : empReader.GetString(4));
                    usrid.Text = empReader.IsDBNull(5) ? null : empReader.GetString(5);
                    ipAddressText.Text=empReader.IsDBNull(6) ? null :  enDeCls.Decrypt(empReader.GetString(6));
                    macAddressText.Text=empReader.IsDBNull(7) ? null :  enDeCls.Decrypt(empReader.GetString(7));
                }

                saveBtn.Hide();
                updateBtn.Show();

            }
        }


        private void updateBtn_Click(object sender, EventArgs e)
        {

            string usrName = usrNameText.Text.Trim();
            string passwrod = passWordTextBox.Text.Trim();
            string scaleId = comboScale.SelectedValue.ToString();
            string port_id = comboPort.SelectedValue.ToString();
            string ipAddress= enDeCls.Encrypt(ipAddressText.Text.Trim());
            string macAddress= enDeCls.Encrypt(macAddressText.Text.Trim());
            string updateString = "", updateString2="";
            MySqlConnection conn = new MySqlConnection(constr);
            if (scaleId == "")
            {
                MessageBox.Show("Please! Select a scale. ");
            }
            else
            {

                if (!passwrod.Equals(""))
                {
                    // MessageBox.Show("1"+ passwrod);
                    updateString2 = "UPDATE users SET password1=SHA1('" + passwrod + "') WHERE id='" + usrid.Text.Trim() + "'";
                    MySqlCommand cmd1 = new MySqlCommand(updateString2, conn);
                    conn.Open();
                    int st1 = cmd1.ExecuteNonQuery();

                    updateString = "UPDATE weighbridge_users SET scale_id='" + scaleId + "',ip_address='"+ipAddress+"',mac_address='"+macAddress+"' WHERE user_id='" + usrid.Text.Trim() + "'";

                    Console.WriteLine(updateString);
                    MySqlCommand cmd = new MySqlCommand(updateString, conn);
                    // conn.Open();
                    int st = cmd.ExecuteNonQuery();
                    if (st == 1 & st1 == 1)
                    {
                        MessageBox.Show("User's password & scale number updated succesfully.");
                           passWordTextBox.Text = "";
                           employeeIDTextBox.Text="";
                           usrNameText.Text="";
                           ipAddressText.Text="";
                           macAddressText.Text="";
                           employeeIDTextBox.Enabled = true;
                           usrNameText.Enabled = true;
                        //this.Close();
                           saveBtn.Show();
                           updateBtn.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Not updated.");
                    }
                }
                else
                {
                    updateString = "UPDATE weighbridge_users SET scale_id='" + scaleId + "',ip_address='"+ipAddress+"',mac_address='"+macAddress+"' WHERE user_id='" + usrid.Text + "'";

                    Console.WriteLine(updateString);
                    MySqlCommand cmd = new MySqlCommand(updateString, conn);
                    conn.Open();
                    int st = cmd.ExecuteNonQuery();
                    if (st == 1)
                    {
                        MessageBox.Show("Scale number updated succesfully.");
                        //this.Close();
                           passWordTextBox.Text = "";
                           employeeIDTextBox.Text="";
                           usrNameText.Text="";
                           ipAddressText.Text="";
                           macAddressText.Text="";
                           employeeIDTextBox.Enabled = true;
                           usrNameText.Enabled = true;

                        //this.Close();
                           saveBtn.Show();
                           updateBtn.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Not updated.");
                    }
                }
                conn.Close();
            }

            showList(portId);
        }



    }
}
