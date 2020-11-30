
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace OnlineYournal.Code.Blog
{
    public class ImportExport : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        // https://localhost:44397/Blog/OnPostImport
        // https://github.com/miladsoft/npoi/blob/master/npoi_Example/Controllers/HomeController.cs
        public ActionResult OnPostImport()
        {
            // using NPOI.HSSF.UserModel;
            // using NPOI.SS.UserModel;
            // using NPOI.XSSF.UserModel;


            // Microsoft.AspNetCore.Http.IFormFile file = Request.Form.Files[0];
            string fullPath = @"C:\Users\Administrator\Downloads\demo2.xlsx";
            System.IO.Stream baseStream = System.IO.File.OpenRead(@"C:\Users\Administrator\Downloads\demo.xlsx");
            Microsoft.AspNetCore.Http.IFormFile file = new Microsoft.AspNetCore.Http.FormFile(baseStream, 0, baseStream.Length, "thePostedFile", "demo.xlsx");

            // string folderName = "Upload";
            // string webRootPath = "_hostingEnvironment.WebRootPath";
            // string newPath = System.IO.Path.Combine(webRootPath, folderName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            // if (!System.IO.Directory.Exists(newPath))
            // {
            // System.IO.Directory.CreateDirectory(newPath);
            // }
            if (file.Length > 0)
            {
                string sFileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();

                NPOI.SS.UserModel.IWorkbook workbook;
                NPOI.SS.UserModel.ISheet sheet;
                // string fullPath = System.IO.Path.Combine(newPath, file.FileName);
                using (var stream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    if (sFileExtension == ".xls")
                    {
                        // This will read the Excel 97-2000 formats  
                        // NPOI.HSSF.UserModel.HSSFWorkbook hssfwb = new NPOI.HSSF.UserModel.HSSFWorkbook(stream);
                        workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(stream);
                    }
                    else
                    {
                        // This will read 2007 Excel format  
                        // NPOI.XSSF.UserModel.XSSFWorkbook hssfwb = new NPOI.XSSF.UserModel.XSSFWorkbook(stream);
                        workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream);
                    }

                    sheet = workbook.GetSheetAt(0); //get first sheet from workbook   

                    NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == NPOI.SS.UserModel.CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            return this.Content(sb.ToString());
        }


        // https://localhost:44397/Blog/OnPostExport
        public async System.Threading.Tasks.Task<IActionResult> OnPostExport()
        {
            // using NPOI.HSSF.UserModel;
            // using NPOI.SS.UserModel;
            // using NPOI.XSSF.UserModel;

            // sstring sWebRootFolder = "_hostingEnvironment.WebRootPath";
            string sFileName = @"demo.xlsx";
            // sFileName = @"demo.xls";
            // string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            // System.IO.FileInfo file = new System.IO.FileInfo(System.IO.Path.Combine(sWebRootFolder, sFileName));


            // System.IO.Stream memory = new System.IO.MemoryStream();
            // using (System.IO.Stream fs = new System.IO.FileStream(System.IO.Path.Combine(sWebRootFolder, sFileName), System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.MemoryStream fs = new System.IO.MemoryStream())
            {
                NPOI.SS.UserModel.IWorkbook workbook;


                string ext = System.IO.Path.GetExtension(sFileName);

                if (".xlsx".Equals(ext, System.StringComparison.InvariantCultureIgnoreCase))
                    workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(); // This will write 2007 Excel format  
                else if (".xls".Equals(ext, System.StringComparison.InvariantCultureIgnoreCase))
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(); // This will write the Excel 97-2000 formats  
                else
                    throw new System.NotSupportedException("Extension \"" + ext + "\" not supported.");



                NPOI.SS.UserModel.ISheet excelSheet = workbook.CreateSheet("Demo");

                NPOI.SS.UserModel.IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Name");
                row.CreateCell(2).SetCellValue("Age");

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Kane Williamson");
                row.CreateCell(2).SetCellValue(29);

                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Martin Guptil");
                row.CreateCell(2).SetCellValue(33);

                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Colin Munro");
                row.CreateCell(2).SetCellValue(23);

                workbook.Write(fs);

                byte[] file = fs.ToArray();


                if (".xlsx".Equals(ext, System.StringComparison.InvariantCultureIgnoreCase))
                    return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
                else if (".xls".Equals(ext, System.StringComparison.InvariantCultureIgnoreCase))
                    return File(file, "application/vnd.ms-excel", sFileName);

                throw new System.NotSupportedException("Extension \"" + ext + "\" not supported.");

                // return File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
            }

            /*
            using (System.IO.Stream stream = new System.IO.FileStream(System.IO.Path.Combine(sWebRootFolder, sFileName), System.IO.FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            
            memory.Position = 0;
            */
            // return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }


    }


}
