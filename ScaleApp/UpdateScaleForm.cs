using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ScaleApp
{
    public partial class updateScaleForm : Form
    {
        ConnectionDetails connDtls = new ConnectionDetails();
      

        public string constr = "";
        public string searchVal;
        public string ManifestUpdaeId, TruckUpdateId;
        private List<getScaleName> lstAutoCompleteData = null;
        private List<getUserName> lstAutoCompleteData_user = null;

        public int userID;
        public updateScaleForm()
        {
            InitializeComponent();
            constr = connDtls.constring();
            GetScaleName();
            GetUserName();

        }
        private void updateBtn_Click(object sender, EventArgs e)
        {
            string userId = usrCombo.SelectedValue.ToString();
            string scaleId = comboScale.SelectedValue.ToString();


           string strUpdate = @"UPDATE weighbridge_users SET scale_id='" + scaleId + "', created_at=now() WHERE user_id='" + userId + "'";

            Console.WriteLine(strUpdate);
            MySqlConnection con = new MySqlConnection(constr);
            con.Open();

            MySqlCommand cmd = new MySqlCommand(strUpdate, con);
            //con.Close();
            //MySqlDataReader mred = cmd.ExecuteReader();
            int stat = cmd.ExecuteNonQuery();

            if (stat==1)
            {
                MessageBox.Show("Updated..");
            }
            else
            {
                MessageBox.Show("Not Updated..");
            }
            con.Close();

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void usrCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAutoCompleteData = new List<getScaleName>();
            string userId = usrCombo.SelectedValue.ToString();

            string str= @" SELECT weighbridge_users.scale_id, weighbridges.scale_name
                                FROM users
                                INNER JOIN weighbridge_users ON weighbridge_users.user_id = users.id
                                INNER JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id
                                WHERE users.id = '"+ userId + "'";
            Console.WriteLine(str);
            MySqlConnection con = new MySqlConnection(constr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(str, con);
            MySqlDataReader mred = cmd.ExecuteReader();
            curScaleLabel.Text = "";
            while (mred.Read())
            {
                curScaleLabel.Text = mred.GetString("scale_name");
              //  comboScale.ValueMember = mred.GetString("scale_id");
               // lstAutoCompleteData.Add(new getScaleName { id = mred.GetString("scale_id"), scale_name = mred.GetString("scale_name") });
            }
            mred.Close();
            con.Close();

        }

        // int usrId;



        private void GetScaleName()
        {
            try
            {
                lstAutoCompleteData = new List<getScaleName>();
                // Auto
                DataTable dt = new DataTable();
                string strScale = "SELECT id,scale_name FROM weighbridges where port_id=1";
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


        private void GetUserName()
        {
            try
            {
                lstAutoCompleteData_user = new List<getUserName>();
                // Auto
                DataTable dt = new DataTable();
                string strUser = @" SELECT weighbridge_users.scale_id, weighbridges.scale_name, users.id, username
                                    FROM users
                                    INNER JOIN weighbridge_users ON weighbridge_users.user_id = users.id
                                    INNER JOIN weighbridges ON weighbridges.id = weighbridge_users.scale_id
                                    WHERE weighbridges.port_id = '1'";
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(strUser, con);
                MySqlDataReader mred = cmd.ExecuteReader();
                lstAutoCompleteData_user.Add(new getUserName { id = "", username = "--Select User--" });
                while (mred.Read())
                {
                    lstAutoCompleteData_user.Add(new getUserName { id = mred.GetString("id"), username = mred.GetString("username") });
                }
                usrCombo.DataSource = lstAutoCompleteData_user;
                usrCombo.DisplayMember = "username";
                usrCombo.ValueMember = "id";

                mred.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
