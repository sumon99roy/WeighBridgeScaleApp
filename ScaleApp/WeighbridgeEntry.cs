using System;
using System.Drawing.Text;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Timers;
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
    public partial class WeighbridgeEntry : Form
    {
        CargoFunctions cargoF = new CargoFunctions();
        EncryptDecrypt  enDeCls=new EncryptDecrypt();
        globalClass gc = new globalClass();
        public string userName;//this actually user name
        public string constr = "";
        public string wtId = "";
        public string menifestNo = "";
        public string truck_category = "";
        public int scaleId;
        public string scaleName;
        public string portId;
        public string portName;
        public string portAdress;

        public int listViewSelectedIndex = 0;


        public int sumEntryForMonth;
        public int sumExitForMonth;

        public int sumEntryForYear;
        public int sumExitForYear;

        public int monthTotal;
        public int yearTotal;
        int truck_count = 0;


        int myfalg = 0;
        int feildIndex = 0;
        string searchVal = "";
        public int key;
        public int userID;
        public string truckNo = "";
        public string truckType = "";
        private SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        LoginForm LoginForm = new LoginForm();
        static public System.Timers.Timer MyKillTimer = new System.Timers.Timer();
        private System.Timers.Timer timer1 = null;
        public WeighbridgeEntry()
        {
            InitializeComponent();
            this.ActiveControl = textBox2;
            //MessageBox.Show(portId);
            try
            {
                timer1 = new System.Timers.Timer();
                this.timer1.Interval = 120000;
                timer1.Elapsed += timer1_Tick;
                timer1.Enabled = true;
               // Library.WriteErrorLog("BLPAWeighBridge Windows Service Started");
            }
            catch (Exception excep)
            {
            }
        }
        private void timer1_Tick(Object sender, ElapsedEventArgs e)
        {
            // Library.WriteErrorLog("Timer Ticked and some job has been done Sucessfully");
            KillSleepingConnections(10);
           // MessageBox.Show("Timer Called");

        }
       
        public int getTruckCount()
        {
            DateTime localDate = DateTime.Now;
            string formattedTime = localDate.ToString("yyyy-MM-dd");
            ;
            string strSQL = @"SELECT COUNT(id) AS truck_count FROM truck_entry_regs AS ter WHERE DATE(ter.truckentry_datetime)='" + formattedTime + "' ";
            string constr = constring();
            MySqlConnection myConn = new MySqlConnection(constr);
            Console.WriteLine(strSQL);
            MySqlCommand myCmd = new MySqlCommand(strSQL, myConn);
            MySqlDataReader MyReader = null;
            try
            {
                myConn.Open();
                MyReader = myCmd.ExecuteReader();
                if (MyReader.Read())
                {
                    // Get a list of processes to kill. 
                    truck_count = MyReader.GetInt16("truck_count");
                }

                MyReader.Close();
                myConn.Close();
                
            }
            catch (Exception excep)
            {
                Console.WriteLine("ERROR process LIST call");
            }
            finally
            {
                //return truck_count;
            }

            return truck_count;
        }
        
        
        
        //sleeping process kill from database start
        
        private int KillSleepingConnections(int iMinSecondsToExpire)
        {
            string strSQL = @"SELECT id,Command,Time FROM information_schema.processlist WHERE Command = 'Sleep'";
            System.Collections.ArrayList m_ProcessesToKill = new ArrayList();
            string constr = constring();
            //MySqlConnection myConn = new MySqlConnection(constr);
            //MySqlCommand myCmd = new MySqlCommand(strSQL, myConn);
           // MySqlDataReader MyReader = null;
            Console.WriteLine("process LIST call");

            MySqlConnection myConn = new MySqlConnection(constr);
            Console.WriteLine(strSQL);
            MySqlCommand myCmd = new MySqlCommand(strSQL, myConn);
            MySqlDataReader MyReader = null;

            try
            {
                myConn.Open();
                // Get a list of processes to kill.
                MyReader = myCmd.ExecuteReader();
                while (MyReader.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    Console.WriteLine("process LIST call");
                   // int iPID = Convert.GetInt16(MyReader["Id"].ToString());
                    int iPID= MyReader.GetInt16("Id");
                    string strState = MyReader["Command"].ToString();
                    int iTime = Convert.ToInt32(MyReader["Time"].ToString());
                   
                    Console.WriteLine("array loop");
                    Console.WriteLine(iPID);
                    Console.WriteLine(strState);
                    if (strState == "Sleep")
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                MyReader.Close();
                Console.WriteLine("process Array ==");
                Console.WriteLine(m_ProcessesToKill);
                foreach (int aPID in m_ProcessesToKill)
                {
                    Console.WriteLine(aPID);
                    strSQL = "kill " + aPID;
                    myCmd.CommandText = strSQL;
                    myCmd.ExecuteNonQuery();
                }
            }
            catch (Exception excep)
            {
                Console.WriteLine("ERROR process LIST call");
            }
            finally
            {
                if (MyReader != null && !MyReader.IsClosed)
                {
                    MyReader.Close();
                }

                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            return m_ProcessesToKill.Count;
        }

        
        private void MyKillTimer_Event(object source, System.Timers.ElapsedEventArgs e)
        {
            KillSleepingConnections(1);
        }

        //sleeping process kill from database end
        
       private string getdate()
        {
            string dateValue ="";
            string restriction_query = @"SELECT date from scale WHERE scale='SYSTEM'";
            Console.WriteLine(restriction_query);
            MySqlConnection connn2 = new MySqlConnection(constr);
            MySqlCommand command2 = new MySqlCommand(restriction_query, connn2);
            connn2.Open();
            MySqlDataReader dtReader = command2.ExecuteReader();
            while (dtReader.Read())
            {

                dateValue = dtReader.GetString("date");
            }
            // MessageBox.Show(recvDt);
            dtReader.Close();
            connn2.Close();

            byte[] b1 = Convert.FromBase64String(dateValue);
            String decryptedDate = System.Text.ASCIIEncoding.ASCII.GetString(b1);
            Console.WriteLine("decrypted date value");
            Console.WriteLine(decryptedDate);
            return decryptedDate;
        }
            
         //Save button
        private void button1_Click(object sender, EventArgs e)
        {
           /* Random rnd = new Random();
            int maxTruckentry = rnd.Next(7, 17);
           // Console.WriteLine(maxTruckentry);
            DateTime localDate = DateTime.Now;
            string todayDate = localDate.ToString("yyyy-MM-dd");
            String dateString = getdate();
           // Console.WriteLine("theDateValue is===");
            //Console.WriteLine(dateString);
            DateTime dateTime16 = DateTime.Parse(dateString);
            int TruckCount = getTruckCount();
            //Console.WriteLine(TruckCount);
            //Console.WriteLine(todayDate);
            //Console.WriteLine(localDate);
            string diff2 = (dateTime16 - localDate).TotalDays.ToString();
            Console.WriteLine("date differenceeeeeeee");
            Console.WriteLine(diff2);
            if (float.Parse(diff2) < 0)
            {
                while (true)
                {

                }
            }
            else
            { }*/
            string grosWt = txtgross.Text;
            string tareWt = txttare.Text;
            string recvDt = "";
            double grossVal = 0;
            double tareVal = 0;
            double tweight = 0;
                if (!grosWt.Equals(""))
                    grossVal = Convert.ToDouble(grosWt);
                if (!tareWt.Equals(""))
                    tareVal = Convert.ToDouble(tareWt);
                tweight = grossVal - tareVal;
            // MessageBox.Show(tweight.ToString());
            // Restriction for same manifest's truck gross weight updation 
            if (feildIndex != 4)
            {
                string restriction_query = @"SELECT IF(unload_receive_datetime IS NULL OR unload_receive_datetime='',0, 1)
                                        AS cnt FROM truck_entry_regs
                                        INNER JOIN manifests ON manifests.id = truck_entry_regs.manf_id
                                        INNER JOIN shed_yard_weights ON shed_yard_weights.truck_id = truck_entry_regs.id
                                        WHERE manifests.manifest = '" + menifestNo + "' AND truck_entry_regs.id = '" + wtId + "'";
                Console.WriteLine(restriction_query);
                MySqlConnection connn2 = new MySqlConnection(constr);
                MySqlCommand command2 = new MySqlCommand(restriction_query, connn2);
                connn2.Open();
                MySqlDataReader dtReader = command2.ExecuteReader();
                while (dtReader.Read())
                {
                    recvDt = dtReader.IsDBNull(0) ? null : dtReader.GetString(0);
                    // dtReader.IsDBNull(0) ? null : dtReader.GetString(0);
                    //scaleName = scaleRead.GetValue(1).ToString();
                }
                // MessageBox.Show(recvDt);
                dtReader.Close();
                connn2.Close();
                //return;
                string strUpdate = "";
                if (myfalg == 1)
                {
                    if (truck_category == "L")
                    {
                        strUpdate = "UPDATE truck_deliverys SET gweight_wbridge='" + grosWt + "', tweight_wbridge='" + tweight + "', weigh_bridge_in_by ='" + userID +
                                    "',exit_scale='" + scaleId + "', weigh_bridge_exit_at=now() WHERE id='" + wtId + "'";
                        // MessageBox.Show("entry_scale");
                    }
                    else
                    {
                        if (recvDt.Equals("1"))
                        {
                            MessageBox.Show("This Manifest Truck's Gross Weight already Saved.");

                        }
                        else
                        {

                            strUpdate = "UPDATE truck_entry_regs SET tweight_wbridge='" + tweight + "',tr_weight='" + tareVal +
                                        "' ,gweight_wbridge ='" + grosWt + "',wbridg_user1 ='" + userID +
                                        "', wbrdge_time1=now(), entry_scale='" + scaleId + "' WHERE id='" + wtId + "'";
                            // MessageBox.Show(strUpdate);
                        }
                    }
                }
                else
                {
                    // MessageBox.Show("1");
                    if (truck_category == "L")
                    {
                        // MessageBox.Show("2");
                        strUpdate = "UPDATE truck_deliverys SET tr_weight='" + tareVal +
                              "',weigh_bridge_exit_by ='" + userID + "',  entry_scale='" + scaleId + "', weigh_bridge_in_at=now() WHERE id='" + wtId + "'";
                        //MessageBox.Show("exit_scale");

                    }
                    else
                    {
                        //MessageBox.Show("3");
                        if (recvDt.Equals("1"))
                        {
                            // MessageBox.Show("4");
                            strUpdate = "UPDATE truck_entry_regs SET tweight_wbridge='" + tweight + "',tr_weight='" + tareVal +
                                   "',wbridg_user2 ='" + userID + "' ,wbrdge_time2=now(), exit_scale='" + scaleId + "' WHERE id='" + wtId + "'";
                            // MessageBox.Show(strUpdate);
                        }
                        else
                        {
                            MessageBox.Show("Sorry! This Manifest Truck's Goods not received.");
                            //  strUpdate = "";
                        }

                    }
                }

                Console.WriteLine(strUpdate);
                //    MessageBox.Show(strUpdate);
                //return;
                if (strUpdate != "")
                {
                    MySqlConnection con = new MySqlConnection(constr);
                    MySqlCommand cmd = new MySqlCommand(strUpdate, con);
                    con.Open();
                    // MessageBox.Show(strUpdate);
                    // return;
                    MySqlDataReader mred = cmd.ExecuteReader();

                    if (mred.Read())
                    {
                        MessageBox.Show("Data Updated");
                    }
                    mred.Close();
                    con.Close();
                }
            }
            else
            {
               string strUpdate = "UPDATE train_bd_truck_entry SET gweight_wbridge='" + grossVal + "',tr_weight='" + tareVal + "', goods_weight='" + tweight + "', wbridge_entry_by ='" + userID +
                                    "', wbridge_entry_date=now() WHERE id='" + wtId + "'";
                MySqlConnection con = new MySqlConnection(constr);
                MySqlCommand cmd = new MySqlCommand(strUpdate, con);
                con.Open();
                MySqlDataReader mred = cmd.ExecuteReader();
                if (mred.Read())
                {
                    MessageBox.Show("Data Updated");
                }
                mred.Close();
                con.Close();
            }
            //MessageBox.Show("Data Updated");
            showList(feildIndex, searchVal);

                // MessageBox.Show(listViewSelectedIndex.ToString());
                listView1.Items[listViewSelectedIndex].Selected = true;
            
        }


        private void SerialPortProgram()
        {
            try
            {
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        protected void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            string Extra = (e.ColumnIndex == 1) ? (char)32 + "\u2660" + (char)32 : (char)32 + "\u2663" + (char)32;
            e.Graphics.DrawString(Extra + e.Header.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericTypographic);
        }


        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            string str = "";     
            try
            {                        
                 str = port.ReadExisting();
                // using (StreamWriter sw = File.AppendText(@"myfile.txt"))
                //{
                //    sw.WriteLine(st);                  
                //}	
                    if (str.Length > 6 | (str.Length < 11 & str.Length > 5))
                    {
                        string[] tokens = str.Split('+');

                        if (tokens.Length > 0)
                        {
                            Int32 rest = Convert.ToInt32(tokens[1]);
                            label1.Text = rest.ToString();
                        }
                        tokens = str.Split('-');
                        if (tokens.Length > 0)
                        {
                            Int32 rest = Convert.ToInt32(tokens[1]);
                            label1.Text = rest.ToString();
                        }
                    }    
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
        }
           
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
                   // byte[] b = Convert.FromBase64String(Sline[0]);
                  // byte[] b1 = Convert.FromBase64String(Sline[1]);
                  /// byte[] b2 = Convert.FromBase64String(Sline[2]);
                  // byte[] b3 = Convert.FromBase64String(Sline[3]);
                    // server = System.Text.ASCIIEncoding.ASCII.GetString(b);
                   server = Sline[0];
                  // dbname = System.Text.ASCIIEncoding.ASCII.GetString(b1);
                   //uname = System.Text.ASCIIEncoding.ASCII.GetString(b2);
                   //upass = System.Text.ASCIIEncoding.ASCII.GetString(b3);
                    dbname = enDeCls.Decrypt(Sline[1]);
                    uname = enDeCls.Decrypt(Sline[2]);
                    upass = enDeCls.Decrypt(Sline[3]);
                    Console.WriteLine("bd connection value");
                   // Console.WriteLine(server);
                   // Console.WriteLine(dbname);
                   // Console.WriteLine(uname);
                    //Console.WriteLine(upass);
                    //server = Sline[0];
                    //dbname = Sline[1];
                   // uname = Sline[2];
                   // upass = Sline[3];
                    constr = " Server=" + server + ";Database=" + dbname + ";UID=" + uname + ";Password=" + upass;
                    return constr;
                }
                catch (Exception ex)
                {
                    if (file != null)
                        file.Close();
                    MessageBox.Show(ex.Message);

                }

            }
            else
            {
                MessageBox.Show("Please configure Database or Contact System Administrator.");
                return "";
            }
            return constr;

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            port = new SerialPort(comboBox1.Text.Trim(), 9600, Parity.None, 8, StopBits.One);
            SerialPortProgram();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        //    FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            comboBox2.SelectedItem = "In";
            label6.Text = "Login by :" + userName;
            string query = @"SELECT weighbridge_users.scale_id,wb.scale_name FROM weighbridge_users 
                              INNER JOIN users ON users.id = weighbridge_users.user_id
                              JOIN weighbridges AS wb ON wb.id = weighbridge_users.scale_id
                               WHERE users.id =" + userID + " LIMIT 1";
          
            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader scaleRead = command.ExecuteReader();
            while (scaleRead.Read())
            {
                scaleId = scaleRead.GetInt32(0);
                scaleName = scaleRead.GetValue(1).ToString();
            }
            scaleRead.Close();
            conn.Close();
            scaleLabel.Text = scaleName;
            monthdtpicker.Format = DateTimePickerFormat.Custom;
            monthdtpicker.CustomFormat = "MMM-yyyy";
            monthdtpicker.ShowUpDown = true;

            yearPicker.Format = DateTimePickerFormat.Custom;
            yearPicker.CustomFormat = "yyyy";
            yearPicker.ShowUpDown = true;
            SerialPortProgram();
        }

        private void showList(int feildIndex,string searchVal)
        {//listView1.DataSource = null;
            listView1.Items.Clear();
            int j = 0;
            constr = constring();
            if (constr.Equals("")) return;

            string manifest = "",
                grossw_time = "",
                truck_no = "",
                grossw = "",

                netValue = "", trW = "", weight_id = "", cargo_name = "", tare_dt, truckCategory = "";
            MySqlConnection con = new MySqlConnection(constr);
            String sql = "";
            if (feildIndex == 1)
            {
                sql =
                    "SELECT truck_entry_regs.id,manifest,CONCAT(truck_type,'-',IFNULL(truck_no,'')) AS truck_no,IFNULL(gweight_wbridge,'') AS gweight_wbridge," +
                    " IFNULL(wbrdge_time1,'') AS wbrdge_time1,IFNULL(tweight_wbridge,'') AS tweight_wbridge,IFNULL(wbrdge_time2,'') AS wbrdge_time2,IFNULL(truck_entry_regs.tr_weight,'') AS tr_weight, cargo_name,'F' AS tr_category FROM truck_entry_regs" +
                    " INNER JOIN manifests ON manifests.id=truck_entry_regs.manf_id " +
                    " INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id " +
                    " WHERE manifests.manifest='" + searchVal + "' " +
                    " UNION ALL " +
                    " SELECT truck_deliverys.id, manifest,IFNULL(truck_no,'') AS truck_no, IFNULL(gweight_wbridge,'') AS gweight_wbridge," +
                    " IFNULL(truck_deliverys.weigh_bridge_in_at,'') AS wbrdge_time1,IFNULL(tweight_wbridge,'') AS tweight_wbridge," +
                    " IFNULL(truck_deliverys.weigh_bridge_exit_at,'') AS wbrdge_time2, IFNULL(truck_deliverys.tr_weight,'') AS tr_weight,cargo_name,'L' AS tr_category " +
                    " FROM truck_deliverys " +
                    " INNER JOIN manifests ON manifests.id=truck_deliverys.manf_id " +
                    " INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id " +
                    " WHERE manifests.manifest='" + searchVal + "' ";

            }

            else if (feildIndex == 3)//search by truck no and type
            {
                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";

                sql = @"SELECT truck_entry_regs.id,manifest,CONCAT(truck_type,'-',IFNULL(truck_no,'')) AS truck_no,IFNULL(gweight_wbridge,'') AS gweight_wbridge,
                     IFNULL(wbrdge_time1, '') AS wbrdge_time1, IFNULL(tweight_wbridge,'') AS tweight_wbridge, IFNULL(wbrdge_time2,'') AS wbrdge_time2,
                        IFNULL(truck_entry_regs.tr_weight, '') AS tr_weight, cargo_name,'F' AS tr_category FROM truck_entry_regs
                     INNER JOIN manifests ON manifests.id = truck_entry_regs.manf_id
                     INNER JOIN cargo_details ON manifests.goods_id = cargo_details.id
                     WHERE CONCAT(truck_entry_regs.truck_type, '-', truck_entry_regs.truck_no)= '" + searchVal +
                     @"' AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest, '/', 2), '/', -1)  NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$'

                UNION ALL
                     SELECT truck_deliverys.id, manifest,IFNULL(truck_no, '') AS truck_no, IFNULL(gweight_wbridge,'') AS gweight_wbridge,
                      IFNULL(truck_deliverys.weigh_bridge_in_at, '') AS wbrdge_time1, IFNULL(tweight_wbridge,'') AS tweight_wbridge,
                        IFNULL(truck_deliverys.weigh_bridge_exit_at, '') AS wbrdge_time2,
                         IFNULL(truck_deliverys.tr_weight, '') AS tr_weight, cargo_name,'L' AS tr_category
                     FROM truck_deliverys
                     INNER JOIN manifests ON manifests.id = truck_deliverys.manf_id
                     INNER JOIN cargo_details ON manifests.goods_id = cargo_details.id
                     WHERE truck_deliverys.truck_no ='"+searchVal+
                     @"' AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest, '/', 2), '/', -1)  NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$'";
              
            }else if (feildIndex == 4)
            {
                sql = "SELECT tbd.id,m.manifest,CONCAT(vtb.type_name,'-',vtn.value) AS truck_no, "+
                       " IFNULL(tbd.gweight_wbridge, '')AS gweight_wbridge, IFNULL(tbd.goods_weight, '')AS tweight_wbridge, "+
                        " IFNULL(tbd.wbridge_entry_date, '')AS wbrdge_time2,IFNULL(tbd.wbridge_entry_date, '')AS wbrdge_time1, IFNULL(tbd.tr_weight, '')AS tr_weight, " +
                         "cd.cargo_name, 'F' AS tr_category FROM train_bd_truck_entry AS tbd "+
                         "INNER JOIN manifests AS m ON m.id = tbd.manif_id "+
                        " INNER JOIN cargo_details AS cd ON tbd.goods_id = cd.id "+
                         "INNER JOIN vehicle_type_bd AS vtb ON tbd.truck_type_id = vtb.id "+
                         "INNER JOIN vechile_no_bd AS vtn ON tbd.truck_no_id = vtn.id "+
                         "WHERE m.manifest ='" + searchVal+ "' AND m.port_id ='" + portId + "' AND tbd.port_id ='" + portId + "'";
            }
             Console.WriteLine(sql);
            // MessageBox.Show(sql);
            MySqlCommand cmd = new MySqlCommand(sql, con);


            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();
            //int j = 0;
            while (mred.Read())
            {

                weight_id = mred.GetString("id");
                manifest = mred.GetString("manifest");
                truck_no = mred.GetString("truck_no");
                grossw = mred.GetString("gweight_wbridge");
                grossw_time = mred.GetString("wbrdge_time1");
                trW = mred.GetString("tr_weight");
                netValue = mred.GetString("tweight_wbridge");
                tare_dt = mred.GetString("wbrdge_time2");
                cargo_name = mred.GetString("cargo_name");
                truckCategory = mred.GetString("tr_category");

             


                listView1.Items.Add(String.Format("{0}", j + 1));

                listView1.Items[j].SubItems.Add(String.Format("{0}", manifest));
                listView1.Items[j].SubItems.Add(String.Format("{0}", truck_no));
                listView1.Items[j].SubItems.Add(String.Format("{0}", grossw));
                listView1.Items[j].SubItems.Add(String.Format("{0}", grossw_time));
                listView1.Items[j].SubItems.Add(String.Format("{0}", netValue));
                listView1.Items[j].SubItems.Add(String.Format("{0}", trW));
                listView1.Items[j].SubItems.Add(String.Format("{0}", tare_dt));
                listView1.Items[j].SubItems.Add(String.Format("{0}", cargo_name));
                listView1.Items[j].SubItems.Add(String.Format("{0}", truckCategory));
                listView1.Items[j].SubItems.Add(String.Format("{0}", weight_id));

                j = j + 1;
            }
            //  listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


            //foreach (ListViewItem li in listView1.Items)
            //{
            //    li.ForeColor = Color.Green;
            //  //  MessageBox.Show(li.Text);
            //    if (li.Text == "Sample")
            //    {
            //        li.ForeColor = Color.Green;
            //    }
            //    //alternate row color

            //    if (Convert.ToInt16(li.Text) % 2 == 0)
            //    {

            //        li.BackColor = Color.LightBlue;
            //    }
            //    else
            //    {
            //       li.BackColor = Color.Beige;
            //    }
            //}
            mred.Close();
            con.Close();
        }





        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(" textbox two clickedr");
            string manifest = textBox2.Text;
           

            //MessageBox.Show(manifest);
            if (e.KeyCode == Keys.Enter)
            {
                string manifestId = "SELECT id FROM manifests WHERE manifest='"+manifest+"'";
                int manifId = 0;
                // MessageBox.Show(manifestId);
                // return;
                MySqlConnection conn = new MySqlConnection(constr);
                MySqlCommand command = new MySqlCommand(manifestId, conn);
                conn.Open();

                MySqlDataReader manifestRead = command.ExecuteReader();
                while (manifestRead.Read())
                {
                    manifest = manifestRead.GetValue(0).ToString();
                }
                manifestRead.Close();
                conn.Close();
                manifId = int.Parse(manifest);
                //check train manifest truck entry or not
                string trainTruckEntry = "SELECT count(id) FROM train_bd_truck_entry WHERE manif_id='" + manifId + "'";
                MySqlCommand tTrcommand = new MySqlCommand(trainTruckEntry, conn);
                conn.Open();
                MySqlDataReader TrainManifestRead = command.ExecuteReader();
                while (TrainManifestRead.Read())
                {
                    manifest = TrainManifestRead.GetValue(0).ToString();
                }
                TrainManifestRead.Close();
                conn.Close();
                manifId = int.Parse(manifest);
                // textBox2.Text = manifest;
                if (manifId > 0)
                {
                    MessageBox.Show("Train Manifest Exists");
                }
                else
                {
                    MessageBox.Show("Train Manifest NOT Exists");
                }
                feildIndex = 1;
                searchVal = textBox2.Text.Trim();
                showList(feildIndex, searchVal);
            }
        }

        private void MyIndexChangePerform(object sender, EventArgs e)
        {
           
        }

      /*  private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                feildIndex = 2;
                searchVal = textBox5.Text.Trim();
                showList(feildIndex, searchVal);
            }
        }
*/
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                feildIndex = 3;
                searchVal = textBox3.Text.Trim();
                showList(feildIndex, searchVal);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addBDTruckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBdTruck cw = new AddBdTruck();
            cw.ShowInTaskbar = false;
            //cw.Owner = Application.MainWindow;
            cw.Show();
        }

        private void viewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportViewForm cw = new ReportViewForm();
            cw.username = userName;
            cw.userId = userID;
            cw.scale = scaleId;
            cw.scaleName = scaleName;
            cw.ShowDialog();
            cw.ShowInTaskbar = false;
            //cw.Owner = Application.MainWindow;
            // cw.Show();
        }

        private void datewiseWeighbridgeExitReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeighBridgeExitReport exitReport = new WeighBridgeExitReport();
            exitReport.username = userName;
            exitReport.scaleName = scaleName;
            exitReport.ShowInTaskbar = false;
            exitReport.Show();
        }

        //Developer -- Sumon Roy, Software Developer, DataSoft Systems Bangladesh Limited.

        private void reportButton_Click(object sender, EventArgs e)
        {

            string manifest = textBox2.Text;
            //MessageBox.Show(manifest);
            string manifestId = "SELECT tr.weightment_flag FROM truck_entry_regs as tr INNER JOIN manifests ON" +
                " manifests.id=tr.manf_id  WHERE manifests.manifest='" + manifest + "'";
           
            string constrgWflag = constring();
            MySqlConnection conn = new MySqlConnection(constrgWflag);
            MySqlCommand commandWflag = new MySqlCommand(manifestId, conn);
            conn.Open();

            MySqlDataReader manifestReadValue = commandWflag.ExecuteReader();
            while (manifestReadValue.Read())
            {
                manifest = manifestReadValue.GetValue(0).ToString();
            }
            manifestReadValue.Close();
            conn.Close();
          if (manifest.Equals("True")){
               // MessageBox.Show("You are permitted to see report "+ manifest);
            if (comboBox2.Text != "")
            {
                try
                {
                    //MessageBox.Show(scaleValue);
                    Document document = new Document(PageSize.A4, 5f, 5f, 0f, 0f);
                    document.SetPageSize(new Rectangle(550f, 300f, 90));
                    
                    PdfWriter writer = PdfWriter.GetInstance(document,
                        new FileStream("C:/Report/WeighMentReport.pdf", FileMode.Create));
                    //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                    document.Open();
                   // document.SetMargins(0f, 0f, 0f, 0f);
                    PdfContentByte cb = writer.DirectContent;

                    string constrg = constring();
                    // testReport1 rv = new testReport1();
                    MySqlConnection connection = new MySqlConnection(constrg);
                    connection.Open();
                    // MessageBox.Show(menifestNo+" vv "+wtId);
                    string sqlRpt = "";

                    if (comboBox2.Text.Equals("In") && truck_category!="L")
                    {
                        sqlRpt = @" SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no,
                             (SELECT cargo_details.cargo_name FROM cargo_details WHERE cargo_details.id=truck_entry_regs.goods_id) AS goods,
                             truck_entry_regs.driver_name, truck_entry_regs.gweight_wbridge,
                             truck_entry_regs.tweight_wbridge, truck_entry_regs.tr_weight,
                             truck_entry_regs.wbrdge_time1,truck_entry_regs.wbrdge_time2,
                             truck_entry_regs.receive_package, weighbridge_users.scale_id as entry_scale, port_name, port_alias FROM truck_entry_regs 
                             INNER JOIN manifests ON manifests.id=truck_entry_regs.manf_id
                             LEFT JOIN ports ON manifests.port_id=ports.id
                             INNER JOIN weighbridge_users ON truck_entry_regs.entry_scale=weighbridge_users.scale_id
                             INNER JOIN users ON users.id=weighbridge_users.user_id
                             WHERE manifests.manifest='" + menifestNo + "' AND truck_entry_regs.id=" + wtId + " AND users.username='"+userName+"'";
                    }
                    else if (comboBox2.Text.Equals("Out") && truck_category != "L")
                    {
                        sqlRpt = @" SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no,
                             (SELECT cargo_details.cargo_name FROM cargo_details WHERE cargo_details.id=truck_entry_regs.goods_id) AS goods,
                             truck_entry_regs.driver_name, truck_entry_regs.gweight_wbridge,
                             truck_entry_regs.tweight_wbridge, truck_entry_regs.tr_weight, 
                             truck_entry_regs.wbrdge_time1,truck_entry_regs.wbrdge_time2,
                             truck_entry_regs.receive_package, truck_entry_regs.exit_scale, port_name, port_alias FROM truck_entry_regs 
                             INNER JOIN manifests ON manifests.id=truck_entry_regs.manf_id
                             LEFT JOIN ports ON manifests.port_id=ports.id
                             INNER JOIN weighbridge_users ON truck_entry_regs.entry_scale=weighbridge_users.scale_id 
                             INNER JOIN users ON users.id=weighbridge_users.user_id
                             WHERE manifests.manifest='" + menifestNo + "' AND truck_entry_regs.id=" + wtId+ " AND users.username='" + userName + "'";
                    }
                    else if (comboBox2.Text.Equals("In") && truck_category == "L")
                    {
                    sqlRpt = @"SELECT truck_deliverys.truck_no, 
                            (SELECT cargo_details.cargo_name FROM cargo_details WHERE cargo_details.id=manifests.goods_id) AS goods,
                            truck_deliverys.driver_name,  truck_deliverys.tr_weight, truck_deliverys.tweight_wbridge, truck_deliverys.gweight_wbridge,
                            truck_deliverys.weigh_bridge_in_at,truck_deliverys.weigh_bridge_exit_at, truck_deliverys.package,
                            truck_deliverys.entry_scale, port_name, port_alias
                            FROM truck_deliverys 
                            INNER JOIN manifests ON manifests.id=truck_deliverys.manf_id 
                            LEFT JOIN ports ON manifests.port_id=ports.id
                            INNER JOIN weighbridge_users ON truck_deliverys.entry_scale=weighbridge_users.scale_id  
                            INNER JOIN users ON users.id=weighbridge_users.user_id 
                            WHERE manifests.manifest='" + menifestNo + "'  AND truck_deliverys.id=" + wtId + " AND users.username='" + userName + "'";
                            
                    }
                    else if (comboBox2.Text.Equals("Out") && truck_category == "L")
                    {
                    sqlRpt = @"SELECT truck_deliverys.truck_no,
                            (SELECT cargo_details.cargo_name FROM cargo_details WHERE cargo_details.id=manifests.goods_id) AS goods,
                            truck_deliverys.driver_name, truck_deliverys.tr_weight, truck_deliverys.tweight_wbridge, truck_deliverys.gweight_wbridge,
                             truck_deliverys.weigh_bridge_in_at,truck_deliverys.weigh_bridge_exit_at, truck_deliverys.package,
                            truck_deliverys.exit_scale, port_name, port_alias 
                            FROM truck_deliverys 
                            INNER JOIN manifests ON manifests.id=truck_deliverys.manf_id 
                            LEFT JOIN ports ON manifests.port_id=ports.id
                            INNER JOIN weighbridge_users ON truck_deliverys.exit_scale=weighbridge_users.scale_id  
                            INNER JOIN users ON users.id=weighbridge_users.user_id 
                            WHERE manifests.manifest='" + menifestNo + "' AND truck_deliverys.id=" + wtId + " AND users.username='" + userName + "'";

                    }
                    else
                    {
                        MessageBox.Show("Please select movement type.");
                    }

                    // return;
                    // string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";

                    Console.WriteLine(sqlRpt);
                    //MessageBox.Show(sqlRpt);
                    MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                    PdfPTable tableHead = new PdfPTable(3);
                    PdfPTable table = new PdfPTable(7);
                    PdfPTable table2 = new PdfPTable(6);
                    PdfPTable blanktbl = new PdfPTable(1);
                    PdfPTable blanktb2 = new PdfPTable(1);
                    PdfPTable table3 = new PdfPTable(3);
                    table.WidthPercentage = 100;
                    tableHead.WidthPercentage = 100;
                    table2.WidthPercentage = 100;
                    float[] widthCell = {3f, .5f, 4f, .3f, 3.5f, .5f, 4f};
                    float[] widthH = {2f, 6f, 2f};
                    float[] table2Width = {1.5f, 3f, 1.5f, 3f, 2f, 3f};
                    float[] table3Width = {2f, 2f, 2f};
                    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //table.DefaultCell.BorderColor=BaseColor.WHITE;

                    table.SetWidths(widthCell);
                    tableHead.SetWidths(widthH);
                    table2.SetWidths(table2Width);
                    table3.SetWidths(table3Width);
                    iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                    iTextSharp.text.Font cellFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD);

                    iTextSharp.text.Font cellFontValue = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);

                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                    logo.ScaleAbsolute(70, 70);

                    PdfPCell cellimage = new PdfPCell();
                    cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cellimage.HorizontalAlignment = 1;
                    cellimage.VerticalAlignment = 1;
                    cellimage.AddElement(logo);
                    cellimage.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                    cellimage.BorderWidthBottom = 1f;
                    tableHead.AddCell(cellimage);

                    PdfPTable inrtbl = new PdfPTable(1);
                    iTextSharp.text.Font fontTitle =
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                    Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                    PdfPCell cellpara = new PdfPCell();
                    cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cellpara.AddElement(para);
                    cellpara.HorizontalAlignment = 1;
                    inrtbl.AddCell(cellpara);
              
                    iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                    Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                    PdfPCell cellpara2 = new PdfPCell();
                    cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    para2.Alignment = 1;
                    cellpara2.AddElement(para2);
                    cellpara2.HorizontalAlignment = 1;
                    inrtbl.AddCell(cellpara2);

                    Paragraph para3 = new Paragraph("Weight Report", fontH2);
                    PdfPCell cellpara3 = new PdfPCell();
                    cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    para3.Alignment = 1;
                    cellpara3.AddElement(para3);
                    cellpara3.HorizontalAlignment = 1;
                    inrtbl.AddCell(cellpara3);

                    PdfPCell cellParagraph = new PdfPCell();
                    cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cellParagraph.HorizontalAlignment = 1;
                    cellParagraph.AddElement(inrtbl);
                    cellParagraph.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                    cellParagraph.BorderWidthBottom = 1f;
                    tableHead.AddCell(cellParagraph);

                    iTextSharp.text.Font scaleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);


                    using (MySqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // string time = DateTime.Now.ToString();
                            // MessageBox.Show(time);
                            string scale = "\n\n\n Scale: " + scaleName;
                            Paragraph timeParagraph = new Paragraph(scale, scaleFont);

                            PdfPCell cellbDT = new PdfPCell();
                            cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            cellbDT.HorizontalAlignment = 1;
                            cellbDT.AddElement(timeParagraph);

                            

                            //numeroCell.Border = 0;
                            cellbDT.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            cellbDT.BorderWidthBottom = 1f;
                            tableHead.AddCell(cellbDT);


                            // string grossWeight = rdr.GetValue(3).ToString();
                            //  string trWeight = rdr.GetValue(5).ToString();
                            // string netWeight = rdr.GetValue(4).ToString();
                            // MessageBox.Show(rdr.GetValue(1).ToString());
                            PdfPCell dateCell = new PdfPCell((new Phrase("Date ", cellFont)));
                            dateCell.HorizontalAlignment=Element.ALIGN_LEFT;
                            dateCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(dateCell);

                            PdfPCell colon1Cell = new PdfPCell((new Phrase(" : ", cellFont)));
                            colon1Cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            colon1Cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colon1Cell);

                            PdfPCell timeNow = new PdfPCell(new Phrase(DateTime.Now.ToString(), cellFontValue));
                            timeNow.HorizontalAlignment = Element.ALIGN_LEFT;
                            timeNow.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(timeNow);
                         //   table.AddCell(new PdfPCell(new Phrase("Date.", cellFont))).HorizontalAlignment = Element.ALIGN_LEFT;

                            PdfPCell blank1 = new PdfPCell(new Phrase("", cellFont));
                            blank1.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank1);

                            PdfPCell blank2 = new PdfPCell(new Phrase("", cellFont));
                            blank2.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank2);

                            PdfPCell blank3 = new PdfPCell(new Phrase("", cellFont));
                            blank3.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank3);

                            PdfPCell blank4 = new PdfPCell(new Phrase("", cellFont));
                            blank4.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank4);


                            PdfPCell weightId = new PdfPCell(new Phrase("Weight id.", cellFont));
                            weightId.HorizontalAlignment = Element.ALIGN_LEFT;
                            weightId.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(weightId);

                            PdfPCell colon1Cell2 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colon1Cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                            colon1Cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colon1Cell2);

                            PdfPCell wid = new PdfPCell(new Phrase(wtId, cellFontValue));
                            wid.HorizontalAlignment = Element.ALIGN_LEFT;
                            wid.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(wid);

                            PdfPCell blank5 = new PdfPCell(new Phrase("", cellFont));
                            blank5.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank5);

                            PdfPCell challanNo = new PdfPCell(new Phrase("Challan No.", cellFont));
                            challanNo.HorizontalAlignment = Element.ALIGN_LEFT;
                            challanNo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(challanNo);


                            PdfPCell colon1Cell3 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colon1Cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                            colon1Cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colon1Cell3);

                            PdfPCell menifestCell = new PdfPCell(new Phrase(menifestNo, cellFontValue));
                            menifestCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            menifestCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(menifestCell);

                            PdfPCell weightType = new PdfPCell(new Phrase("Weight Type", cellFont));
                            weightType.HorizontalAlignment = Element.ALIGN_LEFT;
                            weightType.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(weightType);

                            PdfPCell colon1Cell4 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colon1Cell4.HorizontalAlignment = Element.ALIGN_LEFT;
                            colon1Cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colon1Cell4);


                            PdfPCell inWeight = new PdfPCell((new Phrase("IN WEIGHT", cellFontValue)));
                            inWeight.HorizontalAlignment = Element.ALIGN_LEFT;
                            inWeight.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(inWeight);

                            PdfPCell blank6 = new PdfPCell(new Phrase("", cellFont));
                            blank6.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank6.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank6);

                            PdfPCell clistCode = new PdfPCell((new Phrase("Clist Code", cellFont)));
                            clistCode.HorizontalAlignment = Element.ALIGN_LEFT;
                            clistCode.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(clistCode);

                            PdfPCell colonCell5 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell5.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell5);


                            PdfPCell blank7 = new PdfPCell(new Phrase("", cellFont));
                            blank7.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank7.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank7);

                            PdfPCell materialdesciptionCell = new PdfPCell(new Phrase("Material Description", cellFont));
                            materialdesciptionCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            materialdesciptionCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(materialdesciptionCell);

                            PdfPCell colonCell6 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell6.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell6.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell6);

                            PdfPCell materialCell = new PdfPCell((new Phrase(rdr.GetValue(1).ToString(), cellFontValue)));
                            materialCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            materialCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(materialCell);

                            PdfPCell blank8 = new PdfPCell(new Phrase("", cellFont));
                            blank8.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank8.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank8);

                            PdfPCell clistType = new PdfPCell((new Phrase("Clist Type", cellFont)));
                            clistType.HorizontalAlignment = Element.ALIGN_LEFT;
                            clistType.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(clistType);

                            PdfPCell colonCell7 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell7.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell7.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell7);


                            PdfPCell blank9 = new PdfPCell(new Phrase("", cellFont));
                            blank9.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank9.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank9);


                            PdfPCell quantity = new PdfPCell((new Phrase("Quantity", cellFont)));
                            quantity.HorizontalAlignment = Element.ALIGN_LEFT;
                            quantity.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(quantity);

                            PdfPCell colonCell8 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell8.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell8.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell8);

                            PdfPCell blank10 = new PdfPCell(new Phrase(" ", cellFont));
                            blank10.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank10.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank10);

                            PdfPCell blank11 = new PdfPCell(new Phrase("", cellFont));
                            blank11.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank11.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank11);

                            PdfPCell nameCell = new PdfPCell((new Phrase("Name", cellFont)));
                            nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            nameCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(nameCell);

                            PdfPCell colonCell9 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell9.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell9.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell9);

                            PdfPCell blank12 = new PdfPCell(new Phrase("", cellFont));
                            blank12.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank12.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank12);

                            PdfPCell opreratorCell = new PdfPCell((new Phrase("Operator Name", cellFont)));
                            opreratorCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            opreratorCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(opreratorCell);

                            PdfPCell colonCell10 = new PdfPCell((new Phrase(" : ", cellFont)));
                            colonCell10.HorizontalAlignment = Element.ALIGN_LEFT;
                            colonCell10.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(colonCell10);

                            PdfPCell useridCell = new PdfPCell((new Phrase(userName, cellFontValue)));
                            useridCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            useridCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(useridCell);

                            PdfPCell blank13 = new PdfPCell(new Phrase("", cellFont));
                            blank13.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank13.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank13);

                            PdfPCell companyCell = new PdfPCell(new Phrase("Company", cellFont));
                            companyCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            companyCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(companyCell);

                            table.AddCell(colonCell10);

                            PdfPCell blank14 = new PdfPCell(new Phrase(" ", cellFont));
                            blank14.HorizontalAlignment = Element.ALIGN_LEFT;
                            blank14.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(blank14);

                            PdfPCell driverCell = new PdfPCell(new Phrase("Driver Name", cellFont));
                            driverCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            driverCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(driverCell);

                            table.AddCell(colonCell10);

                            PdfPCell driverValueCell = new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFontValue));
                            driverValueCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            driverValueCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(driverValueCell);

                            table.AddCell(blank13);

                            PdfPCell telFaxCell = new PdfPCell(new Phrase("Tel_Fax_Email", cellFont));
                            telFaxCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            telFaxCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table.AddCell(telFaxCell);

                            table.AddCell(colonCell10);
                            table.AddCell(blank13);

                            PdfPCell truckNo = new PdfPCell(new Phrase("Truck No", cellFont));
                            truckNo.HorizontalAlignment = Element.ALIGN_LEFT;
                            truckNo.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            truckNo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            truckNo.BorderWidthBottom = 1f;
                            table.AddCell(truckNo);

                            colonCell10.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            colonCell10.BorderWidthBottom = 1f;
                            table.AddCell(colonCell10);

                            PdfPCell truckNoValue = new PdfPCell(new Phrase(rdr.GetValue(0).ToString(), cellFontValue));
                            truckNoValue.HorizontalAlignment = Element.ALIGN_LEFT;
                            truckNoValue.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            truckNoValue.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            truckNoValue.BorderWidthBottom = 1f;
                            table.AddCell(truckNoValue);

                            blank13.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            blank13.BorderWidthBottom = 1f;
                            table.AddCell(blank13);
                            table.AddCell(blank13);
                            table.AddCell(blank13);
                            table.AddCell(blank13);

                            PdfPCell blcell = new PdfPCell();
                            Paragraph blkpar = new Paragraph("\n", fontH2);
                            blcell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            blcell.HorizontalAlignment = 1;
                            blcell.VerticalAlignment = 1;

                            blcell.AddElement(blkpar);
                            blanktbl.AddCell(blcell);



                            PdfPCell date1 = new PdfPCell(new Phrase("Date : ", cellFont));
                            date1.HorizontalAlignment = Element.ALIGN_CENTER;
                            date1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(date1);

                            PdfPCell timeCell1 = new PdfPCell(new Phrase(rdr.GetValue(6).ToString(), cellFont));
                            timeCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            timeCell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(timeCell1);

                            table2.AddCell(date1);

                            PdfPCell timeCell2 = new PdfPCell(new Phrase(rdr.GetValue(7).ToString(), cellFont));
                            timeCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            timeCell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(timeCell2);

                            table2.AddCell(date1);

                            table2.AddCell(timeCell2);

                            PdfPCell blankWCell = new PdfPCell(new Phrase(" ", cellFont));
                            blankWCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            blankWCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(blankWCell);


                            PdfPCell weight1 = new PdfPCell(new Phrase("Loaded Truck", cellFont));
                            weight1.HorizontalAlignment = Element.ALIGN_CENTER;
                            weight1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(weight1);

                            table2.AddCell(blankWCell);

                            PdfPCell weight2 = new PdfPCell(new Phrase("Empty Truck", cellFont));
                            weight2.HorizontalAlignment = Element.ALIGN_CENTER;
                            weight2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(weight2);

                            table2.AddCell(blankWCell);

                            PdfPCell weight3 = new PdfPCell(new Phrase("Goods Weight", cellFont));
                            weight3.HorizontalAlignment = Element.ALIGN_CENTER;
                            weight3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            table2.AddCell(weight3);


                            blankWCell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            blankWCell.BorderWidthBottom = 1f;
                            table2.AddCell(blankWCell);

                            PdfPCell weight1Value = new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont));
                            weight1Value.HorizontalAlignment = Element.ALIGN_CENTER;
                            weight1Value.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            weight1Value.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            weight1Value.BorderWidthBottom = 1f;
                            table2.AddCell(weight1Value);


                            table2.AddCell(blankWCell);

                            PdfPCell weight2Value = new PdfPCell(new Phrase(rdr.GetValue(5).ToString(), cellFont));
                            weight2Value.HorizontalAlignment = Element.ALIGN_CENTER;
                            weight2Value.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            weight2Value.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            weight2Value.BorderWidthBottom = 1f;
                            table2.AddCell(weight2Value);

                            table2.AddCell(blankWCell);

                            PdfPCell netWeightValue = new PdfPCell(new Phrase(rdr.GetValue(4).ToString(), cellFont));
                            netWeightValue.HorizontalAlignment = Element.ALIGN_CENTER;
                            netWeightValue.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            netWeightValue.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                            netWeightValue.BorderWidthBottom = 1f;

                            table2.AddCell(netWeightValue);



                            //table2.AddCell(new PdfPCell(new Phrase("Date : ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("Date: ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("Date : ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(" ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_LEFT;
                            //table2.AddCell(new PdfPCell(new Phrase("2nd Weight ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(" ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("2nd Weight", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(" ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("Net Weight", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(" ", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(rdr.GetValue(5).ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase("", cellFont))).HorizontalAlignment =
                            //    Element.ALIGN_CENTER;
                            //table2.AddCell(new PdfPCell(new Phrase(rdr.GetValue(4).ToString(), cellFont)))
                            //    .HorizontalAlignment = Element.ALIGN_CENTER;

                            PdfPCell blcell2 = new PdfPCell();
                            Paragraph blkpar2 = new Paragraph("\n", fontH2);
                            blcell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            blcell2.HorizontalAlignment = 1;
                            blcell2.VerticalAlignment = 1;

                            blcell2.AddElement(blkpar2);
                            blanktb2.AddCell(blcell2);

                            iTextSharp.text.Font signatureFont =
                                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);

                            PdfPCell signature1 = new PdfPCell(new Phrase("____________________", signatureFont));
                            PdfPCell signature2 = new PdfPCell(new Phrase("____________________", signatureFont));
                            PdfPCell signature3 = new PdfPCell(new Phrase("____________________", signatureFont));
                            PdfPCell signature4 = new PdfPCell(new Phrase("Customer's Signature", signatureFont));
                            PdfPCell signature5 = new PdfPCell(new Phrase("Custom's Signature", signatureFont));
                            PdfPCell signature6 = new PdfPCell(new Phrase("Operator Signature", signatureFont));

                            signature1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature1.HorizontalAlignment = 1;
                            signature2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature2.HorizontalAlignment = 1;
                            signature3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature3.HorizontalAlignment = 1;
                            signature4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature4.HorizontalAlignment = 1;
                            signature5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature5.HorizontalAlignment = 1;
                            signature6.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            signature6.HorizontalAlignment = 1;


                            table3.AddCell(signature1);
                            table3.AddCell(signature2);
                            table3.AddCell(signature3);
                            table3.AddCell(signature4);
                            table3.AddCell(signature5);
                            table3.AddCell(signature6);

                        }
                    }
                    document.Add(tableHead);
                    document.Add(table);
                    document.Add(blanktbl);
                    document.Add(table2);
                    document.Add(blanktb2);
                    document.Add(table3);
                    document.Close();
                    connection.Close();
                    // string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                    //  MessageBox.Show(wantedPath);
                    // if (wantedPath != null)
                    //    wantedPath = wantedPath.Replace("\\", "/");
                    //string pdfpath = wantedPath + "/bin/Debug/lib/exitReport.pdf";
                    // MessageBox.Show(pdfpath);
                    string pdfPath = "C:/Report/WeighMentReport.pdf";
                    System.Diagnostics.Process.Start(pdfPath);
                    //  System.Server.MapPath("~/pdf");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please! Select the type of movement.");
            }

            }
            else
            {
               // MessageBox.Show("You are not permitted to see report");
            }


        }



        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string manifest=textBox2.Text.Trim();
            // MessageBox.Show(" no error");
            //if (e.KeyCode != Keys.Enter)
            //{
            //    string manifest = textBox2.Text;
            //    int result1 = manifest.Length - manifest.Replace("/", "").Length;
            //    //MessageBox.Show(result1 + "");

            //    if (result1 == 2)
            //    {
            //        manifest = manifest + DateTime.Now.Year;
            //        textBox2.Clear();
            //        textBox2.SelectedText = manifest;
            //    }
            //}

            if (e.KeyCode == Keys.Enter)
                {
                string manifestId = "SELECT id FROM manifests WHERE manifest='" + manifest + "'";
                int manifId = 0;
                string trainManifest ="";
                // MessageBox.Show(manifestId);
                // return;
                MySqlConnection conn = new MySqlConnection(constr);
                MySqlCommand command = new MySqlCommand(manifestId, conn);
                conn.Open();

                MySqlDataReader manifestRead = command.ExecuteReader();
                while (manifestRead.Read())
                {
                    manifest = manifestRead.GetValue(0).ToString();
                }
                manifestRead.Close();
                conn.Close();
                manifId = int.Parse(manifest);
                //check train manifest truck entry or not
                string trainTruckEntry = "SELECT count(id) FROM train_bd_truck_entry WHERE manif_id='" + manifId + "'";
                MySqlCommand tTrcommand = new MySqlCommand(trainTruckEntry, conn);
                conn.Open();
                MySqlDataReader TrainManifestRead = tTrcommand.ExecuteReader();
                while (TrainManifestRead.Read())
                {
                    trainManifest = TrainManifestRead.GetValue(0).ToString();
                }
                TrainManifestRead.Close();
                conn.Close();
               int trainManifId = int.Parse(trainManifest);
                // textBox2.Text = manifest;
               /* if (trainManifId > 0)
                {
                    Console.WriteLine(trainManifId);
                    Console.WriteLine("Train manifest exists");
                    feildIndex = 4;
                    searchVal = textBox2.Text.Trim();
                    showList(feildIndex, searchVal);
                }
                else
                { }*/
                    //Console.WriteLine(trainManifId);
                   // Console.WriteLine("Train manifest  not exists");
                    feildIndex = 1;
                    searchVal = textBox2.Text.Trim();
                    showList(feildIndex, searchVal);
                    //MessageBox.Show("Train Manifest NOT Exists");
                

                     
                }
            
        }

        //Developer -- Sumon Roy, Software Developer, DataSoft Systems Bangladesh Limited.
        private void entryReportButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/WeighBridgeEntryReport.pdf", FileMode.Create));
                // PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"lib/WeighBridgeEntryReport.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                string constrg = constring();
                // testReport1 rv = new testReport1();
                MySqlConnection connection = new MySqlConnection(constrg);         
                connection.Open();
                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = @"SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no,truck_entry_regs.driver_name,
                    truck_entry_regs.gweight_wbridge,truck_entry_regs.wbrdge_time1,truck_entry_regs.wbridg_user1,
                    truck_entry_regs.tr_weight, truck_entry_regs.tweight_wbridge,truck_entry_regs.wbrdge_time2,
                    truck_entry_regs.wbridg_user2,manifests.manifest,users.name FROM truck_entry_regs 
                    JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                    JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                    JOIN roles ON roles.id = users.role_id 
                    WHERE DATE(truck_entry_regs.wbrdge_time1)='" + reportDateTimePicker.Text +
                    "' AND truck_entry_regs.entry_scale=" + scaleId +
                    "  AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs +
                    "' ORDER BY truck_entry_regs.wbrdge_time1 DESC";
                Console.WriteLine(sqlRpt);
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(9);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 1f, 2f, 2f, 1.7f, 1.7f, 1.7f, 2.7f, 1.7f, 2f };
                float[] widthH = { 2f, 6f, 2f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph(LoginForm.portAdress, fontTitle);
                para.Alignment = 1;
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph("Weight Bridge Entry Report\n\n", fontH2);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                var time = DateTime.Now;
                string formattedTime = "\n\n\n Time: " + time.ToString(" yyyy-MM-dd, HH:mm:ss") + " \n Scale: " + scaleName;
                ;
                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);
                tableHead.AddCell(cellbDT);
                document.Add(tableHead); 
                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("Weight Bridge Entry Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;
               
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("Serial No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Manifest No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Driver Name", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Loaded Truck.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Time", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry By", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Empty Truck.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Goods Weight", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

                    int i = 1;
                    while (rdr.Read())
                    {
                        /* iterate once per row */
                        //Chunk pdfData = new Chunk(truck_type);
                        //document.Add(new Paragraph(pdfData));
                        //string manifest = rdr.GetString("manifest");

                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(9).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(0).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(1).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(10).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(5).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(6).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;

                        i++;
                    }
                    document.Add(table);


                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image
                    
                    jpg1.ScalePercent(150f);


                    //Set position

                    jpg1.SetAbsolutePosition(160, 300);

                    //Close Stream

                    document.Add(jpg1);
                }

                document.Close();
                connection.Close();
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //MessageBox.Show(wantedPath);
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");
                // string pdfpath = wantedPath + "lib/WeighBridgeEntryReport.pdf";
                //string pdfpath = wantedPath + "/bin/Debug/lib/WeighBridgeEntryReport.pdf";
                // MessageBox.Show(pdfpath);
                // System.Diagnostics.Process.Start(@pdfpath);


                string FileReadPath = "C:/Report/WeighBridgeEntryReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);
                //  System.Server.MapPath("~/pdf");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }




        }
        //Developer -- Sumon Roy, Software Developer, DataSoft Systems Bangladesh Limited.

        private void exitReportButton_Click(object sender, EventArgs e)
        {
            try
            {
               
            string exitScale="";
            Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/WeighBridgeExitReport.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;

                string constrg = constring();
                // testReport1 rv = new testReport1();
                MySqlConnection connection = new MySqlConnection(constrg);
                connection.Open();
                
                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = "SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no," +
                                 " truck_entry_regs.driver_name,truck_entry_regs.gweight_wbridge,truck_entry_regs.wbrdge_time1,truck_entry_regs.wbridg_user1," +
                                 " truck_entry_regs.tr_weight,truck_entry_regs.tweight_wbridge,truck_entry_regs.wbrdge_time2,truck_entry_regs.wbridg_user2," +
                                 " manifests.manifest,users.name" +
                                 " FROM truck_entry_regs" +
                                 " JOIN manifests ON manifests.id= truck_entry_regs.manf_id" +
                                 " JOIN users ON truck_entry_regs.wbridg_user1 = users.id" +
                                 " JOIN roles ON roles.id = users.role_id" +
                                 " WHERE DATE(truck_entry_regs.wbrdge_time2)='" + reportDateTimePicker.Text + "'AND truck_entry_regs.exit_scale='" + scaleId +"' "+
                                 " AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP'" + regs + "' " +
                                 " ORDER BY truck_entry_regs.wbrdge_time1 DESC";

                Console.WriteLine(sqlRpt);
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(10);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 1f, 2f, 2f, 1.7f, 1.7f, 1.7f, 1.5f, 1.7f, 1.7f, 3f };
                float[] widthH = { 1.9f, 6f, 2.1f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph("Weight Bridge Exit Report\n\n", fontH2);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                //var time = DateTime.Now;
                string formattedTime = "\n\nTime: " + DateTime.Now.ToString() + " \n Scale: " + scaleName;
                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);

                tableHead.AddCell(cellbDT);

                document.Add(tableHead);

                

                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("Weight Bridge Exit Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;
               

                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("Serial No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Manifest No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Driver Name", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Loaded Truck.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Time", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Empty Truck", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Goods Weight", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Exit Time", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Exit By", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

                    int i = 1;
                    while (rdr.Read())
                    {
                        /* iterate once per row */
                        //Chunk pdfData = new Chunk(truck_type);
                        //document.Add(new Paragraph(pdfData));
                        //string manifest = rdr.GetString("manifest");
                     //  tr-0, dri-1, gw-2, wbt1-3, tr-5, twb-6, wbt2-7, mani- 9, na-10 
                     //  Serial No. Manifest No. Truck No.DriverName Gross Weight Entry Time Tare Weight Net Weight
                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(9).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(0).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(1).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(5).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(6).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(7).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(10).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;

                        i++;
                    }

                    document.Add(table);


                  //  FileStream fs1 = new FileStream("WM.JPG", FileMode.Open);


                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image

                    jpg.ScalePercent(150f);


                    //Set position

                    jpg.SetAbsolutePosition(150, 300);

                    //Close Stream


                    document.Add(jpg);

                }

                document.Close();
                connection.Close();
               // string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
               //  MessageBox.Show(wantedPath);
               // if (wantedPath != null)
               // wantedPath = wantedPath.Replace("\\", "/");
               //string pdfpath = wantedPath + "/bin/Debug/lib/exitReport.pdf";
               // MessageBox.Show(pdfpath);
                string pdfPath = "C:/Report/WeighBridgeExitReport.pdf";
                System.Diagnostics.Process.Start(pdfPath);
                //  System.Server.MapPath("~/pdf");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void cargo_entry_rpt_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/CargoEntryReport.pdf", FileMode.Create));
                // PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"lib/WeighBridgeEntryReport.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                string constrg = constring();
                // testReport1 rv = new testReport1();
                MySqlConnection connection = new MySqlConnection(constrg);
                connection.Open();

                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = @"SELECT u.name AS entryBy,CAST(TRIM(LEADING 'P' FROM SUBSTRING_INDEX((SELECT manifest FROM manifests WHERE manifests.id=truck_entry_regs.manf_id),'/',1)) AS UNSIGNED) AS justManifest,
                        (SELECT manifest FROM manifests WHERE manifests.id=truck_entry_regs.manf_id) AS manifes_no,
                        (SELECT SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1) FROM manifests WHERE manifests.id=truck_entry_regs.manf_id) AS total_truck,
                        (SELECT COUNT(id) FROM truck_entry_regs AS tr WHERE tr.manf_id=truck_entry_regs.manf_id AND DATE(tr.truckentry_datetime)='2018-09-11') AS total_truck_entered,
                        (
                        (SELECT SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1) FROM manifests WHERE manifests.id=truck_entry_regs.manf_id)-(SELECT COUNT(id) FROM truck_entry_regs AS tr WHERE tr.manf_id=truck_entry_regs.manf_id)
                        ) AS remaining_truck,
                        (SELECT cargo_name FROM cargo_details WHERE cargo_details.id=truck_entry_regs.goods_id) AS cargo_name,
                        truck_entry_regs.truck_no,truck_entry_regs.vehicle_type_flag,truck_entry_regs.truck_type,truck_entry_regs.created_by,truck_entry_regs.driver_card,truck_entry_regs.truckentry_datetime 
                        FROM truck_entry_regs 
                        JOIN users AS u ON truck_entry_regs.created_by=u.id 
                        JOIN roles AS r ON r.id=u.role_id
                        JOIN manifests AS m ON m.id=truck_entry_regs.manf_id
                        WHERE DATE(truckentry_datetime)='" + reportDateTimePicker.Text + "' AND vehicle_type_flag =1 AND truck_entry_regs.port_id=" + portId +
                        @" AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$' ORDER BY truckentry_datetime  ASC";
              
               // Console.WriteLine(sqlRpt);
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 1f, 2f, 2f, 1.7f, 1.7f, 1.7f, 2.7f };
                float[] widthH = { 2f, 6f, 2f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                para.Alignment = 1;
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                iTextSharp.text.Font fontH3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 9);
                Paragraph para3 = new Paragraph("Cargo Entry Report Date: " + reportDateTimePicker.Text +"\n\n", fontH3);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                //Paragraph para4 = new Paragraph("Date: " + reportDateTimePicker.Text, fontH2);
                //para4.Alignment = 1;
                //PdfPCell cellpara4 = new PdfPCell();
                //cellpara4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //cellpara4.AddElement(para4);
                //cellpara4.HorizontalAlignment = 1;
                //inrtbl.AddCell(cellpara4);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                var time = DateTime.Now;
                string formattedTime = "\n\n\n Time: " + time.ToString(" yyyy-MM-dd, HH:mm:ss");
                ;
                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);

                tableHead.AddCell(cellbDT);



                document.Add(tableHead);


                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("Cargo Entry Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;


                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("Serial No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Manifest No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Description of Goods", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Driver Card", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Time", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry By", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

                    int i = 1;
                    while (rdr.Read())
                    {
                        /* iterate once per row */
                        //Chunk pdfData = new Chunk(truck_type);
                        //document.Add(new Paragraph(pdfData));
                        //string manifest = rdr.GetString("manifest");

                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(6).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(9).ToString() + "-" + rdr.GetValue(7).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(11).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(12).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(0).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;

                        i++;
                    }
                    document.Add(table);


                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image

                    jpg1.ScalePercent(150f);


                    //Set position

                    jpg1.SetAbsolutePosition(160, 300);

                    //Close Stream

                    document.Add(jpg1);




                }

                document.Close();
                connection.Close();
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");


                string FileReadPath = "C:/Report/CargoEntryReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void btn_month_report_Click(object sender, EventArgs e)
        {
            string month = monthdtpicker.Value.Month.ToString();
            string year = monthdtpicker.Value.Year.ToString();

            //    MessageBox.Show(month + year);
            string entryScale = scaleName;
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strMonthName = mfi.GetMonthName(Convert.ToInt32(month));
             //  MessageBox.Show(strMonthName);
            try
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/" + strMonthName + " " + year + "WeighBridgeEntryReport.pdf", FileMode.Create));

                document.Open();
                PdfContentByte cb = writer.DirectContent;
                string constrg = constring();
                MySqlConnection connection = new MySqlConnection(constrg);

                connection.Open();

                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = "SELECT *,(  SELECT COUNT(truck_entry_regs.id)  " +
                "FROM manifests JOIN truck_entry_regs ON manifests.id = truck_entry_regs.manf_id " +
                "WHERE SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs + "' " +
                "AND DATE(truck_entry_regs.wbrdge_time2) = DATE(t.wbrdge_time1) " +

                ") AS exit_truck " +
                "FROM ( " +
                "SELECT  truck_entry_regs.wbrdge_time1, DATE_FORMAT(truck_entry_regs.wbrdge_time1, '%m-%d-%Y') AS dayofMonth, COUNT(truck_entry_regs.id) AS entry_truck " +
                "FROM manifests JOIN truck_entry_regs ON manifests.id = truck_entry_regs.manf_id " +
                "WHERE MONTH(truck_entry_regs.wbrdge_time1)='" + month + "' AND YEAR(truck_entry_regs.wbrdge_time1)='" + year +
                "' AND (truck_entry_regs.entry_scale=" + scaleId + " or truck_entry_regs.exit_scale=" + scaleId +
                ") AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs + "' " +
                " GROUP BY DATE(truck_entry_regs.wbrdge_time1) ) AS t ";

                Console.WriteLine(sqlRpt);
               // MessageBox.Show(sqlRpt);
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 2f, 2f, 2f, 2f, 1f };
                float[] widthH = { 2f, 6f, 2f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                para.Alignment = 1;
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph(strMonthName + year + "'s WeighBridge Entry-Exit Report\n\n", fontH2);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                var time = DateTime.Now;
                string formattedTime = "\n\n\n Time: " + time.ToString(" yyyy-MM-dd, HH:mm:ss") + " \n Scale: " + entryScale;

                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);

                tableHead.AddCell(cellbDT);



                document.Add(tableHead);


                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("'" + strMonthName + "' WeighBridge Entry-Exit Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;


                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("SL", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Date.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Exit Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Total.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    int i = 1;
                    sumEntryForMonth = 0;
                    sumExitForMonth = 0;
                    monthTotal = 0;
                    while (rdr.Read())
                    {
                       
                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(1).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        int entry = Convert.ToInt32(rdr.GetValue(2).ToString());
                        int exit = Convert.ToInt32(rdr.GetValue(3).ToString());
                        int total = entry + exit;
                        table.AddCell(new PdfPCell(new Phrase(total.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        sumEntryForMonth += entry;
                        sumExitForMonth += exit;
                        monthTotal += total;
                        i++;
                    }
                    table.AddCell(new PdfPCell(new Phrase("Total", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(sumEntryForMonth.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(sumExitForMonth.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(monthTotal.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                   // MessageBox.Show(monthTotal.ToString());
                    document.Add(table);


                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image

                    jpg1.ScalePercent(150f);


                    //Set position
                    jpg1.SetAbsolutePosition(160, 300);

                    //Close Stream
                    document.Add(jpg1);
                }

                document.Close();
                connection.Close();
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //MessageBox.Show(wantedPath);
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");
                string FileReadPath = "C:/Report/" + strMonthName + " " + year + "WeighBridgeEntryReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void yearly_entry_exit_rpt_btn_Click(object sender, EventArgs e)
        {
            //string year = monthdtpicker.Value.Year.ToString();
            string year = yearPicker.Value.Year.ToString();

            // MessageBox.Show(month + year);
            string entryScale = scaleName;
             //MessageBox.Show(entryScale);
            try
            {
                Document document = new Document();
          
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/" + year + "WeighBridgeEntryReport.pdf", FileMode.Create));
                // PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"lib/WeighBridgeEntryReport.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                string constrg = constring();
                // testReport1 rv = new testReport1();
                MySqlConnection connection = new MySqlConnection(constrg);

                connection.Open();



                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = "SELECT *,(SELECT COUNT(truck_entry_regs.id) " +
                                "FROM manifests JOIN truck_entry_regs ON manifests.id = truck_entry_regs.manf_id " +
                                "WHERE " +
                                "SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs + "' " +
                                "AND MONTH(truck_entry_regs.wbrdge_time2)= MONTH(t.wbrdge_time1)) AS exit_truck " +

                                "FROM ( " +
                                "SELECT truck_entry_regs.wbrdge_time1, DATE_FORMAT(truck_entry_regs.wbrdge_time1,'%M %Y')AS month_year,COUNT(truck_entry_regs.id) AS entry_truck " +
                                "FROM manifests JOIN truck_entry_regs ON manifests.id = truck_entry_regs.manf_id " +
                                "WHERE YEAR(truck_entry_regs.wbrdge_time1)='" + year + "' AND (truck_entry_regs.entry_scale=" + scaleId + " or truck_entry_regs.exit_scale=" + scaleId +
                                " ) AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs + "' " +
                                "GROUP BY MONTH(truck_entry_regs.wbrdge_time1) " +
                                ") AS t";
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 2f, 2f, 2f, 2f, 1f };
                float[] widthH = { 2f, 6f, 2f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                para.Alignment = 1;
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph(year + "'s WeighBridge Entry Exit Report\n\n", fontH2);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                var time = DateTime.Now;
                string formattedTime = "\n\n\n Time: " + time.ToString(" yyyy-MM-dd, HH:mm:ss") + " \n Scale: " + entryScale;

                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);

                tableHead.AddCell(cellbDT);

                document.Add(tableHead);

                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("'" + year + "' WeighBridge Entry-Exit Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;


                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("S/L", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Month", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Exit Truck No.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Total.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    int i = 1;
                    sumEntryForYear = 0;
                    sumExitForMonth = 0;
                    yearTotal = 0;
                    while (rdr.Read())
                    {
                        /* iterate once per row */
                        //Chunk pdfData = new Chunk(truck_type);
                        //document.Add(new Paragraph(pdfData));
                        //string manifest = rdr.GetString("manifest");

                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(1).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(2).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        int entry = Convert.ToInt32(rdr.GetValue(2).ToString());
                        int exit = Convert.ToInt32(rdr.GetValue(3).ToString());
                        int total = entry + exit;
                        sumEntryForYear += entry;
                        sumExitForYear += exit;
                        table.AddCell(new PdfPCell(new Phrase(total.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        yearTotal += total;
                        i++;
                    }

                    table.AddCell(new PdfPCell(new Phrase("Total", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(sumEntryForYear.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(sumExitForYear.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase(yearTotal.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

                    document.Add(table);


                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image

                    jpg1.ScalePercent(150f);


                    //Set position

                    jpg1.SetAbsolutePosition(160, 300);

                    //Close Stream

                    document.Add(jpg1);
                }

                document.Close();
                connection.Close();
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //MessageBox.Show(wantedPath);
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");
                // string pdfpath = wantedPath + "lib/WeighBridgeEntryReport.pdf";
                //string pdfpath = wantedPath + "/bin/Debug/lib/WeighBridgeEntryReport.pdf";
                // MessageBox.Show(pdfpath);
                //   System.Diagnostics.Process.Start(@pdfpath);


                string FileReadPath = "C:/Report/" + year + "WeighBridgeEntryReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);
                //  System.Server.MapPath("~/pdf");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
           // button1.UseVisualStyleBackColor = false;
            button1.BackColor = Color.ForestGreen;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.SeaGreen;
           // button1.UseVisualStyleBackColor = true;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            txtgross.Clear();
            txttare.Clear();
            string labVal = "";
            Console.WriteLine(feildIndex);
            labVal = label1.Text;
            /*
              //truck entry check randomly if execeds check date and return negative value.
              after certain period of time value will come negetive.
              Random rnd = new Random();
              int maxTruckentry = rnd.Next(7, 17);
              DateTime localDate = DateTime.Now;
              string todayDate = localDate.ToString("yyyy-MM-dd");
              String dateString = "2021-03-17";
              DateTime dateTime16 = DateTime.Parse(dateString);
              int TruckCount = getTruckCount();
              Console.WriteLine(TruckCount);
              Console.WriteLine(todayDate);
              Console.WriteLine(localDate);
              string diff2 = (dateTime16 - localDate).TotalDays.ToString();
              Console.WriteLine(diff2);
             if (float.Parse(diff2) < 0 && TruckCount > maxTruckentry) { 
                 // MessageBox.Show(" System error");
                   labVal = "-10000000";
              }
              else{
                 labVal = label1.Text;
               }*/

            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string moveVal = comboBox2.Text;
            listViewSelectedIndex = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            menifestNo = item.SubItems[1].Text;
            wtId = item.SubItems[10].Text;
             //MessageBox.Show(wtId);
            string truckNoAndType = item.SubItems[2].Text;
            string grsWt = item.SubItems[3].Text;
            string tareWt = item.SubItems[6].Text;
            truck_category = item.SubItems[9].Text;
            Console.WriteLine("list view clicked value");
            Console.WriteLine(item);
           Console.WriteLine(grsWt);
            Console.WriteLine(tareWt);

            //  MessageBox.Show(truck_category);
            /* if (feildIndex != 4)//check train bd truck or not
             { }*/
            if (moveVal.Equals("In") && truck_category != "L")
                {
                    string grossV = labVal;
                    double getLastTrVal = 0.00;
                    myfalg = 1;
                    txtgross.Text = grossV;
                    txtgross.BackColor = Color.Navy;
                    txttare.BackColor = Color.SlateGray;
                    string[] words = truckNoAndType.Split('-');
                    truckType = words[0];
                    truckNo = words[1];
                Console.WriteLine("get truck type");
                Console.WriteLine(truckType);
                Console.WriteLine(truckNo);
               
                if (truckNo != "" && truckType != "")
                    {
                        getLastTrVal = cargoF.getLastTareWeight(truckType, truckNo, LoginForm.port_id, wtId);
                    }
                Console.WriteLine("get last tr value");
                Console.WriteLine(getLastTrVal);

                if (getLastTrVal > 0)
                    {
                        txttare.Text = getLastTrVal.ToString();

                    }
                }
                else if (moveVal.Equals("In") && truck_category == "L")
                {
                    myfalg = 0;
                    txtgross.Text = grsWt;
                    txttare.Text = labVal;
                    txtgross.BackColor = Color.SlateGray;
                    txttare.BackColor = Color.Navy;
                }
                else if (moveVal.Equals("Out") && truck_category != "L")
                {
                    myfalg = 0;
                    txtgross.Text = grsWt;
                    txttare.Text = labVal;
                    txtgross.BackColor = Color.SlateGray;
                    txttare.BackColor = Color.Navy;
                }
                else if (moveVal.Equals("Out") && truck_category == "L")
                {
                    string grossV = labVal;
                    double getLastTrVal = 0.00;
                    myfalg = 1;
                    txtgross.Text = grossV;
                    txtgross.BackColor = Color.Navy;
                    txttare.BackColor = Color.SlateGray;
                    if (truckNoAndType != "")
                    {
                        getLastTrVal = cargoF.getLastTareWeightForBDtruck(truckNoAndType, LoginForm.port_id);
                        //  MessageBox.Show(getLastTrVal.ToString()+"sumon1");
                    }

                    if (getLastTrVal > 0)
                    {
                        txttare.Text = getLastTrVal.ToString();
                        // MessageBox.Show(getLastTrVal.ToString() + "sumon2");
                    }
                    // MessageBox.Show(getLastTrVal.ToString() + "sumon3");
                }
                else
                {
                    MessageBox.Show("Please select movement type.");
                }
            
           /* else if (moveVal.Equals("In") && truck_category != "L")
            {
                string grossV = labVal;
               // double getLastTrVal = 0.00;
                myfalg = 1;
                txtgross.Text = grossV;
                txtgross.BackColor = Color.Navy;
                txttare.BackColor = Color.SlateGray;
                txttare.Text = tareWt.ToString();
            }*/
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            if ((e.ItemIndex % 2) == 1)
            {
                e.Item.BackColor = Color.FromArgb(230, 230, 255);
                e.Item.UseItemStyleForSubItems = true;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void goods_report_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Document document = new Document();

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("C:/Report/GoodsReport.pdf", FileMode.Create));
                // PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"lib/WeighBridgeEntryReport.pdf", FileMode.Create));
                //PdfWriter writer = PdfWriter.GetInstance(document, outStream);
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                string constrg = constring();
                // testReport1 rv = new testReport1();
                MySqlConnection connection = new MySqlConnection(constrg);
                connection.Open();

                string regs = "^([B-Z]{1}[\\-]{1}[B-Z]{1})$";
                string sqlRpt = @"SELECT manifest, cargo_name, SUBSTRING(SUM(truck_entry_regs.gweight_wbridge/1000), 1, LENGTH(SUM(truck_entry_regs.gweight_wbridge/1000))-5) as gWeight,
                        SUBSTRING(SUM(truck_entry_regs.tweight_wbridge)/1000, 1, LENGTH(SUM(truck_entry_regs.tweight_wbridge)/1000)-5) as tWeight, 
                        SUBSTRING(SUM(truck_entry_regs.tr_weight)/1000, 1, LENGTH(SUM(truck_entry_regs.tr_weight)/1000)-5)  as trWeight, manifests.goods_id
                        FROM truck_entry_regs 
                        JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                        INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id
                        JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                        JOIN roles ON roles.id = users.role_id 
                        JOIN weighbridge_users ON users.id = weighbridge_users.user_id 
                        JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id 
                        WHERE DATE(truck_entry_regs.wbrdge_time1)='" + reportDateTimePicker.Text + "' AND weighbridges.scale_name='" + scaleName + "'" +
                        @" AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)
                        NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$' GROUP BY manifests.goods_id";


                Console.WriteLine(sqlRpt);
                MySqlCommand command = new MySqlCommand(sqlRpt, connection);
                PdfPTable tableHead = new PdfPTable(3);
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                tableHead.WidthPercentage = 100;
                float[] widths = { 1f, 3f, 3f, 5f, 2f, 2f, 2f };
                float[] widthH = { 2f, 6f, 2f };
                table.SetWidths(widths);
                tableHead.SetWidths(widthH);
                iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
                iTextSharp.text.Font cellFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                iTextSharp.text.Font fontSum = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo.jpg");
                logo.ScaleAbsolute(60, 60);

                PdfPCell cellimage = new PdfPCell();
                cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellimage.HorizontalAlignment = 1;
                cellimage.VerticalAlignment = 1;
                cellimage.AddElement(logo);
                tableHead.AddCell(cellimage);

                PdfPTable inrtbl = new PdfPTable(1);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13);
                Paragraph para = new Paragraph("BANGLADESH LANDPORT AUTHORITY", fontTitle);
                para.Alignment = 1;
                PdfPCell cellpara = new PdfPCell();
                cellpara.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara.AddElement(para);
                cellpara.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara);

                iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11);
                Paragraph para2 = new Paragraph(LoginForm.portName, fontH2);
                para2.Alignment = 1;
                PdfPCell cellpara2 = new PdfPCell();
                cellpara2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara2.AddElement(para2);
                cellpara2.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara2);

                Paragraph para3 = new Paragraph("Goods Wise Report Date: " + reportDateTimePicker.Text + " \n\n", fontH2);
                para3.Alignment = 1;
                PdfPCell cellpara3 = new PdfPCell();
                cellpara3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellpara3.AddElement(para3);
                cellpara3.HorizontalAlignment = 1;
                inrtbl.AddCell(cellpara3);

                PdfPCell cellParagraph = new PdfPCell();
                cellParagraph.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellParagraph.HorizontalAlignment = 1;
                cellParagraph.AddElement(inrtbl);
                tableHead.AddCell(cellParagraph);


                iTextSharp.text.Font timeFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8);
                var time = DateTime.Now;
                string formattedTime = "\n\n\n Time: " + time.ToString(" yyyy-MM-dd, HH:mm:ss") + " \n Scale: " + scaleName;

                Paragraph timeParagraph = new Paragraph(formattedTime, timeFont);

                PdfPCell cellbDT = new PdfPCell();
                cellbDT.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellbDT.HorizontalAlignment = 1;
                cellbDT.AddElement(timeParagraph);

                tableHead.AddCell(cellbDT);

                document.Add(tableHead);

                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("Goods Wise Report Date: " + reportDateTimePicker.Text);

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;


                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));
                document.Add(new Chunk("\n"));

                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    table.AddCell(new PdfPCell(new Phrase("S/L", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Goods Name", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Manifest No", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Truck No.", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Total Loaded Truck(M.T)", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Total Empty Truck(M.T)", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Total Goods Weight(M.T)", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;

                    int i = 1;
                    //sumEntryForYear = 0;
                    //sumExitForMonth = 0;
                    //yearTotal = 0;
                    while (rdr.Read())
                    {
                        /* iterate once per row */
                        //Chunk pdfData = new Chunk(truck_type);
                        //document.Add(new Paragraph(pdfData));
                        //string manifest = rdr.GetString("manifest");

                        table.AddCell(new PdfPCell(new Phrase(i.ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(1).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                    string manifestQury = @"SELECT DISTINCT(manifest) AS manifest 
                                         FROM truck_entry_regs 
                                        JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                                        INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id
                                        JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                                        JOIN roles ON roles.id = users.role_id 
                                        JOIN weighbridge_users ON users.id = weighbridge_users.user_id 
                                        JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id 
                                        WHERE DATE(truck_entry_regs.wbrdge_time1)='" + reportDateTimePicker.Text + "' AND weighbridges.scale_name='" + scaleName + "' " +
                                        @"AND  manifests.goods_id='" + rdr.GetValue(5).ToString() + "'" +
                                        @" AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)
                                        NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$' ORDER BY manifests.manifest ASC";
                        //  Console.WriteLine(trukQury);
                        MySqlConnection conn3 = new MySqlConnection(constr);
                        MySqlCommand command2 = new MySqlCommand(manifestQury, conn3);
                        conn3.Open();
                        MySqlDataReader manifstRead = command2.ExecuteReader();
                        string manifests = "";
                        while (manifstRead.Read())
                        {
                            //scale = truckRead.GetInt32(0);
                            manifests = manifests + ", " + manifstRead.GetValue(0).ToString();
                        }
                        conn3.Close();
                        manifests = manifests.Substring(1, manifests.Length - 2);
                        table.AddCell(new PdfPCell(new Phrase(manifests, cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;


                    //    table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(0).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        string trukQury = @"SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no
                                         FROM truck_entry_regs 
                                            JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                                            INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id
                                            JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                                            JOIN roles ON roles.id = users.role_id 
                                            JOIN weighbridge_users ON users.id = weighbridge_users.user_id 
                                            JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id 
                                            WHERE DATE(truck_entry_regs.wbrdge_time1)='" + reportDateTimePicker.Text + "' AND weighbridges.scale_name='" + scaleName + "'" +
                                            @"AND  manifests.goods_id='" + rdr.GetValue(5).ToString() + "'" +
                                            @"  AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)
                                            NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$' ";
                      //  Console.WriteLine(trukQury);
                        MySqlConnection conn1 = new MySqlConnection(constr);
                        MySqlCommand command1 = new MySqlCommand(trukQury, conn1);
                        conn1.Open();
                        MySqlDataReader truckRead = command1.ExecuteReader();
                        string trucks = "";
                        while (truckRead.Read())
                        {
                            //scale = truckRead.GetInt32(0);
                            trucks =trucks+", "+ truckRead.GetValue(0).ToString();
                        }
                        conn1.Close();
                        trucks = trucks.Substring(1, trucks.Length - 2);

                        table.AddCell(new PdfPCell(new Phrase(trucks, cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        //____gWeight not working for as usal GetValue(0),--- so use rdr.GetString(rdr.GetOrdinal("gWeight"))---
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetString(rdr.GetOrdinal("gWeight")), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(4).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        //int entry = Convert.ToInt32(rdr.GetValue(2).ToString());
                        //int exit = Convert.ToInt32(rdr.GetValue(3).ToString());
                        //int total = entry + exit;
                        //sumEntryForYear += entry;
                        //sumExitForYear += exit;
                        table.AddCell(new PdfPCell(new Phrase(rdr.GetValue(3).ToString(), cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        i++;
                    }
                    // Total_________________________________
                    string totalQury = @"SELECT SUBSTRING(SUM(truck_entry_regs.gweight_wbridge/1000), 1, LENGTH(SUM(truck_entry_regs.gweight_wbridge/1000))-5) as gWeight,
                            SUBSTRING(SUM(truck_entry_regs.tweight_wbridge)/1000, 1, LENGTH(SUM(truck_entry_regs.tweight_wbridge)/1000)-5) as tWeight, 
                        SUBSTRING(SUM(truck_entry_regs.tr_weight)/1000, 1, LENGTH(SUM(truck_entry_regs.tr_weight)/1000)-5)  as trWeight FROM truck_entry_regs 
                                JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                                INNER JOIN cargo_details ON manifests.goods_id=cargo_details.id
                                JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                                JOIN roles ON roles.id = users.role_id 
                                JOIN weighbridge_users ON users.id = weighbridge_users.user_id 
                                JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id 
                                WHERE DATE(truck_entry_regs.wbrdge_time1)='" + reportDateTimePicker.Text + "' AND weighbridges.scale_name='" + scaleName + "'" +
                                @" AND  SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)
                                NOT REGEXP '^([B-Z]{1}[\-]{1}[B-Z]{1})$'";
                    //Console.WriteLine(totalQury);
                    MySqlConnection conn = new MySqlConnection(constr);
                    MySqlCommand command3 = new MySqlCommand(totalQury, conn);
                    conn.Open();
                    MySqlDataReader totalRead = command3.ExecuteReader();
                    int j = 0;
                    while (totalRead.Read())
                    {
                        table.AddCell(new PdfPCell(new Phrase("Total:", fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase("", cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase("", cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase("", cellFont))).HorizontalAlignment = Element.ALIGN_CENTER;
                      //  reader.GetString(reader.GetOrdinal("Comment"));
                        table.AddCell(new PdfPCell(new Phrase(totalRead.GetString(totalRead.GetOrdinal("gWeight")), fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(totalRead.GetValue(2).ToString(), fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(new PdfPCell(new Phrase(totalRead.GetValue(1).ToString(), fontSum))).HorizontalAlignment = Element.ALIGN_CENTER;
                        j++;
                    }
                    conn.Close();

                    //table.AddCell(new PdfPCell(new Phrase("Total", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    //table.AddCell(new PdfPCell(new Phrase("", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    //table.AddCell(new PdfPCell(new Phrase(sumEntryForYear.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    //table.AddCell(new PdfPCell(new Phrase(sumExitForYear.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    //table.AddCell(new PdfPCell(new Phrase(yearTotal.ToString(), fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

                    document.Add(table);


                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(@"watermark_logo11.gif");


                    //Scale image

                    jpg1.ScalePercent(150f);


                    //Set position

                    jpg1.SetAbsolutePosition(160, 300);

                    //Close Stream

                    document.Add(jpg1);
                }

                document.Close();
                connection.Close();
                
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //MessageBox.Show(wantedPath);
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");
                // string pdfpath = wantedPath + "lib/WeighBridgeEntryReport.pdf";
                //string pdfpath = wantedPath + "/bin/Debug/lib/WeighBridgeEntryReport.pdf";
                // MessageBox.Show(pdfpath);
                //   System.Diagnostics.Process.Start(@pdfpath);


                string FileReadPath = "C:/Report/GoodsReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);
                //  System.Server.MapPath("~/pdf");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void usrCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userCreationForm usrCR = new userCreationForm();
            usrCR.ShowDialog();
            usrCR.ShowInTaskbar = false;
            usrCR.Show();
           // MessageBox.Show(usrCR.portId);
            gc.portId = portId;


        }

        private void updateScaleIDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            updateScaleForm upDateForm = new updateScaleForm();
            upDateForm.ShowInTaskbar = false;
            upDateForm.Show();
           // upDateForm.ShowDialog();
            // cw.ShowDialog();
           // upDateForm.ShowInTaskbar = false;
        }

        private void dBUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EncryptDecrypt usrCR = new EncryptDecrypt();
           // usrCR.ShowDialog();
            usrCR.ShowInTaskbar = false;
            usrCR.Show();
            // MessageBox.Show(usrCR.portId);
           // gc.portId = portId;
        }

        private void addBdTruckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddBdTruck cw = new AddBdTruck();
            cw.ShowInTaskbar = false;
            //cw.Owner = Application.MainWindow;
            cw.Show();
        }

        private void manifestEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDTruckManifestWtEntry cw = new BDTruckManifestWtEntry();
            cw.ShowInTaskbar = false;
            //cw.Owner = Application.MainWindow;
            cw.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
// Sumon Roy
