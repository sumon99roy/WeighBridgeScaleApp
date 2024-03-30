using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ScaleApp
{
    public partial class AddBdTruck : Form
    {
        public string constr = "";
        ConnectionDetails connDtls = new ConnectionDetails();
        AutoCompleteStringCollection DataTypeCollection = new AutoCompleteStringCollection();
        AutoCompleteStringCollection DataNoCollection = new AutoCompleteStringCollection();
        LoginForm loginForm = new LoginForm();
       
        public string portId = LoginForm.port_id;
        public string usrId = LoginForm.user_id;
        DataTable table = new DataTable();
        long truckTypeId = 0;
        long truckNoId = 0;
        long updateId = 0;
        
        private SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        public AddBdTruck()
        {
            InitializeComponent();
            //constr = constring();
            trDNTextBox.Text = "--";
            constr = connDtls.constring();
            getTruckTypeData(DataTypeCollection);
            getTruckNoData(DataNoCollection);
            trTypeTextBox.AutoCompleteCustomSource = DataTypeCollection;
            trNoTextBox.AutoCompleteCustomSource = DataNoCollection;
            showList(portId,1,1);
            saveBtn.Show();
            updateBtn.Hide();

        }
        private void SerialPortProgram()
        {
            try
            {
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            string str = "";
            try
            {
                str = port.ReadExisting();
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
        private void getTruckTypeData(AutoCompleteStringCollection dataCollection)
        {          
            string sql = "SELECT DISTINCT type_name,id  FROM vehicle_type_bd";
            MySqlConnection con = new MySqlConnection(constr);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader mred = cmd.ExecuteReader();
               
                // MySqlDataReader mred = cmd.ExecuteReader();
                // MessageBox.Show(ds.Tables[0].Rows);
                while (mred.Read())
                {
                    DataTypeCollection.Add(mred.GetString("id"));
                    DataTypeCollection.Add(mred.GetString("type_name"));
                }
                mred.Close();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        private void getTruckNoData(AutoCompleteStringCollection dataCollection)
        {
            string sql = "SELECT DISTINCT value,id  FROM vechile_no_bd";
            MySqlConnection con = new MySqlConnection(constr);
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader mred = cmd.ExecuteReader();
                string id;
                string type;
                // MySqlDataReader mred = cmd.ExecuteReader();
                // MessageBox.Show(ds.Tables[0].Rows);
                while (mred.Read())
                {
                    DataNoCollection.Add( mred.GetString("id"));
                    DataNoCollection.Add(mred.GetString("value"));
                }
                mred.Close();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }




        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string truckType = trTypeTextBox.Text;
            string truckNo = trNoTextBox.Text;
            //string truckWeight = trWtTextBox.Text;
            string driverName = trDNTextBox.Text;
            DateTime localDate = DateTime.Now;
            string formattedDateTime = localDate.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlConnection con = new MySqlConnection(constr);
            //Get truck Type id from Collection

            int indexTruckType = DataTypeCollection.IndexOf(truckType);
            for (int i = 0; i < DataTypeCollection.Count; i++)
            {
                if (i == indexTruckType)
                {
                    // Console.WriteLine("get value");
                    truckTypeId = Int32.Parse(DataTypeCollection[i - 1]);
                    //Console.WriteLine(DataTypeCollection[i]);
                    break;
                }
                else
                {
                    truckTypeId = 0;
                }
            }
            //Get truck No id from Collection

            int indexTruckNo = DataNoCollection.IndexOf(truckNo);
            for (int i = 0; i < DataNoCollection.Count; i++)
            {
                if (i == indexTruckNo)
                {
                    //Console.WriteLine("get value");
                    truckNoId = Int32.Parse(DataNoCollection[i - 1]);
                    break;
                }
                else
                {
                    truckNoId = 0;
                }
            }

            //New truck type save and get id 
            if (truckNoId == 0)
            {
                Console.WriteLine("insert id ");
                string strInsert = "INSERT INTO vechile_no_bd(value,entry_date,entry_by)  VALUES('" + truckNo + "','" + formattedDateTime + "','" + usrId + "')";
                MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                truckNoId = cmdInsert.LastInsertedId;
                Console.WriteLine(truckNoId);
                con.Close();
            }
            //New truck no  save and get id 
            if (truckTypeId == 0)
            {
              //  Console.WriteLine("insert Type id ");
                string strInsert = "INSERT INTO vehicle_type_bd(type_name,vehicle_type,created_at,created_by,port_id)  VALUES('" + truckType + "','" + "1" + "','" + formattedDateTime + "','" + usrId + "','" + "1" + "')";
                MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                truckTypeId = cmdInsert.LastInsertedId;
                //Console.WriteLine("Type Id");
                //Console.WriteLine(truckTypeId);
                con.Close();
            }
            Console.WriteLine("get update truck id and type");
            Console.WriteLine(truckNoId);
            Console.WriteLine(truckTypeId);

            string  updateString = "UPDATE bd_truck_entry SET bd_truck_type='" + truckTypeId + "',bd_truck_no='" + truckNoId + "',bd_truck_weight='" + label1.Text + "',bd_truck_driver_name='" + driverName + "',bd_truck_update_date='" + formattedDateTime + "',bd_truck_update_by='" + usrId + "' WHERE id='" + updateId + "'";



            MySqlCommand cmdBdTruckInsert = new MySqlCommand(updateString, con);
            con.Open();
            int st = cmdBdTruckInsert.ExecuteNonQuery();
            //int st = 1;
            if (st == 1)
            {
                MessageBox.Show("Bd Truck Updated SucessFully");
               // this.Close();
            }
            con.Close();
            showList(portId,1,1);
            saveBtn.Show();
            updateBtn.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {  
           
            string truckType = trTypeTextBox.Text;
            string truckNo = trNoTextBox.Text;
            if(truckType==" " || truckNo == "")
            {
                MessageBox.Show("Please Fill All fields");
                return;
            }
           // string truckWeight = trWtTextBox.Text;
            string driverName = trDNTextBox.Text;
            if(driverName == "")
            {
                driverName = "--";
            }
            DateTime localDate = DateTime.Now;
            string formattedDateTime = localDate.ToString("yyyy-MM-dd HH:mm:ss");
            MySqlConnection con = new MySqlConnection(constr);
            //Get truck Type id from Collection
          
            int indexTruckType = DataTypeCollection.IndexOf(truckType);
            for (int i = 0; i < DataTypeCollection.Count; i++)
            {
                if (i == indexTruckType)
                {
                    // Console.WriteLine("get value");
                    truckTypeId = Int32.Parse(DataTypeCollection[i - 1]);
                    //Console.WriteLine(DataTypeCollection[i]);
                    break;
                }
                else
                {
                    truckTypeId = 0;
                }
            }
            //Get truck No id from Collection
           
            int indexTruckNo = DataNoCollection.IndexOf(truckNo);
            for (int i = 0; i < DataNoCollection.Count; i++)
            {
                if (i == indexTruckNo)
                {
                    truckNoId = Int32.Parse(DataNoCollection[i - 1]);
                    break;
                }
                else
                {
                    truckNoId = 0;
                }
            }

            //New truck type save and get id 
            if (truckNoId == 0)
            {
                Console.WriteLine("insert id ");
                string strInsert = "INSERT INTO vechile_no_bd(value,entry_date,entry_by)  VALUES('" + truckNo + "','" + formattedDateTime + "','" + usrId + "')";
                MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                truckNoId = cmdInsert.LastInsertedId;
                Console.WriteLine(truckNoId);
                con.Close();
            }
            //New truck no  save and get id 
            if (truckTypeId == 0)
            {
                Console.WriteLine("insert Type id ");
                string strInsert = "INSERT INTO vehicle_type_bd(type_name,vehicle_type,created_at,created_by,port_id)  VALUES('" + truckType + "','" + "1" + "','" + formattedDateTime + "','" + usrId + "','" + "1" + "')";
                MySqlCommand cmdInsert = new MySqlCommand(strInsert, con);
                con.Open();
                cmdInsert.ExecuteNonQuery();
                truckTypeId = cmdInsert.LastInsertedId;
                Console.WriteLine("Type Id");
                Console.WriteLine(truckTypeId);
                con.Close();
            }
            double  bdTrExists=0;
            string bdTruckExistsquery = @"SELECT count(id) as tr_count
                                FROM bd_truck_entry AS bte
                                WHERE bte.bd_truck_type='" + truckTypeId + "' AND bte.bd_truck_no = " + truckNoId + " ";

            MySqlCommand cmdBdTruckExists = new MySqlCommand(bdTruckExistsquery, con);
            con.Open();
            MySqlDataReader mred = cmdBdTruckExists.ExecuteReader();
            while (mred.Read())
            {
                bdTrExists = mred.GetValue(0).ToString() != "" ? mred.GetDouble("tr_count") : 0;
            }
            mred.Close();
            con.Close();
            if (bdTrExists == 0)
            {
                string bdTruckInsertData = "INSERT INTO bd_truck_entry(bd_truck_type,bd_truck_no,bd_truck_weight,bd_truck_driver_name,bd_truck_entry_date,bd_truck_entry_by,port_id) VALUES('" + truckTypeId +
                     "','" + truckNoId + "','" + label1.Text + "','" + driverName + "','" + formattedDateTime + "','" + usrId + "','" + portId + "')";
                MySqlCommand cmdBdTruckInsert = new MySqlCommand(bdTruckInsertData, con);
                con.Open();
                int st = cmdBdTruckInsert.ExecuteNonQuery();
                //int st = 1;
                if (st == 1)
                {
                    MessageBox.Show("Bd Truck Saved SucessFully");
                    // this.Close();
                }
                con.Close();
                showList(portId,1,1);
            }
            else
            {
                MessageBox.Show("Truck Already Exists");
            } 

        }
        private void clearText()
        {
            trTypeTextBox.Text="";
            trNoTextBox.Text=""; 
            //trWtTextBox.Text=""; 
            trDNTextBox.Text=""; 
        }
        private void showList(string port,long t_type,long t_no)
        {
            int j = 0;
            table.Clear();
            if (constr.Equals("")) return;
            string sql;
            MySqlConnection con = new MySqlConnection(constr);

            if (t_type == 1 && t_no == 1)
            {
                
                sql = @"SELECT bte.*,vtb.id as tr_type_id,vtb.type_name, vtn.id as tr_no_id,vtn.value
                        FROM bd_truck_entry AS bte
                        INNER JOIN vehicle_type_bd AS vtb ON vtb.id=bte.bd_truck_type
                        INNER JOIN vechile_no_bd AS vtn ON vtn.id=bte.bd_truck_no
                        WHERE bte.port_id = '" + port + "' order by bte.id desc";
            }
            else
            {
                sql = @"SELECT bte.*,vtb.id as tr_type_id,vtb.type_name, vtn.id as tr_no_id,vtn.value
                        FROM bd_truck_entry AS bte
                        INNER JOIN vehicle_type_bd AS vtb ON vtb.id=bte.bd_truck_type
                        INNER JOIN vechile_no_bd AS vtn ON vtn.id=bte.bd_truck_no
                        WHERE  bte.bd_truck_type = '" + t_type + "' and  bte.bd_truck_no = '" + t_no + "' and bte.port_id = '" + port + "' order by bte.id desc";
            }
            Console.WriteLine(sql);
            // MessageBox.Show(sql);
            MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter(sql, con);
            adp.Fill(table);
            MySqlDataReader mred = cmd.ExecuteReader();
            //j = 0; 
            if (mred.Read())
            {
                Console.WriteLine("read data");
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine("Table looppp");

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[j].Cells["Truck_Type"].Value = row["type_name"].ToString();
                    dataGridView1.Rows[j].Cells["Truck_No"].Value = row["value"].ToString();
                    dataGridView1.Rows[j].Cells["Driver_Name"].Value = row["bd_truck_driver_name"].ToString();
                    dataGridView1.Rows[j].Cells["Truck_Weight"].Value = row["bd_truck_weight"].ToString();
                    dataGridView1.Rows[j].Cells["Edit"].Value = "Edit";
                    dataGridView1.Rows[j].Cells["Column1"].Value = row["id"].ToString();

                    j++;
                }
                clearText();
            }
            else
            {
                dataGridView1.Rows.Clear();
                 MessageBox.Show("Bd Truck Not Exists");
            }

            con.Close();
            dataGridView1.Refresh();
            // CollectionViewSource.GetDefaultView(dataGridView1.ItemsSource).Refresh();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1[4, dataGridView1.CurrentRow.Index].Value.ToString();
            updateId = long.Parse(value);
            foreach (DataRow row in table.Rows)
            {
                if (row["id"].ToString() == value)
                {
                    //truck Type
                    trTypeTextBox.Text = row["type_name"].ToString();
                    truckTypeId = long.Parse(row["tr_type_id"].ToString());
                    //Driver Name
                    trDNTextBox.Text = row["bd_truck_driver_name"].ToString();
                    //truckWeight
                   // trWtTextBox.Text = row["bd_truck_weight"].ToString();
                    //truck _no
                    trNoTextBox.Text = row["value"].ToString();
                    truckNoId = long.Parse(row["tr_no_id"].ToString());
                    Console.WriteLine("get update value");
                    Console.WriteLine(truckNoId);
                    Console.WriteLine(truckTypeId);
                }

            }
            saveBtn.Hide();
            updateBtn.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string truckType = trTypeTextBox.Text;
            string truckNo = trNoTextBox.Text;
           
            if (truckType == " " || truckNo == "")
            {
                MessageBox.Show("Please select Type & No");
                return;
            }
            //Get truck Type id from Collection
            int indexTruckType = DataTypeCollection.IndexOf(truckType);
            for (int i = 0; i < DataTypeCollection.Count; i++)
            {
                if (i == indexTruckType)
                {
                    // Console.WriteLine("get value");
                    truckTypeId = Int32.Parse(DataTypeCollection[i - 1]);
                    //Console.WriteLine(DataTypeCollection[i]);
                    break;
                }
                else
                {
                    truckTypeId = 0;
                }
            }
            //Get truck No id from Collection
            int indexTruckNo = DataNoCollection.IndexOf(truckNo);
            for (int i = 0; i < DataNoCollection.Count; i++)
            {
                if (i == indexTruckNo)
                {
                    truckNoId = Int32.Parse(DataNoCollection[i - 1]);
                    break;
                }
                else
                {
                    truckNoId = 0;
                }
            }
           showList(portId,truckTypeId, truckNoId);
        }

        private void AddBdTruck_Load(object sender, EventArgs e)
        {
            SerialPortProgram();
        }
    }
}
