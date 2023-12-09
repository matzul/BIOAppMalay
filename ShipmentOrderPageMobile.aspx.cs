using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShipmentOrderPageMobile : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sUserId = "";
    public String sShipmentNo = "";
    public String sShipmentCat = "";
    public MainModel modCompInfo = new MainModel();
    public MainModel oModShipment = new MainModel();
    public ArrayList lsShipmentLineItem = new ArrayList();
    PdfSharp.Drawing.XGraphics gfx;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initialValues();
            processValues();
        }
    }
    private void initialValues()
    {
        //if (Session["userid"] != null)
        //{
        //    sUserId = Session["userid"].ToString();
        //}
        //if (Session["comp"] != null)
        //{
        //    sCurrComp = Session["comp"].ToString();
        //}
        if (Request.QueryString["userid"] != null)
        {
            sUserId = Request.QueryString["userid"].ToString();
        }
        if (Request.QueryString["comp"] != null)
        {
            sCurrComp = Request.QueryString["comp"].ToString();
        }
        if (Request.QueryString["shipmentno"] != null)
        {
            sShipmentNo = Request.QueryString["shipmentno"].ToString();
        }
    }
    private void processValues()
    {
        modCompInfo = oMainCon.getCompInfoDetails(sCurrComp);
        oModShipment = oMainCon.getShipmentHeaderDetails(sCurrComp, sShipmentNo);
        lsShipmentLineItem = oMainCon.getShipmentDetailsList(sCurrComp, sShipmentNo, 0, "");
        GenerateQRCode();
        generatePDFFile();
    }

    private void GenerateQRCode()
    {
		MainModel modOrdDet = (MainModel)lsShipmentLineItem[0];
        sShipmentNo = modOrdDet.GetSetorderno;
		sShipmentCat = oModShipment.GetSetshipmentcat;
		
		object objQRCode = new
        {
            comp = sCurrComp,
            shipmentno = modOrdDet.GetSetorderno,
            shiptmentcat = oModShipment.GetSetshipmentcat
        };
		String jsonResponse = convertJson(objQRCode);
		
        string urlQR = "https://chart.googleapis.com/chart?chs=250x250&cht=qr&chl=" + jsonResponse +  "&choe=UTF-8";
        WebResponse response = default(WebResponse);
        Stream remoteStream = default(Stream);
        StreamReader readStream = default(StreamReader);
        WebRequest request = WebRequest.Create(urlQR);
        response = request.GetResponse();
        remoteStream = response.GetResponseStream();
        readStream = new StreamReader(remoteStream);
        System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
        byte[] imageArray = imageToByteArray(img);
        string imageFilename2 = "base64:" + Convert.ToBase64String(imageArray);
        img.Save(Server.MapPath("~/Attachment/QRCode.png"));
        response.Close();
        remoteStream.Close();
        readStream.Close();
    }
	
    public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
	
	private string convertJson(object objItem)
    {
        String jsonResponse = "";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        jsonResponse = serializer.Serialize(objItem);

        return jsonResponse;
    }

    private void generatePDFFile()
    {
        // Create a MigraDoc document
        Document doc = new Document();
        doc.Info.Title = "PENGHANTARAN PESANAN";
        doc.Info.Subject = "No. Penghantaran:" + oModShipment.GetSetorderno;
        doc.Info.Author = "ZMartPartner System";

        //set page orientation
        doc.DefaultPageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Portrait;
        doc.DefaultPageSetup.TopMargin = "7.5cm"; //120
        doc.DefaultPageSetup.BottomMargin = "8.5cm"; //150
        doc.DefaultPageSetup.LeftMargin = 40;
        doc.DefaultPageSetup.RightMargin = 40;
        doc.DefaultPageSetup.HeaderDistance = "1.5cm"; //20
        doc.DefaultPageSetup.FooterDistance = "1cm"; //20

        MigraDoc.DocumentObjectModel.Style style = doc.Styles["Normal"];
        // Because all styles are derived from Normal, the next line changes the 
        // font of the whole document. Or, more exactly, it changes the font of
        // all styles and paragraphs that do not redefine the font.
        style.Font.Name = "Verdana";

        // Create a new style called Table based on style Normal
        style = doc.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Verdana";
        style.Font.Name = "Arial";
        style.Font.Size = 8;

        // Each MigraDoc document needs at least one section.
        Section section = doc.AddSection();

        // Put 1st logo in the header
        string comp1_logo = Server.MapPath("~/images/"+modCompInfo.GetSetcomp_logo1);
        MigraDoc.DocumentObjectModel.Shapes.Image image = section.Headers.Primary.AddImage(comp1_logo);
        image.Height = "2cm";
        image.Width = "2cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Left;
        image.WrapFormat.Style = WrapStyle.Through;

        // Put 2nd logo in the header
        string qrcode_logo = Server.MapPath("~/Attachment/QRCode.png");
        MigraDoc.DocumentObjectModel.Shapes.Image image2 = section.Headers.Primary.AddImage(qrcode_logo);
        image2.Height = "3cm";
        image2.Width = "3cm";
        image2.LockAspectRatio = true;
        image2.RelativeVertical = RelativeVertical.Line;
        image2.RelativeHorizontal = RelativeHorizontal.Margin;
        image2.Top = ShapePosition.Top;
        image2.Left = ShapePosition.Right;
        image2.WrapFormat.Style = WrapStyle.Through;

        // Create Header
        Paragraph header = section.Headers.Primary.AddParagraph();
        header.AddText("PENGHANTARAN PESANAN");
        header.Format.Font.Size = 12;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;

        // Create paragraph between header logo
        Paragraph main = section.Headers.Primary.AddParagraph();
        main = section.AddParagraph();

        // Create the item table for header
        //MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.AddTable();
        MigraDoc.DocumentObjectModel.Tables.Table tableTop = section.Headers.Primary.AddTable();
        tableTop.Style = "Table";
        tableTop.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Black;
        tableTop.Borders.Width = 0.25;
        tableTop.Borders.Left.Width = 0.5;
        tableTop.Borders.Right.Width = 0.5;
        tableTop.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column columnTop = tableTop.AddColumn("8cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("2cm");
        columnTop = tableTop.AddColumn("2cm");
        columnTop = tableTop.AddColumn("3cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;
        columnTop = tableTop.AddColumn("3cm");
        columnTop.Format.Alignment = ParagraphAlignment.Left;

        Row rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;

        rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;
        rowTop.Cells[0].AddParagraph();
        rowTop.Cells[0].Borders.Bottom.Visible = false;
        rowTop.Cells[1].AddParagraph();
        rowTop.Cells[2].AddParagraph();

        rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;
        rowTop.Cells[0].AddParagraph("Daripada:");
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_name);
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_address);
        rowTop.Cells[0].AddParagraph(modCompInfo.GetSetcomp_website);
        rowTop.Cells[3].Borders.Visible = false;
		
		rowTop = tableTop.AddRow();
        rowTop.Borders.Visible = false;
        rowTop.Cells[0].AddParagraph("Kepada: ");
        rowTop.Cells[0].AddParagraph(oModShipment.GetSetbpdesc);
        rowTop.Cells[0].AddParagraph(oModShipment.GetSetbpaddress.ToUpper());
        rowTop.Cells[0].AddParagraph(oModShipment.GetSetbpcontact);
        rowTop.Cells[0].MergeDown = 2;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].Borders.Visible = true;
        rowTop.Cells[3].AddParagraph("No. Penghantaran Pesanan");
        rowTop.Cells[3].Borders.Left.Visible = true;
        rowTop.Cells[3].Borders.Right.Visible = true;
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTop.Cells[4].AddParagraph(oModShipment.GetSetshipmentno);
        rowTop.Cells[4].Borders.Visible = true;
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].AddParagraph("Tarikh");
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTop.Cells[4].AddParagraph(oModShipment.GetSetshipmentdate);
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].AddParagraph("Jenis");
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        string shipmentcat = "";
        if (oModShipment.GetSetshipmentcat.Equals("SALES_ORDER"))
        {
            shipmentcat = "PESANAN JUALAN";
        }
        else if (oModShipment.GetSetshipmentcat.Equals("GIVE_ORDER"))
        {
            shipmentcat = "PESANAN AGIHAN";
        }
        else if (oModShipment.GetSetshipmentcat.Equals("TRANSFER_ORDER"))
        {
            shipmentcat = "PESANAN PINDAHAN";
        }
        rowTop.Cells[4].AddParagraph(shipmentcat);
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        // Create the item table
        MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Black;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.25;
        table.Borders.Right.Width = 0.25;
        table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("6cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        // Create the header of the table
        Row row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
        row.Height = "0.5cm";
        row.Cells[0].AddParagraph("#");
        row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[1].AddParagraph("NO. ITEM");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[2].AddParagraph("KETERANGAN");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[3].AddParagraph("KUANTITI");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[4].AddParagraph("CATATAN");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        int totalQty = 0;
        for (int i = 0; i < lsShipmentLineItem.Count; i++)
        {
            MainModel modOrdDet = (MainModel)lsShipmentLineItem[i];

            // Each item fills two rows
            Row row1 = table.AddRow();
            row1.Height = "1cm";
            row1.TopPadding = 1;
            //row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            row1.Cells[0].AddParagraph((i+1).ToString());
            row1.Cells[1].AddParagraph(modOrdDet.GetSetitemno);
            row1.Cells[2].AddParagraph(modOrdDet.GetSetitemdesc);
            row1.Cells[3].AddParagraph(modOrdDet.GetSetshipment_quantity.ToString());
            row1.Cells[4].AddParagraph(modOrdDet.GetSetremarks);
            totalQty += modOrdDet.GetSetshipment_quantity;

            if (i>0 && ((i+1)%10)==0)
            {
                row1.Borders.Bottom.Visible = true;
            }
            else
            {
                row1.Borders.Bottom.Visible = false;
            }
        }
        if ((lsShipmentLineItem.Count % 10) > 0)
        {
            int totalremainingrow = 10 - (lsShipmentLineItem.Count % 10);
            for (int j = 0; j < totalremainingrow; j++)
            {
                Row rowRemain = table.AddRow();
                rowRemain.Height = "1cm";
                rowRemain.Cells[0].AddParagraph();
                rowRemain.Cells[1].AddParagraph();
                rowRemain.Cells[2].AddParagraph();
                rowRemain.Cells[3].AddParagraph();
                rowRemain.Cells[4].AddParagraph();

                if (j == (totalremainingrow-1))
                {
                    rowRemain.Borders.Bottom.Visible = true;
                }
                else if (j > 0 && (j % (totalremainingrow-1)) == 0)
                {
                    rowRemain.Borders.Bottom.Visible = false;
                }
                else
                {
                    rowRemain.Borders.Bottom.Visible = false;
                }
            }
        }

        Row rowTot = table.AddRow();
        rowTot.Height = "1cm";
        rowTot.Format.Font.Bold = true;
        rowTot.Cells[0].AddParagraph();
        rowTot.Cells[0].Borders.Left.Visible = false;
        rowTot.Cells[0].Borders.Right.Visible = false;
        rowTot.Cells[0].Borders.Bottom.Visible = false;
        rowTot.Cells[0].MergeRight = 1;

        rowTot.Cells[2].Borders.Right.Visible = false;
        rowTot.Cells[2].AddParagraph("JUMLAH KUANTITI");
        rowTot.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        rowTot.Cells[2].VerticalAlignment = VerticalAlignment.Center;
        rowTot.Cells[2].Borders.Left.Visible = false;
        //rowTot.Cells[7].Borders.Right.Visible = false;

        rowTot.Cells[3].AddParagraph(totalQty.ToString());
        rowTot.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        rowTot.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        rowTot.Cells[4].Borders.Left.Visible = false;
        rowTot.Cells[4].Borders.Right.Visible = false;
        rowTot.Cells[4].Borders.Bottom.Visible = false;

        //footer.AddText("Footer");
        //footer.Format.Font.Size = 9;
        //footer.Format.Alignment = ParagraphAlignment.Center;

        // Create the item table for footer
        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Headers.Primary.AddImage(logo_lima);
        MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.Footers.Primary.AddTable();
        //MigraDoc.DocumentObjectModel.Tables.Table tblBtm = section.AddTable();
        tblBtm.Style = "Table";
        tblBtm.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Black;
        tblBtm.Borders.Width = 0.25;
        tblBtm.Borders.Left.Width = 0.5;
        tblBtm.Borders.Right.Width = 0.5;
        tblBtm.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;
        colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;
        colTblBtm = tblBtm.AddColumn("6cm");
        colTblBtm.Format.Alignment = ParagraphAlignment.Left;

        Row rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Catatan:");
        rowTblBtm.Cells[0].MergeRight = 2; 
        
        rowTblBtm = tblBtm.AddRow();
        //rowTblBtm.Borders.Left.Visible = false;
        //rowTblBtm.Borders.Right.Visible = false;
        //rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph(oModShipment.GetSetorderremarks);
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Height = "2cm";
        //rowTblBtm.Borders.Left.Visible = false;
        //rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("1. Pesanan Penghantaran ini sah sehingga tiga(3) bulan dari tarikh ia dikeluarkan.");
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("2. Sila pastikan Pesanan Penghantaran ini ditandatangani sebelum salinan dikembalikan ke "+System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modCompInfo.GetSetcomp_name.ToLower())+".");
        rowTblBtm.Cells[0].MergeRight = 2;

        // rowTblBtm = tblBtm.AddRow();
        // rowTblBtm.Borders.Left.Visible = false;
        // rowTblBtm.Borders.Right.Visible = false;
        // rowTblBtm.Borders.Top.Visible = false;
        // rowTblBtm.Borders.Bottom.Visible = false;
        // rowTblBtm.Cells[0].AddParagraph("3. Jabatan Kewangan hanya akan memproses bayaran ke atas Pesanan Terimaan ini jika Invois disertakan bersama.");
        // rowTblBtm.Cells[0].MergeRight = 2; 
        
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        //rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[0].MergeRight = 1;
        rowTblBtm.Cells[1].AddParagraph("");
        rowTblBtm.Cells[2].AddParagraph("");

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Visible = false;
        rowTblBtm.Cells[0].AddParagraph();
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[2].AddParagraph();
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Format.Font.Bold = true;
        rowTblBtm.Cells[0].AddParagraph("Disediakan Oleh:");
        rowTblBtm.Cells[0].Borders.Top.Visible = true;
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[1].Borders.Visible = false;
        rowTblBtm.Cells[2].AddParagraph("Disahkan Oleh:");
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[1].Borders.Visible = false;
        rowTblBtm.Height = "2cm";
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Nama:");
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[1].Borders.Visible = false;
        rowTblBtm.Cells[2].AddParagraph("Nama:");
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("Jawatan:");
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[1].Borders.Visible = false;
        rowTblBtm.Cells[2].AddParagraph("Jawatan:");
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Cells[0].AddParagraph("Tarikh:");
        rowTblBtm.Cells[1].AddParagraph();
        rowTblBtm.Cells[1].Borders.Visible = false;
        rowTblBtm.Cells[2].AddParagraph("Tarikh:");

        // Create a renderer for PDF that uses Unicode font encoding
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

        // Set the MigraDoc document
        pdfRenderer.Document = doc;

        // Create the PDF document
        pdfRenderer.RenderDocument();

        // Save the document...
        string pdfFilename = "PENGHANTARAN PESANANAN "+sShipmentNo + ".pdf";
        string file = Server.MapPath("~/App_Data/" + pdfFilename);
        //string file = HttpContext.Current.Server.MapPath("~/pdf/" + pdfFilename);
        //string file = "C:/TEMP/" + pdfFilename;

        // ...and start a viewer //
        //pdfRenderer.Save(file);
        //Process.Start(file);

        // Send PDF to browser //
        MemoryStream stream = new MemoryStream();
        pdfRenderer.Save(stream, false);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();

        //download file //
        //Response.ContentType = "Application/pdf";
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdfFilename);
        //Response.TransmitFile(file);
        //Response.End();

    }

}