using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaleApp
{
    class CargoFunctions
    {
        DbConnection dbconn = new DbConnection();
        ConnectionDetails connDtls = new ConnectionDetails();
        public double getLastTareWeight(string truckType, string truckNo, string portId,string id)
        {
            string constr = connDtls.constring();
            double tareWeight = 0.00;
            double trainTrId = 0;
            string query = " ";
            //check bd truck train truck id
            string trainquery = @"SELECT tr.truck_id
                                FROM train_bd_truck_entry AS tr
                                WHERE tr.truck_id='" + id +"' AND tr.port_id = " + portId + " ";

            MySqlConnection con = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand(trainquery, con);
            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();
            while (mred.Read())
            {
                trainTrId = mred.GetValue(0).ToString() != "" ? mred.GetDouble("truck_id") : 0;
            }
            mred.Close();
            con.Close();
            if(trainTrId != 0)
            {
                query = @"SELECT tr.tr_weight
                              FROM truck_entry_regs AS tr
                              WHERE tr.id='" + id +
                             "'  AND tr.port_id = " + portId + " AND tr.tr_weight IS NOT NULL ORDER BY tr.id DESC LIMIT 1";
            }
            else{
                query = @"SELECT tr.tr_weight
                              FROM truck_entry_regs AS tr
                              WHERE tr.truck_type='" + truckType +
                             "' AND tr.truck_no ='" + truckNo +
                             "' AND tr.port_id = " + portId + " AND tr.tr_weight IS NOT NULL ORDER BY tr.id DESC LIMIT 1";  
            }

            MySqlConnection conTr = new MySqlConnection(constr);
            MySqlCommand cmdTr = new MySqlCommand(query, conTr);
            conTr.Open();
            MySqlDataReader mredTr = cmdTr.ExecuteReader();
            while (mredTr.Read())
            {
               tareWeight = mredTr.GetValue(0).ToString() != "" ? mredTr.GetDouble("tr_weight") : 0.00;
            }
            mredTr.Close();
            conTr.Close();
            return tareWeight;

        }



        public double getLastTareWeightForBDtruck(string truckNo, string portId)
        {
            string constr = connDtls.constring();
            //  if (constr.Equals("")) return;

            double tareWeight = 0.00;


            string query = @"SELECT tr.tr_weight
                            FROM truck_deliverys AS tr
                            WHERE tr.truck_no ='" + truckNo +
                            "' AND tr.port_id = " + portId + " AND tr.tr_weight IS NOT NULL ORDER BY tr.id DESC LIMIT 1";
            Console.WriteLine(query);
            MySqlConnection con = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader mred = cmd.ExecuteReader();



            while (mred.Read())
            {

                tareWeight = mred.GetValue(0).ToString() != "" ? mred.GetDouble("tr_weight") : 0.00;
            }
            mred.Close();
            con.Close();
            return tareWeight;

        }
    }
}
