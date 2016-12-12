using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CHUYAChuya.Web.Models;
using CHUYAChuya.EntidadesNegocio.Reportes;
using CHUYAChuya.LogicaNegocio;
using Newtonsoft.Json;
using System.Web.Security;
using CHUYAChuya.Seguridad.Filters;
using System.Net;
using CHUYAChuya.Web.Helper;
using CHUYAChuya.Web.Controllers.Base;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Excel = Microsoft.Office.Interop.Excel;

using System.Data;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;


namespace CHUYAChuya.Web.Controllers
{
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/

        public ActionResult Reportes()
        {
            return View();
        }

        public JsonResult ListarVentasIngresos(DateTime dIni, DateTime dFin)
        {
            List<RepVentas> oRep = new List<RepVentas>();
            ReporteLN oReporteLN = new ReporteLN();
            oRep = oReporteLN.ListarVentasIngresos(dIni, dFin);
            return Json(JsonConvert.SerializeObject(oRep));
        }

        public JsonResult ListarCompras(DateTime dIni, DateTime dFin)
        {
            List<RepCompras> oRep = new List<RepCompras>();
            ReporteLN oReporteLN = new ReporteLN();
            oRep = oReporteLN.ListarCompras(dIni, dFin);
            return Json(JsonConvert.SerializeObject(oRep));
        }

        public JsonResult ExportToExcel(DateTime dIni, DateTime dFin)
        {
            List<RepVentas> oRep = new List<RepVentas>();
            ReporteLN oReporteLN = new ReporteLN();
            oRep = oReporteLN.ListarVentasIngresos(dIni, dFin);


            //var products = new System.Data.DataTable("teste");
            //products.Columns.Add("col1", typeof(int));
            //products.Columns.Add("col2", typeof(string));

            //products.Rows.Add(1, "product 1");
            //products.Rows.Add(2, "product 2");
            //products.Rows.Add(3, "product 3");
            //products.Rows.Add(4, "product 4");
            //products.Rows.Add(5, "product 5");
            //products.Rows.Add(6, "product 6");
            //products.Rows.Add(7, "product 7");

            //var grid = new GridView();
            //grid.DataSource = products;
            //grid.DataBind();

            var grid = new GridView();
            //grid.DataSource = oRep;

            grid.DataSource = from data in oRep
                              select new
                              {
                                  N_Reg = data.nNumero,
                                  Fecha_Pago = data.dFechaE,
                                  Tipo_Tabla = data.cTipoTabla,
                                  Serie_Imp = data.cSerie,
                                  N_Coprobante = data.cCorrelativo,
                                  DOI = data.cDOI,
                                  Cliente = data.cPersDesc,
                                  Importe_Total = data.nNotaSub
                              };

            grid.AutoGenerateColumns = true;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return Json(1);
        }


        public void GetExcelVentas(string dIni, string dFin)
        {

            List<RepVentas> oRep = new List<RepVentas>();
            ReporteLN oReporteLN = new ReporteLN();
            oRep = oReporteLN.ListarVentasIngresos(Convert.ToDateTime(dIni), Convert.ToDateTime(dFin));

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "Web App";
                excelPackage.Workbook.Properties.Title = "Reporte de Ventas";
                var sheet = excelPackage.Workbook.Worksheets.Add("Rep_Ventas");
                // output a line for the headers

                //CreateHeader(sheet);
                sheet.Cells["A1"].Value = "N° DEL REGISTRO";
                sheet.Cells["B1"].Value = "FECHA DE EMISIÓN";
                sheet.Cells["C1"].Value = "TIPO DE TABLA 10";
                sheet.Cells["D1"].Value = "N° SERIE";
                sheet.Cells["E1"].Value = "NUMERO";
                sheet.Cells["F1"].Value = "DOI";
                sheet.Cells["G1"].Value = "CLIENTE";
                sheet.Cells["H1"].Value = "IMPORTE TOTAL";


                using (ExcelRange rng = sheet.Cells["A1:H1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }


                sheet.Name = "Rep_Ventas";
                // all indexes start at 1
                var rowIndex = 2;
                foreach (var item in oRep)
                {
                    var col = 1;
                    sheet.Cells[rowIndex, col++].Value = item.nNumero;
                    sheet.Cells[rowIndex, col++].Value = item.dFechaE;
                    sheet.Cells[rowIndex, col++].Value = item.cTipoTabla;
                    sheet.Cells[rowIndex, col++].Value = item.cSerie;
                    sheet.Cells[rowIndex, col++].Value = item.cCorrelativo;
                    sheet.Cells[rowIndex, col++].Value = item.cDOI;
                    sheet.Cells[rowIndex, col++].Value = item.cPersDesc;
                    sheet.Cells[rowIndex, col++].Value = item.nNotaSub;
                    rowIndex++;
                }

                sheet.Column(8).Style.Numberformat.Format = "#,##0.00";
                // You could just save on ExcelPackage here but we need it in 
                // memory to stream it back to the browser
                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteVentas.xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }

        public void GetExcelCompras(string dIni, string dFin)
        {

            List<RepCompras> oRep = new List<RepCompras>();
            ReporteLN oReporteLN = new ReporteLN();
            oRep = oReporteLN.ListarCompras(Convert.ToDateTime(dIni), Convert.ToDateTime(dFin));

            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "Web App";
                excelPackage.Workbook.Properties.Title = "Reporte de Compras";
                var sheet = excelPackage.Workbook.Worksheets.Add("Rep_Compras");
                // output a line for the headers

                //CreateHeader(sheet);
                sheet.Cells["A1"].Value = "N° DEL REGISTRO";
                sheet.Cells["B1"].Value = "FECHA DE EMISIÓN";
                sheet.Cells["C1"].Value = "TIPO DE TABLA 10";
                sheet.Cells["D1"].Value = "N° COMPROBANTE";
                sheet.Cells["E1"].Value = "DOI";
                sheet.Cells["F1"].Value = "PROVEEDOR";
                sheet.Cells["G1"].Value = "IMPORTE TOTAL";


                using (ExcelRange rng = sheet.Cells["A1:G1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }


                sheet.Name = "Rep_Compras";
                // all indexes start at 1
                var rowIndex = 2;
                foreach (var item in oRep)
                {
                    var col = 1;
                    sheet.Cells[rowIndex, col++].Value = item.nNumero;
                    sheet.Cells[rowIndex, col++].Value = item.dFechaE;
                    sheet.Cells[rowIndex, col++].Value = item.cTipoTabla;
                    sheet.Cells[rowIndex, col++].Value = item.cComprobante;
                    sheet.Cells[rowIndex, col++].Value = item.cDOI;
                    sheet.Cells[rowIndex, col++].Value = item.cPersDesc;
                    sheet.Cells[rowIndex, col++].Value = item.nMonto;
                    rowIndex++;
                }

                sheet.Column(7).Style.Numberformat.Format = "#,##0.00";
                // You could just save on ExcelPackage here but we need it in 
                // memory to stream it back to the browser
                Response.ClearContent();
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.AddHeader("content-disposition",
                          "attachment;filename=ReporteCompras.xlsx");
                Response.ContentType = "application/excel";
                Response.Flush();
                Response.End();
            }
        }




    }
}
