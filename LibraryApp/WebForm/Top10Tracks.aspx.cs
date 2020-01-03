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
    public partial class Top10Tracks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT top (10)  tr.Name, count(inl.TrackId) s
                                                FROM Artist ar
                                                inner join Album  al on ar.ArtistId = al.ArtistId
                                                inner join Track tr on al.AlbumId = tr.AlbumId
                                                inner join InvoiceLine inl on tr.TrackId = inl.TrackId
                                                inner join Invoice inv on inl.InvoiceId = inv.InvoiceId
                                                where inv.InvoiceDate between @From and @To
                                                group by tr.Name
                                                order by s desc ", con);
            if (FromDate3.Text == "")
            {
                FromDate3.Text = "1960-01-01";
            }
            if (ToDate3.Text == "")
            {
                ToDate3.Text = "2050-12-30";
            }
            cmd.Parameters.AddWithValue("@From", FromDate3.Text);
            cmd.Parameters.AddWithValue("@To", ToDate3.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("table3");
            da.Fill(dt);
            ReportViewer3.ProcessingMode = ProcessingMode.Local;
            ReportViewer3.LocalReport.ReportPath = Server.MapPath("~/Reports/Top10Tracks.rdlc");
            ReportViewer3.LocalReport.DataSources.Clear();
            ReportViewer3.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (valid(FromDate3.Text, ToDate3.Text))
            {
                GetData();
            }
        }

        private bool valid(string from, string to)
        {
            if (from == "" && to == "")
            {
                FromError3.Text = "One date must be valid";
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

        protected void Home_Click(object sender, EventArgs e)
        {

        }
    }
}