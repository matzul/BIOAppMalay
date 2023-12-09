using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;

public partial class TransferOrderPage : System.Web.UI.Page
{
    public MainController oMainCon = new MainController();
    public String sCurrComp = "";
    public String sOpenComp = "";
    public String sUserId = "";
    public String sOrderNo = "";
    public MainModel modCompInfo = new MainModel();
    public MainModel oModOrder = new MainModel();
    public ArrayList lsOrderLineItem = new ArrayList();

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
        if (Session["userid"] != null)
        {
            sUserId = Session["userid"].ToString();
        }
        if (Session["comp"] != null)
        {
            sCurrComp = Session["comp"].ToString();
        }
        if (Request.QueryString["comp"] != null)
        {
            sOpenComp = Request.QueryString["comp"].ToString();
        }
        if (Request.QueryString["orderno"] != null)
        {
            sOrderNo = Request.QueryString["orderno"].ToString();
        }
    }
    private void processValues()
    {
        oModOrder = oMainCon.getTransferOrderHeaderDetails(sOpenComp, "", "", sOrderNo);
        lsOrderLineItem = oMainCon.getTransferOrderDetailsList(oModOrder.GetSetCompFromDetails.GetSetcomp, sOrderNo, 0, "");
        modCompInfo = oMainCon.getCompInfoDetails(oModOrder.GetSetCompFromDetails.GetSetcomp);
        GenerateQRCode();
        generatePDFFile();
    }

    private void GenerateQRCode()
    {
		
		object objQRCode = new
        {
            comp = sCurrComp,
            orderno = oModOrder.GetSetorderno,
            ordercat = "TRANSFER_ORDER"
        };
		String jsonResponse = convertJson(objQRCode);
		
        string urlQR = "https://chart.googleapis.com/chart?chs=250x250&cht=qr&chl=" + jsonResponse + "&choe=UTF-8";
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
        doc.Info.Title = "PESANAN PINDAHAN";
        doc.Info.Subject = "No. Pesanan:" + oModOrder.GetSetorderno;
        doc.Info.Author = "BIOApp System";

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
        string logo_lima = Server.MapPath("~/images/"+modCompInfo.GetSetcomp_logo1);
        MigraDoc.DocumentObjectModel.Shapes.Image image = section.Headers.Primary.AddImage(logo_lima);
        image.Height = "2cm";
        image.Width = "2cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Right;
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
        header.AddText("PESANAN PINDAHAN");
        header.Format.Font.Size = 12;
        header.Format.Font.Bold = true;
        header.Format.Alignment = ParagraphAlignment.Center;

        // Create main section for Purchase Order 
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
        rowTop.Cells[0].AddParagraph(oModOrder.GetSetCompToDetails.GetSetcomp_name);
        rowTop.Cells[0].AddParagraph(oModOrder.GetSetCompToDetails.GetSetcomp_address);
        rowTop.Cells[0].AddParagraph(oModOrder.GetSetCompToDetails.GetSetcomp_contact);
        rowTop.Cells[0].MergeDown = 2;
        rowTop.Cells[0].Borders.Right.Visible = false;
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].Borders.Visible = true;
        rowTop.Cells[3].AddParagraph("No. Pesanan");
        rowTop.Cells[3].Borders.Left.Visible = true;
        rowTop.Cells[3].Borders.Right.Visible = true;
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTop.Cells[4].AddParagraph(oModOrder.GetSetorderno);
        rowTop.Cells[4].Borders.Visible = true;
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        rowTop = tableTop.AddRow();
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].AddParagraph("Tarikh");
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTop.Cells[4].AddParagraph(oModOrder.GetSetorderdate);
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;
		
		String orderType = "";
		if(oModOrder.GetSetordertype == "PUSAT_BEKALAN"){
			orderType = "PUSAT BEKALAN";
		}
		else{
			orderType = oModOrder.GetSetordertype;
		}
        rowTop = tableTop.AddRow();
        rowTop.Cells[1].Borders.Visible = false;
        rowTop.Cells[2].Borders.Visible = false;
        rowTop.Cells[3].AddParagraph("Jenis");
        rowTop.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        rowTop.Cells[4].AddParagraph(orderType);
        rowTop.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        // Create the item table
        MigraDoc.DocumentObjectModel.Tables.Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = MigraDoc.DocumentObjectModel.Colors.Black;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = table.AddColumn("0.5cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("1.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        // Create the header of the table
        Row row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = MigraDoc.DocumentObjectModel.Colors.LightGray;
        row.Cells[0].AddParagraph("#");
        row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[1].AddParagraph("NO. ITEM");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[2].AddParagraph("KETERANGAN");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[3].AddParagraph("HARGA UNIT");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[4].AddParagraph("DISKAUN");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[5].AddParagraph("QTY");
        row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[6].AddParagraph("HARGA");
        row.Cells[6].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[6].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[7].AddParagraph("TAX");
        row.Cells[7].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[7].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[8].AddParagraph("JUMLAH");
        row.Cells[8].Format.Alignment = ParagraphAlignment.Center;
        row.Cells[8].VerticalAlignment = VerticalAlignment.Bottom;

        for (int i = 0; i < lsOrderLineItem.Count; i++)
        {
            MainModel modOrdDet = (MainModel)lsOrderLineItem[i];

            // Each item fills two rows
            Row row1 = table.AddRow();
            row1.Height = "1cm";
            row1.TopPadding = 1;
            //row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row1.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[7].Format.Alignment = ParagraphAlignment.Right;
            row1.Cells[8].Format.Alignment = ParagraphAlignment.Right;

            row1.Cells[0].AddParagraph((i+1).ToString());
            row1.Cells[1].AddParagraph(modOrdDet.GetSetitemno);
            row1.Cells[2].AddParagraph(modOrdDet.GetSetitemdesc);
            row1.Cells[3].AddParagraph(modOrdDet.GetSetunitprice.ToString("#,##0.00"));
            row1.Cells[4].AddParagraph(modOrdDet.GetSetdiscamount.ToString("#,##0.00"));
            row1.Cells[5].AddParagraph(modOrdDet.GetSetquantity.ToString());
            row1.Cells[6].AddParagraph(modOrdDet.GetSetorderprice.ToString("#,##0.00"));
            row1.Cells[7].AddParagraph(modOrdDet.GetSettaxamount.ToString("#,##0.00"));
            row1.Cells[8].AddParagraph(modOrdDet.GetSettotalprice.ToString("#,##0.00"));

            if (i>0 && ((i+1)%10)==0)
            {
                row1.Borders.Bottom.Visible = true;
            }
            else
            {
                row1.Borders.Bottom.Visible = false;
            }
        }
        if ((lsOrderLineItem.Count % 10) > 0)
        {
            int totalremainingrow = 10 - (lsOrderLineItem.Count % 10);
            for (int j = 0; j < totalremainingrow; j++)
            {
                Row rowRemain = table.AddRow();
                rowRemain.Height = "1cm";

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
        rowTot.Borders.Visible = false;
        rowTot.Cells[0].MergeRight = 6;
		
        rowTot.Cells[6].Borders.Right.Visible = false;

        rowTot.Cells[7].AddParagraph("JUMLAH BESAR");
        rowTot.Cells[7].Format.Alignment = ParagraphAlignment.Left;
        rowTot.Cells[7].VerticalAlignment = VerticalAlignment.Center;
        rowTot.Cells[7].Borders.Left.Visible = true;
        rowTot.Cells[7].Borders.Right.Visible = true;
        rowTot.Cells[7].Borders.Top.Visible = true;
        rowTot.Cells[7].Borders.Bottom.Visible = true;

        rowTot.Cells[8].AddParagraph(oModOrder.GetSettotalamount.ToString("#,##0.00"));
        rowTot.Cells[8].Format.Alignment = ParagraphAlignment.Right;
        rowTot.Cells[8].VerticalAlignment = VerticalAlignment.Center;
        rowTot.Cells[8].Borders.Left.Visible = true;
        rowTot.Cells[8].Borders.Right.Visible = true;
        rowTot.Cells[8].Borders.Top.Visible = true;
        rowTot.Cells[8].Borders.Bottom.Visible = true;

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
        rowTblBtm.Cells[0].AddParagraph(oModOrder.GetSetorderremarks);
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
        rowTblBtm.Cells[0].AddParagraph("1. Pesanan Pindahan ini sah sehingga tiga(3) bulan dari tarikh ia dikeluarkan.");
        rowTblBtm.Cells[0].MergeRight = 2;

        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("2. Sila pastikan Pesanan Pindahan ini ditandatangani sebelum salinan dikembalikan ke "+System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(modCompInfo.GetSetcomp_name.ToLower())+".");
        rowTblBtm.Cells[0].MergeRight = 2;

        /*
        rowTblBtm = tblBtm.AddRow();
        rowTblBtm.Borders.Left.Visible = false;
        rowTblBtm.Borders.Right.Visible = false;
        rowTblBtm.Borders.Top.Visible = false;
        rowTblBtm.Borders.Bottom.Visible = false;
        rowTblBtm.Cells[0].AddParagraph("3. Jabatan Kewangan hanya akan memproses bayaran ke atas Pesanan Pindahan ini jika Invois disertakan bersama.");
        rowTblBtm.Cells[0].MergeRight = 2; 
        */
        
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
        string pdfFilename = sOrderNo + ".pdf";
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