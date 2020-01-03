using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Reporting.WebForms;

namespace LibraryApp.WebForm
{
    public partial class TopSellingArtist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT  ar.Name,  sum(inv.Total) s
                                                FROM Artist ar
                                                inner join Album  al on ar.ArtistId = al.ArtistId
                                                inner join Track tr on al.AlbumId = tr.AlbumId
                                                inner join InvoiceLine inl on tr.TrackId = inl.TrackId
                                                inner join Invoice inv on inl.InvoiceId = inv.InvoiceId
                                                where inv.InvoiceDate between @From and @To
                                                group by ar.Name
                                                order by s desc ", con);
            if(FromDate2.Text =="")
            {
                FromDate2.Text = "1960-01-01";
            }
            if(ToDate2.Text =="")
            {
                ToDate2.Text = "2050-12-30";
            }
            cmd.Parameters.AddWithValue("@From", FromDate2.Text);
            cmd.Parameters.AddWithValue("@To", ToDate2.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("table2");
            da.Fill(dt);
            ReportViewer2.ProcessingMode = ProcessingMode.Local;
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/TopSellingArtist.rdlc");
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (valid(FromDate2.Text, ToDate2.Text))
            {
                GetData();
            }
        }

        private bool valid(string from, string to)
        {
            if (from == "" && to == "")
            {
                FromError2.Text = "One date must be valid";
                return false;
            }
            else if (match(from) && match(to))
            {
                return true;
            }
            return false;
        }

        private bool match(string date)
        {
            if (date == "")
            {
                return true;
            }
            DateTime dt;
            if (DateTime.TryParseExact(date, "yyyy-mm-dd", null, System.Globalization.DateTimeStyles.None, out dt) == true)
            {
                return true;
            }
            else
            {
                DateValid.Visible = true;
                return false;
            }
        }
    }
}