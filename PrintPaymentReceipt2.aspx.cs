using System;
using System.Data;
using System.IO;

public partial class PrintPaymentReceipt2 : System.Web.UI.Page
{
    public String sPayRcptNo = "";
    public String sCurrComp = "";
    public String sUserId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
        }

        string sReturnReport = this.Page.Request.QueryString["ReturnReport"];

        if ((sReturnReport != null) && (sReturnReport.Trim().Length != 0))
        {
            ReportGen rpt = null;

            this.Page.Response.Buffer = true;

            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("dtDONumber", typeof(String)));
                dt.Columns.Add(new DataColumn("dtItemNumber", typeof(String)));
                dt.Columns.Add(new DataColumn("dtItemDesc", typeof(String)));

                // and few rows:
                DataRow dr1 = dt.NewRow();
                dr1.ItemArray = new object[] { "LZNK", "1101", "ZAKAT PENDAPATAN" };
                DataRow dr2 = dt.NewRow();
                dr2.ItemArray = new object[] { "LZNK", "1110", "ZAKAT FITRAH" };
                dt.Rows.Add(dr1);
                dt.Rows.Add(dr2);

                rpt = new ReportGen();
                rpt.DataSource = dt;
                rpt.Run(false);
                //rpt.Document.Printer.PrinterSettings.PrinterName = "EPSON L3150 Series";
                rpt.Document.Print(false, false);

            }
            catch (DataDynamics.ActiveReports.ReportException eRunReport)
            {
                this.Trace.Warn("Report failed to run:\n" + eRunReport.ToString());
            }

            // Create a memory stream to put the report document RDF in
            MemoryStream outStream = new MemoryStream();
            // Save the report document into the memory stream
            rpt.Document.Save(outStream, DataDynamics.ActiveReports.Document.RdfFormat.AR20);

            // Move the postion back to the beginning of the stream
            outStream.Seek(0, SeekOrigin.Begin);

            // Create a byte array buffer to read the memory stream into
            byte[] bytes = new byte[outStream.Length];
            // Fill the byte array buffer with the bytes from the memory stream
            outStream.Read(bytes, 0, (int)outStream.Length);

            // Clear anything that might have been written by the aspx page
            this.Page.Response.ClearContent();
            this.Page.Response.ClearHeaders();

            // Write the report document byte array to the requestor:
            this.Page.Response.BinaryWrite(bytes);
            this.Page.Response.End();
        }
    }
    private void initialValues()
    {
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["payrcptno"] != null)
        {
            sPayRcptNo = Request.QueryString["payrcptno"].ToString();
        }
    }
}