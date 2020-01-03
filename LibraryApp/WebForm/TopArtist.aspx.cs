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
    public partial class TopArtist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private void GetData()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChinookConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(@"SELECT  gen.Name, count(inl.TrackId) s
                                                    FROM Artist ar
                                                    inner join Album  al on ar.ArtistId = al.ArtistId
                                                    inner join Track tr on al.AlbumId = tr.AlbumId
                                                    inner join InvoiceLine inl on tr.TrackId = inl.TrackId
                                                    inner join Invoice inv on inl.InvoiceId = inv.InvoiceId
                                                    inner join Genre gen on tr.GenreId = gen.GenreId
                                                    where inv.InvoiceDate between @From and @To
                                                    group by gen.Name
                                                    order by s desc ", con);
            if (FromDate.Text == "")
            {
                FromDate.Text = "1960-01-01";
            }
            if (ToDate.Text == "")
            {
                ToDate.Text = "2050-12-30";
            }
            cmd.Parameters.AddWithValue("@From", FromDate.Text);
            cmd.Parameters.AddWithValue("@To", ToDate.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable("table1");
            da.Fill(dt);
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/TopArtistByDate.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (valid(FromDate.Text, ToDate.Text))
            {
                GetData();
            }
        }

        private bool valid(string from, string to)
        {
            if (from == "" && to == "")
            {
                FromError.Text = "One date must be filled";
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