using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;



namespace ScaleApp
{
    public partial class ReportViewForm : Form
    {
        public string username;
        public int userId;
        public int scale;
        public string scaleName;
        EncryptDecrypt  enDeCls=new EncryptDecrypt();
        public ReportViewForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Customers dsCustomers = GetData();
            // crystalReportViewForm cr = new crystalReportViewForm();
            //   string constr = constring();
            // testReport1 rv = new testReport1();
            //  MySqlConnection con = new MySqlConnection(constr);
            //MySqlCommand cmd = new MySqlCommand("select userpass from users where username='" + textBox1.Text.Trim() + "' ", con);
            //  MySqlCommand cmd = new MySqlCommand("SELECT manf_id, truck_no, truck_type, goods_id FROM truck_entry_regs WHERE DATE(truckentry_datetime) BETWEEN '"+fromDateTimePicker.Text+"' AND '"+toDateTimePicker.Text+"'", con);


            //   con.Open();
            //  MySqlDataReader mred = cmd.ExecuteReader();

            // while(mred.Read())
            //   {
            // MessageBox.Show(""+mred.GetValue(0));
            //   }


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
                //string sqlRpt = "SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no,truck_entry_regs.driver_name," +
                //    "truck_entry_regs.gweight_wbridge,truck_entry_regs.wbrdge_time1,truck_entry_regs.wbridg_user1," +
                //    "truck_entry_regs.tr_weight, truck_entry_regs.tweight_wbridge,truck_entry_regs.wbrdge_time2," +
                //    "truck_entry_regs.wbridg_user2,manifests.manifest,users.name FROM truck_entry_regs " +
                //    "JOIN manifests ON manifests.id= truck_entry_regs.manf_id " +
                //    "JOIN users ON truck_entry_regs.wbridg_user1 = users.id " +
                //    "JOIN roles ON roles.id = users.role_id " +
                //    "WHERE DATE(truck_entry_regs.wbrdge_time1)='" + fromDateTimePicker.Text +
                //    "' AND users.id=" + userId + " AND manifests.transshipment_flag = 0 " +
                //    "AND SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs + "' " +
                //    "ORDER BY truck_entry_regs.wbrdge_time1 DESC";
          string sqlRpt = @"SELECT CONCAT(truck_entry_regs.truck_type,'-',truck_entry_regs.truck_no) AS truck_no,truck_entry_regs.driver_name,
                    truck_entry_regs.gweight_wbridge,truck_entry_regs.wbrdge_time1,truck_entry_regs.wbridg_user1,
                    truck_entry_regs.tr_weight, truck_entry_regs.tweight_wbridge,truck_entry_regs.wbrdge_time2,
                    truck_entry_regs.wbridg_user2,manifests.manifest,users.name FROM truck_entry_regs 
                    JOIN manifests ON manifests.id= truck_entry_regs.manf_id 
                    JOIN users ON truck_entry_regs.wbridg_user1 = users.id 
                    JOIN roles ON roles.id = users.role_id 
                    WHERE DATE(truck_entry_regs.wbrdge_time1)='" + fromDateTimePicker.Text +
                    "' AND truck_entry_regs.entry_scale=" + scale +
                    " AND  SUBSTRING_INDEX(SUBSTRING_INDEX(manifest,'/',2),'/',-1)  NOT REGEXP '" + regs +
                    "' ORDER BY truck_entry_regs.wbrdge_time1 DESC";
                // A condition subtract from above query--   "manifests.transshipment_flag = 0 "
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

                //logo.ScaleToFit(document.PageSize);
                //logo.SetAbsolutePosition(1, 1);
                //document.Add(logo);

                //cellimage.Colspan = 3;
                //cellimage.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //tableHead.AddCell(cellimage);


                //tableHead.AddCell(new PdfPCell(new Phrase("BANGLADESH LANDPORT AUTHORITY"))).HorizontalAlignment = Element.ALIGN_CENTER;

