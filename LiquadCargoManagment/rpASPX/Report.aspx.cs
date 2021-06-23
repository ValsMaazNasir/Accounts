using LiquadCargoManagment.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiquadCargoManagment.rpASPX
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var db = new LCMEntities())
                {
                   

                    ReportDataSource datasource = null;
                    string reportname = Request.QueryString["reportname"].ToString();
                    long[] ids = (long[])Session["biltyid"];
                    long Id = Request.QueryString["id"] == null ? 0 : Convert.ToInt64(Request.QueryString["id"]);
                    
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mydb"].ConnectionString))
                    {
                        if (reportname == "rptChallanNew")
                        {
                            SqlDataAdapter adapte = new SqlDataAdapter("select * from rptchallan where challanid = @id", con);
                            adapte.SelectCommand.Parameters.AddWithValue("id", Id);
                            DataTable dt = new DataTable();
                            adapte.Fill(dt);
                            datasource = new ReportDataSource("DataSet1", dt);
                        }
                        else if (reportname == "rptBilty")
                        {
                            SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_Compactbilty where biltyid = @id", con);
                            adapte.SelectCommand.Parameters.AddWithValue("id", Id);
                            DataTable dt = new DataTable();
                            adapte.Fill(dt);
                            datasource = new ReportDataSource("DataSet1", dt);
                        }
                        else if (reportname == "rptBillformat" || reportname == "rptBillformat3" || reportname == "rptBillformat2" || reportname == "rptBillformat4" || reportname == "rptBillformat5" || reportname == "rptBillformat6" || reportname == "rptBillformat7")
                        {
                            if (ids != null)
                            {
                                string IdsList = "";
                                foreach (var item in ids)
                                {
                                    IdsList += "'" + item + "',";
                                }
                                IdsList = IdsList.TrimEnd(',');

                                SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_compactBilllist where biltyid IN (" + IdsList + ") ", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                            else if (Id != 0)
                            {
                                SqlDataAdapter adapte = new SqlDataAdapter("select* from rpt_compactBilllist where biltyid IN(SELECT BiltyID from CompactCheckBill where [CompactBillID] ='" + Id + "')", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                        }
                        else if (reportname == "rptUniversalBilty")
                        {
                        
                            SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_Compactbilty where biltyid = @id", con);
                            adapte.SelectCommand.Parameters.AddWithValue("id", Id);
                            DataTable dt = new DataTable();
                            adapte.Fill(dt);
                            datasource = new ReportDataSource("DataSet1", dt);
                        }
                        else if (reportname == "rptUniverbill")
                        {
                            if (Id != 0)
                            {
                                SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_UniversalBilty where ID IN(SELECT UniversalBiltyID from UniversalCheckBill where UniversalBillID = '" + Id + "')", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                            else if (ids != null)
                            {
                                string IdsList = "";
                                foreach (var item in ids)
                                {
                                    IdsList += "'" + item + "',";
                                }
                                IdsList = IdsList.TrimEnd(',');
                                SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_UniversalBilty where ID IN (" + IdsList + ")", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                        }
                        else if (reportname == "rptParchoonBill1" || reportname == "rptParchoonBill2" || reportname == "rptParchoonBill3" || reportname == "rptParchoonBill4" || reportname == "rptParchoonBill5" || reportname == "rptParchoonBill6" || reportname == "rptParchoonBill7" || reportname == "rptParchoonBill8" || reportname == "rptParchoonBill9" || reportname == "rptParchoonBill10")
                        {
                            if (Id != 0)
                            {
                                SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_ParchoonBilty where ID IN(SELECT ParchoonBiltyID from ParchoonCheckBill where ParchoonBillID = '" + Id + "')", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                            else if (ids != null)
                            {
                                string IdsList = "";
                                foreach (var item in ids)
                                {
                                    IdsList += "'" + item + "',";
                                }
                                IdsList = IdsList.TrimEnd(',');
                                SqlDataAdapter adapte = new SqlDataAdapter("select * from rpt_ParchoonBilty where ID IN (" + IdsList + ")", con);
                                DataTable dt = new DataTable();
                                adapte.Fill(dt);
                                datasource = new ReportDataSource("DataSet1", dt);
                            }
                        }
                    }
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.ProcessingMode = ProcessingMode.Local;
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("../Report/" + reportname + ".rdlc");
                    ReportViewer.LocalReport.DataSources.Add(datasource);
                }
            }
        }
    }
}