                //tableHead.AddCell(new PdfPCell(new Phrase("BANGLADESH LANDPORT AUTHORITY"))).HorizontalAlignment=Element.ALIGN_CENTER;
                //tableHead.AddCell(new PdfPCell(new Phrase("Benapole Land Port, Jessore"))).HorizontalAlignment=Element.ALIGN_CENTER;
                //tableHead.AddCell(new PdfPCell(new Phrase("Weight Bridge Entry Report"))).HorizontalAlignment=Element.ALIGN_CENTER;

                document.Add(tableHead);

                //   iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(MapPath("~/lib/logo.jpg"));
                //   image1.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                //   document.Add(image1);

                //Create a single cell
                /*               tableHead.AddCell(new PdfPCell(new Phrase("BANGLADESH LANDPORT AUTHORITY"))).HorizontalAlignment = Element.ALIGN_CENTER;
                              PdfPCell c2 = new PdfPCell(new Phrase("Benapole Land Port, Jessore")).HorizontalAlignment = Element.ALIGN_CENTER; ;
                              var c3 = new PdfPCell(new Phrase("Weight Bridge Entry Report")).HorizontalAlignment = Element.ALIGN_CENTER; ;
                               tableHead.AddCell(c1);
                               tableHead.AddCell(c2);
                               tableHead.AddCell(c3);
              */

                //     c1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //     c2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //     c3.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //Tell the cell to vertically align in the middle

                //c1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //c2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                //c3.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //Create a test paragraph
                var p1 = new Paragraph("BANGLADESH LANDPORT AUTHORITY");
                var p2 = new Paragraph(LoginForm.portName);
                var p3 = new Paragraph("Weight Bridge Entry Report");

                p2.Font.Size = 10;
                p3.Font.Size = 9;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                p3.Alignment = Element.ALIGN_CENTER;
                // c1.AddElement(p1);
                // c2.AddElement(p2);
                // c3.AddElement(p3);
                //c1.HorizontalAlignment = Element.ALIGN_RIGHT; 



                // tableHead.AddCell(c1);
                // tableHead.AddCell(c2);
                // tableHead.AddCell(c3);

                //Add the table to the document
                /*document.Add(p1);
                document.Add(p2);
                document.Add(p3);*/

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
                    table.AddCell(new PdfPCell(new Phrase("Gross Weight.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry Time", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Entry By", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Tare Weight.", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(new PdfPCell(new Phrase("Net Weight", fontH1))).HorizontalAlignment = Element.ALIGN_CENTER;

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
                }

                document.Close();
                string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                //MessageBox.Show(wantedPath);
                if (wantedPath != null)
                    wantedPath = wantedPath.Replace("\\", "/");
                // string pdfpath = wantedPath + "lib/WeighBridgeEntryReport.pdf";
                //string pdfpath = wantedPath + "/bin/Debug/lib/WeighBridgeEntryReport.pdf";
                // MessageBox.Show(pdfpath);
                //   System.Diagnostics.Process.Start(@pdfpath);
                string FileReadPath = "C:/Report/WeighBridgeEntryReport.pdf";
                System.Diagnostics.Process.Start(@FileReadPath);
                //  System.Server.MapPath("~/pdf");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }





            /*FileStream fs = new FileStream("Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.Read);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            doc.Add(new Paragraph("Hello World"));
            doc.Close();

            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                document.Open();
                document.Add(new Paragraph("Hello World"));
                document.Close();
                writer.Close();
                ms.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition", "attachment;filename=First_PDF_document.pdf");
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }
            */
        }







        private string constring()
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
                   // byte[] b2 = Convert.FromBase64String(Sline[2]);
                   // byte[] b3 = Convert.FromBase64String(Sline[3]);
                    // server = System.Text.ASCIIEncoding.ASCII.GetString(b);
                   
                   // dbname = System.Text.ASCIIEncoding.ASCII.GetString(b1);
                   // uname = System.Text.ASCIIEncoding.ASCII.GetString(b2);
                   // upass = System.Text.ASCIIEncoding.ASCII.GetString(b3);
                    server = Sline[0];
                    dbname = enDeCls.Decrypt(Sline[1]);
                    uname = enDeCls.Decrypt(Sline[2]);
                    upass = enDeCls.Decrypt(Sline[3]);
                   // server = Sline[0];
                   // dbname = Sline[1];
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

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.Dispose();
        }

    }
}
